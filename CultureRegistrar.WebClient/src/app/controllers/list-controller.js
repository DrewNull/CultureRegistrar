var app = require('../app');

app.controller('ListController', [
    '$rootScope', '$scope', 'Constants', 'CultureService', 
    function ($rootScope, $scope, constants, cultureService) {
        
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

        $scope.$on(constants.eventNames.culturesRegistered, function(event, args) {
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
                    list();
                    $rootScope.$broadcast(constants.eventNames.culturesUnregistered);
                }
            );
        };

        list();
    }
]);