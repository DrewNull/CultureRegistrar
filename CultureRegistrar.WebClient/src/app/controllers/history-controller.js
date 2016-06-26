var app = require('../app');

app.controller('HistoryController', [
    '$scope', 'HistoryService', 
    function ($scope, historyService) {

        function list() {
            historyService.list().then(
                function(logEntryBatches) {
                    $scope.logEntryBatches = logEntryBatches; 
                }
            )
        }

        $scope.logEntryBatches = [];

        $scope.$on('culturesRegistered', function() {
            list();
        });

        $scope.$on('culturesUnregistered', function() {
            list();
        });

        list();
    }
]);