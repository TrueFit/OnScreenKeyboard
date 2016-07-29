var App;
(function (App) {
    "use strict";
    OnScreenKeyboardDirective.$inject = ["$window"];
    function OnScreenKeyboardDirective($window) {
        return {
            restrict: "EA",
            link: link,
            templateUrl: "/Scripts/app/onScreenKeyboard/on-screen-keyboard.html",
            controller: App.OnScreenKeyboardController,
            controllerAs: "vm"
        };
        function link(scope, element, attrs) {
        }
    }
    angular.module("app").directive("onScreenKeyboard", OnScreenKeyboardDirective);
})(App || (App = {}));
