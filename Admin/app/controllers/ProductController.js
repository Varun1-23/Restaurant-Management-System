angular.module("ainapp")
    .controller("ProductController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
        function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {

            $scope.init = function () {


              
                $scope.Categories = [];

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

                $scope.Products = [];
                MainService.GetProducts().then(function (response) {
                    if (response.data) {
                        $scope.Products = response.data;
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

                $scope.addData = {
                    Name: "",
                    Photo: "",
                    Description: "",
                    CategoryId: "",
                    RestaurantId: "",
                    Price: "",
                    OfferPrice: "",

                };
            };

            $scope.init();

            $scope.getCategories = function(){
                $scope.Categories = [];
                MainService.GetCategoriesByRestaurant($scope.addData.RestaurantId).then(function (response) {
                    if (response.data) {
                        $scope.Categories = response.data;
                    } else {
                        toastr.error("Failed ");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });
            }


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
                } else if ($scope.addData.Description == "") {
                    toastr.warning("Enter Description");
                } else if ($scope.addData.CategoryId == "") {
                    toastr.warning("Select Category");
                } else if ($scope.addData.RestaurantId == "") {
                    toastr.warning("Select Restaurant");
                } else if ($scope.addData.Price == "") {
                    toastr.warning("Enter Price");
                } else if ($scope.addData.OfferPrice == "") {
                    toastr.warning("Enter OfferPrice");
                } else {
                    MainService.SaveProduct($scope.addData).then(function (response) {
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
                MainService.DeleteProduct(data.Id).then(function (response) {
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

            $scope.editData = function (data) {
                $scope.addData = data;
            };

        }]);
