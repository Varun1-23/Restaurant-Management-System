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

                    .state("state", {
                        parent: "init",
                        url: "/state",
                        templateUrl: "templates/state.html",
                        controller: "stateController",
                    })

                    .state("district", {
                        parent: "init",
                        url: "/district",
                        templateUrl: "templates/district.html",
                        controller: "districtController",
                    })
                    .state("Location", {
                        parent: "init",
                        url: "/Location",
                        templateUrl: "templates/Location.html",
                        controller: "LocationController",
                    })
                    .state("Restaurant", {
                        parent: "init",
                        url: "/Restaurant",
                        templateUrl: "templates/Restaurant.html",
                        controller: "RestaurantController",
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
                    .state("Status", {
                        parent: "init",
                        url: "/Status",
                        templateUrl: "templates/Status.html",
                        controller: "StatusController",
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
