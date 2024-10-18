const {
  withNativeFederation,
  shareAll,
  share,
} = require('@angular-architects/native-federation/config');

module.exports = withNativeFederation({
  shared: {
    ...shareAll(
      {
        singleton: true,
        strictVersion: true,
        requiredVersion: 'auto',
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
    ...share({
      'libphonenumber-js/core': {
        singleton: true,
        strictVersion: true,
        requiredVersion: 'auto',
      },
    }),
  },
});
