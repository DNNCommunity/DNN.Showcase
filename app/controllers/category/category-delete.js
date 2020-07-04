dnnShowcase.controller('categoryDeleteController', ['$scope', '$uibModalInstance', 'toastr', 'categoryService', 'category', function ($scope, $uibModalInstance, toastr, categoryService, category) {

    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.category = category;

    $scope.deleteCategory = function () {
        categoryService.remove(category.id).then(
            function () {
                toastr.success("The category '" + category.name + "' was deleted.", "Success");
                $uibModalInstance.close();
            },
            function (response) {
                console.log('deleteCategory failed', response);
                toastr.error("There was a problem deleteing the category", "Error");
            }
        );
    };

}]);

