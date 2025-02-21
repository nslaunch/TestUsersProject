// module.exports = function(config) {
//   config.set({
//     // base path that will be used to resolve all patterns (eg. files, exclude)
//     basePath: '',

//     // frameworks to use
//     frameworks: ['jasmine', 'requirejs'],  // Ensure 'requirejs' is included

//     // list of files / patterns to load in the browser
//     files: [
//       // 'node_modules/requirejs/require.js',  // Load require.js
//       // 'node_modules/jquery/dist/jquery.min.js',  // Load jQuery
//       // 'node_modules/jasmine-core/lib/jasmine-core/jasmine.js', // Jasmine core
//       // 'node_modules/jasmine-core/lib/jasmine-core/jasmine-html.js', // Jasmine HTML reporter
//       // 'node_modules/jasmine-core/lib/jasmine-core/boot0.js', // Boot Jasmine tests

//       // Include your source and test files
//       'Scripts/test/**/*.js',  
//       'Scripts/test/**/*.test.js'
//     ],

//     // list of files / patterns to exclude
//     exclude: [],

//     // preprocess matching files before serving them to the browser
//     preprocessors: {},

//     // test results reporter to use
//     reporters: ['progress', 'kjhtml'],
//     // htmlReporter: {
//     //   outputDir: 'karma_html_report', // The output directory for the report
//     //   templatePath: 'node_modules/karma-html-reporter/jasmine_template.html'
//     // },

//     // plugins: [
//     //   'karma-jasmine',
//     //   'karma-chrome-launcher',
//     //   'karma-jasmine-html-reporter',
//     //   'karma-requirejs'  // Add the requirejs plugin
//     // ],

//     // web server port
//     port: 9876,

//     // enable / disable colors in the output (reporters and logs)
//     colors: true,

//     // level of logging
//     logLevel: config.LOG_INFO,

//     // enable / disable watching file and executing tests whenever any file changes
//     autoWatch: true,

//     // start these browsers
//     browsers: ['Chrome'],

//     // Continuous Integration mode
//     singleRun: false,

//     // // RequireJS configuration for resolving dependencies
//     // requireConfig: {
//     //   paths: {
//     //     'jquery': 'node_modules/jquery/dist/jquery.min', // Path to jQuery
//     //     'jasmine': 'node_modules/jasmine-core/lib/jasmine-core/jasmine', // Path to Jasmine core
//     //   },
//     //   shim: {
//     //     'jquery': {
//     //       exports: '$'  // Define jQuery as the global $
//     //     },
//     //     'jasmine': {
//     //       exports: 'jasmine'  // Define Jasmine as a global variable
//     //     }
//     //   }
//     // },

//     // Concurrency level: how many browser instances should be started simultaneously
//     concurrency: Infinity
//   });
// };
module.exports = function(config) {
  config.set({
    frameworks: ['jasmine'],  // Use Jasmine as the testing framework
    files: [
      // 'node_modules/requirejs/require.js',  // Load require.js
      'node_modules/jquery/dist/jquery.min.js',  // Load jQuery
      'Scripts/test/**/*.js',  // Include your source files
      'Scripts/test/**/*.test.js',  // Include your test files

    ],
    browsers: ['Chrome'],  // Run the tests in Chrome (can be any browser)
    singleRun: false,  // Run tests once
    // reporters: ['progress'],  // Set the reporter type
   
       reporters: ['progress', 'kjhtml'],
    htmlReporter: {
      outputDir: 'karma_html_report', // The output directory for the report
      templatePath: 'node_modules/karma-html-reporter/jasmine_template.html'
    },
    port: 9876,  // Port for Karma to run on
    colors: true,  // Use colored output in the terminal
    logLevel: config.LOG_INFO,  // Log level (can be LOG_DEBUG, LOG_INFO, etc.)
    autoWatch: true,  // Enable watch mode (tests run on file change)
    plugins: [
      'karma-jasmine',
      'karma-chrome-launcher',
      'karma-jasmine-html-reporter',
      'karma-requirejs'  // Add the requirejs plugin
    ],

    //     // RequireJS configuration for resolving dependencies
    // requireConfig: {
    //   paths: {
    //     'jquery': 'node_modules/jquery/dist/jquery.min', // Path to jQuery
    //     'jasmine': 'node_modules/jasmine-core/lib/jasmine-core/jasmine', // Path to Jasmine core
    //   },
    //   shim: {
    //     'jquery': {
    //       exports: '$'  // Define jQuery as the global $
    //     },
    //     'jasmine': {
    //       exports: 'jasmine'  // Define Jasmine as a global variable
    //     }
    //   }
    // },
  });
};
