(function () {

    function User(username, displayname, email, adminRole) {
        var self = this;
        self.username = ko.observable(username);
        self.displayname = ko.observable(displayname);
        self.email = ko.observable(email);
        self.adminRole = ko.observable(adminRole);
        self.changeRole = function (newRole) {
            $.ajax({
                url: "/Manage/ChangeUserRole",
                type: "get",
                data: {
                    username: self.username,
                    newRole: newRole
                },
                success: function () {
                    self.adminRole(newRole);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }

    }

    var MU = new function ManageUsersViewModel() {
        var self = this;
        self.numberToFetch = 10;
        self.maxPagination = 5;
        self.searchParameter = "";
        self.adminFilter = ko.observableArray(["SuperAdmin","Admin","User"]);
        self.order = ko.observable("unordered");
        self.orientation = ko.observable("");
        self.totalUsers = ko.observable(0);
        self.numberOfPages = ko.computed(function() {
            return Math.ceil(self.totalUsers() / self.numberToFetch)
        }, this);
        self.middlePagination = self.maxPagination - 2;
        self.paginationNumber = ko.computed(function () {
            return Math.min(self.numberOfPages(), self.maxPagination)
        }, this);
        self.currentPage = ko.observable(1);
        self.users = ko.observableArray([]);
        self.addUser = function (user) {
            self.users.push(user);
        }
        self.deleteUser = function (user) {
            $.ajax({
                url: "/Manage/DeleteUser",
                type: "get",
                data: {
                    username: user.username,
                },
                success: function () {
                    self.users.remove(user);
                    self.fetchUsers();
                },
                error: function (error) {
                    console.log(error);
                }
            });
            
        }
        self.updateCurrentPage = function (page) {
            self.currentPage(page);
            self.fetchUsers();
        }
        self.updateOrdering = function (order) {
            var newOrder="", newOrientation="";
            if (self.order() == order) {
                newOrder = self.order();
                if (self.orientation() == "ascending")
                    newOrientation = "descending";
                else
                    newOrder = "unordered";
            }
            else {
                newOrder = order;
                newOrientation = "ascending";
            }
            self.fetchUsers(newOrder,newOrientation);
        }
        self.updateSearch = function () {
            self.searchParameter = $("#searchParameter").val();
            self.fetchUsers(self.order(), self.orientation());
        }
        self.middlePageValue = function (index) {
            return self.currentPage()-(Math.floor(self.middlePagination/2) - index);
        }
        self.fetchUsers = function (order, orientation) {
            $.ajax({
                url: "/Manage/UsersJson",
                type: "get",
                contentType: "application/json",
                traditional:true,
                data: {
                    currentPage: self.currentPage,
                    numberToFetch: self.numberToFetch,
                    order: order,
                    orientation: orientation,
                    searchParameter: self.searchParameter,
                    adminFilter: self.adminFilter()
                },
                dataType: "json",
                success: function (result) {
                    self.users.removeAll();
                    self.order(order);
                    self.orientation(orientation);
                    self.totalUsers(result.totalUsers);
                    for (var i = 0; i < result.users.length; i++) {
                        self.addUser(new User(result.users[i].userName, result.users[i].displayName, result.users[i].email, result.users[i].adminRole));
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    }
    
    ko.applyBindings(MU);
    MU.fetchUsers("unordered","");
})();

