(function () {
    'use strict';
    angular.module('ainapp', ['ui.router', 'ui.bootstrap', 'LocalStorageModule', 'ngLoader'])
        .value('app.config', {

            basePath: 'http://localhost:55908/'


        })
        .config(['$stateProvider', '$urlRouterProvider', '$locationProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider) {

                $urlRouterProvider.otherwise('login');
                $stateProvider

                    .state("login", {
                        url: "/login",
                        templateUrl: "templates/Login.tpl.html",
                        controller: "LoginController",
                    })

                    ///////////// *************** home *********** //////////


                    .state("init", {
                        abstract: true,
                        url: "/init",
                        templateUrl: "templates/Init.tpl.html",
                        controller: "InitController",
                    })

                    .state("home", {
                        parent: "init",
                        url: "/home",
                        templateUrl: "templates/Home.tpl.html",
                        controller: "HomeController",
                    })

                    .state("changePassword", {
                        parent: "init",
                        url: "/changePassword",
                        templateUrl: "templates/changePassword.tpl.html",
                    })

                    .state("register", {
                       // parent: "init",
                        url: "/register",
                        templateUrl: "templates/register.tpl.html",
                        controller: "registerController",
                    })


                    .state("Category", {
                        parent: "init",
                        url: "/Category",
                        templateUrl: "templates/Category.html",
                        controller: "CategoryController",
                    })
                    .state("Employee", {
                        parent: "init",
                        url: "/Employee",
                        templateUrl: "templates/Employee.html",
                        controller: "EmployeeController",
                    })
                    .state("Product", {
                        parent: "init",
                        url: "/Product",
                        templateUrl: "templates/Product.html",
                        controller: "ProductController",
                    })

                    .state("Customer", {
                        parent: "init",
                        url: "/Customer",
                        templateUrl: "templates/Customer.html",
                        controller: "CustomerController",
                    })
                    .state("Booking", {
                        parent: "init",
                        url: "/Booking",
                        templateUrl: "templates/Booking.html",
                        controller: "BookingController",
                    })

                    .state("BookingDetails", {
                        parent: "init",
                        url: "/BookingDetails/:BookId",
                        templateUrl: "templates/BookingDetails.html",
                        controller: "BookingDetailsController",
                    })

                    .state("Bill", {
                        parent: "init",
                        url: "/Bill",
                        templateUrl: "templates/Bill.html",
                        controller: "BillsController",
                    })

                    .state("pos", {
                        parent: "init",
                        url: "/pos",
                        templateUrl: "templates/pos.html",
                        controller: "posController",
                    })

                    .state("Cuisine", {
                        parent: "init",
                        url: "/Cuisine",
                        templateUrl: "templates/Cuisine.html",
                        controller: "CuisineController",
                    })

                    .state("CuisineBookings", {
                        parent: "init",
                        url: "/CuisineBookings",
                        templateUrl: "templates/CuisineBookings.html",
                        controller: "CuisineBookingsController",
                    })


                    .state("drivers", {
                        parent: "init",
                        url: "/drivers",
                        templateUrl: "templates/drivers.html",
                        controller: "driversController",
                    })
                    .state("MyReviews", {
                        parent: "init",
                        url: "/MyReviews",
                        templateUrl: "templates/MyReviews.html",
                        controller: "MyReviewsController",
                    })





        }])

        .config(['localStorageServiceProvider', function (localStorageServiceProvider) {
            localStorageServiceProvider
                .setPrefix('ainapp')
                .setStorageType('localStorage')
                .setNotify(true, true);
    }])
        .config(['$httpProvider', function ($httpProvider) {
            $httpProvider.interceptors.push("AuthInterceptor")
    }]);

})();
