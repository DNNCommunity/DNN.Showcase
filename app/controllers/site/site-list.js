﻿dnnShowcase.controller('siteListController', ['$rootScope', '$scope', '$q', '$uibModal', 'toastr', 'siteService', function ($rootScope, $scope, $q, $uibModal, toastr, siteService) {

    $scope.loading = true;
    $scope.sites = [];

    $scope.getSites = function () {
        var deferred = $q.defer();
        $scope.loading = true;

        var filter = {};
        if ($rootScope.user_id) {
            filter.user_id = $rootScope.user_id;
        }

        siteService.list(filter).then(
            function (response) {
                $scope.sites = response.data.items;
                $scope.loading = false;
                deferred.resolve();
            },
            function (response) {
                console.log('getSites failed', response);
                toastr.error("There was a problem loading sites", "Error");
                $scope.loading = false;
                deferred.reject();
            }
        );
        return deferred.promise;
    };
    $scope.addSite = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/DesktopModules/Dnn.Showcase/app/views/site/site-edit.html?c=' + new Date().getTime(),
            controller: 'siteEditController',
            size: 'lg',
            backdrop: 'static',
            resolve: {
                id: function () {
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
    $scope.editSite = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/DesktopModules/Dnn.Showcase/app/views/site/site-edit.html?c=' + new Date().getTime(),
            controller: 'siteEditController',
            size: 'lg',
            backdrop: 'static',
            resolve: {
                id: function () {
                    return id;
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
    $scope.deleteSite = function (site) {
        var modalInstance = $uibModal.open({
            templateUrl: '/DesktopModules/Dnn.Showcase/app/views/site/site-delete.html?c=' + new Date().getTime(),
            controller: 'siteDeleteController',
            size: 'lg',
            backdrop: 'static',
            resolve: {
                site: function () {
                    return site;
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

    init = function () {
        var promises = [];
        promises.push($scope.getSites());
        return $q.all(promises);
    };
    init();
}]);

