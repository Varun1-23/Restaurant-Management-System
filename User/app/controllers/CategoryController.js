angular.module("ainapp")
    .controller("CategoryController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
            function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {

            $scope.Categories = [];


            MainService.GetCategories().then(function (response) {
                if (response.data) {
                    $scope.Categories = response.data;
                } else {
                    toastr.error("Failed");
                }
            }, function (err) {
                toastr.error("Network Error");
            });

        }]);
