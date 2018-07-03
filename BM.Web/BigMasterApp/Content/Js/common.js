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
    },
    /*获取经纬度信息*/
    getLocation_h5: function (callback,errorBack) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                //console.log(position);
                position.getSuccess = true;
                if (typeof callback == 'function') {
                    callback(position);
                }                
            }, errorBack);//获取位置信息
        } else {            
            console.log("不支持地理微信信息获取");
            callback({ latitude:0, longitude:0, getSuccess: false });
        }
    }
}

function is_weixn() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == "micromessenger") {
        return true;
    } else {
        return false;
    }
}

