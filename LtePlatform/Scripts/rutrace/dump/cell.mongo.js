﻿app.controller('dump.cell.mongo', function ($scope, $uibModalInstance, dumpProgress, appFormatService, dumpPreciseService,
    dialogTitle, eNodebId, sectorId, pci, begin, end) {
    $scope.dialogTitle = dialogTitle;

    $scope.dateRecords = [];
    $scope.currentDetails = [];

    $scope.ok = function () {
        $uibModalInstance.close($scope.dateRecords);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.queryRecords = function () {
        angular.forEach($scope.dateRecords, function (record) {
            dumpProgress.queryExistedItems(eNodebId, sectorId, record.date).then(function (result) {
                for (var i = 0; i < $scope.dateRecords.length; i++) {
                    if ($scope.dateRecords[i].date === result.date) {
                        $scope.dateRecords[i].existedRecords = result.value.item1;
                        $scope.dateRecords[i].existedStat = result.value.item2;
                        break;
                    }
                }
            });
            dumpProgress.queryMongoItems(eNodebId, pci, record.date).then(function (result) {
                for (var i = 0; i < $scope.dateRecords.length; i++) {
                    if ($scope.dateRecords[i].date === result.date) {
                        $scope.dateRecords[i].mongoRecords = result.value;
                        break;
                    }
                }
            });
            dumpProgress.queryMongoCellStat(eNodebId, pci, record.date).then(function (result) {
                for (var i = 0; i < $scope.dateRecords.length; i++) {
                    if ($scope.dateRecords[i].date === result.date) {
                        $scope.dateRecords[i].mongoStat = result.value;
                        break;
                    }
                }
            });
        });
    };

    $scope.updateDetails = function(records) {
        $scope.currentDetails = records;
    };

    $scope.dumpRecords = function (record) {
        if (record.mongoRecords.length > record.existedRecords) {
            dumpPreciseService.dumpRecords(record.mongoRecords, 0, eNodebId, sectorId, $scope.queryRecords);
        }
        if (!record.existedStat && record.mongoStat) {
            dumpProgress.dumpCellStat(record.mongoStat).then(function() {
                $scope.queryRecords();
            });
        }
        
    };

    $scope.dumpAllRecords = function () {
        dumpPreciseService.dumpAllRecords($scope.dateRecords, 0, 0, eNodebId, sectorId, $scope.queryRecords);
    };

    var startDate = new Date(begin);
    while (startDate < end) {
        var date = new Date(startDate);
        $scope.dateRecords.push({
            date: date,
            existedRecords: 0,
            existedStat: false
        });
        startDate.setDate(date.getDate() + 1);
    }
    $scope.queryRecords();
});