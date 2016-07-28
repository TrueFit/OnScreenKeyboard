module App {
    "use strict";

    interface IOnScreenKeyboardService {
        calculateResults(alphabet: string, searchTerms: string): any;
    }

    export class OnScreenKeyboardService implements IOnScreenKeyboardService {

        static $inject: string[] = ["$http"];
        constructor(private $http: ng.IHttpService) {
        }

        public calculateResults(alphabet: string, searchTerms: string): any {
            //this.$http.post("/api/onscreenkeyboard/calculateResults", {
            //    alphabet: alphabet,
            //    searchTerms: searchTerms
            //}).then((response: ng.IHttpPromiseCallbackArg<string[]>) => {
            //    return response.data;
            //}).catch(((reason: ng.IHttpPromiseCallbackArg<string[]>) => {
            //    return null;
            //}));
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