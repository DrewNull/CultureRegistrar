var app = angular.module('CultureRegistrarApp', []);

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

app.factory('HistoryService', [
    '$http', 
    function ($http) {
        var url = 'http://culture-registrar/history';
        return {
            list: function() {
                var promise = $http({
                    method: 'GET', 
                    url: url
                }).then(
                    function (response) {
                        $log.info('list=>success', response);
                        return response.data;
                    }, 
                    function (response) {
                        $log.error('list=>error', response);
                        return [];
                    }
                );
                return promise;
            }
        };
    }
]);

app.controller('HomeController', [
    '$rootScope', '$scope', 'CultureService', 
    function ($rootScope, $scope, cultureService) {
        
        $scope.cultures = [];

        $scope.selectedCultures = [];

        function list() {
            cultureService.list().then(
                function(cultures) {
                    $scope.cultures = cultures;
                }
            );
        }

        $scope.hasSelectedCultures = function() {
            return $scope.selectedCultures.length > 0;
        };

        $scope.isSelected = function(culture) {
            return _.some($scope.selectedCultures, culture);
        };

        $scope.$on('culturesRegistered', function() {
            list();
        });

        $scope.toggleSelect = function(culture) { 
            $scope.selectedCultures = _.xorBy($scope.selectedCultures, [culture], 'code');
            console.log($scope.selectedCultures);
        }

        $scope.unregister = function() {
            var cultures = _.map($scope.selectedCultures, 'code');
            cultureService.unregister(cultures).finally(
                function() {

                    // 1. refresh list
                    list();

                    // 2. emit culturesUnregistered event
                    $rootScope.$broadcast('culturesUnregistered');

                }
            );
        };

        list();
    }
]);

app.controller('RegistrationController', [
    '$rootScope', '$scope', 'CultureService', 
    function($rootScope, $scope, cultureService) {

        function splitCulureString() {
            return  $scope.cultureString.match(/[^\s]+/g);
        }

        $scope.cultureString = '';

        $scope.hasCultureString = function() { 
            return !!$scope.cultureString;
        };

        $scope.register = function () {
            var cultures = splitCulureString();
            cultureService.register(cultures).finally(
                function() { 

                    // 1. clear textarea
                    $scope.cultureString = '';

                    // 2. emit culturesRegistered event
                    $rootScope.$broadcast('culturesRegistered');
                    
                }
            );
        }
    }
]);

app.controller('HistoryController', [
    '$scope', 'HistoryService', 
    function ($scope, historyService) {

    }
]);