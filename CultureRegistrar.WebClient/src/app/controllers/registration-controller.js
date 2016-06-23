var app = require('../app');

app.controller('RegistrationController', [
    '$rootScope', '$scope', 'CultureService', 
    function($rootScope, $scope, cultureService) {

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

                    // 1. clear textarea
                    $scope.cultureString = '';

                    // 2. emit culturesRegistered event
                    $rootScope.$broadcast('culturesRegistered');
                    
                }
            );
        }
    }
]);