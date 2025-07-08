angular.module("ainapp")
    .controller("LocationController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {


            $scope.init = function () {

                $scope.States = [];
                MainService.GetStates().then(function (response) {
                    if (response.data) {
                        $scope.States = response.data;
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

                $scope.Districts = [];
                MainService.GetDistricts().then(function (response) {
                    if (response.data) {
                        $scope.Districts = response.data;
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

                $scope.Locations = [];
                MainService.GetLocations().then(function (response) {
                    if (response.data) {
                        $scope.Locations = response.data;
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

                $scope.addData = {
                    Name: "",                
                    DistrictId: "",
                    StateId: "",
                }

                


            }
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

            $scope.saveData = function () {
                if ($scope.addData.Name == "") {
                    toastr.warning("Enter Name");
                } else if ($scope.addData.DistrictId == "") {
                    toastr.warning("Select District");
                } else if ($scope.addData.StateId == "") {
                    toastr.warning("Select State");
                }
                else {
                    MainService.SaveLocation($scope.addData).then(function (response) {
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
            }

            $scope.deleteData = function (data) {
                if (confirm("Are you sure you want to delete this state?")) {
                MainService.DeleteLocation(data.Id).then(function (response) {
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
        }
            $scope.editData = function (data) {
                $scope.addData = data;
            }


                    }])
