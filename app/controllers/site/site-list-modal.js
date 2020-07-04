dnnShowcase.controller('siteListModalController', [ '$scope', '$uibModalInstance', function ($scope, $uibModalInstance) {
    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };
}]);

