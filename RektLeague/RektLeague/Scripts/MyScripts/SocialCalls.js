(function(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.6";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

////$.ajax({ url: 'http://platform.twitter.com/widgets.js', dataType: 'script', cache: true });
//!function (d, s, id){
//    var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https';
//    if (!d.getElementById(id)){
//        js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs);
//    }
//}(document, 'script', 'twitter-wjs');

//(function () {
//    var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
//    po.src = 'https://apis.google.com/js/platform.js';
//    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
//})();