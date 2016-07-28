var App;
(function (App) {
    "use strict";
    var OnScreenKeyboardService = (function () {
        function OnScreenKeyboardService($http) {
            this.$http = $http;
            this.languages = {
                "Alphabetical English": "ABCDEF\nGHIJKL\nMNOPQR\nSTUVWX\nYZ1234\n567890",
                "QWERTY English": "QWERTYUIOP\nASDFGHJKL\nZXCVBNM\n1234567890"
            };
        }
        OnScreenKeyboardService.prototype.calculateResults = function (keyboardLayout, searchTerms) {
            return this.$http.post("/api/onscreenkeyboard/calculateResults", {
                keyboardLayout: keyboardLayout,
                searchTerms: searchTerms
            });
        };
        OnScreenKeyboardService.prototype.getKeyboardLayout = function (language) {
            return this.languages[language];
        };
        OnScreenKeyboardService.$inject = ["$http"];
        return OnScreenKeyboardService;
    }());
    App.OnScreenKeyboardService = OnScreenKeyboardService;
    angular.module("app").service("OnScreenKeyboardService", OnScreenKeyboardService);
})(App || (App = {}));
//# sourceMappingURL=OnScreenKeyboardService.js.map