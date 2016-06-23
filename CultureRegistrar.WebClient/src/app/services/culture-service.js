var app = require('../app');

app.factory('CultureService', [
    '$http', '$log',  
    function($http, $log) {
        var url = 'http://culture-registrar/registration';
        return {
            list: function() {
                var promise = $http({
                    method: 'GET', 
                    url: url
                }).then(
                    function (response) {
                        $log.info('list=>success', response);
                        return response.data; // should be an array of strings (e.g., ['en-US', 'de-DE', 'es-MX'])
                    }, 
                    function (response) {
                        $log.error('list=>error', response);
                    }
                );
                return promise;
            }, 
            register: function(cultures) {
                var promise = $http({
                    data: cultures, 
                    headers: {
                        'Content-Type': 'application/json'
                    },  
                    method: 'PUT', 
                    url: url
                }).then(
                    function (response) {
                        $log.info('register=>success', response);
                    }, 
                    function (response) {
                        $log.error('register=>error', response);
                    }
                );
                return promise;
            }, 
            unregister: function(cultures) {
                var promise = $http({
                    data: cultures, 
                    headers: {
                        'Content-Type': 'application/json'
                    },  
                    method: 'DELETE', 
                    url: url
                }).then(
                    function (response) {
                        $log.info('unregister=>success', response);
                    }, 
                    function (response) {
                        $log.error('unregister=>error', response);
                    }
                );
                return promise;
            }
        };
    }
]);