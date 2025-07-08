angular.module("ainapp")
    .service('MainService', ['$q', '$window', "$http", "app.config",
        function ($q, $window, $http, config) {


            this.ChangePassword = function (data) {
                return $http.post(config.basePath + "ChangePassword", data);
            };



            //////////////.  Categories .//////////////////

            this.checkEmail = function (data) {
                return $http.post(config.basePath + "checkEmail", data);
            };

            this.SaveReview = function (data) {
                return $http.post(config.basePath + "SaveReview", data);
            };

            this.SaveCategory = function (data) {
                return $http.post(config.basePath + "SaveCategory", data);
            };

            this.GetCategories = function (data) {
                return $http.get(config.basePath + "GetCategories");
            }

            this.GetReviewsByCusId = function (id) {
                return $http.get(config.basePath + "GetReviewsByCusId/" + id);
            };
            this.DeleteCategory = function (id) {
                return $http.get(config.basePath + "DeleteCategory/" + id);
            };
            this.GetCategoriesByRestaurant = function (id) {
                return $http.get(config.basePath + "GetCategoriesByRestaurant/" + id);
            };
            this.GetDistrictsByState = function (id) {
                return $http.get(config.basePath + "GetDistrictsByState/" + id);
            };
            this.GetLocationsByDistId = function (id) {
                return $http.get(config.basePath + "GetLocationsByDistId/" + id);
            };
            this.GetCustomersAddress = function (id) {
                return $http.get(config.basePath + "GetCustomersAddress/" + id);
            };
            this.GetBookingsByCustomer = function (id) {
                return $http.get(config.basePath + "GetBookingsByCustomer/" + id);
            };
            this.GetBookingsByCustomer2 = function (data) {
                return $http.post(config.basePath + "GetBookingsByCustomer2", data);
            };
            //////////////.  Employees .//////////////////

            this.SaveEmployee = function (data) {
                return $http.post(config.basePath + "SaveEmployee", data);
            };

            this.GetEmployees = function (data) {
                return $http.get(config.basePath + "GetEmployees");
            }

            this.DeleteEmployee = function (id) {
                return $http.get(config.basePath + "DeleteEmployee/" + id);
            };

            //////////////.  Location .//////////////////

            this.GetLocations = function (data) {
                return $http.get(config.basePath + "GetLocations");
            }
            this.GetLocationsByDistId = function (id) {
                return $http.get(config.basePath + "GetLocationsByDistId/" + id);
            };

            //////////////.  State .//////////////////


            this.GetStates = function (data) {
                return $http.get(config.basePath + "GetStates");
            }

            //////////////.  District .//////////////////


            this.GetDistricts = function (data) {
                return $http.get(config.basePath + "GetDistricts");
            };
            this.GetDistrictsByState = function (id) {
                return $http.get(config.basePath + "GetDistrictsByState/" + id);
            };



            //////////////.  Restaurants .//////////////////


            this.GetRestaurants = function (data) {
                return $http.get(config.basePath + "GetRestaurants");
            }
            this.GetRestaurantsByCatId = function (id) {
                return $http.get(config.basePath + "GetRestaurantsByCatId/" + id);
            };
            this.GetRestaurantsByDistrictId = function (id) {
                return $http.get(config.basePath + "GetRestaurantsByDistrictId/" + id);
            };
            this.GetRestaurantsByLocationId = function (id) {
                return $http.get(config.basePath + "GetRestaurantsByLocationId/" + id);
            };
            this.GetRestaurantsByKeyword = function (id) {
                return $http.get(config.basePath + "GetRestaurantsByKeyword/" + id);
            };



            //////////////.  Products .//////////////////

            this.SaveProduct = function (data) {
                return $http.post(config.basePath + "SaveProduct", data);
            };

            this.GetProducts = function (data) {
                return $http.get(config.basePath + "GetProducts");
            }


            this.GetProductsByCategory = function (id) {
                return $http.get(config.basePath + "GetProductsByCategory/" + id);
            };

            this.DeleteProduct = function (id) {
                return $http.get(config.basePath + "DeleteProduct/" + id);
            };

            //////////////.  Statuses .//////////////////


            this.GetStatuses = function (data) {
                return $http.get(config.basePath + "GetStatuses");
            }


            //////////////.  Customers .//////////////////

            this.SaveCustomer = function (data) {
                return $http.post(config.basePath + "SaveCustomer", data);
            };

            this.GetCustomers = function (data) {
                return $http.get(config.basePath + "GetCustomers");
            }

            this.DeleteCustomer = function (id) {
                return $http.get(config.basePath + "DeleteCustomer/" + id);
            };
            this.GetCuisinesByRestId = function (id) {
                return $http.get(config.basePath + "GetCuisinesByRestId/" + id);
            };

            //////////////.  Bookings .//////////////////

            this.SaveBooking = function (data) {
                return $http.post(config.basePath + "SaveBooking", data);
            };

            this.SaveAddress = function (data) {
                return $http.post(config.basePath + "SaveAddress", data);
            };

            this.GetBookings = function (data) {
                return $http.get(config.basePath + "GetBookings");
            }

            this.GetCustomerById = function (id) {
                return $http.get(config.basePath + "GetCustomerById/" + id);
            }
            this.GetBookingsDetail = function (id) {
                return $http.get(config.basePath + "GetBookingsDetail/" + id);
            }
            this.CancelBooking = function (id) {
                return $http.get(config.basePath + "CancelBooking/" + id);
            }
            this.DeleteBooking = function (id) {
                return $http.get(config.basePath + "DeleteBooking/" + id);
            }
            this.GetBookingsByRestaurant = function (id) {
                return $http.get(config.basePath + "GetBookingsByRestaurant/" + id);
            };

            this.GetProductsByCategory = function (id) {
                return $http.get(config.basePath + "GetProductsByCategory/" + id);
            };


            //////////////.  BookingDetails .//////////////////

            this.SaveBookingDetails = function (data) {
                return $http.post(config.basePath + "SaveBookingDetails", data);
            };
            this.SaveCuisineBooking = function (data) {
                return $http.post(config.basePath + "SaveCuisineBooking", data);
            };

            this.GetBookingDetails = function (data) {
                return $http.get(config.basePath + "GetBookingDetails");
            }

            this.DeleteBookingDetails = function (id) {
                return $http.get(config.basePath + "DeleteBookingDetails/" + id);
            };


            this.GetCuisineBookingsByCustomerId = function (id) {
                return $http.get(config.basePath + "GetCuisineBookingsByCustomerId/" + id);
            };





        }]);
