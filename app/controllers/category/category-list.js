dnnShowcase.controller('categoryListController', ['$rootScope', '$scope', '$q', '$uibModal', '$uibModalInstance', 'toastr', 'categoryService', function ($rootScope, $scope, $q, $uibModal, $uibModalInstance, toastr, categoryService) {

    $scope.loading = true;
    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };


    $scope.categorys = [];

    $scope.getCategorys = function () {
        var deferred = $q.defer();
        $scope.loading = true;

        categoryService.list($rootScope.user_id).then(
            function (response) {
                $scope.categorys = response.data;
                $scope.loading = false;
                deferred.resolve();
            },
            function (response) {
                console.log('getCategorys failed', response);
                toastr.error("There was a problem loading categorys", "Error");
                $scope.loading = false;
                deferred.reject();
            }
        );
        return deferred.promise;
    };
    $scope.addCategory = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/DesktopModules/Dnn.Showcase/app/views/category/category-edit.html?c=' + new Date().getTime(),
            controller: 'categoryEditController',
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
                $scope.getCategorys();
            },
            function () {
                $scope.getCategorys();
            }
        );

    };
    $scope.editCategory = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: '/DesktopModules/Dnn.Showcase/app/views/category/category-edit.html?c=' + new Date().getTime(),
            controller: 'categoryEditController',
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
                $scope.getCategorys();
            },
            function () {
                $scope.getCategorys();
            }
        );

    };
    $scope.deleteCategory = function (category) {
        var modalInstance = $uibModal.open({
            templateUrl: '/DesktopModules/Dnn.Showcase/app/views/category/category-delete.html?c=' + new Date().getTime(),
            controller: 'categoryDeleteController',
            size: 'lg',
            backdrop: 'static',
            resolve: {
                category: function () {
                    return category;
                }
            }
        });

        modalInstance.result.then(
            function () {
                $scope.getCategorys();
            },
            function () {
                $scope.getCategorys();
            }
        );
    };

    init = function () {
        var promises = [];
        promises.push($scope.getCategorys());
        return $q.all(promises);
    };
    init();
}]);

