angular.module("ainapp")
    .controller("stateController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
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


                $scope.addData = {
                    Name: ""
                }


            }
            $scope.init();


            $scope.saveData = function () {
                if ($scope.addData.Name == "") {
                    toastr.warning("Enter Name");
                } else {
                    MainService.SaveState($scope.addData).then(function (response) {
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
                MainService.DeleteState(data.Id).then(function (response) {
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
