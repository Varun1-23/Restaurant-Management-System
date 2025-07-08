angular.module("ainapp")
    .controller("HomeController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {


            $scope.init = function () {


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


                MainService.GetDistricts().then(function (response) {
                    if (response.data) {
                        $scope.Districts = response.data;
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

                $scope.Categories = [];


                MainService.GetCategories().then(function (response) {
                    if (response.data) {
                        $scope.Categories = response.data;
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

            }

            $scope.init();



            $scope.gotoDetails = function (data) {
                $state.go("RestaurantDetails", {
                    RestId: data.Id
                });

            }



                    }])
