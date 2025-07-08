angular.module("ainapp")
    .controller("BookingDetailsController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
    function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {


            $scope.StatusList = [];
            MainService.GetStatuses($stateParams.BookId).then(function (response) {
                if (response.data) {
                    $scope.StatusList = response.data;
                } else {
                    toastr.error("Failed ");
                }
            }, function (err) {
                toastr.error("Network Error");
            });


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
                MainService.UpdateStatus($scope.BookingDetail.Id, $scope.BookingDetail.StatusId).then(function (response) {
                    if (response.data == 1) {
                        toastr.success("Success");
                        $('#myModal').modal('hide');
                        $scope.init();
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });
            };



        }]);
