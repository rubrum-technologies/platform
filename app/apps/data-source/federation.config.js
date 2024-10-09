const {
  withNativeFederation,
  shareAll,
} = require('@angular-architects/native-federation/config');

module.exports = withNativeFederation({
  shared: {
    ...shareAll(
      {
        singleton: true,
        strictVersion: true,
        requiredVersion: 'auto',
        includeSecondaries: true,
      },
      [
        '@angular/router/upgrade',
        '@angular/common/upgrade',
        'rxjs/ajax',
        'rxjs/fetch',
        'rxjs/testing',
        'rxjs/webSocket',
        '@angular-architects/native-federation',
        'tslib',
        'zone.js',
        '@apollo/client',
        'winbox',
      ],
    ),
  },
});
