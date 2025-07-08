angular.module("ainapp")
    .controller("RestaurantDetailsController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
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


                if ($scope.IsLoggedIn == false) {
                    toastr.warning("Please Login To Continue");
                    $state.go("login");
                } else {
                    $scope.query = {};
                    $scope.queryBy = '$'


                    $scope.Bill = {
                        Date: new Date(),
                        DeliveryDate: new Date(),
                        RestaurantId: $stateParams.RestId,
                        BookingDetails: [],

                        Subtotal: 0,
                        TotalDiscount: 0,
                        GrandTotal: 0,
                    }

                    $scope.Categories = [];
                    MainService.GetCategoriesByRestaurant($stateParams.RestId).then(function (response) {
                        if (response.data) {
                            $scope.Categories = response.data;
                            $scope.getProducts($scope.Categories[0]);
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
                }



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
                if (e.Quantity <= 0) {
                    e.Quantity = 1;
                }
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
                if (!$scope.IsLoggedIn) {
                    toastr.warning("Please Login To Continue");
                } else if ($scope.Bill.BookingDetails.length == 0) {
                    toastr.warning("Items Missing");
                } else {
                    $('#addressModal').modal('show');
                }
            };

            $scope.SaveBooking2 = function () {
                if ($scope.Bill.BookingDetails.length == 0) {
                    toastr.warning("Items Missing");
                } else {
                    $scope.Bill.CustomerId = $scope.TokenInfo.CustomerId;
                    $scope.Bill.IsWebBooking = true;
                    $scope.Bill.CustomerAddressId = $scope.selectedAddress.Id;
                    MainService.SaveBooking($scope.Bill).then(function (response) {
                        if (response.data) {
                            toastr.success("Success");
                            $scope.init();
                            $state.go("home");
                        } else {
                            toastr.error("Failed");
                        }
                    }, function (err) {
                        toastr.error("Network Error");
                    });
                }
            };

            $scope.selectAddress = function (data) {
                $scope.selectedAddress = data;
                $scope.onlinePayment();
            }


            $scope.onlinePayment = function () {

                var options = {
                    //"key": "rzp_live_9szK78ZFq7QJd2",
                    "key": "rzp_test_ADmXxIWJhBdCYh",
                    "amount": ($scope.Bill.GrandTotal * 100).toString(),
                    "currency": "INR",
                    "name": "Resto Nest",
                    "description": "Payment Completion",
                    "image": "../images/fav.png",
                    "handler": function (response) {
                        // toastr.success("Payment Success, Verifying Account....");
                        // $scope.updateMembership(data, response.razorpay_payment_id);
                        $scope.SaveBooking2();
                        //alert(response.razorpay_payment_id);
                    },
                    "prefill": {
                        "name": "Anoop",
                        "email": "",
                        "contact": "",
                    },
                    "notes": {
                        "address": "Kerala"
                    },
                    "theme": {
                        "color": "#3399cc"
                    }
                };
                var rzp1 = new Razorpay(options);
                rzp1.open();
                rzp1.on('payment.failed', function (response) {
                    // alert(response.error.code + "fffff");
                    $scope.SaveBooking2();
                    alert(response.error.description);
                    //                    alert(response.error.source);
                    //                    alert(response.error.step);
                    //                    alert(response.error.reason);
                    //                    alert(response.error.metadata.order_id);
                    //                    alert(response.error.metadata.payment_id);
                });

            }





        }]);
