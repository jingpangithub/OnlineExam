layui.config({
    base: "/js/"
}).use(['form', 'layer'], function () {

    if (self != top) {
        //如果在iframe中，则跳转
        top.location.replace("/Login/Index");
    }

    var form = layui.form,
		layer = layui.layer,
		$ = layui.jquery;

    // Cloud Float...
    var $main = $cloud = mainwidth = null;
    var offset1 = 450;
    var offset2 = 0;
    var offsetbg = 0;

    $(document).ready(
        function () {
            $main = $("#mainBody");
            $body = $("body");
            $cloud1 = $("#cloud1");
            $cloud2 = $("#cloud2");

            mainwidth = $main.outerWidth();
        }
    );

    var _hmt = _hmt || [];
    (function() {
        var hm = document.createElement("script");
        hm.src = "//hm.baidu.com/hm.js?0558502420ce5fee054b31425e77ffa6";
        var s = document.getElementsByTagName("script")[0];
        s.parentNode.insertBefore(hm, s);
    })();

    setInterval(function flutter() {
        if (offset1 >= mainwidth) {
            offset1 = -580;
        }

        if (offset2 >= mainwidth) {
            offset2 = -580;
        }

        offset1 += 1.1;
        offset2 += 1;
        $cloud1.css("background-position", offset1 + "px 100px")

        $cloud2.css("background-position", offset2 + "px 460px")
    }, 70);

    setInterval(function bg() {
        if (offsetbg >= mainwidth) {
            offsetbg = -580;
        }

        offsetbg += 0.9;
        $body.css("background-position", -offsetbg + "px 0")
    }, 90);

    //登录按钮事件
    form.on("submit(login)", function () {
        if ($("#username").val() == "admin" && $("#password").val() == "123456") {
            window.location.href = "../Areas/Admin/Admin/Index";
        }
        else if ($("#username").val() == "teacher" && $("#password").val() == "123456") {
            window.location.href = "../Areas/Teacher/Teacher/Index";
        }
        else if ($("#username").val() == "student" && $("#password").val() == "123456") {
            window.location.href = "../Areas/Student/Student/Index";
        }
        else
            layer.msg("用户名或密码错误");

        return false;
    })

})
