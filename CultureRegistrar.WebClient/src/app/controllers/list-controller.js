var app = require('../app');

app.controller('ListController', [
    '$rootScope', '$scope', 'CultureService', 
    function ($rootScope, $scope, cultureService) {
        
        $scope.cultures = [];

        $scope.selectedCultures = [];

        function list() {
            cultureService.list().then(
                function(cultures) {
                    $scope.cultures = cultures;
                }
            );
        }

        $scope.hasSelectedCultures = function() {
            return $scope.selectedCultures.length > 0;
        };

        $scope.isSelected = function(culture) {
            return _.some($scope.selectedCultures, culture);
        };

        $scope.$on('culturesRegistered', function() {
            list();
        });

        $scope.toggleSelect = function(culture) { 
            $scope.selectedCultures = _.xorBy($scope.selectedCultures, [culture], 'code');
            console.log($scope.selectedCultures);
        }

        $scope.unregister = function() {
            var cultures = _.map($scope.selectedCultures, 'code');
            cultureService.unregister(cultures).finally(
                function() {

                    // 1. refresh list
                    list();

                    // 2. emit culturesUnregistered event
                    $rootScope.$broadcast('culturesUnregistered');

                }
            );
        };

        list();
    }
]);