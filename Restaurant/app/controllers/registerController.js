angular.module("ainapp")
    .controller("registerController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
    function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {


            $scope.init = function () {


                $scope.States = [];
            MainService.GetStates().then(function (response) {
                if (response.data) {
                    $scope.States = response.data;
                } else {
                    toastr.error("Failed ");
                }
            }, function (err) {
                toastr.error("Network Error");
            });

            $scope.getDistricts = function () {
                $scope.Districts = [];
                MainService.GetDistrictsByState($scope.addData.StateId).then(function (response) {
                    if (response.data) {
                        $scope.Districts = response.data;
                    } else {
                        toastr.error("Failed ");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

            }

            $scope.getLocations = function () {
                $scope.Locations = [];
                MainService.GetLocationsByDistId($scope.addData.DistrictId).then(function (response) {
                    if (response.data) {
                        $scope.Locations = response.data;
                    } else {
                        toastr.error("Failed ");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });
            }

                $scope.addData = {
                    Name: "",
                    Photo: "",
                    MobileNo: "",
                    Email: "",
                    Description: "",
                    StateId: "",
                    DistrictId: "",
                    LocationId: "",
                    Cost: "",
                    Type: "",
                    Password: "",
                    License: ""
                };

                $scope.ROTP = "";
                $scope.OTP = "";

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
                        $scope.addData.License = evt.target.result;
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
                } else if ($scope.addData.DistrictId == "") {
                    toastr.warning("Select District");
                } else if ($scope.addData.StateId == "") {
                    toastr.warning("Select State");
                } else if ($scope.addData.LocationId == "") {
                    toastr.warning("Select Location");
                } else if ($scope.addData.Password == "" && !$scope.addData.Id > 0) {
                    toastr.warning("Enter Password");
                } else  {
                    MainService.checkEmailRest($scope.addData).then(function (response) {
                        if (response.data != 2) {
                            $scope.ROTP = response.data;
                            $('#myModal2').modal('show');
                        } else if (response.data == 2) {
                            toastr.error("Email Id Already Registered");
                        } else {
                            toastr.error("Failed");
                        }
                    }, function (err) {
                        toastr.error("Network Error");
                    });
                }
            };

            $scope.saveData2 = function () {
                if ($scope.OTP != $scope.ROTP) {
                    toastr.warning("Invalid OTP");
                } else {
                    MainService.SaveRestaurant($scope.addData).then(function (response) {
                        if (response.data) {
                            $('#myModal2').modal('hide');
                            toastr.success("Registration Successful, Your will get approval from admin. Then after you can login to portal.");
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
                MainService.DeleteRestaurant(data.Id).then(function (response) {
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
