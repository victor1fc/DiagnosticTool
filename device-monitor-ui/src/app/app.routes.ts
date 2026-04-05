import { Routes } from '@angular/router';
import { TerminalComponent } from './features/terminal/pages/terminal/terminal.component';


export const routes: Routes = [

  { path: "**" , component: TerminalComponent},
];
