var app = require('../app');

app.factory('HistoryService', [
    '$http', '$log', 'ConfigService', 
    function ($http, $log, configService) {
        return {

            list: function() {
                return configService.getConfig().then(
                    function(config) {
                        var promise = $http({
                            method: 'GET', 
                            url: config.serverUrl + '/history'
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
                );
            }

        };
    }
]);