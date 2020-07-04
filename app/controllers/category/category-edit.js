dnnShowcase.controller('categoryEditController', ['$scope', '$q', '$uibModalInstance', 'toastr', 'categoryService', 'id', function ($scope, $q, $uibModalInstance, toastr, categoryService, id) {

    $scope.loading = false;

    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };
    $scope.submitted = false;

    $scope.category = {
        id: id
    };

    $scope.getCategory = function () {
        var deferred = $q.defer();
        $scope.loading = true;

        categoryService.get($scope.category.id).then(
            function (response) {
                $scope.category = response.data;

                $scope.loading = false;
            },
            function (response) {
                console.log('getCategory failed', response);
                toastr.error("There was a problem loading the category", "Error");
                $scope.loading = false;
                deferred.reject();
            }
        );
        return deferred.promise;
    };
    $scope.saveCategory = function () {
        $scope.submitted = true;
        $scope.loading = true;

        var isNew = $scope.category.id === null;

        if ($scope.form.$valid) {

            categoryService.save($scope.category).then(
                function (response) {
                    $scope.category = response.data;

                    $scope.loading = false;
                    $scope.submitted = false;

                    if (isNew) {
                        toastr.success("The category '" + $scope.category.name + "' was created.", "Success");
                    }
                    else {
                        toastr.success("The category '" + $scope.category.name + "' was saved.", "Success");
                    }
                    $uibModalInstance.close($scope.category);
                },
                function (response) {
                    console.log('saveCategory failed', response);
                    if (response.status === 406) {
                        toastr.error("That is not a valid DNN webcategory", "Error");
                    }
                    else {
                        toastr.error("There was a problem saving the category", "Error");
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

    $scope.init = function () {
        var promises = [];
        if ($scope.category.id) {
            promises.push($scope.getCategory());
        }
        return $q.all(promises);
    };
    $scope.init();

}]);

