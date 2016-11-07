
myApp.factory('serviceAPI', ['$rootScope', '$http', '$q', '$injector',
function ($rootScope, $http, $q, $injector) {

    function callbackHandle(requestAsync) {
        var responseAsync = $q.defer();
        requestAsync.then(function successCallback(response) {
            errorHandle(response);
            responseAsync.resolve(response);
        }, function errorCallback(error) {
            errorHandle(error);
            responseAsync.reject(error);
        });
        return responseAsync;
    }

    function errorHandle(response) {

    }

    function del(url, config) {
        var deleteAsync = $http.delete(url, config);
        var responseAsync = callbackHandle(deleteAsync);
        return responseAsync.promise;
    }

    function get(url, config) {
        var getAsync = $http.get(url, config);
        var responseAsync = callbackHandle(getAsync);
        return responseAsync.promise;
    }

    function post(url, data, config) {
        var postAsync = $http.post(url, data, config);
        var responseAsync = callbackHandle(postAsync);
        return responseAsync.promise;
    }

    function put(url, data, config) {
        var putAsync = $http.put(url, data, config);
        var responseAsync = callbackHandle(putAsync);
        return responseAsync.promise;
    }

    return {
        del: del,
        get: get,
        post: post,
        put: put
    };

}]);
