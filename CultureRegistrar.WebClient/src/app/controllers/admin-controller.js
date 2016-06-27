var app = require('../app');

app.controller('AdminController', [
    '$scope', 'AdminService', 'ConfigService', 
    function ($scope, adminService, configService) {

        $scope.serverUrl = '';

        $scope.dependentAppPoolNames = [];

        $scope.recycleDependentAppPools = function() {
            adminService.recycleDependentAppPools();
        };

        function loadConfig() {
            configService
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