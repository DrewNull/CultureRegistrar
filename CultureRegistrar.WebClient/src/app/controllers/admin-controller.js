var app = require('../app');

app.controller('AdminController', [
    '$scope', 'AdminService', 'ConfigService', 
    function ($scope, adminService, configService) {

        $scope.serverUrl = '';

        $scope.dependentAppPoolNames = [];

        $scope.recycleDependentAppPools = function() {
            return configService.getConfig().then(
                function (config) {
                    adminService.recycleDependentAppPools(config);
                }
            );
        };

        function loadConfig() {
            return configService.getConfig().then(
                function(config) {
                    $scope.serverUrl = config.serverUrl;
                    $scope.dependentAppPoolNames = config.dependentAppPoolNames;
                }
            );
        }

        loadConfig();
    }
]);