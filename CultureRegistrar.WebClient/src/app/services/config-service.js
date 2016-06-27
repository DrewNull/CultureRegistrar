var app = require('../app');

app.factory('ConfigService', [
    '$http', '$log', 
    function ($http, $log) { 
        
        var config;

        function loadConfig() {
            var promise = $http
                .get('./static/js/config.json')
                .then(
                    function (response) {
                        $log.info('admin.getConfig=>success');
                        config = response.data;
                        return config;
                    }, 
                    function (response) {
                        $log.error('admin.getConfig=>error');
                        $log.error('Rename "/static/js/config.json.example" to "/static/js/config.json".');
                    }
                );
            return promise;
        }

        return { 

            getConfig: function() { 
                if (config) {
                    return new Promise(function(resolve) {
                        resolve(config);
                    });
                } else {
                    return loadConfig();
                }
            }

        }
    }
]);