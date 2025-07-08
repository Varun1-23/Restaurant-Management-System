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


            $scope.DeliverOrder = function () {
                MainService.DeliverOrder($scope.BookingDetail.Id).then(function (response) {
                    if (response.data == 1) {
                        toastr.success("Success");
                        $state.go("home");
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });
            };



        }]);
