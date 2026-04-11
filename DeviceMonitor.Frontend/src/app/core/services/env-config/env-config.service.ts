import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class EnvConfigService {

constructor() { }

  getApiUrl(){
    return environment.api_hostname;
  }

  getHubUrl(){
    return environment.hub_hostname;
  }
}
