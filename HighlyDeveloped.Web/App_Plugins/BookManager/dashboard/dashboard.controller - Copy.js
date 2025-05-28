angular.module("umbraco")
.controller("BookManagerDashboardController", function($scope, $http) {
    var vm = this;
    vm.books = [];
    vm.editing = false;
    vm.currentBook = {};

    //vm.loadBooks = function() {
    //    $http.get("/umbraco/backoffice/api/BookApi/GetAll").then(function(res) {
    //        vm.books = res.data;
    //    });
    //};

    vm.loadBooks = function () {
        $http.get("/umbraco/backoffice/api/BookApi/GetAll")
            .then(function (res) {
                console.log("Books loaded from API:", res.data);  // <-- log here
                vm.books = res.data;
            })
            .catch(function (err) {
                console.error("Error loading books:", err);
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

    vm.saveBook = function () {
        console.log("Saving book", vm.currentBook);
        $http.post("/umbraco/backoffice/api/BookApi/PostSave", vm.currentBook).then(function () {
            vm.editing = false;
            vm.loadBooks();
        }, function (err) {
            console.error("save failed", err);
        });
    };

    vm.deleteBook = function(id) {
        $http.post("/umbraco/backoffice/api/BookApi/PostDelete", id).then(function() {
            vm.loadBooks();
            console.log("deleted")
        }).catch (function(err) {
            console.error("Delete failed:", err);
        });

            
    };

    vm.cancel = function() {
        vm.editing = false;
    };

    vm.loadBooks();
});