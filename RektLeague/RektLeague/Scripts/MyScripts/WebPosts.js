(function () {
    var wpvm = new function WebPostsViewModel() {
        var self = this;
        self.webpostNumber = 0;
        self.webPosts = ko.observableArray([]);
        self.addWebPost = function (wb) {
            var newWebPost = new WebPost();
            newWebPost.buildWebPost(wb);
            self.webPosts.push(newWebPost);

        }
        self.fetchWebPosts = function () {
            $.ajax({
                url: "/WebPost/WebPostsJson",
                type: "get",
                data: {
                    webpostNumber: self.webpostNumber,
                    category: category,
                    author: author,
                    search: search,
                    exhibit: exhibit
                },
                dataType: "json",
                success: function (result) {
                    for (var i = 0 ; i < result.length; i++) {
                        self.addWebPost(result[i]);
                    }
                    self.webpostNumber += result.length;
                    canfetch = true;
                },
                error: function (error) {
                    console.log(error);
                    canfetch = true;
                }
            });
        }
    }
    ko.applyBindings(wpvm);
    wpvm.fetchWebPosts();
    var canfetch = true;
    window.addEventListener("scroll", function () {
        var body = document.body,
        html = document.documentElement;
        var height = Math.max(body.scrollHeight, body.offsetHeight,
                       html.clientHeight, html.scrollHeight, html.offsetHeight);
        //console.log(window.innerHeight +"/"+ window.scrollY + "/" + document.body.offsetHeight +"/" + height);
        if ((window.innerHeight + window.scrollY) >= 0.8 * height) {
            if (canfetch){
                wpvm.fetchWebPosts();
                canfetch = false;
            }
                
        }
    });
    
})();

