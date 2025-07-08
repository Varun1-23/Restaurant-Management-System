angular.module("ainapp")
    .controller("RestaurantsByDistrictController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {

            $scope.Restaurants = [];

            $scope.query = {};
            $scope.queryBy = '$'


            MainService.GetRestaurantsByDistrictId($stateParams.DistrictId).then(function (response) {
                if (response.data) {
                    $scope.Restaurants = response.data;
                } else {
                    toastr.error("Failed");
                }
            }, function (err) {
                toastr.error("Network Error");
            });

        }]);
