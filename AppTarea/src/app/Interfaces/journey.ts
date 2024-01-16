export interface Journey {
    origin: string;
    destination: string;
    price: number;
    selectedCurrency: string;
    NumberOfFlights: number;
    flights: Flight[]; 
    Stops: number;
}

export interface Flight {
  flightNumber: string;
  departure: string;
  arrival: string;
  stops: string[]; 
}

