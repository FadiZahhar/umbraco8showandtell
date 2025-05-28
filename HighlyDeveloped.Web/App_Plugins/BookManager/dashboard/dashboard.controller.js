angular.module("umbraco")
    .controller("BookManagerDashboardController", function ($scope, $http) {
        var vm = this;
        vm.books = [];
        vm.editing = false;
        vm.currentBook = {};
        vm.validationErrors = null;
        vm.searchTerm = "";
        vm.page = 1;
        vm.pageSize = 2;
        vm.totalCount = 0;
        vm.totalPages = 1;
        vm.submitted = false;

        var vm = this;
        vm.submitted = false;
        vm.currentBook = {};

        vm.searchBooks = function () {
            $http.get("/umbraco/backoffice/api/BookApi/Search", {
                params: {
                    term: vm.searchTerm,
                    page: vm.page,
                    pageSize: vm.pageSize
                }
            }).then(function (res) {
                vm.books = res.data.books;
                vm.totalCount = res.data.totalCount;
                vm.totalPages = Math.ceil(vm.totalCount / vm.pageSize);
            });
        };

        vm.goToPage = function (page) {
            if (page < 1 || page > vm.totalPages) return;
            vm.page = page;
            vm.searchBooks();
        };

        // Override loadBooks to use search
        vm.loadBooks = function () {
            vm.page = 1;
            vm.searchBooks();
        };


        vm.newBook = function () {
            vm.editing = true;
            vm.currentBook = {};
            vm.validationErrors = null;
        };

        vm.editBook = function (book) {
            vm.editing = true;
            vm.currentBook = angular.copy(book);
            vm.validationErrors = null;
        };

        // Accept the form as a parameter so we can touch all fields if needed
        vm.saveBook = function (form) {
            vm.submitted = true;
            // Touch all fields if invalid (shows validation errors)
            if (form.$invalid) {
                angular.forEach(form.$error, function (fields) {
                    angular.forEach(fields, function (field) {
                        field.$setTouched();
                    });
                });
                return;
            }
            $http.post("/umbraco/backoffice/api/BookApi/PostSave", vm.currentBook)
                .then(function () {
                    vm.editing = false;
                    vm.loadBooks();
                    vm.validationErrors = null;
                })
                .catch(function (err) {
                    if (err.data && err.data.ModelState) {
                        var errors = [];
                        angular.forEach(err.data.ModelState, function (messages) {
                            messages.forEach(function (message) {
                                errors.push(message);
                            });
                        });
                        vm.validationErrors = errors;
                    } else if (err.data && err.data.Message) {
                        vm.validationErrors = [err.data.Message];
                    } else {
                        vm.validationErrors = ["An unknown error occurred."];
                    }
                });
        };

        vm.deleteBook = function (id) {
            $http.post("/umbraco/backoffice/api/BookApi/PostDelete", id).then(function () {
                vm.loadBooks();
            });
        };

        vm.cancel = function () {
            vm.editing = false;
            vm.validationErrors = null;
        };

        vm.loadBooks();

        vm.exportBooks = function () {
            var url = "/umbraco/backoffice/api/BookApi/ExportCsv";
            window.open(url, '_blank');
        };

        $scope.showSearch = false;

        $scope.toggleSearch = function () {
            $scope.showSearch = !$scope.showSearch;
        };

      
    });
    