import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TuiButton } from '@taiga-ui/core';

@Component({
  standalone: true,
  imports: [RouterModule, TuiButton],
  selector: 'ds-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.less',
})
export class AppComponent {
  title = 'data-source';
}
