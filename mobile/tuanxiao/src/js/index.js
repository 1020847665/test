document.documentElement.style.fontSize = document.documentElement.clientWidth / 750 * 16 + 'px';
window.onresize = function() {
    document.documentElement.style.fontSize = document.documentElement.clientWidth / 750 * 16 + 'px';
};
    // 设置title
    var setTitle = function(title) {
      console.log(title);
        var u = navigator.userAgent;
        var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Linux') > -1;
        var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/);
        if (isAndroid) {
            document.title = title;
        } else if (isiOS) {
            var body = document.body;
            document.title = title;
            var iframe = document.createElement('iframe');
            iframe.setAttribute("src", "/favicon.ico");
            iframe.onload = function() {
                iframe.appendTo(body);
            };
            window.setTimeout(function() {
                iframe.offload = function() {
                    body.removeChild(iframe);
                };
            }, 0);
        }
    };
