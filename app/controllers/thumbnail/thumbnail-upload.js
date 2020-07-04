dnnShowcase.controller('thumbnailUploadController', ['$scope', '$uibModalInstance', 'toastr', 'thumbnailService', function ($scope, $uibModalInstance, toastr, thumbnailService) {

    $scope.loading = false;

    $scope.close = function () {
        $uibModalInstance.close($scope.path);
    };

    $scope.temp_file_name = Date.now();

    $scope.path = null;

    $scope.thumbnail = {};
    $scope.slim_thumbnail = {
        init: function (data, slim) {
            $scope.thumbnail = slim;
        },
        service: function (formdata, progress, success, failure, slim) {

            var dto = {
                name: $scope.temp_file_name,
                image: slim.dataBase64.output.image
            };

            thumbnailService.save(dto).then(
                function (response) {
                    $scope.path = response.data;
                    success("image upload success");
                    toastr.success("Thumbnail saved.", "Success");
                },
                function (response) {
                    console.log(response);
                    failure("image upload failed");
                }
            );
        },
        remove: function () {
            $scope.path = null;
        }
    };

}]);

