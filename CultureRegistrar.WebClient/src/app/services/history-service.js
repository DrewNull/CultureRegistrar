var app = require('../app');

app.factory('HistoryService', [
    '$http', '$log', 
    function ($http, $log) {
        var url = 'http://culture-registrar/history';
        return {
            list: function() {
                var promise = $http({
                    method: 'GET', 
                    url: url
                }).then(
                    function (response) {
                        $log.info('history.list=>success', response);
                        return response.data;
                    }, 
                    function (response) {
                        $log.error('history.list=>error', response);
                        return [];
                    }
                );
                return promise;
            }
        };
    }
]);