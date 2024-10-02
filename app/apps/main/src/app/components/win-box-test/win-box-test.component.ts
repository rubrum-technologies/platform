import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TuiButton } from '@taiga-ui/core';
import { WinboxService } from '../../services';

@Component({
  selector: 'app-win-box-test',
  standalone: true,
  imports: [CommonModule, TuiButton],
  templateUrl: './win-box-test.component.html',
  styleUrl: './win-box-test.component.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class WinBoxTestComponent {
  constructor(private readonly winboxService: WinboxService) {}

  public onIncrement() {
    this.winboxService.increment();
  }
}
