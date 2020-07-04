dnnShowcase.controller('siteDetailController', ['$scope', '$q', '$uibModalInstance', 'toastr', 'siteService', 'id', function($scope, $q, $uibModalInstance, toastr, siteService, id) {

    $scope.loading = false;

    $scope.close = function() {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.site = {
        id: id
    };

    $scope.getSite = function() {
        var deferred = $q.defer();
        $scope.loading = true;

        siteService.get($scope.site.id).then(
            function(response) {
                $scope.site = response.data;


                deferred.resolve();
                $scope.loading = false;
            },
            function(response) {
                console.log('getSite failed', response);
                toastr.error("There was a problem loading the site", "Error");
                $scope.loading = false;
                deferred.reject();
            }
        );
        return deferred.promise;
    };

    $scope.init = function() {
        var promises = [];
        return $q.all(promises);
    };
    $scope.init().then(
        function() {
            if ($scope.site.id) {
                $scope.getSite();
            }
        }
    );
}]);

