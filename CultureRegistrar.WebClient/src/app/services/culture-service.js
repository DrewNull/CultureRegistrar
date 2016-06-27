var app = require('../app');

app.factory('CultureService', [
    '$http', '$log',  
    function($http, $log) {

        function getRegistrationUrl(config) {
            return config.serverUrl + '/registration';
        }

        return {

            list: function(config) {
                var promise = $http({
                    method: 'GET', 
                    url: getRegistrationUrl(config)
                }).then(
                    function (response) {
                        $log.info('culture.list=>success', response);
                        return response.data; // should be an array of strings (e.g., ['en-US', 'de-DE', 'es-MX'])
                    }, 
                    function (response) {
                        $log.error('culture.list=>error', response);
                    }
                );
                return promise;
            }, 

            register: function(config, cultures) {
                var promise = $http({
                    data: cultures, 
                    headers: {
                        'Content-Type': 'application/json'
                    },  
                    method: 'PUT', 
                    url: getRegistrationUrl(config)
                }).then(
                    function (response) {
                        $log.info('culture.register=>success', response);
                    }, 
                    function (response) {
                        $log.error('culture.register=>error', response);
                    }
                );
                return promise;
            }, 

            unregister: function(config, cultures) {
                var promise = $http({
                    data: cultures, 
                    headers: {
                        'Content-Type': 'application/json'
                    },  
                    method: 'DELETE', 
                    url: getRegistrationUrl(config)
                }).then(
                    function (response) {
                        $log.info('culture.unregister=>success', response);
                    }, 
                    function (response) {
                        $log.error('culture.unregister=>error', response);
                    }
                );
                return promise;
            }

        };
    }
]);