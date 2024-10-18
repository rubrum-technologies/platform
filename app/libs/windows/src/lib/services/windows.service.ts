import { DOCUMENT } from '@angular/common';
import {
  ApplicationRef,
  createComponent,
  EnvironmentInjector,
  inject,
  Injectable,
  Type,
} from '@angular/core';

import { Window, WindowOptions } from '../models';
import { WINDOW_OPTIONS } from './window.options';

@Injectable({
  providedIn: 'root',
})
export class WindowsService {
  private readonly document = inject(DOCUMENT);
  private readonly applicationRef = inject(ApplicationRef);

  public open<C>(
    title: string,
    component: Type<C>,
    environmentInjector: EnvironmentInjector,
  ): Window {
    const componentRef = createComponent(component, {
      environmentInjector,
    });

    const options: WindowOptions = componentRef.injector.get(WINDOW_OPTIONS);

    const window = new Window(
      title,
      this.document.querySelector('#windows-content'),
      options,
    );

    window.body.appendChild(componentRef.location.nativeElement);

    this.applicationRef.attachView(componentRef.hostView);

    return window;
  }
}
