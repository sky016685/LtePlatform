﻿angular.module('huawei.mongo.parameters', ['myApp.url'])
    .factory('cellHuaweiMongoService', function($q, $http, appUrlService) {
        return {
            queryCellParameters: function(eNodebId, sectorId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('CellHuaweiMongo'),
                        params: {
                            eNodebId: eNodebId,
                            sectorId: sectorId
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryLocalCellDef: function (eNodebId) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CellHuaweiMongo'),
                    params: {
                        eNodebId: eNodebId
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            }
        };
    })
     .factory('cellPowerService', function ($q, $http, appUrlService) {
         return {
             queryCellParameters: function (eNodebId, sectorId) {
                 var deferred = $q.defer();
                 $http({
                     method: 'GET',
                     url: appUrlService.getApiUrl('CellPower'),
                     params: {
                         eNodebId: eNodebId,
                         sectorId: sectorId
                     }
                 }).success(function (result) {
                     deferred.resolve(result);
                 })
                     .error(function (reason) {
                         deferred.reject(reason);
                     });
                 return deferred.promise;
             }
         };
     });