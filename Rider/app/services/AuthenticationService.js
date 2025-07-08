angular.module("ainapp")
    .service('AuthenticationService', ['$q', '$window', '$http', "app.config", "localStorageService",
        function ($q, $window, $http, config, localStorageService) {
            var tokenInfo;

            this.init = function () {
                tokenInfo = this.getTokenInfo();
            };

            this.setTokenInfo = function (tokenInfo) {
                localStorageService.set("AinsoftDeliveryTokenInfo", JSON.stringify(tokenInfo));
            };

            this.getTokenInfo = function () {
                tokenInfo = JSON.parse(localStorageService.get("AinsoftDeliveryTokenInfo"));
                return tokenInfo;
            };

            this.removeTokenInfo = function () {
                localStorageService.clearAll();
            };

            this.setLanInfo = function (LanInfo) {
                localStorageService.set("AinsoftDelLanguage", JSON.stringify(LanInfo));
            };

            this.getlanInfo = function () {
                LanInfo = JSON.parse(localStorageService.get("AinsoftDelLanguage"));
                return LanInfo;
            };

            this.setHeader = function (config) {
                var token = tokenInfo ? tokenInfo.Token : null;
                if (token) {
                    config.headers['Authorization'] = 'Basic ' + token;
                }
            };

            this.init();
        }
    ])
