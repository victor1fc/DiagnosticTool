import { Component, OnDestroy, OnInit,signal, ViewChild  } from '@angular/core';
import { DeviceService } from '../../service/device.service';
import { ConfirmationService } from 'primeng/api';
import { Table, TableModule } from 'primeng/table';
import { AsyncPipe, CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { RatingModule } from 'primeng/rating';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { InputIconModule } from 'primeng/inputicon';
import { IconFieldModule } from 'primeng/iconfield';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { Device } from '../../../core/models/device';

interface Column {
    field: string;
    header: string;
    customExportHeader?: string;
}

@Component({
  selector: 'app-device',
  standalone: true,
  imports:[CommonModule,
          TableModule,
          FormsModule,
          ButtonModule,
          RippleModule,
          ToastModule,
          ToolbarModule,
          RatingModule,
          InputTextModule,
          TextareaModule,
          SelectModule,
          RadioButtonModule,
          InputNumberModule,
          DialogModule,
          TagModule,
          InputIconModule,
          IconFieldModule,
          ConfirmDialogModule,
    ],
  templateUrl: './device.component.html',
  styleUrls: ['./device.component.css'],
  providers:[ DeviceService, ConfirmationService]
})
export class DeviceComponent implements OnInit, OnDestroy {

    @ViewChild('dt') dt!: Table;

    devices: Device[] = [];

    device!: Device;
    submitted: boolean = false;
    deviceDialog: boolean = false;
    selectedDevices!: Device[];
    cols!: Column[];

    constructor(private deviceService : DeviceService,
                private confirmationService: ConfirmationService,
    ) { }

    ngOnInit() {
        this.deviceService.devices$.subscribe(items => {
            this.devices = items;
        })
    }

    ngOnDestroy(): void {
    }

    exportCSV() {
        this.dt.exportCSV();
    }

    openNew() {
        this.device = {};
        this.submitted = false;
        this.deviceDialog = true;
    }

    deleteSelectedDevices() {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete the selected devices?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                this.deviceService.delete(this.selectedDevices)
                this.selectedDevices = [];
            }
        });
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    getSeverity(status: string) {
        switch (status) {
            case 'CONNECTED':
                return 'success';
            case 'CONNECTING':
                return 'warn';
            case 'DISCONNECTED':
                return 'danger';
            default:
                return 'info';
        }
    }

    editProduct(device: Device) {
        this.device = { ...device };
        this.deviceDialog = true;
    }

    deleteProduct(device: Device) {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete ' + device.code + '?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                const selected : Device[] = [device];
                this.deviceService.delete(selected);
                this.device = {};
            }
        });
    }

    hideDialog() {
        this.deviceDialog = false;
        this.submitted = false;
    }

    saveDevice(device : Device) {
        this.submitted = true;
        if (this.device.code?.trim()) {

            this.deviceService.addOrUpdate(device);

            this.deviceDialog = false;
            this.device = {};
        }
    }

}
