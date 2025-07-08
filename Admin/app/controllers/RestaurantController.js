angular.module("ainapp")
    .controller("RestaurantController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
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

                $scope.Districts = [];
                MainService.GetDistricts().then(function (response) {
                    if (response.data) {
                        $scope.Districts = response.data;
                    } else {
                        toastr.error("Failed ");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

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

                $scope.Restaurants = [];
                MainService.GetRestaurants().then(function (response) {
                    if (response.data) {
                        $scope.Restaurants = response.data;
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
                    Description: "",
                    StateId: "",
                    DistrictId: "",
                    LocationId: "",
                    // Cost: "",
                    Type: "",
                    License: "",
                    Password: ""
                };
            };

            $scope.init();
            $scope.getDistricts = function(){
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
            $scope.getLocations = function(){
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
            
            $scope.RejectRestaurant = function(id) {
                var reason = prompt("Please enter the rejection reason:");
                if (!reason || reason.trim() === "") {
                    alert("Rejection reason is required.");
                    return;
                }
            
                MainService.RejectRestaurant(id, reason).then(function (response) {
                    if (response.data === true) {
                        alert("Restaurant has been rejected with the reason: " + reason);
                    } else {
                        alert("Failed to reject the restaurant. Please try again.");
                    }
                }, function(error) {
                    alert("An error occurred while rejecting the restaurant.");
                });
        };

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

            $scope.handleLicenseSelect = function (evt) {
                var file = evt.currentTarget.files[0];
                var reader = new FileReader();
                reader.onload = function (evt) {
                    $scope.$apply(function ($scope) {
                        $scope.myImage = evt.target.result;
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
                } else {
                    MainService.SaveRestaurant($scope.addData).then(function (response) {
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
                if (confirm("Are you sure you want to delete this state?")) {
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
        }
            $scope.Approve = function (data) {
                MainService.ApproveRestaurant(data.Id).then(function (response) {
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
