angular.module("ainapp")
.controller("CustomerController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
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

                $scope.Customers = [];
                MainService.GetCustomers().then(function (response) {
                    if (response.data) {
                        $scope.Customers = response.data;
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
                    LocationId: "",                  
                    StateId: "",
                    DistrictId: "",                                                    
                    Address: ""                  
                };
            };

            $scope.init();

            
            $scope.saveData = function () {
                if ($scope.addData.Name == "") {
                    toastr.warning("Enter Name");
                } else if ($scope.addData.MobileNo == "") {
                    toastr.warning("Enter Mobile Number");
                } else if ($scope.addData.Email == "") {
                    toastr.warning("Enter Email");
                }else if ($scope.addData.LocationId == "") {
                    toastr.warning("Select Location");                
                }else if ($scope.addData.StateId == "") {
                    toastr.warning("Select State");
                } else if ($scope.addData.DistrictId == "") {
                    toastr.warning("Select District");
                } else if ($scope.addData.Address == "") {
                    toastr.warning("Enter Address");
                }   else {
                    MainService.SaveCustomer($scope.addData).then(function (response) {
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
                MainService.DeleteCustomer(data.Id).then(function (response) {
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
