var app = require('../app');

app.factory('AdminService', [
    '$http', '$log', 
    function($http, $log) {
        return { 

            recycleDependentAppPools: function(config) {
                var promise = $http({
                        data: appPoolNames, 
                        headers: {
                            'Content-Type': 'application/json'
                        },  
                        method: 'POST', 
                        url: config + '/admin'
                    }).then(
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