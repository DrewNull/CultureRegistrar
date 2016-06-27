var app = require('../app');

app.controller('HistoryController', [
    '$scope', 'ConfigService', 'Constants', 'HistoryService', 
    function ($scope, configService, constants, historyService) {

        function list() {
            return configService.getConfig().then(
                function(config) {
                    return historyService.list(config).then(
                        function(logEntryBatches) {
                            console.log('logEntryBatches', logEntryBatches);
                            $scope.logEntryBatches = logEntryBatches; 
                        }
                    );
                }
            );
        }

        $scope.logEntryBatches = [];

        $scope.$on(constants.eventNames.culturesRegistered, function() {
            list();
        });

        $scope.$on(constants.eventNames.culturesUnregistered, function() {
            list();
        });

        list();
    }
]);