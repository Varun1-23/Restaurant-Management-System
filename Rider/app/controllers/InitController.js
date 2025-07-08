angular.module("ainapp")
    .controller("InitController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService", "$window",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService, $window) {

            $scope.basePath = config.basePath;

            $scope.TokenInfo = AuthenticationService.getTokenInfo();

            $scope.logOut = function () {
                var token = AuthenticationService.getTokenInfo();
                $scope.working = true;
                LoginService.logOut(token)
                    .then(function (response) {
                        $scope.working = false;
                        if (response) {
                            AuthenticationService.removeTokenInfo();
                            $scope.IsLoggedIn = false;
                            //                            $scope.InitMain();
                            $state.go("login");
                        }
                    }, function (err) {
                        $scope.working = false;
                    })
            };

            $scope.goBack = function () {
                $window.history.back();
            }


            $scope.UserUpdate = {
                Password: "",
                CPassword: ""
            }
            $scope.changePassword = function () {

                if ($scope.UserUpdate.Password == "") {
                    toastr.error("Enter Password");
                } else if ($scope.UserUpdate.CPassword == "") {
                    toastr.error("Confirm Password");
                } else if ($scope.UserUpdate.Password != $scope.UserUpdate.CPassword) {
                    toastr.error("Password not matching");
                } else {
                    $scope.Load = true;
                    MainService.ChangePassword($scope.UserUpdate).then(function (response) {
                        if (response.data) {
                            toastr.success("Success");
                            $scope.UserUpdate = {
                                Password: "",
                                CPassword: ""
                            }
                        } else {
                            toastr.error("Network Error");
                        }
                        $scope.Load = false;
                    }, function (err) {
                        toastr.error("Network Error");
                        $scope.Load = false;
                    })

                }

            }

            $scope.extractDate = function (date) {
                var Month = date.getMonth() + 1;
                Month = Month.toString();
                if (Month.length == 1) {
                    Month = '0' + Month;
                }
                var Day = date.getDate();
                Day = Day.toString();
                if (Day.length == 1) {
                    Day = '0' + Day;
                }
                var Year = date.getFullYear();
                Year = Year.toString();
                var newDate = Day + "/" + Month + "/" + Year;
                return newDate;
            }

            




    }]);
