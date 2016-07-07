function getRootUrl() {
    return window.location.origin ? window.location.origin + '/' : window.location.protocol + '/' + window.location.host + '/';
}
function Element(elementType, text, imgBytes) {
    var self = this;
    self.elementType = ko.observable(elementType);
    if (elementType == 2 && text != null)
        text = text.replace("watch?v=", "v/");
    self.text = ko.observable(text);

    imgBytes = "data:image/png;base64," + imgBytes;
    self.imgBytes = ko.observable(imgBytes);

}
function WebPost() {
    var self = this;
    self.title = ko.observable();
    self.category = ko.observable();
    self.categoryName = ko.observable();
    self.id = ko.observable(0);
    self.pageUrl = ko.computed(function () {
        return getRootUrl() + "WebPost/WebPost/" + self.id();
    }, this);
    
    self.day = ko.observable();
    self.month = ko.observable();
    self.year = ko.observable();
    self.author = ko.observable();
    self.authorName = ko.observable();
    self.authorImage = ko.observable("/Content/Images/notloggeduser.png");
    self.elements = ko.observableArray([]);
    self.addElement = function (elementType, text, imgBytes) {
        self.elements.push(new Element(elementType, text, imgBytes));
    }
    var monthNames = ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];
    self.buildWebPost = function (json) {
            self.id(json.id);
            self.title(json.title);
            self.category(json.category);
            self.categoryName(json.categoryName)
            self.author(json.author);
            self.authorName(json.authorName);
            if(json.authorImage != "")
                self.authorImage(json.authorImage);
            var myDate = new Date(json.publicationDate);
            self.day(myDate.getDate());
            self.month(monthNames[myDate.getMonth()]);
            self.year(myDate.getFullYear());
            self.elements.removeAll();
            for (var i = 0; i < json.elements.length; i++) {
                self.addElement(json.elements[i].elementType, json.elements[i].postString, json.elements[i].postBytes);
            }
    }
     
}


