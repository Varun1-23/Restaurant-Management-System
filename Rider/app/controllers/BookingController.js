angular.module("ainapp")
    .controller("BookingController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
    function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {



            $scope.init = function () {

                $scope.Bookings = [];
                MainService.GetAllBillsBydriver($scope.TokenInfo.DriverId).then(function (response) {
                    if (response.data) {
                        $scope.Bookings = response.data;
                    } else {
                        toastr.error("Failed");
                    }
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




        }]);
