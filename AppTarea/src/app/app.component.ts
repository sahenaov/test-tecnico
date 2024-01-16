import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Journey } from './Interfaces/journey';
import { ChangeDetectorRef } from '@angular/core';
import { JourneyService } from './Services/journey.service';
import { tap } from 'rxjs/operators';

type ConversionFactors = {
  [key: string]: number;
};

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  listaRutas: Journey[] = [];
  formularioRuta: FormGroup;
  conversionFactors: ConversionFactors = { 'USD': 1, 'COP': 3.9, 'EUR': 0.9 };
  cargandoRutas: boolean = false;
  errorEnRutas: string = '';

  constructor(
    private cdr: ChangeDetectorRef,
    private _journeyServicio: JourneyService,
    private fb: FormBuilder
  ){
    this.formularioRuta = this.fb.group({
      origin: ['', Validators.required],
      destination: ['', Validators.required],
      selectedCurrency: ['USD', Validators.required]
    });

    this.listenMonedaChanges();
  }

  private listenMonedaChanges(): void {
    const selectedCurrencyControl = this.formularioRuta.get('selectedCurrency');

    if (selectedCurrencyControl) {
      this.formularioRuta.get('selectedCurrency')!.valueChanges
        .pipe(
          tap(nuevaMoneda => this.actualizarPrecios(nuevaMoneda))
        )
        .subscribe();
    }
  }

  private actualizarPrecios(nuevaMoneda: string): void {
    // Recorrer la lista de rutas y actualizar los precios
    this.listaRutas.forEach(ruta => {
      ruta.price = this.convertirMoneda(ruta.price, ruta.selectedCurrency, nuevaMoneda);
      ruta.selectedCurrency = nuevaMoneda;
    });
  }

  obtenerRutas(): void {
    const originControl = this.formularioRuta.get('origin');
    const destinationControl = this.formularioRuta.get('destination');
    const selectedCurrencyControl = this.formularioRuta.get('selectedCurrency');

    if (originControl && destinationControl && selectedCurrencyControl) {
      const origin = originControl.value;
      const destination = destinationControl.value;
      const selectedCurrency = selectedCurrencyControl.value || 'USD';

      this.cargandoRutas = true;
      this.errorEnRutas = '';
      this.cdr.detectChanges();

      this._journeyServicio.searchRoutes(origin, destination, selectedCurrency).subscribe(
        (rutas) => {
          this.listaRutas = rutas.map(ruta => {
            return {
              ...ruta,
              NumberOfFlights: ruta.flights.length,
              Stops: ruta.flights.filter(flight => flight != null).length - 1
            };
          });
          this.cargandoRutas = false;
        },
        (error) => {
          this.errorEnRutas = 'Error al buscar rutas: ' + error.message;
          this.cargandoRutas = false;
        }
      );
    } else {
      console.error('Alguno de los controles (origin, destination) es nulo.');
    }
  }

  convertirMoneda(valor: number, monedaOrigen: string, monedaDestino: string): number {
    const conversionFactorOrigen = this.conversionFactors[monedaOrigen] || 1;
    const conversionFactorDestino = this.conversionFactors[monedaDestino] || 1;
    return (valor / conversionFactorOrigen) * conversionFactorDestino;
  }

  ngOnInit(): void {  }
}
