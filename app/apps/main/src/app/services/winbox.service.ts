import {
  ApplicationRef,
  createComponent,
  EnvironmentInjector,
  Injectable,
  signal,
} from '@angular/core';
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-expect-error
import WinBox from 'winbox/src/js/winbox';
import { WinBoxTestComponent } from '../components';

@Injectable({
  providedIn: 'root',
})
export class WinboxService {
  public readonly testNumber = signal<number>(0);

  constructor(
    private appRef: ApplicationRef,
    private injector: EnvironmentInjector,
  ) {}

  public openBox() {
    const winBox = new WinBox('Basic Window');
    const componentRef = createComponent(WinBoxTestComponent, {
      environmentInjector: this.injector,
      hostElement: winBox.body,
    });
    console.log(winBox);
    this.appRef.attachView(componentRef.hostView);
    componentRef.changeDetectorRef.detectChanges();
    console.log(componentRef);
  }

  public increment() {
    this.testNumber.set(this.testNumber() + 1);
  }
}
