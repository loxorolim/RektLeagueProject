(function () {

    var elementId = 0;
    function Element(initialType) {
        var self = this;
        self.ID = elementId++;

        self.elementType = ko.observable(initialType);
        self.elementText = ko.observable("");
        self.uploadable = ko.observable(false);
        self.name = ko.computed(function () {
            return self.elementType() + self.ID;
        });
        self.setUploadable = function () { self.uploadable(true); }

    }
    function ElementsViewModel() {
        var self = this;
        self.elements = ko.observableArray([
        new Element("Text")]);

        self.availableTypes = [
            "Text",
            "Image/Gif",
            "YoutubeURL"
        ];

        self.addElement = function () {
            self.elements.push(new Element(self.availableTypes[0]));
        }
        self.removeElement = function (element) { self.elements.remove(element) }

    }
    ko.applyBindings(new ElementsViewModel(),$("#writePost")[0]);


    function readURL(input) {

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#blah').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }



})();