import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TuiButton, TuiLabel, TuiRoot } from '@taiga-ui/core';
import { WinboxService } from './services';

@Component({
  standalone: true,
  imports: [RouterModule, TuiRoot, TuiButton, TuiLabel],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.less',
})
export class AppComponent {
  protected readonly number = this.winboxService.testNumber;

  public constructor(private readonly winboxService: WinboxService) {}

  public onOpen() {
    this.winboxService.openBox();
  }
}
