import { Routes } from '@angular/router';
import { DeviceComponent } from './cruds/device/device.component';


export default [
    { path: 'device', component: DeviceComponent },
    { path: '**', redirectTo: '/notfound' }
] as Routes;
