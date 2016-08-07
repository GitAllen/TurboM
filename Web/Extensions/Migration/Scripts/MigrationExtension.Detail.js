(function ($, cvf, global) {
    var exp = cvf.extensions['MigrationExtension'];
    exp.windows = exp.windows || {};
    var subscription = exp.subscription = null, currentMonitorType = null, currentUsageAction = "";

    var detailWin = exp.windows.detail = cvf.window.create({
        title: 'Detail',
        width: 'large',
        url: exp.templates.detail,
        tools: [
            {
                name: 'export', icon: 'fa-download',
                handler: function () {
                    editWin.title('Download ' + subscription.Name)
                        .open().render();
                }
            }
        ],
        afterRender: function (data) {
            this.bootstrap();
        }
    });

    angular
        .module('subscriptionDetailApp', ['ngResource', 'cvfDateRange', 'cvfMorris'])
        .controller('detailController', [
            "$scope", function ($scope) {
                $.getJSON('wwwroot/data/migrationsummary.json', { subscriptionId: exp.subscriptionId },
                    function (data) {
                        detailWin.title(data.Name);
                        subscription = exp.subscription = data;
                        $.each($scope.usageBars, function () {
                            var bar = this;
                            var max = data['Max' + bar.field], current = data['Current' + bar.field], percent = Math.round(current * 100 / max, 2);
                            bar.max = max, bar.current = current, bar.percent = percent;
                            bar.hint = hint = percent > 80 ? 'danger' : (percent > 60 ? 'warning' : (percent > 40 ? 'info' : 'success'));
                        });
                        $scope.subscription = data;
                        $scope.$digest();
                        $scope.loadViewer();
                        detailWin.unprogress();
                    });
                detailWin.progress();
            }
        ]);
})(jQuery, cvf, window);