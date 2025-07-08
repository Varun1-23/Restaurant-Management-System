angular.module("ainapp")
    .service('MainService', ['$q', '$window', "$http", "app.config",
        function ($q, $window, $http, config) {


            this.ChangePassword = function (data) {
                return $http.post(config.basePath + "ChangePassword", data);
            };

            //////////////.  State .//////////////////

            this.SaveState = function (data) {
                return $http.post(config.basePath + "SaveState", data);
            };

            this.GetStates = function (data) {
                return $http.get(config.basePath + "GetStates");
            }

            this.DeleteState = function (id) {
                return $http.get(config.basePath + "DeleteState/" + id);
            };
            this.GetBookingsDetail = function (id) {
                return $http.get(config.basePath + "GetBookingsDetail/" + id);
            };

            //////////////.  District .//////////////////

            this.SaveDistrict = function (data) {
                return $http.post(config.basePath + "SaveDistrict", data);
            };

            this.GetDistricts = function (data) {
                return $http.get(config.basePath + "GetDistricts");
            }

            this.DeleteDistrict = function (id) {
                return $http.get(config.basePath + "DeleteDistrict/" + id);
            };

            this.GetDistrictsByState = function (id) {
                return $http.get(config.basePath + "GetDistrictsByState/" + id);
            };

            this.ApproveRestaurant = function (id) {
                return $http.get(config.basePath + "ApproveRestaurant/" + id);
            };
            
            
            this.GetBookings2 = function (data) {
                return $http.post(config.basePath + "GetBookings2", data);
            };

            //////////////.  Location .//////////////////
            
            this.SaveLocation = function (data) {
                return $http.post(config.basePath + "SaveLocation", data);
            };

            this.GetLocations = function (data) {
                return $http.get(config.basePath + "GetLocations");
            }

            this.DeleteLocation = function (id) {
                return $http.get(config.basePath + "DeleteLocation/" + id);
            };

            this.GetLocationsByDistId = function (id) {
                return $http.get(config.basePath + "GetLocationsByDistId/" + id);
            };

            //////////////.  Restaurants .//////////////////

            this.SaveRestaurant = function (data) {
                return $http.post(config.basePath + "SaveRestaurant", data);
            };

            this.GetRestaurants = function (data) {
                return $http.get(config.basePath + "GetRestaurants");
            }

            this.DeleteRestaurant = function (id) {
                return $http.get(config.basePath + "DeleteRestaurant/" + id);
            };

            
            this.RejectRestaurant = function (id,reason) {
                return $http.post(config.basePath + "RejectRestaurant/" + id + "?reason=" + encodeURIComponent(reason));
            };

            //////////////.  Categories .//////////////////

            this.SaveCategory = function (data) {
                return $http.post(config.basePath + "SaveCategory", data);
            };

            this.GetCategories = function (data) {
                return $http.get(config.basePath + "GetCategories");
            }

            this.DeleteCategory = function (id) {
                return $http.get(config.basePath + "DeleteCategory/" + id);
            };


            this.GetCategoriesByRestaurant = function (id) {
                return $http.get(config.basePath + "GetCategoriesByRestaurant/" + id);
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

            //////////////.  Products .//////////////////

            this.SaveProduct = function (data) {
                return $http.post(config.basePath + "SaveProduct", data);
            };

            this.GetProducts = function (data) {
                return $http.get(config.basePath + "GetProducts");
            }

            this.DeleteProduct = function (id) {
                return $http.get(config.basePath + "DeleteProduct/" + id);
            };

            //////////////.  Statuses .//////////////////

            this.SaveStatus = function (data) {
                return $http.post(config.basePath + "SaveStatus", data);
            };

            this.GetStatuses = function (data) {
                return $http.get(config.basePath + "GetStatuses");
            }

            this.DeleteStatus = function (id) {
                return $http.get(config.basePath + "DeleteStatus/" + id);
            };

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

            this.DeleteBooking = function (id) {
                return $http.get(config.basePath + "DeleteBooking/" + id);
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





        }]);
