import { ChangeDetectionStrategy, Component } from '@angular/core';
import { windowOptionsProvider } from '@rubrum.platform/windows';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    windowOptionsProvider({
      background: 'black',
    }),
  ],
})
export class HomeComponent {}
