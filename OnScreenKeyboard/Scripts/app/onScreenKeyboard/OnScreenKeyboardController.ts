module App {
    "use strict";

    interface IOnScreenKeyboardController {
        values: string[];
        errorMessage: string;
        isVisibleErrorMessage: boolean
    }

    export class OnScreenKeyboardController implements IOnScreenKeyboardController {
        errorMessage: string = "";
        isVisibleErrorMessage: boolean = false;
        values: string[] = [];

        static $inject: string[] = ["$http", "$window"];
        constructor(private $http: ng.IHttpService, private $window: ng.IWindowService) {
            this.getValues();
        }

        private getValues(): void {
            this.$http.get("/api/onscreenkeyboard")
                .then((response: ng.IHttpPromiseCallbackArg<string[]>) => {
                    this.isVisibleErrorMessage = false;
                    this.values = response.data;
                })
                .catch(((reason: ng.IHttpPromiseCallbackArg<string[]>) => {
                    this.isVisibleErrorMessage = true;
                    this.errorMessage = reason.statusText;
                    return this.values;
            }));
        }
    }

    angular.module("app").controller("OnScreenKeyboardController", OnScreenKeyboardController);
}