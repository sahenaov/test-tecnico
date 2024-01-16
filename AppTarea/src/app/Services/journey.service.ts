import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Journey } from '../Interfaces/journey';

@Injectable({
  providedIn: 'root'
})
export class JourneyService {
  private endpoint:string = environment.endPoint;
  private apiUrl: string= "https://localhost:7182/api";

  constructor(private http:HttpClient) { }

  searchRoutes(origin: string, destination: string, selectedCurrency:string): Observable<Journey[]> {
    const url = `/api/Journey?origin=${origin}&destination=${destination}&selectedCurrency: string`;
    const params = new HttpParams()
      .set('origin', origin)
      .set('destination', destination)
      .set('selectedCurrency',selectedCurrency);
    return this.http.get<Journey[]>(url);
  }
  
  



}
