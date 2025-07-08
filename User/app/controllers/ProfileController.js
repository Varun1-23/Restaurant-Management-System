angular.module("ainapp")
    .controller("ProfileController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
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
                MainService.GetDistrictsByState($scope.Address.StateId).then(function (response) {
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
                MainService.GetLocationsByDistId($scope.Address.DistrictId).then(function (response) {
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

                $scope.TokenInfo = AuthenticationService.getTokenInfo();

                $scope.Address = {
                    Address1: "",
                    Address2: "",
                    Pincode: "",
                    LocationId: "",
                    StateId: "",
                    DistrictId: "",
                    CustomerId: $scope.TokenInfo.CustomerId,
                }


                $scope.profile = {};
                MainService.GetCustomerById($scope.TokenInfo.CustomerId).then(function (response) {
                    if (response.data) {
                        $scope.profile = response.data;
                    } else {
                        toastr.error("Failed ");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

                $scope.CustomerAddress = [];
                MainService.GetCustomersAddress($scope.TokenInfo.CustomerId).then(function (response) {
                    if (response.data) {
                        $scope.CustomerAddress = response.data;
                    } else {
                        toastr.error("Failed ");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

            };
            $scope.init();



            $scope.handleFileSelect = function (evt) {
                var file = evt.currentTarget.files[0];
                var reader = new FileReader();
                reader.onload = function (evt) {
                    $scope.$apply(function ($scope) {
                        $scope.myImage = evt.target.result;
                        $scope.profile.Photo = evt.target.result;
                    });
                };
                reader.readAsDataURL(file);
            };



            $scope.saveData = function () {
                MainService.SaveCustomer($scope.profile).then(function (response) {
                    if (response.data == 1) {
                        toastr.success("Success");
                        $('#myModal').modal('hide');
                        $scope.init();
                    } else if (response.data == 2) {
                        toastr.error("Email Id Already Registered");
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });
            };



            $scope.editAddress = function (data) {
                $scope.Address = data;
                $scope.getDistricts();
                $scope.getLocations();
                $('#myModal2').modal('show');
            }


            $scope.addNewAddress = function (data) {
                $scope.Address = {
                    Address1: "",
                    Address2: "",
                    Pincode: "",
                    LocationId: "",
                    StateId: "",
                    DistrictId: "",
                    CustomerId: $scope.TokenInfo.CustomerId,
                }
                $('#myModal2').modal('show');
            }

            $scope.saveAddress = function () {
                MainService.SaveAddress($scope.Address).then(function (response) {
                    if (response.data) {
                        toastr.success("Success");
                        $('#myModal2').modal('hide');
                        $scope.init();
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });
            };





        }]);
