var App;
(function (App) {
    "use strict";
    var OnScreenKeyboardController = (function () {
        function OnScreenKeyboardController($http, $window, OnScreenKeyboardService) {
            this.$http = $http;
            this.$window = $window;
            this.OnScreenKeyboardService = OnScreenKeyboardService;
            this.errorMessage = "";
            this.isVisibleErrorMessage = false;
            this.alphabet = "";
            this.searchTerms = "";
            this.alphabetPlaceholder = "ABCDEF\nGHIJKL\nMNOPQR\nSTUVWX\nYZ1234\n567890";
            this.searchTermsPlaceholder = "IT Crowd";
            this.results = "";
        }
        OnScreenKeyboardController.prototype.determineResults = function () {
            if (this.alphabet == "" || this.searchTerms == "") {
                this.errorMessage = "Both the alphabet and the search terms are required to determine the results.";
                this.isVisibleErrorMessage = true;
                return;
            }
            this.isVisibleErrorMessage = false;
            this.results = this.OnScreenKeyboardService.calculateResults(this.alphabet, this.searchTerms);
            if (this.results == null || this.results == "") {
                this.errorMessage = "Something went wrong with determining the results. Check the alphabet and search terms and try again.";
                this.isVisibleErrorMessage = true;
            }
        };
        OnScreenKeyboardController.prototype.clearEntries = function () {
            this.alphabet = "";
            this.searchTerms = "";
            this.results = "";
            this.errorMessage = "";
            this.isVisibleErrorMessage = false;
        };
        // TODO make this generic for any language (pass in lang, use service to get alphabet?) wasn't working when passing in english\
        // TODO try using a select instead of a dropdown?
        OnScreenKeyboardController.prototype.populateEnglish = function () {
            this.alphabet = this.alphabetPlaceholder;
        };
        OnScreenKeyboardController.$inject = ["$http", "$window", "OnScreenKeyboardService"];
        return OnScreenKeyboardController;
    }());
    App.OnScreenKeyboardController = OnScreenKeyboardController;
    angular.module("app").controller("OnScreenKeyboardController", OnScreenKeyboardController);
})(App || (App = {}));
//# sourceMappingURL=OnScreenKeyboardController.js.map