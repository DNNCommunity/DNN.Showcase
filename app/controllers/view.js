﻿dnnShowcase.controller('viewController', ['$rootScope', '$scope', '$q', 'toastr', '$uibModal', 'categoryService', 'siteService', function ($rootScope, $scope, $q, toastr, $uibModal, categoryService, siteService) {

    $rootScope.user_id = user_id;

    $scope.categories = [];
    $scope.sites = [];
    $scope.category = null;

    $scope.isEditable = isEditable;

    $scope.page_size_options = [
        { "value": 6, "text": "6 per page" },
        { "value": 12, "text": "12 per page" },
        { "value": 24, "text": "24 per page" },
        { "value": 48, "text": "48 per page" },
        { "value": 48, "text": "96 per page" }
    ];

    $scope.pagination = {
        page_number: 1,
        max_size: 5,
        items_per_page: 12,
        total_items: null,
        total_pages: null
    };

    init = function () {
        var promises = [];
        return $q.all(promises);
    };

    $scope.listShowcases = function () {

        var modalInstance = $uibModal.open({
            templateUrl: '/DesktopModules/Dnn.Showcase/app/views/site/site-list.html?c=' + new Date().getTime(),
            controller: 'siteListController',
            size: 'lg',
            backdrop: 'static',
            resolve: {
                user_id: function () {
                    return user_id;
                }
            }
        });

        modalInstance.result.then(
            function () {
                $scope.getSites();
            },
            function () {
                $scope.getSites();
            }
        );
    };
    $scope.listAllShowcases = function () {

        var modalInstance = $uibModal.open({
            templateUrl: '/DesktopModules/Dnn.Showcase/app/views/site/site-list.html?c=' + new Date().getTime(),
            controller: 'siteListController',
            size: 'lg',
            backdrop: 'static',
            resolve: {
                user_id: function () {
                    return null;
                }
            }
        });

        modalInstance.result.then(
            function () {
                $scope.getSites();
            },
            function () {
                $scope.getSites();
            }
        );
    };
    $scope.listCategories = function () {

        var modalInstance = $uibModal.open({
            templateUrl: '/DesktopModules/Dnn.Showcase/app/views/category/category-list.html?c=' + new Date().getTime(),
            controller: 'categoryListController',
            size: 'lg',
            backdrop: 'static'
        });

        modalInstance.result.then(
            function () {
                $scope.getCategories();
            },
            function () {
                $scope.getCategories();
            }
        );
    };

    $scope.getCategories = function () {
        var deferred = $q.defer();
        $scope.loading = true;

        categoryService.list().then(
            function (response) {
                $scope.categories = response.data;

                $scope.loading = false;
                deferred.resolve();
            },
            function (response) {
                console.log('getCategories failed', response);
                toastr.error("There was a problem loading the categories", "Error");
                $scope.loading = false;
                deferred.reject();
            }
        );
        return deferred.promise;
    };

    $scope.getSites = function () {
        var deferred = $q.defer();
        $scope.loading = true;

        var filter = {
            page_number: $scope.pagination.page_number,
            page_size: $scope.pagination.items_per_page,

            category_id: $scope.category ? $scope.category.id : null,
            user_id: null,
            active: true
        };

        siteService.list(filter).then(
            function (response) {
                $scope.sites = response.data.items;
                $scope.pagination.total_items = response.data.total_items;
                $scope.pagination.total_pages = response.data.total_pages;

                $scope.loading = false;
                deferred.resolve();
            },
            function (response) {
                console.log('getSites failed', response);
                toastr.error("There was a problem loading the sites", "Error");
                $scope.loading = false;
                deferred.reject();
            }
        );
        return deferred.promise;
    };
    $scope.pageChanged = function () {
        $scope.getSites();
    };
    $scope.pageSizeChanged = function () {
        $scope.getSites();
    };

    $scope.filterCategory = function (category) {

        $scope.category = category;
        $scope.pagination.page_number = 1;
        $scope.getSites();
    };

    $scope.viewSite = function (site_id) {
        $uibModal.open({
            templateUrl: '/DesktopModules/Dnn.Showcase/app/views/site/site-detail.html?c=' + new Date().getTime(),
            controller: 'siteDetailController',
            size: 'lg',
            backdrop: 'static',
            resolve: {
                id: function () {
                    return site_id;
                }
            }
        });
    };

    $scope.init = function () {
        var promises = [];
        promises.push($scope.getCategories());
        return $q.all(promises);
    };

    $scope.init().then(
        function () {
            $scope.getSites();
        }
    );
}]);

