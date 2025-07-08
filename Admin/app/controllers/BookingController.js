angular.module("ainapp")
    .controller("BookingController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
    function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {

        
        $scope.getBookings = function () {
            $scope.Bookings = [];
            MainService.GetBookings2($scope.filter).then(function (response) {
                if (response.data) {
                    $scope.Bookings = response.data;
                } else {
                    toastr.error("Failed");
                }
            }, function (err) {
                toastr.error("Network Error");
            });

        }

            $scope.init = function () {

                var date = new Date();
                $scope.filter = {
                    FromDate: new Date(date.getFullYear(), date.getMonth() - 1, 1),
                    ToDate: new Date(),
                }

                $scope.getBookings();


            };

            $scope.init();




        }]);
