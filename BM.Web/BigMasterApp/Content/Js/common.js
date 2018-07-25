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
     /*回到上一页*/
      function hist() {
        history.go(-1);
    }
      function doGet(url, data, callback) {
        $.request({
            type: "GET",
            dataType: "json",
            url: url,
            data: data,
            success: function (res) {
                callback(res);
            }
        }, false, false);
}
      function doPost(url, data, callback) {
          $.request({
            type: "post",
            dataType: "json",
            url: url,
            data: JSON.stringify(data),
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
            //if (config.xhrFields == undefined) {
            //    config.xhrFields = {
            //        "Access-Control-Allow-Origin": '*',
            //        withCredentials: true,
            //    };
            //}
            if (config.contentType == undefined) {
                config.contentType = "application/json";
            }
            //var accessToken = "";
            //if (accessToken != null && accessToken != undefined && accessToken.length > 0) {
            //    config.headers = {
            //        Authorization: 'Bearer ' + accessToken
            //    };
            //}
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

      function bm_setCookie(name, value) {
          var Days = 30;
          var exp = new Date();
          exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
          document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/";
      }

      //读取cookies 
      function bm_getCookie(name) {
          var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
          if (arr = document.cookie.match(reg))
              return unescape(arr[2]);
          else
              return null;
      }

      //删除cookies 
      function delCookie(name) {
          var exp = new Date();
          exp.setTime(exp.getTime() - 1);
          var cval = hb_getCookie(name);
          if (cval != null)
              document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/";
      }

