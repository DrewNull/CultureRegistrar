var app = require('../app');

app.controller('RegistrationController', [
    '$rootScope', '$scope', 'Constants', 'CultureService', 
    function($rootScope, $scope, constants, cultureService) {

        function splitCulureString() {
            return  $scope.cultureString.match(/[^\s]+/g);
        }

        $scope.cultureString = '';

        $scope.hasCultureString = function() { 
            return !!$scope.cultureString;
        };

        $scope.register = function () {
            var cultures = splitCulureString();
            cultureService.register(cultures).finally(
                function() { 
                    $scope.cultureString = '';
                    $rootScope.$broadcast(constants.eventNames.culturesRegistered);
                }
            );
        }
    }
]);