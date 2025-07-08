angular.module("ainapp")
    .controller("HomeController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {


            $scope.init = function () {


                $scope.Bookings = [];
                MainService.GetBookingsByRestaurant($scope.TokenInfo.RestaurantId).then(function (response) {
                    $scope.Bookings = response.data;
                }, function (err) {
                    toastr.error("Network Error");
                });

                $scope.Bookings2 = [];
                MainService.GetBillsByRestaurant($scope.TokenInfo.RestaurantId).then(function (response) {
                    $scope.Bookings2 = response.data;
                }, function (err) {
                    toastr.error("Network Error");
                });


            }
            $scope.init();

            $scope.viewDetail = function (data) {
                $state.go("BookingDetails", {
                    BookId: data.Id,
                });
            }


                    }])
