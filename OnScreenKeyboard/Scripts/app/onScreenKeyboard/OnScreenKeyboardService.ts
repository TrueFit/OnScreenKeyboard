module App {
    "use strict";

    interface IOnScreenKeyboardService {
        calculateResults(alphabet: string, searchTerms: string): string;
    }

    export class OnScreenKeyboardService implements IOnScreenKeyboardService {

        static $inject: string[] = ["$http"];
        constructor(private $http: ng.IHttpService) {
        }

        public calculateResults(alphabet: string, searchTerms: string): string {
            //this.$http.post("/api/onscreenkeyboard/calculateResults")
            return "got search terms " + searchTerms;
        }
    }

    angular.module("app").service("OnScreenKeyboardService", OnScreenKeyboardService);
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