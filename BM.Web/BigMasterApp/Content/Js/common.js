/**全局对象*/
var hb = {
    version: 0.1,
    /**获取URL
    controller:控制器
    action:action
    */
    getControllerUrl: function (controller, action) {
        var url = "";
        var rootUrl = global_site_root;
        if (rootUrl != undefined && rootUrl != null && rootUrl.length > 0) {
            url = window.location.origin + rootUrl + controller + "/" + action;
        } else {
            url = window.location.origin + "/" + controller + "/" + action;
        }
        return url;
    },
    getUrl: function (path) {
        var url = "";
        var rootUrl = global_site_root;
        if (rootUrl != undefined && rootUrl != null && rootUrl.length > 0) {
            url = window.location.origin + rootUrl + path
        } else {
            url = window.location.origin + "/" + path;
        }
        return url;
    }
}

      function doGet(url, data, callback) {
        $.request({
            type: "GET",
            dataType: "json",
            url: url,
            data: data,
            xhrFields: {
                "Access-Control-Allow-Origin": '*',
                withCredentials: true
            },
            success: function (res) {
                callback(res);
            }
        }, false, false);
}
      function doPost(url, data, callback) {
    $.request({
        type: "GET",
        dataType: "json",
        url: url,
        data: JSON.stringify(data),
        xhrFields: {
            "Access-Control-Allow-Origin": '*',
            withCredentials: true
        },
        success: function (res) {
            callback(res);
        }
    }, false, false);
}
      (function ($) {
        /**请求数据
        *loding:是否遮罩
        *checkLogin:是否认证登录
        *ajax参数
        **/
        $.request = function (config, loding, checkLogin) {
            if (config.xhrFields == undefined) {
                config.xhrFields = {
                    "Access-Control-Allow-Origin": '*',
                    withCredentials: true,
                };
            }
            if (config.contentType == undefined) {
                config.contentType = "application/json";
            }
            var accessToken = getHbAccessToken();
            if (accessToken != null && accessToken != undefined && accessToken.length > 0) {
                config.headers = {
                    Authorization: 'Bearer ' + accessToken
                };
            }
            $.ajax(config);
        };
    })(jQuery);
      function is_weixn() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == "micromessenger") {
        return true;
    } else {
        return false;
    }
}

