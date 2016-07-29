var App;
(function (App) {
    "use strict";
    var OnScreenKeyboardController = (function () {
        function OnScreenKeyboardController(OnScreenKeyboardService) {
            this.OnScreenKeyboardService = OnScreenKeyboardService;
            this.errorMessage = "";
            this.isVisibleErrorMessage = false;
            this.keyboardLayout = "";
            this.searchTerms = "";
            this.keyboardLayoutDisabled = true;
            this.selectedKeyboardLayout = "No layout selected";
            this.searchTermsPlaceholder = "Example: IT Crowd";
            this.results = "";
        }
        OnScreenKeyboardController.prototype.determineResults = function () {
            var _this = this;
            if (this.keyboardLayout == "" || this.searchTerms == "") {
                this.errorMessage = "Both the keyboard layout and the search terms are required to determine the results.";
                this.isVisibleErrorMessage = true;
                return;
            }
            this.isVisibleErrorMessage = false;
            var promise = this.OnScreenKeyboardService.calculateResults(this.keyboardLayout, this.searchTerms);
            promise.then(function (response) {
                _this.results = response.data;
                if (_this.results == null || _this.results == "") {
                    _this.errorMessage = "No results found. Check the keyboard layout and search terms and try again.";
                    _this.isVisibleErrorMessage = true;
                }
            }).catch((function (reason) {
                _this.isVisibleErrorMessage = true;
                _this.errorMessage = reason.statusText;
            }));
        };
        OnScreenKeyboardController.prototype.clearEntries = function () {
            this.searchTerms = "";
            this.results = "";
            this.errorMessage = "";
            this.isVisibleErrorMessage = false;
        };
        OnScreenKeyboardController.prototype.populateKeyboardLayout = function (language) {
            if (language === 'Custom') {
                this.keyboardLayoutDisabled = false;
            }
            else {
                this.keyboardLayoutDisabled = true;
                this.keyboardLayout = this.OnScreenKeyboardService.getKeyboardLayout(language);
            }
            this.selectedKeyboardLayout = language;
        };
        OnScreenKeyboardController.$inject = ["OnScreenKeyboardService"];
        return OnScreenKeyboardController;
    }());
    App.OnScreenKeyboardController = OnScreenKeyboardController;
    angular.module("app").controller("OnScreenKeyboardController", OnScreenKeyboardController);
})(App || (App = {}));
