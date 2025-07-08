angular.module("ainapp")
    .controller("MyReviewsController", ["app.config", "$scope", "LoginService", "AuthenticationService", "$state", "$stateParams", "MainService", "localStorageService",
    function (config, $scope, LoginService, AuthenticationService, $state, $stateParams, MainService, localStorageService) {



            $scope.init = function () {

                $scope.Reviews = [];
                MainService.GetReviewsByRestId($scope.TokenInfo.RestaurantId).then(function (response) {
                    if (response.data) {
                        $scope.Reviews = response.data;
                    } else {
                        toastr.error("Failed");
                    }
                }, function (err) {
                    toastr.error("Network Error");
                });


            };
            $scope.init();






        }]);
