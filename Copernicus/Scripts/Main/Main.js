$(document).ready(function () {
    $('input').focus(function () {
        $('input').each(function (x, y) {
            $(y).parent().css('border', '1px solid #D5D5D5');
        });
        $(this).parent().css('border', '1px solid #5A93C9');
    });
    $('input').blur(function () {
        $(this).parent().css('border', '1px solid #D5D5D5');
    });
    $('.inputWrapper').click(function () {
        $(this).children('input').focus();
    });
    ko.applyBindings(new SearchModel(), $('#search')[0]);
    ko.applyBindings(new NotificationModel(), $('#notifications')[0]);
    ko.applyBindings(new UsersModel(), $('#onlineUsers')[0]);
    var recentUpdates = new RecentUpdatesModel();
    var upcomingActivities = new UpcomingActivitiesModel();
    var newCompaniesAndPeople = new NewCompaniesAndPeopleModel();
    var projects = new ProjectsModel();
    $('#Home').click(function () {
        if ($('#recentUpdates').length == 0)
            return true;
        recentUpdates.setToggle(true);
        upcomingActivities.setToggle(true);
        newCompaniesAndPeople.setToggle(true);
        projects.setToggle(true);
        return false;
    });
    ko.applyBindings(recentUpdates, $('#recentUpdates')[0]);
    ko.applyBindings(upcomingActivities, $('#activities')[0]);
    ko.applyBindings(newCompaniesAndPeople, $('#newCompaniesAndPeople')[0]);
    ko.applyBindings(projects, $('#ProjectsList')[0]);
});

function ProjectsModel() {
    var self = this;
    self.projects = ko.observableArray([{ Text: ' ' }]);
    self.toggle = ko.observable(true);
    self.setToggle = function (value) {
        self.toggle(value);
        if (self.toggle())
            self.getProjects();
    };
    self.getProjects = function () {
        $.ajax({
            type: "POST",
            url: '/API/Projects',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            processdata: true,
            success: function (msg) {
                if (msg == null || msg.length === 0)
                    return;
                self.projects(msg);
            }
        });
    };
    self.getProjects();
}

function UsersModel() {
    var self = this;
    self.users = ko.observableArray([{ Name: 'Jimmy Jones' }]);
    self.toggle = ko.observable(false);
    self.setToggle = function () {
        self.toggle(!self.toggle());
    };
    self.getOnlineUsers = function () {
        $.ajax({
            type: "POST",
            url: '/API/OnlineUsers',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            processdata: true,
            success: self.users
        });
        setTimeout(self.getOnlineUsers, 30000);
    };
    self.getOnlineUsers();
}

function NotificationModel() {
    var self = this;
    self.notifications = ko.observableArray([{ Text: 'Blah blah blah' }]);
    self.toggle = ko.observable(false);
    self.setToggle = function () {
        self.toggle(!self.toggle());
    };
    self.getNotifications = function () {
        $.ajax({
            type: "POST",
            url: '/API/Notification',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            processdata: true,
            success: function (msg) {
                if (msg == null || msg.length === 0)
                    return;
                self.notifications(msg);
            }
        });
        setTimeout(self.getNotifications, 30000);
    };
    self.getNotifications();
}

function RecentUpdatesModel() {
    var self = this;
    self.recentUpdates = ko.observableArray([{ Text: ' ' }]);
    self.toggle = ko.observable(true);
    self.setToggle = function (value) {
        self.toggle(value);
        if (self.toggle())
            self.getRecentUpdates();
    };
    self.getRecentUpdates = function () {
        $.ajax({
            type: "POST",
            url: '/API/RecentUpdates',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            processdata: true,
            success: function (msg) {
                if (msg == null || msg.length === 0)
                    return;
                self.recentUpdates(msg);
            }
        });
    };
    self.getRecentUpdates();
}

function UpcomingActivitiesModel() {
    var self = this;
    self.upcomingActivities = ko.observableArray([{ Text: ' ' }]);
    self.toggle = ko.observable(true);
    self.setToggle = function (value) {
        self.toggle(value);
        if (self.toggle())
            self.getUpcomingActivities();
    };
    self.getUpcomingActivities = function () {
        $.ajax({
            type: "POST",
            url: '/API/UpcomingActivities',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            processdata: true,
            success: function (msg) {
                if (msg == null || msg.length === 0)
                    return;
                self.upcomingActivities(msg);
            }
        });
    };
    self.getUpcomingActivities();
}

function NewCompaniesAndPeopleModel() {
    var self = this;
    self.newCompaniesAndPeople = ko.observableArray([{ Text: ' ' }]);
    self.toggle = ko.observable(true);
    self.setToggle = function (value) {
        self.toggle(value);
        if (self.toggle())
            self.getNewCompaniesAndPeople();
    };
    self.getNewCompaniesAndPeople = function () {
        $.ajax({
            type: "POST",
            url: '/API/NewCompaniesAndPeople',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            processdata: true,
            success: function (msg) {
                if (msg == null || msg.length === 0)
                    return;
                self.newCompaniesAndPeople(msg);
            }
        });
    };
    self.getNewCompaniesAndPeople();
}

function SearchModel() {
    var self = this;
    self.recent = ko.observableArray([]);
    self.results = ko.observableArray();
    self.showRecent = ko.observable(false);
    self.showResults = ko.observable(false);
    self.searchCount = 0;

    self.doSearch = function () {
        var value = $('#q').val();
        if (value.length < 3) {
            self.results(null);
            self.showResults(false);
            return;
        }
        $.ajax({
            type: "POST",
            url: '/API/Search',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            processdata: true,
            data: JSON.stringify({ "Value": value, "SearchCount": ++self.searchCount }),
            success: function (msg) {
                if (msg.Count != self.searchCount)
                    return;
                self.results(msg.Results);
                self.showResults(msg.Results.length > 0);
                self.showRecent(false);
                $('.searchResults').css('left', $('#header>#search>.inputWrapper').offset().left);
                $('.searchResults').css('width', parseInt($('#header>#search>.inputWrapper').css('width').replace('px', '')) + 10 + 'px');
                $('.search').css('left', $('#header>#search>.inputWrapper').offset().left);
                $('.search').css('width', parseInt($('#header>#search>.inputWrapper').css('width').replace('px', '')) + 10 + 'px');
            }
        });
    };

    self.setRecent = function () {
        self.showResults(self.showRecent() && self.results().length > 0);
        self.showRecent(!self.showRecent());
        $('.searchResults').css('left', $('#header>#search>.inputWrapper').offset().left);
        $('.searchResults').css('width', parseInt($('#header>#search>.inputWrapper').css('width').replace('px', '')) + 10 + 'px');
        $('.search').css('left', $('#header>#search>.inputWrapper').offset().left);
        $('.search').css('width', parseInt($('#header>#search>.inputWrapper').css('width').replace('px', '')) + 10 + 'px');
        return false;
    };

    self.addRecent = function () {
        var value = $('#q').val();
        if (value != '') {
            if (self.recent().length > 6)
                self.recent.pop();
            var Found = false;
            for (var x = 0; x < self.recent().length; ++x) {
                if (self.recent()[x].text === value) {
                    Found = true;
                    break;
                }
            }
            if (!Found)
                self.recent.unshift(new RecentSearch(value, "#" + value));
            $('.recentSearches>a').click(function () {
                $('#q').val($(this).attr('href').replace('#', ''));
                $('#q').keyup();
                return false;
            });
        }
    };
}

function RecentSearch(text, href) {
    var self = this;
    self.text = text;
    self.href = href;
}