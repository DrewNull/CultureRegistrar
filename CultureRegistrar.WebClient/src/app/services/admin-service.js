var app = require('../app');

app.factory('AdminService', [
    '$http', '$log', 'ConfigService', 
    function($http, $log, configService) {
        
        return { 

            recycleDependentAppPools: function() {
                return configService.getConfig().then(
                    function(config) {
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
                );
            }

        };
    }
]);