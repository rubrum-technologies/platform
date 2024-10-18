import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { TuiButton, TuiScrollbar } from '@taiga-ui/core';
import { TuiBadge } from '@taiga-ui/kit';
import { TuiNavigation } from '@taiga-ui/layout';

import { MenuAppsComponent } from '../menu-apps';

@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [
    CommonModule,
    TuiNavigation,
    TuiButton,
    MenuAppsComponent,
    TuiBadge,
    TuiScrollbar,
  ],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NavigationComponent {
  protected expanded = false;
  protected open = false;

  protected onOpenedApp() {
    this.open = false;
  }
}
