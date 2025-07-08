angular.module("ainapp")
    .controller("walletController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
    function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {



            $scope.init = function () {

                $scope.Total = 0;
                $scope.Bookings = [];
                MainService.GetDriverWallet($scope.TokenInfo.DriverId).then(function (response) {
                    if (response.data) {
                        $scope.Bookings = response.data;
                        angular.forEach($scope.Bookings, function (e) {
                            $scope.Total = $scope.Total + e.Amount;
                        })
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });



            }
            $scope.init();






        }]);
