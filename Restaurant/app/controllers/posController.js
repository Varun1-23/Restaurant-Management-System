angular.module("ainapp")
    .controller("posController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
        function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {

            $scope.selectedCategory = {};
            $scope.getProducts = function (data) {
                $scope.selectedCategory = data;

                $scope.Products = [];
                MainService.GetProductsByCategory($scope.selectedCategory.Id).then(function (response) {
                    if (response.data) {
                        $scope.Products = response.data;
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });

            }


            $scope.init = function () {

                $scope.Bill = {
                    Date: new Date(),
                    DeliveryDate: new Date(),
                    RestaurantId: $scope.TokenInfo.RestaurantId,
                    BookingDetails: [],

                    Subtotal: 0,
                    TotalDiscount: 0,
                    GrandTotal: 0,
                    CustomerName: "",
                    CustomerMob: "",
                    CustomerEmail: "",
                }

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

            };
            $scope.init();

            $scope.calculateTotal = function () {

                $scope.Bill.Subtotal = 0;
                $scope.Bill.GrandTotal = 0;
                angular.forEach($scope.Bill.BookingDetails, function (e) {
                    $scope.Bill.Subtotal = $scope.Bill.Subtotal + e.Total;
                })

                $scope.Bill.GrandTotal = $scope.Bill.Subtotal - $scope.Bill.TotalDiscount;

            }

            $scope.applydiscount = function () {
                $scope.Bill.GrandTotal = $scope.Bill.Subtotal - $scope.Bill.TotalDiscount;
            }


            $scope.addQty = function (e) {
                e.Quantity = e.Quantity + 1;
                e.Total = e.Quantity * e.OfferPrice;
                $scope.calculateTotal();
            }

            $scope.minusQty = function (e) {
                e.Quantity = e.Quantity - 1;
                e.Total = e.Quantity * e.OfferPrice;
                $scope.calculateTotal();
            }

            $scope.deleteItem = function (data) {
                var index = $scope.Bill.BookingDetails.indexOf(data);
                $scope.Bill.BookingDetails.splice(index, 1);
                $scope.calculateTotal();
            }

            $scope.addToBill = function (data) {

                var isExist = false;
                angular.forEach($scope.Bill.BookingDetails, function (e) {
                    if (e.ProductId == data.Id) {

                        e.Quantity = e.Quantity + 1;
                        e.Total = e.Quantity * e.OfferPrice;

                        isExist = true;
                    }
                })


                if (isExist == false) {
                    var detail = {
                        ProductId: data.Id,
                        Product: data,
                        Price: data.Price,
                        OfferPrice: data.OfferPrice,
                        Quantity: 1,
                    }

                    detail.Total = detail.Quantity * detail.OfferPrice;

                    $scope.Bill.BookingDetails.push(detail);
                }

                $scope.calculateTotal();

            }

            $scope.SaveBooking = function () {
                if ($scope.Bill.BookingDetails.length == 0) {
                    toastr.warning("Items Missing");
                } else {
                    MainService.SaveBooking($scope.Bill).then(function (response) {
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



        }]);
