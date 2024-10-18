import { FactoryProvider } from '@angular/core';
import { tuiCreateToken, tuiProvideOptions } from '@taiga-ui/cdk';

import { WindowOptions } from '../models';

export const WINDOW_OPTIONS = tuiCreateToken({});

export function windowOptionsProvider(
  options: Partial<WindowOptions>,
): FactoryProvider {
  return tuiProvideOptions(WINDOW_OPTIONS, options, {});
}
