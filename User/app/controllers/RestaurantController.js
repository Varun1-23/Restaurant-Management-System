angular.module("ainapp")
    .controller("RestaurantController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {


            $scope.query = {};
            $scope.queryBy = '$'

            $scope.Restaurants = [];


            MainService.GetRestaurants().then(function (response) {
                if (response.data) {
                    $scope.Restaurants = response.data;
                } else {
                    toastr.error("Failed");
                }
            }, function (err) {
                toastr.error("Network Error");
            });

        }]);
