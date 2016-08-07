(function ($, cvf, global) {
    var exp = cvf.extensions['MigrationExtension'];
    exp.windows = exp.windows || {};

    var listWin = cvf.window.create({
        title: 'Global Customer Migration Center',
        width: 'small',
        controls: true,
        url: '/Extensions/Migration/Views/MigrationExtension.List.html',
        autoInitControls: false,
        afterRender: function () {
            listWin.bootstrap();
        }
    });
    cvf.start.register({
        icon: 'fa-cloud',
        color: '#2a5caa',
        title: 'Global Customer Migration Center',
        onClick: function () {
            cvf.clear();
            listWin.open().render();
        }
    });

    angular
        .module('subscriptionApp', ['ngResource', 'cvfMorris'])
        .controller("SubscriptionList", ["$scope", function ($scope) {
            listWin.progress();
            $.getJSON('wwwroot/data/migrations.json', function (data) {
                $scope.subscriptions = data;
                $scope.$digest();
                listWin.initControls();
                listWin.unprogress();
            });

            $scope.showDetail = function (sub) {
                $scope.currentSub = sub;
                listWin.openChild(exp.windows.detail);
                exp.subscriptionId = sub.Id;
                exp.windows.detail.render();
            }
        }]);

})(jQuery, cvf, window);