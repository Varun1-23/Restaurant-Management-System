(function () {
    'use strict';
    angular.module('ainapp', ['ui.router', 'ui.bootstrap', 'LocalStorageModule', 'ngLoader', 'jkAngularRatingStars'])
        .value('app.config', {

            basePath: 'http://localhost:55908/'


        })
        .config(['$stateProvider', '$urlRouterProvider', '$locationProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider) {

                $urlRouterProvider.otherwise('init/home');
                $stateProvider



                    ///////////// *************** home *********** //////////


                    .state("init", {
                        abstract: true,
                        url: "/init",
                        templateUrl: "templates/Init.tpl.html",
                        controller: "InitController",
                    })

                    .state("login", {
                        url: "/login",
                        parent: "init",
                        templateUrl: "templates/Login.tpl.html",
                        controller: "LoginController",
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
                        //controller: "HomeController",
                    })

                    .state("profile", {
                        parent: "init",
                        url: "/profile",
                        templateUrl: "templates/profile.tp.html",
                        controller: "ProfileController",
                    })

                    .state("register", {
                        parent: "init",
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
                        controller: "BookingController",
                    })

                    .state("pos", {
                        parent: "init",
                        url: "/pos",
                        templateUrl: "templates/pos.html",
                        controller: "posController",
                    })

                    .state("Restaurant", {
                        parent: "init",
                        url: "/Restaurant",
                        templateUrl: "templates/Restaurant.html",
                        controller: "RestaurantController",
                    })
                    .state("Restaurant2", {
                        parent: "init",
                        url: "/Restaurant2",
                        templateUrl: "templates/Restaurant2.html",
                        controller: "RestaurantController",
                    })

                    .state("RestaurantByCat", {
                        parent: "init",
                        url: "/RestaurantByCat/:CatId/:CatName",
                        templateUrl: "templates/Restaurant.html",
                        controller: "RestaurantByCatController",
                    })

                    .state("RestaurantsByDistrict", {
                        parent: "init",
                        url: "/RestaurantsByDistrict/:DistrictId/:DistrictName",
                        templateUrl: "templates/Restaurant.html",
                        controller: "RestaurantsByDistrictController",
                    })

                    .state("RestaurantsByLocation", {
                        parent: "init",
                        url: "/RestaurantsByLocation/:LocationId/:LocationName",
                        templateUrl: "templates/Restaurant.html",
                        controller: "RestaurantsByLocationController",
                    })

                    .state("RestaurantsByKeyword", {
                        parent: "init",
                        url: "/RestaurantsByKeyword/:Keyword",
                        templateUrl: "templates/Restaurant.html",
                        controller: "RestaurantsByKeywordController",
                    })

                    .state("RestaurantDetails", {
                        parent: "init",
                        url: "/RestaurantDetails/:RestId/:RestName",
                        templateUrl: "templates/RestaurantDetails.html",
                        controller: "RestaurantDetailsController",
                    })
                    .state("BookingSeats", {
                        parent: "init",
                        url: "/BookingSeats",
                        templateUrl: "templates/BookingSeats.html",
                        controller: "BookingSeatsController",
                    })
                    .state("MyCuisineBookings", {
                        parent: "init",
                        url: "/MyCuisineBookings",
                        templateUrl: "templates/MyCuisineBookings.html",
                        controller: "MyCuisineBookingsController",
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
