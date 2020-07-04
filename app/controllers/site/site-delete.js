dnnShowcase.controller('siteDeleteController', ['$scope', '$uibModalInstance', 'toastr', 'siteService', 'site', function ($scope, $uibModalInstance, toastr, siteService, site) {

    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.site = site;

    $scope.deleteSite = function () {
        siteService.remove(site.id).then(
            function () {
                toastr.success("The site '" + site.name + "' was deleted.", "Success");
                $uibModalInstance.close();
            },
            function (response) {
                console.log('deleteSite failed', response);
                toastr.error("There was a problem deleteing the site", "Error");
            }
        );
    };

}]);

