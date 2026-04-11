import { Injectable } from '@angular/core';
import { EnvConfigService } from '../env-config/env-config.service';
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr"

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  private connection: HubConnection;
  private connectionStart!: Promise<void>;

  constructor(private envConfigService: EnvConfigService) {
    const hubUrl = envConfigService.getHubUrl();
    this.connection = new HubConnectionBuilder().withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();

    this.start();
  }

  async start(): Promise<void> {
    this.connectionStart = this.connection.start();
    return this.connectionStart;
  }

  async invoke(methodName: string, ...args: any[]): Promise<any> {

    await this.connectionStart;
    return this.connection.invoke(methodName, ...args);
  }

  on(eventName: string, callback: (...args: any[]) => void): void {
    this.connection.on(eventName, callback);
  }
}
