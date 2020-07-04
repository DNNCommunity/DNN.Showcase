dnnShowcase.controller('siteEditController', ['$scope', '$q', '$uibModal', '$uibModalInstance', 'toastr', 'siteService', 'categoryService', 'id', function ($scope, $q, $uibModal, $uibModalInstance, toastr, siteService, categoryService, id) {

    $scope.loading = false;

    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };
    
    $scope.categories = [];
    $scope.site = {
        id: id
    };

    $scope.uploadThumbnail = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/DesktopModules/Dnn.Showcase/app/views/thumbnail/thumbnail-upload.html?c=' + new Date().getTime(),
            controller: 'thumbnailUploadController',
            size: 'lg',
            backdrop: 'static',
            resolve: {}
        });

        modalInstance.result.then(
            function (response) {
                if (response) {
                    $scope.site.thumbnail = response;
                }
            },
            function () { }
        );
    };

    $scope.getSite = function () {
        var deferred = $q.defer();
        $scope.loading = true;

        siteService.get($scope.site.id).then(
            function (response) {
                $scope.site = response.data;

                // set the selected value on categories
                for (var x = 0; x < $scope.site.site_categories.length; x++) {
                    var site_category = $scope.site.site_categories[x];
                    for (var y = 0; y < $scope.categories.length; y++) {
                        var category = $scope.categories[y];
                        if (site_category.category_id === category.id) {
                            category.selected = true;
                        }
                    }
                }

                deferred.resolve();
                $scope.loading = false;
            },
            function (response) {
                console.log('getSite failed', response);
                toastr.error("There was a problem loading the site", "Error");
                $scope.loading = false;
                deferred.reject();
            }
        );
        return deferred.promise;
    };
    $scope.saveSite = function () {
        $scope.form.$submitted = true;
        $scope.loading = true;

        var isNew = $scope.site.id === null;

        if ($scope.form.$valid) {

            siteService.save($scope.site).then(
                function (response) {
                    $scope.site = response.data;

                    $scope.loading = false;
                    $scope.submitted = false;

                    if (isNew) {
                        toastr.success("The site '" + $scope.site.name + "' was created.", "Success");
                    }
                    else {
                        toastr.success("The site '" + $scope.site.name + "' was saved.", "Success");
                    }
                    $uibModalInstance.close($scope.site);
                },
                function (response) {
                    console.log('saveSite failed', response);
                    if (response.status === 406) {
                        toastr.error("That is not a valid DNN website", "Error");
                    }
                    else {
                        toastr.error("There was a problem saving the site", "Error");
                    }
                    $scope.submitted = false;
                    $scope.loading = false;
                }
            );
        }
        else {
            $scope.loading = false;
            $('#form').find('.ng-invalid:visible:first').focus();
        }
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

    $scope.init = function () {
        var promises = [];
        promises.push($scope.getCategories());
        return $q.all(promises);
    };
    $scope.init().then(
        function () {
            if ($scope.site.id) {
                $scope.getSite();
            }
        }
    );

    // Watch categories for changes
    $scope.$watch('categories | filter:{selected:true}', function (nv) {
        $scope.site.site_categories = nv.map(function (site_category) {
            return { category_id: site_category.id };
        });
    }, true);
}]);

