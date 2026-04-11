import { Injectable } from '@angular/core';
import { SignalrService } from '../../../core/services/signalr/signalr.service';
import { Subject } from 'rxjs';
import { CommandRequest } from '../models/command-request';
import { ConnectResponse } from '../models/connect-response';
import { CommandResponse } from '../models/command-response';
import { ConnectRequest } from '../models/connect-request';

@Injectable({
  providedIn: 'root'
})
export class TerminalService {

  private connectionSubject = new Subject<ConnectResponse>();
  public connection$ = this.connectionSubject.asObservable();
  private commandSubject = new Subject<CommandResponse>();
  public command$ = this.commandSubject.asObservable();

  constructor(private signalrService: SignalrService) {
    this.signalrService.on("ReceiveConnection", (response) => this.connectionSubject.next(response));
    this.signalrService.on("ReceiveCommandOutput", (response) => this.commandSubject.next(response));
  }

  connect(data: ConnectRequest): Promise<any> {
    return this.signalrService.invoke("ConnectToDevice", data);
  }

  sendCommand(request: CommandRequest) {
    return this.signalrService.invoke("SendCommand", request);

  }
}
