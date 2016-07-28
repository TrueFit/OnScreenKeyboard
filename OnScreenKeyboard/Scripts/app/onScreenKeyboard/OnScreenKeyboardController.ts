module App {
    "use strict";

    interface IOnScreenKeyboardController {
        values: string[];
        errorMessage: string;
        isVisibleErrorMessage: boolean;

        alphabet: string;
        searchTerms: string;
        results: string;

        determineOnScreenKeyboard(): void;
        clearEntries(): void;
    }

    export class OnScreenKeyboardController implements IOnScreenKeyboardController {
        errorMessage: string = "";
        isVisibleErrorMessage: boolean = false;
        values: string[] = [];

        alphabet: string = "";
        searchTerms: string = "";

        results: string = "D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#"

        static $inject: string[] = ["$http", "$window"];
        constructor(private $http: ng.IHttpService, private $window: ng.IWindowService) {
            //this.getValues();
        }

        public determineOnScreenKeyboard(): void {
        }

        public clearEntries(): void {
            this.alphabet = "";
            this.searchTerms = "";
            this.results = "";
        }

        //private getValues(): void {
        //    this.$http.get("/api/onscreenkeyboard")
        //        .then((response: ng.IHttpPromiseCallbackArg<string[]>) => {
        //            this.isVisibleErrorMessage = false;
        //            this.values = response.data;
        //        })
        //        .catch(((reason: ng.IHttpPromiseCallbackArg<string[]>) => {
        //            this.isVisibleErrorMessage = true;
        //            this.errorMessage = reason.statusText;
        //            return this.values;
        //    }));
        //}
    }

    angular.module("app").controller("OnScreenKeyboardController", OnScreenKeyboardController);
}