var app = angular.module('app', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'app/components/keyboard/keyboardView.html',
            controller: 'keyboardController'
        })

});