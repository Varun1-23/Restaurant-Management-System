angular.module("ainapp")
    .controller("CategoryController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {

            $scope.init = function () {

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

                // $scope.Categories = [];

                // MainService.GetCategories().then(function (response) {
                //     if (response.data) {
                //         $scope.Categories = response.data;
                //     } else {
                //         toastr.error("Failed");
                //     }
                // }, function (err) {
                //     toastr.error("Network Error");
                // });
                $scope.Categories = [];
                MainService.GetCategoriesByRestaurant($scope.TokenInfo.RestaurantId).then(function (response) {
                    if (response.data) {
                        $scope.Categories = response.data;
                        $scope.getProducts($scope.Categories[0]);
                    } else {
                        toastr.error("Failed ");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

                $scope.addData = {

                    Name: "",
                    Photo: "",
                    RestaurantId: $scope.TokenInfo.RestaurantId
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


            $scope.saveData = function () {
                if ($scope.addData.Name == "") {
                    toastr.warning("Enter Name");
                } else if ($scope.addData.RestaurantId == "") {
                    toastr.warning("Select a Restaurant");
                } else {
                    MainService.SaveCategory($scope.addData).then(function (response) {
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
                MainService.DeleteCategory(data.Id).then(function (response) {
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
