var App;
(function (App) {
    "use strict";
    MyAppDirective.$inject = ["$window"];
    function MyAppDirective($window) {
        return {
            restrict: "EA",
            link: link,
            templateUrl: "/Scripts/app/onScreenKeyboard/on-screen-keyboard.html",
            controller: App.MyAppController,
            controllerAs: "vm"
        };
        function link(scope, element, attrs) {
        }
    }
    angular.module("app").directive("myApp", MyAppDirective);
})(App || (App = {}));
