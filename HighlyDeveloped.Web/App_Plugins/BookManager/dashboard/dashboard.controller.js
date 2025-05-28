angular.module("umbraco")
.controller("BookManagerDashboardController", function($scope, $http) {
    var vm = this;
    vm.books = [];
    vm.editing = false;
    vm.currentBook = {};

    vm.loadBooks = function() {
        $http.get("/umbraco/backoffice/api/BookApi/GetAll").then(function(res) {
            vm.books = res.data;
        });
    };

    vm.newBook = function() {
        vm.editing = true;
        vm.currentBook = {};
    };

    vm.editBook = function(book) {
        vm.editing = true;
        vm.currentBook = angular.copy(book);
    };

    vm.saveBook = function() {
        $http.post("/umbraco/backoffice/api/BookApi/PostSave", vm.currentBook).then(function() {
            vm.editing = false;
            vm.loadBooks();
        });
    };

    vm.deleteBook = function(id) {
        $http.post("/umbraco/backoffice/api/BookApi/PostDelete", id).then(function() {
            vm.loadBooks();
        });
    };

    vm.cancel = function() {
        vm.editing = false;
    };

    vm.loadBooks();
});