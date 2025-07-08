angular.module("ainapp")
    .controller("MyCuisineBookingsController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
    function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {



            $scope.init = function () {

                $scope.Bookings = [];
                MainService.GetCuisineBookingsByCustomerId($scope.TokenInfo.CustomerId).then(function (response) {
                    if (response.data) {
                        $scope.Bookings = response.data;
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });


            };
            $scope.init();






        }]);
