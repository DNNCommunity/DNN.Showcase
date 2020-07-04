dnnShowcase.directive('siteList', function () {
    return {
        templateUrl: '/DesktopModules/Dnn.Showcase/app/views/site/site-list.html',
        controller: 'siteListController',
        scope: {
            userId: '='
        }
    };
});
