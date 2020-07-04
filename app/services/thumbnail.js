dnnShowcase.factory('thumbnailService', ['$http', function thumbnailService($http) {

    var base_path = apiUrlBase + "/thumbnail";

    // interface
    var service = {
        save: save
    };
    return service;

    // implementation    


    // save
    function save(item) {
        var request = $http({
            method: "post",
            url: base_path + "/save",
            data: item
        });
        return request;
    }

}]);
