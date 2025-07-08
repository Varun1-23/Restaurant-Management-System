angular.module("ainapp")
    .controller("BookingController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
    function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {


            $scope.getBookings = function () {
                $scope.Bookings = [];
                MainService.GetBookingsByCustomer2($scope.filter).then(function (response) {
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
                    CustomerId: $scope.TokenInfo.CustomerId
                }

                $scope.getBookings();



            };
            $scope.init();

            $scope.viewDetail = function (data) {
                $state.go("BookingDetails", {
                    BookId: data.Id,
                });
            }



            $scope.goReview = function (data) {
                $scope.rating = {
                    Rating: 0,
                    Description: "",
                    RestaurantId: data.RestaurantId,
                    BookingId: data.Id,
                    CustomerId: $scope.TokenInfo.CustomerId,
                }
                $('#myModal').modal('show');
            }

            $scope.SendReview = function () {

                MainService.SaveReview($scope.rating).then(function (response) {
                    if (response.data) {
                        toastr.success("success");
                        $('#myModal').modal('hide');
                    } else {
                        toastr.error("Network Error");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                })
            }






        }]);
