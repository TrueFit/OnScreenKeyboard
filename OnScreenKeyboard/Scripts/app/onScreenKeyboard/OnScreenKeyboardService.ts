module App {
    "use strict";

    interface IOnScreenKeyboardService {
        languages: { [key: string]: string; };

        calculateResults(keyboardLayout: string, searchTerms: string): any;
        getKeyboardLayout(language: string): string;
    }

    export class OnScreenKeyboardService implements IOnScreenKeyboardService {

        languages: { [key: string]: string; } = {
            "Alphabetical English": "ABCDEF\nGHIJKL\nMNOPQR\nSTUVWX\nYZ1234\n567890",
            "QWERTY English": "QWERTYUIOP\nASDFGHJKL\nZXCVBNM\n1234567890"
        };

        static $inject: string[] = ["$http"];
        constructor(private $http: ng.IHttpService) {
        }

        public calculateResults(keyboardLayout: string, searchTerms: string): any {
            return this.$http.post("/api/onscreenkeyboard/calculateResults", {
                keyboardLayout: keyboardLayout,
                searchTerms: searchTerms
            });
        }

        public getKeyboardLayout(language: string): string {
            return this.languages[language];
        }
    }

    angular.module("app").service("OnScreenKeyboardService", OnScreenKeyboardService);
}