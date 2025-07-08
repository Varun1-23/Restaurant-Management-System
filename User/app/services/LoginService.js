angular.module("ainapp")
    .service('LoginService', ['$q', '$window', "$http", "app.config",
        function ($q, $window, $http, config) {

            this.User = {};
            this.setUserName = function (data) {
                $window.sessionStorage.setItem("UserName", data);
                this.User.UserName = data;
            }

            this.clearUserName = function () {
                $window.sessionStorage.removeItem("UserName");
                this.User = {};
            }

            this.getUserName = function () {
                if ($window.sessionStorage["UserName"] != undefined) {
                    this.User.UserName = $window.sessionStorage["UserName"];
                } else {
                    this.User = {};
                }
                return this.User;
            }

            this.Login = function (data) {
                return $http.post(config.basePath + "Login", data);
            }

            this.ChangePassword = function (data) {
                return $http.post(config.basePath + "ChangePassword", data);
            }

            this.logOut = function (data) {
                return $http.post(config.basePath + "LogOut", data);
            };
        }
    ]);
