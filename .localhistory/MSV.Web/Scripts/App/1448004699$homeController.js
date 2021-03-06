//'use strict';

//
// For this trivial demo we have just a unique MainController 
// for everything
//
app.controller('MainController', function ($scope, $http, $modal, toaster) {

    getBanks();


    function getBanks() {
        $http.get('/api/Lottery/GetBanks')
        .success(function (data, status, headers, config) {
            $scope.banks = data;
            console.log(JSON.stringify(data));
        })
        .error(function (data, status, headers, config) {
            $scope.error = "Error has occured!";
            toaster.pop('error', $scope.error);
        });
    }
});
