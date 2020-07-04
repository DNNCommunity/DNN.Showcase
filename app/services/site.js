dnnShowcase.factory('siteService', ['$http', function siteService($http) {

    var base_path = apiUrlBase + "/site";

    // interface
    var service = {
        list: list,
        get: get,
        insert: insert,
        update: update,
        remove: remove,
        save: save,
        saveThumbnail: saveThumbnail
    };
    return service;

    // implementation    

    // list
    function list(
        {

            category_id = null,
            user_id = null,
            active = null,
            //random = null,

            page_number = null,
            page_size = null,
            order_by = 'name',
            ascending = true

        } = {}) {

        var request = $http({
            method: "get",
            url: base_path + "?category_id=" + category_id + "&user_id=" + user_id + "&active=" + active + "&page_number=" + page_number + "&page_size=" + page_size + "&order_by=" + order_by + "&ascending=" + ascending
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


    // save thumnbnail 
    function saveThumbnail(item) {
        var request = $http({
            method: "post",
            url: base_path + "/saveThumbnail",
            data: item
        });
        return request;
    }

}]);
