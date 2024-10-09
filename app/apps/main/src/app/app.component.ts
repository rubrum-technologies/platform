import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TuiButton, TuiRoot } from '@taiga-ui/core';

@Component({
  standalone: true,
  imports: [RouterModule, TuiRoot, TuiButton],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.less',
})
export class AppComponent {}
