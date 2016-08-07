(function ($, cvf, global) {
    var exp = cvf.extensions['SubscriptionExtension'];

    var addWin = cvf.window.create({
        title: '1 - Form data',
        width: 'small',
        url: exp.templates.new,
        autoInitControls: false,
        tools: [
            {
                name: 'save', icon: 'fa-save', handler: function () {
                    addWin.submit();
                }
            }
        ],
        afterRender: function () {
            this.bootstrap();
        }, submitSuccess: function (data, statu, xhr, form) {
            if (data.TypeName == "success") {
                addWin.clear("#subscriptionId");
            }
        }
    }, {
        title: '2 - Form data',
        width: 'small',
        url: exp.templates.new,
        autoInitControls: false,
        tools: [
            {
                name: 'save', icon: 'fa-save', handler: function () {
                    addWin.submit();
                }
            }
        ],
        afterRender: function () {
            this.bootstrap();
        }, submitSuccess: function (data, statu, xhr, form) {
            if (data.TypeName == "success") {
                addWin.clear("#subscriptionId");
            }
        }
    }, {
        title: '3 - Form data',
        width: 'small',
        url: exp.templates.new,
        autoInitControls: false,
        tools: [
            {
                name: 'save', icon: 'fa-save', handler: function () {
                    addWin.submit();
                }
            }
        ],
        afterRender: function () {
            this.bootstrap();
        }, submitSuccess: function (data, statu, xhr, form) {
            if (data.TypeName == "success") {
                addWin.clear("#subscriptionId");
            }
        }
    }, {
        title: '4 - Form data',
        width: 'small',
        url: exp.templates.new,
        autoInitControls: false,
        tools: [
            {
                name: 'save', icon: 'fa-save', handler: function () {
                    addWin.submit();
                }
            }
        ],
        afterRender: function () {
            this.bootstrap();
        }, submitSuccess: function (data, statu, xhr, form) {
            if (data.TypeName == "success") {
                addWin.clear("#subscriptionId");
            }
        }
    });
    cvf.nav.registerNew({
        title: '1 - Wizard step 1',
        description: 'Register your subscription.',
        icon: 'fa-align-left',
        handler: function() {
            cvf.clear();
            addWin.open().ajax("/Subscription/GetUserList");
        }
    });
    cvf.nav.registerNew({
        title: '2 - Wizard step 2',
        description: 'Register your subscription.',
        icon: 'fa-align-left',
        handler: function() {
            cvf.clear();
            addWin.open().ajax("/Subscription/GetUserList");
        }
    });
    cvf.nav.registerNew({
        title: '3 - Wizard step 3',
        description: 'Register your subscription.',
        icon: 'fa-align-left',
        handler: function() {
            cvf.clear();
            addWin.open().ajax("/Subscription/GetUserList");
        }
    });
    cvf.nav.registerNew({
        title: '3 - Wizard step 3',
        description: 'Register your subscription.',
        icon: 'fa-align-left',
        handler: function() {
            cvf.clear();
            addWin.open().ajax("/Subscription/GetUserList");
        }
    });

    angular
        .module("addSubscriptionApp", [])
        .controller("addController", ["$scope", function ($scope) {
            $scope.subscription = {
                ServiceEndpoint: 'https://management.core.chinacloudapi.cn'
            };
            addWin.progress();
            $.getJSON("Subscription/GetUserList", function (data) {
                $scope.users = data;
                $scope.$digest();
                addWin.initControls();
                addWin.unprogress();
            });
        }]);
})(jQuery, cvf, window);