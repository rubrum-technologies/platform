import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  EnvironmentInjector,
  inject,
  output,
} from '@angular/core';
import { WindowsService } from '@rubrum.platform/windows';
import { TuiButton } from '@taiga-ui/core';

import { HomeComponent } from '../home';

@Component({
  selector: 'app-menu-apps',
  standalone: true,
  imports: [CommonModule, TuiButton],
  templateUrl: './menu-apps.component.html',
  styleUrl: './menu-apps.component.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MenuAppsComponent {
  private readonly injector = inject(EnvironmentInjector);
  private readonly windowsService = inject(WindowsService);

  public readonly openedApp = output();

  public onOpen() {
    this.windowsService.open(
      'Тестовое приложение',
      HomeComponent,
      this.injector,
    );

    this.openedApp.emit();
  }
}
