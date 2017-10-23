var app = angular.module('IdeoTest', ['angularTreeview']);
app.controller('myController', ['$scope', '$http', function ($scope, $http) {
    $http.get('/Home/GetFileStructure').then(function (response) {
        $scope.List = response.data.treeList;
    })
}])