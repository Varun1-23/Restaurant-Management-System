angular.module("ainapp")
    .controller("BookingController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
        function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {



            $scope.getBookings = function () {
                $scope.Bookings = [];
                MainService.GetBookingsByRestaurant2($scope.filter).then(function (response) {
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
                    RestaurantId: $scope.TokenInfo.RestaurantId
                }

                $scope.getBookings();


                $scope.Employees = [];
                MainService.GetDriversbyRestId($scope.TokenInfo.RestaurantId).then(function (response) {
                    if (response.data) {
                        $scope.Employees = response.data;
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

                $scope.Statuses = [];
                MainService.GetStatuses().then(function (response) {
                    if (response.data) {
                        $scope.Statuses = response.data;
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


            $scope.changeDriver = function (data) {
                MainService.UpdateDriver(data.Id, data.DriverId).then(function (response) {
                    if (response.data) {
                        toastr.success("Success");
                        $scope.init();
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });
            }


            $scope.changeStatus = function (data) {
                MainService.UpdateStatus(data.Id, data.StatusId).then(function (response) {
                    if (response.data) {
                        toastr.success("Success");
                        $scope.init();
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });
            }





        }]);
