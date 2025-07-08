angular.module("ainapp")
    .controller("registerController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {


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




            $scope.init = function () {

                $scope.ROTP = "";
                $scope.OTP = "";
                $scope.addData = {
                    Name: "",
                    MobileNo: "",
                    Email: "",
                    LocationId: "",
                    StateId: "",
                    DistrictId: "",
                    Address: "",
                    Password: ""
                };
            };


            $scope.init();

            $scope.handleFileSelect = function (evt) {
                var file = evt.currentTarget.files[0];
                var reader = new FileReader();
                reader.onload = function (evt) {
                    $scope.$apply(function ($scope) {
                        $scope.Photo = evt.target.result;
                        $scope.addData.Photo = evt.target.result;
                    });
                };
                reader.readAsDataURL(file);
            };



            $scope.saveData = function () {
                if ($scope.addData.Name == "") {
                    toastr.warning("Enter Name");
                } else {
                    MainService.checkEmail($scope.addData).then(function (response) {
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
                    MainService.SaveCustomer($scope.addData).then(function (response) {
                        if (response.data == 1) {
                            toastr.success(" Success ");
                            $('#myModal2').modal('hide');
                            setTimeout(function () {
                                $scope.Login();
                            }, 2000);

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

            $scope.Login = function () {
                $scope.User = {
                    UserName: $scope.addData.Email,
                    Password: $scope.addData.Password,
                }
                $scope.load = true;
                LoginService.Login($scope.User)
                    .then(function (response) {
                        $scope.load = false;
                        if (response.data != null) {
                            if (response.data.Role == "Customer") {
                                $scope.TokenInfo = response.data;
                                $scope.loginError = false;
                                $scope.IsLoggedIn = true;
                                AuthenticationService.setTokenInfo($scope.TokenInfo);
                                $scope.init();
                                $scope.InitMain();
                                $state.go("home");
                            }
                        } else {
                            $scope.IsLoggedIn = false;
                            $scope.loginError = true;
                        }
                    }, function (err) {
                        $scope.load = false;
                        $scope.loginError = true;
                    });
            };



        }]);
