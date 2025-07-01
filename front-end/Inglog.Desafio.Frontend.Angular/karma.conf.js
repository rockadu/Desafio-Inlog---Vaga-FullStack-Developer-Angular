// karma.conf.js
module.exports = function (config) {
  config.set({
    basePath: '',
    frameworks: ['jasmine', '@angular-devkit/build-angular'],
    plugins: [
      require('karma-jasmine'),
      require('karma-chrome-launcher'), // ainda é usado como base
      require('karma-jasmine-html-reporter'),
      require('karma-coverage'),
      require('@angular-devkit/build-angular/plugins/karma')
    ],
    client: {
      jasmine: {
        // configurações do Jasmine
      },
      clearContext: false // deixa os resultados visíveis no navegador
    },
    coverageReporter: {
      dir: require('path').join(__dirname, './coverage'),
      subdir: '.',
      reporters: [{ type: 'html' }, { type: 'text-summary' }]
    },
    reporters: ['progress', 'kjhtml'],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: true,
    browsers: ['Edge'],
    customLaunchers: {
      EdgeHeadless: {
        base: 'Chrome',
        flags: [
          '--headless=new',
          '--disable-gpu',
          '--remote-debugging-port=9223'
        ],
        executablePath: 'C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe'
      }
    },
    singleRun: false,
    restartOnFileChange: true
  });
};
