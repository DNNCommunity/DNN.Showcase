dnnShowcase.factory('categoryService', ['$http', function categoryService($http) {

    var base_path = apiUrlBase + "/category";

    // interface
    var service = {
        list: list,
        get: get,
        insert: insert,
        update: update,
        remove: remove,
        save: save
    };
    return service;

    // implementation    

    // list
    function list(user_id = null, category = '', url = '', skip = null, take = null) {

        var request = $http({
            method: "get",
            url: base_path + '?user_id=' + user_id + '&category=' + category + '&url=' + url + '&skip=' + skip + '&take=' + take
        });
        return request;
    }

    // get 
    function get(id) {
        var request = $http({
            method: "get",
            url: base_path + '/' + id
        });
        return request;
    }

    // insert
    function insert(item) {
        var request = $http({
            method: "post",
            url: base_path,
            data: item
        });
        return request;
    }

    // update 
    function update(item) {
        var request = $http({
            method: "put",
            url: base_path,
            data: item
        });
        return request;
    }

    // delete
    function remove(id) {
        var request = $http({
            method: "delete",
            url: base_path + '/' + id
        });
        return request;
    }

    // save
    function save(item) {
        if (item.id > 0) {
            return update(item);
        }
        else {
            return insert(item);
        }
    }

}]);
