var app = angular.module('HomeApp', ['ngAnimate']);
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
    $interval(function () {
        $scope.getStates();
    }, 10000);

    //Get application states to start
    $scope.getStates();
}]);


app.directive('animateOnChange', function () {
    return function(scope, elem, attr) {

        scope.$watch(attr.animateOnChange, function(newValue, oldValue) {

            var classes = ["bg-danger", "bg-success", "bg-warning"];
            var classFound = "";
            for (var i = 0; i < classes.length; i++) {
                if ($(elem).attr("class").search(classes[i]) > -1) {
                    classFound = classes[i];
                    console.warn($(elem).attr("class"));
                    break;
                }
            }
            $(elem).switchClass(classFound, newValue, 100, "easeInOutQuad");
        });
    };
})