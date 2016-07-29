module App {
    "use strict";

    interface IOnScreenKeyboardController {
        errorMessage: string;
        isVisibleErrorMessage: boolean;

        keyboardLayout: string;
        searchTerms: string;

        keyboardLayoutDisabled: boolean;
        selectedKeyboardLayout: string;

        searchTermsPlaceholder: string;

        results: string;

        determineResults(): void;
        clearEntries(): void;
        populateKeyboardLayout(language: string): void;
    }

    export class OnScreenKeyboardController implements IOnScreenKeyboardController {
        errorMessage: string = "";
        isVisibleErrorMessage: boolean = false;

        keyboardLayout: string = "";
        searchTerms: string = "";

        keyboardLayoutDisabled: boolean = true;
        selectedKeyboardLayout: string = "No layout selected";

        searchTermsPlaceholder = "Example: IT Crowd";

        results: string = "";

        static $inject: string[] = ["OnScreenKeyboardService"];
        constructor(private OnScreenKeyboardService: OnScreenKeyboardService) {
        }

        public determineResults(): void {
            if (this.keyboardLayout == "" || this.searchTerms == "") {
                this.errorMessage = "Both the keyboard layout and the search terms are required to determine the results.";
                this.isVisibleErrorMessage = true;
                return;
            }

            this.isVisibleErrorMessage = false;

            var promise = this.OnScreenKeyboardService.calculateResults(this.keyboardLayout, this.searchTerms);
            
            promise.then((response: ng.IHttpPromiseCallbackArg<string>) => {
                this.results = response.data;

                if (this.results == null || this.results == "") {
                    this.errorMessage = "No results found. Check the keyboard layout and search terms and try again.";
                    this.isVisibleErrorMessage = true;
                }
            }).catch(((reason: ng.IHttpPromiseCallbackArg<string[]>) => {
                this.isVisibleErrorMessage = true;
                this.errorMessage = reason.statusText;
            }));
        }

        public clearEntries(): void {
            this.searchTerms = "";
            this.results = "";

            this.errorMessage = "";
            this.isVisibleErrorMessage = false;
        }

        public populateKeyboardLayout(language: string): void {
            if (language === 'Custom') {
                this.keyboardLayoutDisabled = false;
            } else {
                this.keyboardLayoutDisabled = true;
                this.keyboardLayout = this.OnScreenKeyboardService.getKeyboardLayout(language);
            }

            this.selectedKeyboardLayout = language;
        }
    }

    angular.module("app").controller("OnScreenKeyboardController", OnScreenKeyboardController);
}