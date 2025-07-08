angular.module("ainapp")
    .controller("BookingSeatsController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {


            $scope.Restaurants = [];
            $scope.query = {};
            $scope.queryBy = '$'


            $scope.init = function () {

                $scope.TokenInfo = AuthenticationService.getTokenInfo();

                MainService.GetRestaurants().then(function (response) {
                    if (response.data) {
                        $scope.Restaurants = response.data;
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

                $scope.book = {
                    Date: new Date(),
                    Time: "",
                    CuisineId: "",
                    RestaurantId: "",
                    CustomerId: "",
                }

            }
            $scope.init();

            $scope.goToRestaurantTables = function (data) {
                $scope.book.RestaurantId = data.Id;
                $scope.Tables = [];
                MainService.GetCuisinesByRestId(data.Id).then(function (response) {
                    if (response.data) {
                        $scope.Tables = response.data;
                        $('#viewModal').modal('show');
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

            }


            $scope.selectTable = function (data) {
                angular.forEach($scope.Tables, function (e) {
                    e.IsSelected = false;
                })
                data.IsSelected = true;
                $scope.book.CuisineId = data.Id;
            }


            $scope.bookCuisine = function () {
                if ($scope.IsLoggedIn == false) {
                    toastr.info("Please Login To Continue");
                } else if ($scope.book.CuisineId == "") {
                    toastr.info("Choose Cuisine");
                } else if ($scope.book.Time == "") {
                    toastr.info("Enter Time");
                } else {
                    $scope.book.CustomerId = $scope.TokenInfo.CustomerId;
                    MainService.SaveCuisineBooking($scope.book).then(function (response) {
                        if (response.data) {
                            toastr.success("Successfully Booked");
                            $('#viewModal').modal('hide');
                            $scope.init();
                        } else {
                            toastr.error("Network Error");
                        }
                    }, function (err) {
                        toastr.error("Network Error");
                    });

                }
            }



}])
