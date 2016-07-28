var App;
(function (App) {
    "use strict";
    var OnScreenKeyboardController = (function () {
        function OnScreenKeyboardController($http, $window) {
            this.$http = $http;
            this.$window = $window;
            this.errorMessage = "";
            this.isVisibleErrorMessage = false;
            this.values = [];
            this.alphabet = "";
            this.searchTerms = "";
            this.results = "D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#";
            //this.getValues();
        }
        OnScreenKeyboardController.prototype.determineOnScreenKeyboard = function () {
        };
        OnScreenKeyboardController.prototype.clearEntries = function () {
            this.alphabet = "";
            this.searchTerms = "";
            this.results = "";
        };
        OnScreenKeyboardController.$inject = ["$http", "$window"];
        return OnScreenKeyboardController;
    }());
    App.OnScreenKeyboardController = OnScreenKeyboardController;
    angular.module("app").controller("OnScreenKeyboardController", OnScreenKeyboardController);
})(App || (App = {}));
