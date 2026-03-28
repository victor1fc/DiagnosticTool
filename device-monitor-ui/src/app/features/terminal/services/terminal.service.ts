import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TerminalService {

  private api = "http://localhost:5000/api/device";

  constructor(private httpClient : HttpClient) { }

  connect(data : any){
    return this.httpClient.post<any>(`${this.api}/connect`, data);
  }

  sendCommand(command : string){
    return this.httpClient.post<any>(`${this.api}/command`, {command});
  }
}
