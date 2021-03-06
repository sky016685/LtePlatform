﻿app.config(function ($stateProvider, $urlRouterProvider) {
        var viewDir = "/appViews/Parameters/";
        $stateProvider
            .state('list', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.root"
                    },
                    "contents": {
                        templateUrl: viewDir + "List.html",
                        controller: "parameters.list"
                    }
                },
                url: "/"
            })
            .state('query', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.root"
                    },
                    "contents": {
                        templateUrl: viewDir + "QueryMap.html",
                        controller: "query.map"
                    }
                },
                url: "/query"
            })
            .state('eNodebList', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.town"
                    },
                    "contents": {
                        templateUrl: viewDir + "Region/ENodebTable.html",
                        controller: "eNodeb.list"
                    }
                },
                url: "/eNodebList/:city/:district/:town"
            })
            .state('btsList', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.town"
                    },
                    "contents": {
                        templateUrl: viewDir + "Region/BtsTable.html",
                        controller: "bts.list"
                    }
                },
                url: "/btsList/:city/:district/:town"
            })
            .state('eNodebInfo', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.lte"
                    },
                    "contents": {
                        templateUrl: viewDir + "Region/ENodebInfo.html",
                        controller: "eNodeb.info"
                    }
                },
                url: "/eNodebInfo/:eNodebId/:name"
            })
            .state('Alarm', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.lte"
                    },
                    "contents": {
                        templateUrl: viewDir + "Region/Alarm.html",
                        controller: "eNodeb.alarm"
                    }
                },
                url: "/alarm/:eNodebId/:name"
            })
            .state('btsInfo', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.cdma"
                    },
                    "contents": {
                        templateUrl: viewDir + "Region/BtsInfo.html",
                        controller: "bts.info"
                    }
                },
                url: "/btsInfo/:btsId/:name"
            })
            .state('cellInfo', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.lte"
                    },
                    "contents": {
                        templateUrl: viewDir + "Region/CellInfo.html",
                        controller: "cell.info"
                    }
                },
                url: "/cellInfo/:eNodebId/:name/:sectorId"
            })
            .state('cdmaCellInfo', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.cdma"
                    },
                    "contents": {
                        templateUrl: viewDir + "Region/CdmaCellDetails.html",
                        controller: "cdmaCell.info"
                    }
                },
                url: "/cdmaCellInfo/:btsId/:name/:sectorId"
            });
        $urlRouterProvider.otherwise('/');
    })
    .run(function($rootScope) {
        var rootUrl = "/Parameters/List#";
        $rootScope.menuItems = [
            {
                displayName: "总体情况",
                isActive: true,
                subItems: [
                    {
                        displayName: "基础数据总揽",
                        url: rootUrl + "/"
                    }, {
                        displayName: "小区地图查询",
                        url: rootUrl + "/query"
                    }
                ]
            }, {
                displayName: "详细查询",
                isActive: false,
                subItems: []
            }
        ];
        $rootScope.menu = {
            accordions: {
        
            }
        };
        $rootScope.rootPath = rootUrl + "/";

        $rootScope.viewData = {
            workItems: []
        };
        $rootScope.page = {
            title: "基础数据总揽"
        };
    });
