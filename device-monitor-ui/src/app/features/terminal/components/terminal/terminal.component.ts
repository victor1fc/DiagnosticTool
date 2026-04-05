import { Component, OnDestroy, OnInit } from '@angular/core';
import { TerminalService } from '../../services/terminal.service';
import { CommandRequest } from '../../models/command-request';
import { Subscription } from 'rxjs';
import { Status } from '../../enums/status.enum';
import { ConnectRequest } from '../../models/connect-request';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-terminal',
  imports: [CommonModule],
  templateUrl: './terminal.component.html',
  styleUrl: './terminal.component.css'
})
export class TerminalComponent implements OnInit, OnDestroy {

  public isConnected: boolean = false;
  public outputHistory: any[] = [];

  private _connection! : Subscription;
  private _command!: Subscription;

  constructor(private terminalService: TerminalService) {

  }

  ngOnInit(): void {
    this._connection = this.terminalService.connection$.subscribe((response) => {
      console.log(response);
      if (response.status == Status.Success) {
        this.isConnected = true;
      } else {
        this.isConnected = false;
      }
    });

    this._command = this.terminalService.command$.subscribe((response) => {
      console.log(response);
      this.outputHistory.push(response);
    });
  }

  ngOnDestroy(): void {
    this._connection.unsubscribe();
    this._command.unsubscribe();
  }

  onConnect(data: ConnectRequest) {
    this.terminalService.connect(data);
  }

  onSendCommand(command: string) {
    const request = new CommandRequest();
    request.command = command;
    this.terminalService.sendCommand(request);
  }

}
