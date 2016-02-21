﻿app.controller("rutrace.interference", function ($scope, $http, $location, appUrlService) {
    
    $scope.page.title = "TOP指标干扰分析: " + $scope.topStat.current.eNodebName + "-" + $scope.topStat.current.sectorId;
    $scope.oneAtATime = false;
    $scope.interferenceCells = [];
    $scope.victimCells = [];
    $scope.interferenceLevelOrder = "interferenceLevel";
    $scope.updateMessages = [];

    $scope.showInterference = function() {
        var cell = $scope.topStat.current;
        $scope.interferenceCells = [];
        $scope.victimCells = [];
        $scope.interferencePanelTitle = cell.eNodebName + "-" + cell.sectorId + "干扰小区列表";
        $http({
            method: 'GET',
            url: appUrlService.getApiUrl('Cell'),
            params: {
                'eNodebId': cell.cellId,
                'sectorId': cell.sectorId
            }
        }).success(function(result) {
            $scope.currentCell.longtitute = result.longtitute;
            $scope.currentCell.lattitute = result.lattitute;
        });
        
        $http({
            method: 'GET',
            url: appUrlService.getApiUrl('InterferenceNeighbor'),
            params: {
                'cellId': cell.cellId,
                'sectorId': cell.sectorId
            },
            headers: {
                'Authorization': 'Bearer ' + appUrlService.getAccessToken()
            }
        }).success(function () {
            $http({
                method: 'GET',
                url: appUrlService.getApiUrl('InterferenceNeighbor'),
                params: {
                    'begin': $scope.beginDate.value,
                    'end': $scope.endDate.value,
                    'cellId': cell.cellId,
                    'sectorId': cell.sectorId
                },
                headers: {
                    'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                }
            }).success(function (result) {
                $scope.interferenceCells = result;
            });
        });

        $http({
            method: 'GET',
            url: appUrlService.getApiUrl('InterferenceVictim'),
            params: {
                'begin': $scope.beginDate.value,
                'end': $scope.endDate.value,
                'cellId': cell.cellId,
                'sectorId': cell.sectorId
            },
            headers: {
                'Authorization': 'Bearer ' + appUrlService.getAccessToken()
            }
        }).success(function(result) {
            $scope.victimCells = result;
        });

        $http({
            method: 'GET',
            url: appUrlService.getApiUrl('InterferenceNeighbor'),
            params: {
                neighborCellId: cell.cellId,
                neighborSectorId: cell.sectorId
            },
            headers: {
                'Authorization': 'Bearer ' + appUrlService.getAccessToken()
            }
        }).success(function(result) {
            $scope.updateMessages.push({
                cellName: cell.eNodebName + '-' + cell.sectorId,
                counts: result
            });
        });
    };
    $scope.closeAlert = function (index) {
        $scope.updateMessages.splice(index, 1);
    }
    if ($scope.topStat.current.eNodebName === undefined || $scope.topStat.current.eNodebName === "")
        $location.path($scope.rootPath + "top");
    else {
        $scope.showInterference();
    }
});