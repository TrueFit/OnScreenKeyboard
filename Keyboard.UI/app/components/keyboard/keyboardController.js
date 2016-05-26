app.controller('keyboardController', function ($scope, $http, keyboardService, connectionService, animationService) {

    $scope.Term = "";
    $scope.KeyboardType = "OnScreen";
    $scope.Rows;
    $scope.Path = {};
    $scope.TextActions = "";
    $scope.HasError = false;
    $scope.Errors = [];

    function initKeyboard() {
        keyboardService.getKeyboard('OnScreen').then(
            function (Rows) {
                $scope.Rows = Rows;
            }, function (error) {
                $scope.Validate.Errors.Add(error.data.ExceptionMessage);
            }
        );
    }

    $scope.Process = {
        File: function (event) {

            $scope.Validate.File.All();

            if ($scope.HasError == false) {

                var uploadedFile = document.getElementById("textUpload").files[0];
                var reader = new FileReader();

                reader.onload = function (e) {
                    output = e.target.result;
                    $scope.Process.Content(output);
                };

                reader.readAsText(uploadedFile);
            }
        },
        Content: function (txt) {
            $scope.Term = txt;
            keyboardService.getKeyboardPath(txt, 'OnScreen').then(
                function (Result) {
                    $scope.Path = Result;
                    $scope.TextActions = $scope.Process.PathString(Result).join(",");
                    $scope.Process.Animation(Result);
                },
                function (error) {
                    $scope.Validate.Errors.Add(error.data.ExceptionMessage);
                }
            );
        },
        PathString: function (Result) {
            var actions = new Array();
            for (var i = 0; i < Result.length; i++)
                actions.push(Result[i].Name);

            return actions;
        },
        Animation: function (Result) {
            var currentX = 0;
            var currentY = 0;

            animationService.animationCount = 0;
            $(".key").clearQueue();

            for (var i = 0; i < Result.length; i++) {

                currentX += Result[i].XMovement;
                currentY += Result[i].YMovement;

                var selector = "[key-coor='" + currentX + "," + currentY + "']";

                var animation = Result[i].Name == "#" ? "pulseBackground" : "pulseBorder";

                if (Result[i].Name != "S")
                    animationService.queueAnimation(selector, animation);

            }
        }
    }

    $scope.Validate = {
        File: {
            All: function () {
                
                $scope.Validate.Errors.Clear();

                var uploadedFiles = document.getElementById("textUpload").files;

                if ($scope.Validate.File.HasFile(uploadedFiles) == false)
                    $scope.Validate.Errors.Add("You must select a file");

                if ($scope.HasError == false) {
                    if ($scope.Validate.File.Count(uploadedFiles) == false)
                        $scope.Validate.Errors.Add("You can only upload one file at a time");

                    if ($scope.Validate.File.Type(uploadedFiles[0]) == false)
                        $scope.Validate.Errors.Add("Only text files are allowed");

                    if ($scope.Validate.File.Size(uploadedFiles[0]) == false)
                        $scope.Validate.Errors.Add("The file size cannot be larger than 500kb");
                }
                
            },
            Type: function (file) {
                return file.type == "text/plain";
            },
            Size: function (file) {
                return file.size < 500000;
            },
            Count: function (files) {
                return files.length == 1;
            },
            HasFile: function (files) {
                return files.length > 0;
            }
        },
        Errors: {
            Clear: function() {
                $scope.HasError = false;
                $scope.Errors = [];
            },
            Add: function(message) {
                $scope.HasError = true;
                $scope.Errors.push(message);
            }
        }
    }

    initKeyboard();

});