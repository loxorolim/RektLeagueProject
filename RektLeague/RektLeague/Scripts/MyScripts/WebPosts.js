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
                    exhibit: exhibit
                },
                dataType: "json",
                success: function (result) {
                    for (var i = 0 ; i < result.length; i++) {
                        self.addWebPost(result[i]);
                    }
                    self.webpostNumber += result.length;
                }
            });
        }
    }
    ko.applyBindings(wpvm);
    wpvm.fetchWebPosts();
   
    window.onscroll = function () {
        //console.log((window.innerHeight + window.scrollY) + "/" + document.body.offsetHeight);
        if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {

            wpvm.fetchWebPosts();
        }
    };
    
})();

