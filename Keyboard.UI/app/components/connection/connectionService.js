app.service('connectionService', function () {
    this.getBaseServiceURL = function () {
        return 'http://localhost:56688/api/';
    };
});