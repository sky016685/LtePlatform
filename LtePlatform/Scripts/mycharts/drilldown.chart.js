﻿function DrilldownChart() {
    var self = this;

    self.title = {
        text: 'Drill-down pie chart'
    };
    self.series = [
    {
        name: "Drill-down pie chart",
        colorByPoint: true,
        data: []
    }];
    self.drilldown = {
        series: []
    };

    self.options = {
        chart: {
            type: 'column'
        },
        title: self.title,
        subtitle: {
            text: 'Click the slices to view versions.'
        },
        plotOptions: {
            series: {
                dataLabels: {
                    enabled: true,
                    format: '{point.name}: {point.y}'
                }
            }
        },

        tooltip: {
            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}</b><br/>'
        },

        series: self.series,

        drilldown: self.drilldown
    };
}

DrilldownChart.prototype.addOneSeries = function (name, value, subData) {
    var self = this;
    self.series[0].data.push({
        name: name,
        y: value,
        drilldown: name
    });
    self.drilldown.series.push({
        name: name,
        id: name,
        data: subData
    });
};