angular.module("ainapp")
    .controller("BookingDetailsController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
    function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {


            $scope.init = function () {


                $scope.BookingDetail = {};
                MainService.GetBookingsDetail($stateParams.BookId).then(function (response) {
                    if (response.data) {
                        $scope.BookingDetail = response.data;
                    } else {
                        toastr.error("Failed ");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });


            };
            $scope.init();

            $scope.cancelBooking = function () {
                $('#myModal').modal('show');
            }

            $scope.cancelBookingConfirm = function () {
                MainService.CancelBooking($scope.BookingDetail.Id).then(function (response) {
                    if (response.data == 1) {
                        toastr.success("Success");
                        $('#myModal').modal('hide');
                        $scope.init();
                    } else if (response.data == 2) {
                        toastr.warning("Order Started Processing, Cannot Cancel. Pls contact us in case any trouble.");
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });
            };



        }]);
