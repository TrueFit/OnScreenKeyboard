app.service('keyboardService', function (connectionService, $http, $q) {

    var self = this;

    this.getKeyboardPath = function (term, keyboardType) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: connectionService.getBaseServiceURL() + 'keyboard/GetKeyboardPath?searchTerm=' + term + '&keyboardType=' + keyboardType
        }).then(
        function successCallback(response) {
            deferred.resolve(response.data);
        },
        function errorCallback(response) {
            deferred.reject(response);
        });
        return deferred.promise;
    }

    this.getKeyboard = function (keyboardType) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: connectionService.getBaseServiceURL() + 'keyboard/GetKeyboard?keyboardType=' + keyboardType
        }).then(
        function successCallback(response) {
            deferred.resolve(response.data);
        },
        function errorCallback(response) {
            deferred.reject(response);
        });
        return deferred.promise;
    }

});