angular.module("ainapp")
    .service('MainService', ['$q', '$window', "$http", "app.config",
        function ($q, $window, $http, config) {


            this.ChangePassword = function (data) {
                return $http.post(config.basePath + "ChangePassword", data);
            };


            this.checkEmailRest = function (data) {
                return $http.post(config.basePath + "checkEmailRest", data);
            };
            this.GetReviewsByRestId = function (id) {
                return $http.get(config.basePath + "GetReviewsByRestId/" + id);
            };
            this.GetBookingsByRestaurant2 = function (data) {
                return $http.post(config.basePath + "GetBookingsByRestaurant2", data);
            };
            this.GetBillsByRestaurant2 = function (data) {
                return $http.post(config.basePath + "GetBillsByRestaurant2", data);
            };

            //////////////.  Categories .//////////////////

            this.SaveCategory = function (data) {
                return $http.post(config.basePath + "SaveCategory", data);
            };

            this.GetCategories = function (data) {
                return $http.get(config.basePath + "GetCategories");
            }

            this.GetRestaurants = function (data) {
                return $http.get(config.basePath + "GetRestaurants");
            }

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

            //////////////.  Employees .//////////////////

            this.SaveCuisine = function (data) {
                return $http.post(config.basePath + "SaveCuisine", data);
            };

            this.GetCuisinesByRestId = function (id) {
                return $http.get(config.basePath + "GetCuisinesByRestId/" + id);
            }

            this.DeleteCuisine = function (id) {
                return $http.get(config.basePath + "DeleteCuisine/" + id);
            };

            this.GetCuisineBookingsByRestaurantId = function (id) {
                return $http.get(config.basePath + "GetCuisineBookingsByRestaurantId/" + id);
            };



            //////////////.  Location .//////////////////

            this.GetLocations = function (data) {
                return $http.get(config.basePath + "GetLocations");
            }

            //////////////.  State .//////////////////


            this.GetStates = function (data) {
                return $http.get(config.basePath + "GetStates");
            }

            //////////////.  District .//////////////////


            this.GetDistricts = function (data) {
                return $http.get(config.basePath + "GetDistricts");
            }

            //////////////.  Restaurants .//////////////////

            this.SaveRestaurant = function (data) {
                return $http.post(config.basePath + "SaveRestaurant", data);
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

            this.GetProductsByRestaurant = function (id) {
                return $http.get(config.basePath + "GetProductsByRestaurant/" + id);
            };

            this.DeleteProduct = function (id) {
                return $http.get(config.basePath + "DeleteProduct/" + id);
            };
            this.ApproveDriver = function (id) {
                return $http.get(config.basePath + "ApproveDriver/" + id);
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

            //////////////.  Bookings .//////////////////

            this.SaveBooking = function (data) {
                return $http.post(config.basePath + "SaveBooking", data);
            };

            this.GetBookings = function (data) {
                return $http.get(config.basePath + "GetBookings");
            }

            this.GetBookingsDetail = function (id) {
                return $http.get(config.basePath + "GetBookingsDetail/" + id);
            }
            this.DeleteBooking = function (id) {
                return $http.get(config.basePath + "DeleteBooking/" + id);
            }
            this.GetBookingsByRestaurant = function (id) {
                return $http.get(config.basePath + "GetBookingsByRestaurant/" + id);
            };
            this.GetBillsByRestaurant = function (id) {
                return $http.get(config.basePath + "GetBillsByRestaurant/" + id);
            };

            this.GetProductsByCategory = function (id) {
                return $http.get(config.basePath + "GetProductsByCategory/" + id);
            };

            this.UpdateStatus = function (id, sid) {
                return $http.get(config.basePath + "UpdateStatus/" + id + "/" + sid);
            };


            //////////////.  BookingDetails .//////////////////

            this.SaveBookingDetails = function (data) {
                return $http.post(config.basePath + "SaveBookingDetails", data);
            };

            this.GetBookingDetails = function (data) {
                return $http.get(config.basePath + "GetBookingDetails");
            }

            this.DeleteBookingDetails = function (id) {
                return $http.get(config.basePath + "DeleteBookingDetails/" + id);
            };

            this.SaveDriver = function (data) {
                return $http.post(config.basePath + "SaveDriver", data);
            };

            this.GetDriversbyRestId = function (id) {
                return $http.get(config.basePath + "GetDriversbyRestId/" + id);
            };
            this.DeleteDriver = function (id) {
                return $http.get(config.basePath + "DeleteDriver/" + id);
            };
            this.UpdateDriver = function (id, id2) {
                return $http.get(config.basePath + "UpdateDriver/" + id + "/" + id2);
            };
            this.UpdateStatus = function (id, id2) {
                return $http.get(config.basePath + "UpdateStatus/" + id + "/" + id2);
            };





        }]);
