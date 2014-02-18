var app = angular.module('HomeApp', []);
app.controller("HomeController", ['$scope', '$http', '$interval', function ($scope, $http, $interval) {
    
    //function to get the sates from the api controller
    $scope.getStates = function () {
        $http.get("/api/applicationstateapi/get")
            .success(function (data) {
            $scope.states = data;
            }).error(function (err) {
                console.log("get error : " + err);
            });
    };
    
    //get the state of the application every second
    $interval(function() {
        $scope.getStates();
    }, 1000);
    
    //Get application states to start
    $scope.getStates();
}]);