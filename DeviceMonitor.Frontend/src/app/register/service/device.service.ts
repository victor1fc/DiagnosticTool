import { Injectable } from '@angular/core';
import { Device } from '../../core/models/device';
import { BehaviorSubject } from 'rxjs';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

@Injectable({
  providedIn: 'root',
})
export class DeviceService {

 idCount : number = 3 ;
private devicesSubject = new BehaviorSubject<Device[]>(
    [   {
            id: 0,
            code: "DVABC",
            macAddress: "2D:D2:BD:16:E4:E2",
            status: "CONNECTING"
        },
        {
            id: 1,
            code: "DV123",
            macAddress: "53:5E:66:25:CA:0B",
            status: "CONNECTED"
        },
        {
            id: 2,
            code: "DVXYZ",
            macAddress: "53:5E:66:25:CA:0B",
            status: "DISCONNECTED"
        },
    ]);
public devices$ = this.devicesSubject.asObservable();

constructor(private messageService: MessageService) { }

    addOrUpdate(device : Device) : Device {
        let current: Device[] = this.devicesSubject.value;
        if(device.id != null) {
           const updated = current.map(item => item.id === device.id ? device : item);
            this.messageService.add({
                    severity: 'info',
                    summary: 'Info',
                    detail: 'Device Edited',
                    life: 3000
                });
            this.devicesSubject.next(updated);
        }
        else
        {
            device.id = this.idCount + 1;
            this.idCount++;
            this.messageService.add({
                    severity: 'info',
                    summary: 'Info',
                    detail: 'Device added successfully ✅',
                    life: 3000
            });
            this.devicesSubject.next([...current, device]);
        }
        return device;
    }
    delete(devices: Device[]){
        let current: Device[] = this.devicesSubject.value;
        devices.forEach((device)=>{
            current = current = current.filter(item => item.id !== device.id);
        });

        if (devices.length === 1){
             this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Device Deleted',
                    life: 3000
                });
        }
        else{
            this.messageService.add({
                        severity: 'success',
                        summary: 'Successful',
                        detail: 'Devices Deleted',
                        life: 3000
                    });
        }
        this.devicesSubject.next(current);
    }
}
