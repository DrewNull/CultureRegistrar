var app = require('../app');

app.factory('AdminService', [
    '$http', '$log', 
    function($http, $log) {

        return { 

            getConfig: function() {
                var promise = $http
                    .get('./static/js/config.json')
                    .then(
                        function (response) {
                            $log.info('admin.getConfig=>success');
                            return response.data;
                        }, 
                        function (response) {
                            $log.error('admin.getConfig=>error');
                            $log.error('Rename "/static/js/config.json.example" to "/static/js/config.json".');
                        }
                    );
                return promise;
            }, 

            recycleDependentAppPools: function() {
                var url = '';
                var promise = $http
                    .post(url)
                    .then(
                        function(response) {
                            $log.info('admin.recycleDependentAppPools=>success', response);
                        }, 
                        function(response) {
                            $log.error('admin.recycleDependentAppPools=>error', response);
                        }
                    );
                return promise;
            }
        };
    }
]);