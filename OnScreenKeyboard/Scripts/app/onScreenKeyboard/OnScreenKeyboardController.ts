module App {
    "use strict";

    // TODO add tranlations?

    interface IOnScreenKeyboardController {
        errorMessage: string;
        isVisibleErrorMessage: boolean;

        alphabet: string;
        searchTerms: string;

        alphabetPlaceholder: string;
        searchTermsPlaceholder: string;

        results: string;

        determineResults(): void;
        clearEntries(): void;
        populateEnglish(): void;
    }

    export class OnScreenKeyboardController implements IOnScreenKeyboardController {
        errorMessage: string = "";
        isVisibleErrorMessage: boolean = false;

        alphabet: string = "";
        searchTerms: string = "";

        alphabetPlaceholder: string = "ABCDEF\nGHIJKL\nMNOPQR\nSTUVWX\nYZ1234\n567890";
        searchTermsPlaceholder = "IT Crowd";

        results: string = "";

        static $inject: string[] = ["$http", "$window", "OnScreenKeyboardService"];
        constructor(private $http: ng.IHttpService, private $window: ng.IWindowService, private OnScreenKeyboardService: OnScreenKeyboardService) {
        }

        public determineResults(): void {
            if (this.alphabet == "" || this.searchTerms == "") {
                this.errorMessage = "Both the alphabet and the search terms are required to determine the results.";
                this.isVisibleErrorMessage = true;
                return;
            }

            this.isVisibleErrorMessage = false;
            this.results = this.OnScreenKeyboardService.calculateResults(this.alphabet, this.searchTerms);

            if (this.results == null || this.results == "") {
                this.errorMessage = "Something went wrong with determining the results. Check the alphabet and search terms and try again.";
                this.isVisibleErrorMessage = true;
            }
        }

        public clearEntries(): void {
            this.alphabet = "";
            this.searchTerms = "";
            this.results = "";

            this.errorMessage = "";
            this.isVisibleErrorMessage = false;
        }

        // TODO make this generic for any language (pass in lang, use service to get alphabet?) wasn't working when passing in english\
        // TODO try using a select instead of a dropdown?
        public populateEnglish(): void {
            this.alphabet = this.alphabetPlaceholder;
        }
    }

    angular.module("app").controller("OnScreenKeyboardController", OnScreenKeyboardController);
}