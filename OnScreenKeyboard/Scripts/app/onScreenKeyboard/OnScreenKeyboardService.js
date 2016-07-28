var App;
(function (App) {
    "use strict";
    var OnScreenKeyboardService = (function () {
        function OnScreenKeyboardService($http) {
            this.$http = $http;
        }
        OnScreenKeyboardService.prototype.calculateResults = function (alphabet, searchTerms) {
            //this.$http.post("/api/onscreenkeyboard/calculateResults", {
            //    alphabet: alphabet,
            //    searchTerms: searchTerms
            //}).then((response: ng.IHttpPromiseCallbackArg<string[]>) => {
            //    return response.data;
            //}).catch(((reason: ng.IHttpPromiseCallbackArg<string[]>) => {
            //    return null;
            //}));
        };
        OnScreenKeyboardService.$inject = ["$http"];
        return OnScreenKeyboardService;
    }());
    App.OnScreenKeyboardService = OnScreenKeyboardService;
    angular.module("app").service("OnScreenKeyboardService", OnScreenKeyboardService);
})(App || (App = {}));
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
