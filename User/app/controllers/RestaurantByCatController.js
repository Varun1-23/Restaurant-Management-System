angular.module("ainapp")
    .controller("RestaurantByCatController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {

            $scope.Restaurants = [];


            MainService.GetRestaurantsByCatId($stateParams.CatId).then(function (response) {
                if (response.data) {
                    $scope.Restaurants = response.data;
                } else {
                    toastr.error("Failed");
                }
            }, function (err) {
                toastr.error("Network Error");
            });

        }]);
