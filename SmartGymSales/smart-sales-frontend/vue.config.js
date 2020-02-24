// vue.config.js
module.exports = {
    // options...
    publicPath: './',
    devServer: {
      proxy: {
        '/api': {
          target: 'http://localhost:3131', // this configuration needs to correspond to the Spring Boot backends' application.properties server.port
          ws: true,
          changeOrigin: true
        }
      }
    },
    // Change build paths to make them Maven compatible
    // see https://cli.vuejs.org/config/
    outputDir: 'dist',
    assetsDir: 'static'
  }