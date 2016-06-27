var app = require('../app');

app.controller('HistoryController', [
    '$scope', 'Constants', 'HistoryService', 
    function ($scope, constants, historyService) {

        function list() {
            historyService.list().then(
                function(logEntryBatches) {
                    $scope.logEntryBatches = logEntryBatches; 
                }
            )
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