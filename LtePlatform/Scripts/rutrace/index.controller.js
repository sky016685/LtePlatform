﻿
app.controller("rutrace.index", function ($scope, appRegionService, appKpiService, appFormatService) {
    $scope.page.title = "指标总体情况";
    var yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    $scope.statDate = {
        value: yesterday,
        opened: false
    };
    $scope.city = {
        selected: "",
        options: []
    };
    $scope.showKpi = function() {
        appKpiService.getRecentPreciseRegionKpi($scope.city.selected, $scope.statDate.value)
            .then(function (result) {
                $scope.statDate.value = appFormatService.getDate(result.statDate);
                angular.forEach(result.districtPreciseViews, function(view) {
                    view.objectRate = appKpiService.getPreciseObject(view.district);
                });
                $scope.overallStat.districtStats = result.districtPreciseViews;
                $scope.overallStat.townStats = result.townPreciseViews;
                $scope.overallStat.currentDistrict = result.districtPreciseViews[0].district;
                $scope.overallStat.cityStat
                    = appKpiService.getCityStat($scope.overallStat.districtStats, $scope.city.selected);
                $scope.overallStat.cityStat.objectRate = 92.6;
                $scope.overallStat.dateString = appFormatService.getDateString($scope.statDate.value, "yyyy年MM月dd日");
            });
    };
    appRegionService.initializeCities()
        .then(function(result) {
            $scope.city = result;
            $scope.showKpi();
        });
});
