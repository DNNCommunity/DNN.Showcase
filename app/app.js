﻿
var dnnShowcase = angular.module('DNN_Showcase', ['ngMessages', 'ngAnimate', 'ui.bootstrap', 'toastr', 'slim'], function ($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
});

dnnShowcase.filter('search', function () {
    return function (list, field, partialString) {
        // if the search string is invalid or empty - return the entire list
        if (!angular.isString(partialString) || partialString.length === 0) return list;

        var results = [];

        angular.forEach(list, function (item) {
            if (angular.isString(item[field])) {
                if (item[field].search(new RegExp(partialString, "i")) > -1) {
                    results.push(item);
                }
            }
        });

        return results;
    };
});

dnnShowcase.config(function ($httpProvider) {
    $httpProvider.defaults.withCredentials = true;

    var httpHeaders = {
        "ModuleId": sf.getModuleId(),
        "TabId": sf.getTabId(),
        "RequestVerificationToken": sf.getAntiForgeryValue()
    };
    angular.extend($httpProvider.defaults.headers.common, httpHeaders);
});

dnnShowcase.config(function (toastrConfig) {
    angular.extend(toastrConfig, {
        positionClass: 'toast-top-right',
        timeOut: 3000,
        maxOpened: 1,
        progressBar: true,
        tapToDismiss: true,
        autoDismiss: true,
        toastClass: 'toastr'
    });
});