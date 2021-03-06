﻿
app.controller("rutrace.trend", function ($scope, appRegionService, appKpiService, appFormatService) {
    $scope.page.title = "指标变化趋势";
    var lastWeek = new Date();
    lastWeek.setDate(lastWeek.getDate() - 7);
    $scope.beginDate = {
        value: lastWeek,
        opened: false
    };
    $scope.endDate = {
        value: new Date(),
        opened: false
    };

    $scope.showTrend = function () {
        appKpiService.getDateSpanPreciseRegionKpi($scope.city.selected, $scope.beginDate.value, $scope.endDate.value)
            .then(function (result) {
                $scope.trendStat.stats = appKpiService.generateDistrictStats($scope.trendStat.districts, result);
                if (result.length > 0) {
                    appKpiService.generateTrendStatsForPie($scope.trendStat, result);
                    $scope.trendStat.stats.push(appKpiService.calculateAverageRates($scope.trendStat.stats));
                }
                $scope.trendStat.beginDateString = appFormatService.getDateString($scope.beginDate.value, "yyyy年MM月dd日");
                $scope.trendStat.endDateString = appFormatService.getDateString($scope.endDate.value, "yyyy年MM月dd日");
            });
    };
    appRegionService.initializeCities()
        .then(function (result) {
            $scope.city = result;
            var city = $scope.city.selected;
            appRegionService.queryDistricts(city)
                .then(function (districts) {
                    $scope.trendStat.districts = districts;
                    $scope.showTrend();
            });
        });
})