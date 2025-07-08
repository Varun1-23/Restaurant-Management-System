angular.module("ainapp")
    .controller("driversController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
        function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {

            $scope.init = function () {
                $scope.Locations = [];
                MainService.GetLocations().then(function (response) {
                    if (response.data) {
                        $scope.Locations = response.data;
                    } else {
                        toastr.error("Failed ");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });


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

                $scope.addData = {
                    Name: "",
                    Photo: "",
                    MobileNo: "",
                    Email: "",
                    Designation: "",
                    LocationId: "",
                    Address: "",
                    RestaurantId: $scope.TokenInfo.RestaurantId,
                    IdExpiry: new Date(),
                    IdPhoto: "",
                    Password: ""
                };
            };

            $scope.init();

            $scope.handleFileSelect = function (evt) {
                var file = evt.currentTarget.files[0];
                var reader = new FileReader();
                reader.onload = function (evt) {
                    $scope.$apply(function ($scope) {
                        $scope.myImage = evt.target.result;
                        $scope.addData.Photo = evt.target.result;
                    });
                };
                reader.readAsDataURL(file);
            };

            $scope.handleFileSelect2 = function (evt) {
                var file = evt.currentTarget.files[0];
                var reader = new FileReader();
                reader.onload = function (evt) {
                    $scope.$apply(function ($scope) {
                        $scope.myImage2 = evt.target.result;
                        $scope.addData.IdPhoto = evt.target.result;
                    });
                };
                reader.readAsDataURL(file);
            };


            $scope.saveData = function () {
                if ($scope.addData.Name == "") {
                    toastr.warning("Enter Name");
                } else if ($scope.addData.MobileNo == "") {
                    toastr.warning("Enter Mobile Number");
                } else if ($scope.addData.Email == "") {
                    toastr.warning("Enter Email");
                } else if ($scope.addData.Designation == "") {
                    toastr.warning("Enter Designation");
                } else if ($scope.addData.LocationId == "") {
                    toastr.warning("Select Location");
                } else if ($scope.addData.Address == "") {
                    toastr.warning("Enter Address");
                } else {
                    MainService.SaveDriver($scope.addData).then(function (response) {
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
            };


            $scope.deleteData = function (data) {
                MainService.DeleteDriver(data.Id).then(function (response) {
                    if (response.data) {
                        toastr.success("Success");
                        $scope.init();
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });
            };

            $scope.Approve = function (data) {
                MainService.ApproveDriver(data.Id).then(function (response) {
                    if (response.data) {
                        toastr.success("Success");
                        $scope.init();
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });
            };


            $scope.editData = function (data) {
                $scope.addData = data;
            };

        }]);
