var webpack = require('webpack');
var path = require('path');

module.exports = {
  entry: {
    app: ['babel-polyfill', './src/main.js'],
    vendor: [      
      'axios',
      'bootstrap',
      'jquery',
      'lodash',      
      'mobx',
      'mobx-react',
      'react',
      'react-bootstrap',
      'react-dom'             
    ]
  },
  output: {
    path: path.join(__dirname, 'public'),
    filename: 'bundle.js',
    publicPath: 'public/'
  },
  devServer: {
    inline: true,
    port: 3000
  },
  module: {
    loaders: [
      // Used for compiling ES2015 JavaScript
      {
        test: /\.js$/,
        exclude: /node_modules/,
        loader: 'babel-loader'
      },      
      // Used for Bootstrap Less Source Files
      {
        test: /\.less$/,
        loader: 'style!css!less'
      },
      // Used for Bootstrap Less Source Files
      {
        test: /\.css$/,
        loader: 'style!css'
      },
      // Used for Bootstrap Glyphicon Fonts
      {
        test: /\.(woff2|woff|ttf|svg|eot)$/,
        loader: 'file-loader'
      },
      {
        test: /\.json$/,
        loader: 'json-loader'
      },
      {
        test: /\.(jpeg|jpg|png|gif|svg)$/,
        loader: 'url?limit=10000!img?progressive=true'
      }
    ]
  },
  plugins: [
    new webpack.ProvidePlugin({
      $: 'jquery',
      jQuery: 'jquery'
    }),
    new webpack.optimize.CommonsChunkPlugin('vendor', 'vendor.bundle.js')
  ],
  resolve: {
    extensions: ['', '.js', '.jsx']
  }

}
