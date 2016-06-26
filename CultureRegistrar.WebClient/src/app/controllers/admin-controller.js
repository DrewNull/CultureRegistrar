var app = require('../app');

app.controller('AdminController', [
    '$scope', 'AdminService', 
    function ($scope, adminService) {

        $scope.serverUrl = '';

        $scope.dependentAppPoolNames = [];

        $scope.recycleDependentAppPools = function() {
            adminService
                .recycleDependentAppPools();
        };

        function loadConfig() {
            adminService
                .getConfig()
                .then(
                    function(config) {
                        $scope.serverUrl = config.serverUrl;
                        $scope.dependentAppPoolNames = config.dependentAppPoolNames;
                    }
                );
        }

        loadConfig();
    }
]);