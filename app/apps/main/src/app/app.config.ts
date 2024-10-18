import {
  ApplicationConfig,
  inject,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { InMemoryCache } from '@apollo/client/core';
import { windowOptionsProvider } from '@rubrum.platform/windows';
import { NG_EVENT_PLUGINS } from '@taiga-ui/event-plugins';
import { provideApollo } from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideAnimations(),
    NG_EVENT_PLUGINS,
    provideApollo(() => {
      const httpLink = inject(HttpLink);

      return {
        link: httpLink.create({ uri: '/graphql' }),
        cache: new InMemoryCache(),
      };
    }),
    windowOptionsProvider({
      top: 48,
      left: 48,
    }),
  ],
};
