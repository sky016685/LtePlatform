﻿function MapViewModel(app, dataModel) {
    var self = this;

    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.collegeInfos = ko.observableArray([]);
    self.collegeNames = ko.observableArray([]);

    self.showRates = function() {
        sendRequest(app.dataModel.college4GTestUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            upload: 1
        }, function(download) {
            var downloadRates = matchCollegeStats(self.collegeNames(), download);
            sendRequest(app.dataModel.college4GTestUrl, "GET", {
                begin: self.beginDate(),
                end: self.endDate(),
                upload: 0
            }, function(upload) {
                var uploadRates = matchCollegeStats(self.collegeNames(), upload);
                sendRequest(app.dataModel.college3GTestUrl, "GET", {
                    begin: self.beginDate(),
                    end: self.endDate()
                }, function(evdo) {
                    var evdoRates = matchCollegeStats(self.collegeNames(), evdo);
                    showCollegeRates(self.collegeNames(), downloadRates, uploadRates, evdoRates, "#college-rates");
                });
            });
        });
    };

    self.showUsers = function() {
        sendRequest(app.dataModel.college4GTestUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            kpiName: "users"
        }, function(lte) {
            var lteUsers = matchCollegeStats(self.collegeNames(), lte);
            sendRequest(app.dataModel.college3GTestUrl, "GET", {
                begin: self.beginDate(),
                end: self.endDate(),
                kpiName: "users"
            }, function(evdo) {
                var evdoUsers = matchCollegeStats(self.collegeNames(), evdo);
                showCollegeUsers(self.collegeNames(), lteUsers, evdoUsers, "#college-users");
            });
        });
    };

    self.showCoverage = function() {
        sendRequest(app.dataModel.college4GTestUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            kpiName: "rsrp"
        }, function(rsrp) {
            var rsrpStats = matchCollegeStats(self.collegeNames(), rsrp);
            sendRequest(app.dataModel.college4GTestUrl, "GET", {
                begin: self.beginDate(),
                end: self.endDate(),
                kpiName: "sinr"
            }, function(sinr) {
                var sinrStats = matchCollegeStats(self.collegeNames(), sinr);
                showCollegeCoverage(self.collegeNames(), rsrpStats, sinrStats, "#college-coverage");
            });
        });
    };

    self.showInterference = function() {
        sendRequest(app.dataModel.college3GTestUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            kpiName: "minRssi"
        }, function(minRssi) {
            var minRssiStats = matchCollegeStats(self.collegeNames(), minRssi);
            sendRequest(app.dataModel.college3GTestUrl, "GET", {
                begin: self.beginDate(),
                end: self.endDate(),
                kpiName: "maxRssi"
            }, function(maxRssi) {
                var maxRssiStats = matchCollegeStats(self.collegeNames(), maxRssi);
                sendRequest(app.dataModel.college3GTestUrl, "GET", {
                    begin: self.beginDate(),
                    end: self.endDate(),
                    kpiName: "vswr"
                }, function(vswr) {
                    var vswrStats = matchCollegeStats(self.collegeNames(), vswr);
                    showCollegeInterference(self.collegeNames(), minRssiStats, maxRssiStats, vswrStats, "#college-interference");
                });
            });
        });
    };

    self.showAlarms = function() {
        sendRequest(app.dataModel.collegeAlarmUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate()
        }, function(results) {
            var chart = new DrilldownColumn();
            chart.title.text = "校园网告警分布图";
            chart.series[0].data = [];
            chart.drilldown.series = [];
            chart.series[0].name = "校园名称";
            chart.options.yAxis = {
                title: {
                    text: '告警总数'
                }

            };
            for (var i = 0; i < self.collegeNames().length; i++) {
                var collegeName = self.collegeNames()[i];
                if (results[collegeName] !== undefined) {
                    var collegeAlarms = 0;
                    var subData = [];
                    for (var j = 0; j < results[collegeName].length; j++) {
                        collegeAlarms += results[collegeName][j].item2;
                        subData.push([results[collegeName][j].item1, results[collegeName][j].item2]);
                    }
                    chart.addOneSeries(collegeName, collegeAlarms, subData);
                }
            }
            showChartDialog("#college-alarms", chart);
        });
    };

    self.showFlows = function() {
        sendRequest(app.dataModel.collegeKpiUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            kpiName: "users"
        }, function (users) {
            var usersStats = matchCollegeStats(self.collegeNames(), users);
            sendRequest(app.dataModel.collegeKpiUrl, "GET", {
                begin: self.beginDate(),
                end: self.endDate(),
                kpiName: "downloadFlow"
            }, function (downloadFlow) {
                var downloadFlowStats = matchCollegeStats(self.collegeNames(), downloadFlow);
                sendRequest(app.dataModel.collegeKpiUrl, "GET", {
                    begin: self.beginDate(),
                    end: self.endDate(),
                    kpiName: "uploadFlow"
                }, function (uploadFlow) {
                    var uploadFlowStats = matchCollegeStats(self.collegeNames(), uploadFlow);
                    sendRequest(app.dataModel.collegeKpiUrl, "GET", {
                        begin: self.beginDate(),
                        end: self.endDate(),
                        kpiName: "flow3G"
                    }, function(flow3G) {
                        var flow3GStats = matchCollegeStats(self.collegeNames(), flow3G);
                        showCollegeFlows(self.collegeNames(), usersStats, downloadFlowStats, uploadFlowStats, flow3GStats, "#college-flows");
                    });
                });
            });
        });
    };

    self.showConnection = function() {
        sendRequest(app.dataModel.collegeKpiUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            kpiName: "rrcConnection"
        }, function (rrcConnection) {
            var rrcConnectionStats = matchCollegeStats(self.collegeNames(), rrcConnection);
            sendRequest(app.dataModel.collegeKpiUrl, "GET", {
                begin: self.beginDate(),
                end: self.endDate(),
                kpiName: "erabConnection"
            }, function (erabConnection) {
                var erabConnectionStats = matchCollegeStats(self.collegeNames(), erabConnection);
                sendRequest(app.dataModel.collegeKpiUrl, "GET", {
                    begin: self.beginDate(),
                    end: self.endDate(),
                    kpiName: "connection2G"
                }, function (connection2G) {
                    var connection2GStats = matchCollegeStats(self.collegeNames(), connection2G);
                    sendRequest(app.dataModel.collegeKpiUrl, "GET", {
                        begin: self.beginDate(),
                        end: self.endDate(),
                        kpiName: "connection3G"
                    }, function (connection3G) {
                        var connection3GStats = matchCollegeStats(self.collegeNames(), connection3G);
                        showCollegeConnection(self.collegeNames(), rrcConnectionStats, erabConnectionStats, connection2GStats, connection3GStats, $("#college-connection"));
                    });
                });
            });
        });
    };

    self.showDrop = function() {
        sendRequest(app.dataModel.collegeKpiUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            kpiName: "erlang3G"
        }, function (erlang3G) {
            var erlang3GStats = matchCollegeStats(self.collegeNames(), erlang3G);
            sendRequest(app.dataModel.collegeKpiUrl, "GET", {
                begin: self.beginDate(),
                end: self.endDate(),
                kpiName: "erabDrop"
            }, function (erabDrop) {
                var erabDropStats = matchCollegeStats(self.collegeNames(), erabDrop);
                sendRequest(app.dataModel.collegeKpiUrl, "GET", {
                    begin: self.beginDate(),
                    end: self.endDate(),
                    kpiName: "drop3G"
                }, function (drop3G) {
                    var drop3GStats = matchCollegeStats(self.collegeNames(), drop3G);
                    showCollegeDrop(self.collegeNames(), erlang3GStats, erabDropStats, drop3GStats, $("#college-drop"));
                });
            });
        });
    };

    self.toggleCollegeMarkers = function () {
        toggleDisplay(map.collegeMarkers);
    };

    self.toggleCollegeENodebs = function() {
        toggleDisplay(map.eNodebMarkers);
    };

    self.toggleCollegeCells = function() {
        toggleDisplay(map.lteSectors);
    };

    self.toggleCollegeLteDistributions = function() {
        toggleDisplay(map.lteDistributions);
    };

    self.toggleCollegeBtss = function() {
        toggleDisplay(map.btsMarkers);
    };

    self.toggleCollegeCdmaCells = function() {
        toggleDisplay(map.cdmaSectors);
    };

    self.toggleCollegeCdmaDistributions = function() {
        toggleDisplay(map.cdmaDistributions);
    };

    self.focusCollege = function (name) {
        for (var i = 0; i < self.collegeInfos().length; i++) {
            if (self.collegeInfos()[i].name === name) {
                var cell = {
                    baiduLongtitute: self.collegeInfos()[i].centerx,
                    baiduLattitute: self.collegeInfos()[i].centery
                };
                setCellFocus(cell);
                break;
            }
        }
    };

    return self;
}

app.addViewModel({
    name: "Map",
    bindingMemberName: "collegeMap",
    factory: MapViewModel
});