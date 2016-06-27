var app = require('../app');

app.controller('RegistrationController', [
    '$rootScope', '$scope', 'ConfigService', 'Constants', 'CultureService', 
    function($rootScope, $scope, configService, constants, cultureService) {

        function splitCultureString() {
            return  $scope.cultureString.match(/[^\s]+/g);
        }

        $scope.cultureString = '';

        $scope.hasCultureString = function() { 
            return !!$scope.cultureString;
        };

        $scope.register = function () {
            var cultures = splitCultureString();
            return configService.getConfig().then(
                function(config) {
                    return cultureService.register(config, cultures).then(
                        function() { 
                            $scope.cultureString = '';
                            $rootScope.$broadcast(constants.eventNames.culturesRegistered);
                        }
                    );
                }
            );
        }

    }
]);