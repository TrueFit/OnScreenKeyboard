module App {
    "use strict";

    interface IOnScreenKeyboardDirective extends ng.IDirective {
    }

    interface IOnScreenKeyboardDirectiveScope extends ng.IScope {
    }

    interface IOnScreenKeyboardAttributes extends ng.IAttributes {
    }

    OnScreenKeyboardDirective.$inject = ["$window"];
    function OnScreenKeyboardDirective($window: ng.IWindowService): IOnScreenKeyboardDirective {
        return {
            restrict: "EA",
            link: link,
            templateUrl: "/Scripts/app/onScreenKeyboard/on-screen-keyboard.html",
            controller: OnScreenKeyboardController,
            controllerAs: "vm"
        }

        function link(scope: IOnScreenKeyboardDirectiveScope, element: ng.IAugmentedJQuery, attrs: IOnScreenKeyboardAttributes) {
        }
    }

    angular.module("app").directive("onScreenKeyboard", OnScreenKeyboardDirective);
}