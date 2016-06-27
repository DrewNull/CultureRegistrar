var app = require('../app');

app.controller('ListController', [
    '$rootScope', '$scope', 'ConfigService', 'Constants', 'CultureService', 
    function ($rootScope, $scope, configService, constants, cultureService) {
        
        $scope.cultures = [];

        $scope.selectedCultures = [];

        function list() {
            return configService.getConfig().then(
                function(config) {
                    return cultureService.list(config).then(
                        function(cultures) {
                            $scope.cultures = cultures;
                        }
                    );
                });
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
        }

        $scope.unregister = function() {
            var cultures = _.map($scope.selectedCultures, 'code');
            return configService.getConfig().then(
                function(config) {
                    return cultureService.unregister(config, cultures).then(
                        function() {
                            list();
                            $rootScope.$broadcast(constants.eventNames.culturesUnregistered);
                        }
                    );
                });
        };

        list();
    }
]);