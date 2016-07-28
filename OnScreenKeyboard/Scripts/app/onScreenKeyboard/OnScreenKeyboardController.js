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
            this.getValues();
        }
        OnScreenKeyboardController.prototype.getValues = function () {
            var _this = this;
            this.$http.get("/api/onscreenkeyboard")
                .then(function (response) {
                _this.isVisibleErrorMessage = false;
                _this.values = response.data;
            })
                .catch((function (reason) {
                _this.isVisibleErrorMessage = true;
                _this.errorMessage = reason.statusText;
                return _this.values;
            }));
        };
        OnScreenKeyboardController.$inject = ["$http", "$window"];
        return OnScreenKeyboardController;
    }());
    App.OnScreenKeyboardController = OnScreenKeyboardController;
    angular.module("app").controller("OnScreenKeyboardController", OnScreenKeyboardController);
})(App || (App = {}));
//# sourceMappingURL=OnScreenKeyboardController.js.map