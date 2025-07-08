
angular.module("ainapp")
    .controller("CuisineController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {

            $scope.init = function () {

               
                $scope.Cuisines = [];
                MainService.GetCuisinesByRestId($scope.TokenInfo.RestaurantId).then(function (response) {
                    if (response.data) {
                        $scope.Cuisines = response.data;
                    } else {
                        toastr.error("Failed ");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

                $scope.addData = {

                    Name: "",
                    NoOfPeople: 0,
                    RestaurantId: $scope.TokenInfo.RestaurantId
                };
            };

            $scope.init();


            $scope.SaveCuisine = function () {
                if ($scope.addData.Name == "") {
                    toastr.warning("Enter Name");
                } else if ($scope.addData.NoOfPeople == 0) {
                    toastr.warning("Enter No Of People");
                } else {
                    MainService.SaveCuisine($scope.addData).then(function (response) {
                        if (response.data) {
                            toastr.success(" Success ");
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
                MainService.DeleteCuisine(data.Id).then(function (response) {
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
