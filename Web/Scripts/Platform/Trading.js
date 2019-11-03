$(function () {

    var PlatformTrading = function () {

        var tableActualTransaction;
        var tableLogUser;
        var chartTrading;
        var candleStickData;
        var data;
        var chart;


        this.initClass = function () {
            initEvent();
            initDataTable();

            initPlatformHub();
            initChart();
        };


        var initChart = function () {
            var container = $("div#chartPlatform")[0];
            chart = LightweightCharts.createChart(container, {
                width: container.offsetWidth,
                height: container.offsetHeight,
                crosshair: {
                    mode: LightweightCharts.CrosshairMode.Normal,
                },
                timeScale: {
                    timeVisible: true,
                    secondsVisible: true,
                },
                localization: {
                    dateFormat: 'yyyy-MM-dd',
                },
            });
            candleStickData = chart.addCandlestickSeries();

            data = [
                //{ time: '2018-10-19T00:00:00', open: 54.62, high: 55.50, low: 54.52, close: 54.90 },
                //{ time: '2018-10-22T00:00:00', open: 55.08, high: 55.27, low: 54.61, close: 54.98 },
                //{ time: '2018-10-23T00:00:00', open: 56.09, high: 57.47, low: 56.09, close: 57.21 },
                //{ time: '2018-10-24T00:00:00', open: 57.00, high: 58.44, low: 56.41, close: 57.42 },
                //{ time: '2018-10-25T00:00:00', open: 57.46, high: 57.63, low: 56.17, close: 56.43 },
                //{ time: '2018-10-26T00:00:00', open: 56.26, high: 56.62, low: 55.19, close: 55.51 },
                //{ time: '2018-10-29T00:00:00', open: 55.81, high: 57.15, low: 55.72, close: 56.48 },
                //{ time: '2018-10-30T00:00:00', open: 56.92, high: 58.80, low: 56.92, close: 58.18 },
                //{ time: '2018-10-31T00:00:00', open: 58.32, high: 58.32, low: 56.76, close: 57.09 },
                //{ time: '2018-11-01T00:00:00', open: 56.98, high: 57.28, low: 55.55, close: 56.05 },
                //{ time: '2018-11-02T00:00:00', open: 56.34, high: 57.08, low: 55.92, close: 56.63 },
                //{ time: '2018-11-05T00:00:00', open: 56.51, high: 57.45, low: 56.51, close: 57.21 },
                //{ time: '2018-11-06T00:00:00', open: 57.02, high: 57.35, low: 56.65, close: 57.21 },
                //{ time: '2018-11-07T00:00:00', open: 57.55, high: 57.78, low: 57.03, close: 57.65 },
                //{ time: '2018-11-08T00:00:00', open: 57.70, high: 58.44, low: 57.66, close: 58.27 },
                //{ time: '2018-11-09T00:00:00', open: 58.32, high: 59.20, low: 57.94, close: 58.46 },
                //{ time: '2018-11-12T00:00:00', open: 58.84, high: 59.40, low: 58.54, close: 58.72 },
                //{ time: '2018-11-13T00:00:00', open: 59.09, high: 59.14, low: 58.32, close: 58.66 },
                //{ time: '2018-11-14T00:00:00', open: 59.13, high: 59.32, low: 58.41, close: 58.94 },
                //{ time: '2018-11-15T00:00:00', open: 58.85, high: 59.09, low: 58.45, close: 59.08 },
                //{ time: '2018-11-16T00:00:00', open: 59.06, high: 60.39, low: 58.91, close: 60.21 },
                //{ time: '2018-11-19T00:00:00', open: 60.25, high: 61.32, low: 60.18, close: 60.62 },
                //{ time: '2018-11-20T00:00:00', open: 61.03, high: 61.58, low: 59.17, close: 59.46 },
                //{ time: '2018-11-21T00:00:00', open: 59.26, high: 59.90, low: 58.88, close: 59.16 },
                //{ time: '2018-11-23T00:00:00', open: 58.86, high: 59.00, low: 58.29, close: 58.64 },
                //{ time: '2018-11-26T00:00:00', open: 58.64, high: 59.51, low: 58.31, close: 59.17 },
                //{ time: '2018-11-27T00:00:00', open: 59.21, high: 60.70, low: 59.18, close: 60.65 },
                //{ time: '2018-11-28T00:00:00', open: 60.70, high: 60.73, low: 59.64, close: 60.06 },
                //{ time: '2018-11-29T00:00:00', open: 59.42, high: 59.79, low: 59.26, close: 59.45 },
                //{ time: '2018-11-30T00:00:00', open: 59.57, high: 60.37, low: 59.48, close: 60.30 },
                //{ time: '2018-12-03T00:00:00', open: 59.50, high: 59.75, low: 57.69, close: 58.16 },
                //{ time: '2018-12-04T00:00:00', open: 58.10, high: 59.40, low: 57.96, close: 58.09 },
                //{ time: '2018-12-06T00:00:00', open: 58.18, high: 58.64, low: 57.16, close: 58.08 },
                //{ time: '2018-12-07T00:00:00', open: 57.91, high: 58.43, low: 57.34, close: 57.68 },
                //{ time: '2018-12-10T00:00:00', open: 57.80, high: 58.37, low: 56.87, close: 58.27 },
                //{ time: '2018-12-11T00:00:00', open: 58.77, high: 59.40, low: 58.63, close: 58.85 },
                //{ time: '2018-12-12T00:00:00', open: 57.79, high: 58.19, low: 57.23, close: 57.25 },
                //{ time: '2018-12-13T00:00:00', open: 57.00, high: 57.50, low: 56.81, close: 57.09 },
                //{ time: '2018-12-14T00:00:00', open: 56.95, high: 57.50, low: 56.75, close: 57.08 },
                //{ time: '2018-12-17T00:00:00', open: 57.06, high: 57.31, low: 55.53, close: 55.95 },
                //{ time: '2018-12-18T00:00:00', open: 55.94, high: 56.69, low: 55.31, close: 55.65 },
                //{ time: '2018-12-19T00:00:00', open: 55.72, high: 56.92, low: 55.50, close: 55.86 },
                //{ time: '2018-12-20T00:00:00', open: 55.92, high: 56.01, low: 54.26, close: 55.07 },
                //{ time: '2018-12-21T00:00:00', open: 54.84, high: 56.53, low: 54.24, close: 54.92 },
                //{ time: '2018-12-24T00:00:00', open: 54.68, high: 55.04, low: 52.94, close: 53.05 },
                //{ time: '2018-12-26T00:00:00', open: 53.23, high: 54.47, low: 52.40, close: 54.44 },
                //{ time: '2018-12-27T00:00:00', open: 54.31, high: 55.17, low: 53.35, close: 55.15 },
                //{ time: '2018-12-28T00:00:00', open: 55.37, high: 55.86, low: 54.90, close: 55.27 },
                //{ time: '2018-12-31T00:00:00', open: 55.53, high: 56.23, low: 55.07, close: 56.22 },
                //{ time: '2019-01-02T00:00:00', open: 56.16, high: 56.16, low: 55.28, close: 56.02 },
                //{ time: '2019-01-03T00:00:00', open: 56.30, high: 56.99, low: 56.06, close: 56.22 },
                //{ time: '2019-01-04T00:00:00', open: 56.49, high: 56.89, low: 55.95, close: 56.36 },
                //{ time: '2019-01-07T00:00:00', open: 56.76, high: 57.26, low: 56.55, close: 56.72 },
                //{ time: '2019-01-08T00:00:00', open: 57.27, high: 58.69, low: 57.05, close: 58.38 },
                //{ time: '2019-01-09T00:00:00', open: 57.68, high: 57.72, low: 56.85, close: 57.05 },
                //{ time: '2019-01-10T00:00:00', open: 57.29, high: 57.70, low: 56.87, close: 57.60 },
                //{ time: '2019-01-11T00:00:00', open: 57.84, high: 58.26, low: 57.42, close: 58.02 },
                //{ time: '2019-01-14T00:00:00', open: 57.83, high: 58.15, low: 57.67, close: 58.03 },
                //{ time: '2019-01-15T00:00:00', open: 57.74, high: 58.29, low: 57.58, close: 58.10 },
                //{ time: '2019-01-16T00:00:00', open: 57.93, high: 57.93, low: 57.00, close: 57.08 },
                //{ time: '2019-01-17T00:00:00', open: 57.16, high: 57.40, low: 56.21, close: 56.83 },
                //{ time: '2019-01-18T00:00:00', open: 56.92, high: 57.47, low: 56.84, close: 57.09 },
                //{ time: '2019-01-22T00:00:00', open: 57.23, high: 57.39, low: 56.40, close: 56.99 },
                //{ time: '2019-01-23T00:00:00', open: 56.98, high: 57.87, low: 56.93, close: 57.76 },
                //{ time: '2019-01-24T00:00:00', open: 57.61, high: 57.65, low: 56.50, close: 57.07 },
                //{ time: '2019-01-25T00:00:00', open: 57.18, high: 57.47, low: 56.23, close: 56.40 },
                //{ time: '2019-01-28T00:00:00', open: 56.12, high: 56.22, low: 54.80, close: 55.07 },
                //{ time: '2019-01-29T00:00:00', open: 53.62, high: 54.30, low: 52.97, close: 53.28 },
                //{ time: '2019-01-30T00:00:00', open: 53.10, high: 54.02, low: 52.28, close: 54.00 },
                //{ time: '2019-01-31T00:00:00', open: 54.05, high: 55.19, low: 53.53, close: 55.06 },
                //{ time: '2019-02-01T00:00:00', open: 55.21, high: 55.30, low: 54.47, close: 54.55 },
                //{ time: '2019-02-04T00:00:00', open: 54.60, high: 54.69, low: 53.67, close: 54.04 },
                //{ time: '2019-02-05T00:00:00', open: 54.10, high: 54.34, low: 53.61, close: 54.14 },
                //{ time: '2019-02-06T00:00:00', open: 54.11, high: 54.37, low: 53.68, close: 53.79 },
                //{ time: '2019-02-07T00:00:00', open: 53.61, high: 53.73, low: 53.02, close: 53.57 },
                //{ time: '2019-02-08T00:00:00', open: 53.36, high: 53.96, low: 53.30, close: 53.95 },
                //{ time: '2019-02-11T00:00:00', open: 54.13, high: 54.37, low: 53.86, close: 54.05 },
                //{ time: '2019-02-12T00:00:00', open: 54.45, high: 54.77, low: 54.19, close: 54.42 },
                //{ time: '2019-02-13T00:00:00', open: 54.35, high: 54.77, low: 54.28, close: 54.48 },
                //{ time: '2019-02-14T00:00:00', open: 54.38, high: 54.52, low: 53.95, close: 54.03 },
                //{ time: '2019-02-15T00:00:00', open: 54.48, high: 55.19, low: 54.32, close: 55.16 },
                //{ time: '2019-02-19T00:00:00', open: 55.06, high: 55.66, low: 54.82, close: 55.44 },
                //{ time: '2019-02-20T00:00:00', open: 55.37, high: 55.91, low: 55.24, close: 55.76 },
                //{ time: '2019-02-21T00:00:00', open: 55.55, high: 56.72, low: 55.46, close: 56.15 },
                //{ time: '2019-02-22T00:00:00', open: 56.43, high: 57.13, low: 56.40, close: 56.92 },
                //{ time: '2019-02-25T00:00:00', open: 57.00, high: 57.27, low: 56.55, close: 56.78 },
                //{ time: '2019-02-26T00:00:00', open: 56.82, high: 57.09, low: 56.46, close: 56.64 },
                //{ time: '2019-02-27T00:00:00', open: 56.55, high: 56.73, low: 56.35, close: 56.72 },
                //{ time: '2019-02-28T00:00:00', open: 56.74, high: 57.61, low: 56.72, close: 56.92 },
                //{ time: '2019-03-01T00:00:00', open: 57.02, high: 57.15, low: 56.35, close: 56.96 },
                //{ time: '2019-03-04T00:00:00', open: 57.15, high: 57.34, low: 55.66, close: 56.24 },
                //{ time: '2019-03-05T00:00:00', open: 56.09, high: 56.17, low: 55.51, close: 56.08 },
                //{ time: '2019-03-06T00:00:00', open: 56.19, high: 56.42, low: 55.45, close: 55.68 },
                //{ time: '2019-03-07T00:00:00', open: 55.76, high: 56.40, low: 55.72, close: 56.30 },
                //{ time: '2019-03-08T00:00:00', open: 56.36, high: 56.68, low: 56.00, close: 56.53 },
                //{ time: '2019-03-11T00:00:00', open: 56.76, high: 57.62, low: 56.75, close: 57.58 },
                //{ time: '2019-03-12T00:00:00', open: 57.63, high: 58.11, low: 57.36, close: 57.43 },
                //{ time: '2019-03-13T00:00:00', open: 57.37, high: 57.74, low: 57.34, close: 57.66 },
                //{ time: '2019-03-14T00:00:00', open: 57.71, high: 58.09, low: 57.51, close: 57.95 },
                //{ time: '2019-03-15T00:00:00', open: 58.04, high: 58.51, low: 57.93, close: 58.39 },
                //{ time: '2019-03-18T00:00:00', open: 58.27, high: 58.32, low: 57.56, close: 58.07 },
                //{ time: '2019-03-19T00:00:00', open: 58.10, high: 58.20, low: 57.31, close: 57.50 },
                //{ time: '2019-03-20T00:00:00', open: 57.51, high: 58.05, low: 57.11, close: 57.67 },
                //{ time: '2019-03-21T00:00:00', open: 57.58, high: 58.49, low: 57.57, close: 58.29 },
                //{ time: '2019-03-22T00:00:00', open: 58.16, high: 60.00, low: 58.13, close: 59.76 },
                //{ time: '2019-03-25T00:00:00', open: 59.63, high: 60.19, low: 59.53, close: 60.08 },
                //{ time: '2019-03-26T00:00:00', open: 60.30, high: 60.69, low: 60.17, close: 60.63 },
                //{ time: '2019-03-27T00:00:00', open: 60.56, high: 61.19, low: 60.48, close: 60.88 },
                //{ time: '2019-03-28T00:00:00', open: 60.88, high: 60.89, low: 58.44, close: 59.08 },
                //{ time: '2019-03-29T00:00:00', open: 59.20, high: 59.27, low: 58.32, close: 59.13 },
                //{ time: '2019-04-01T00:00:00', open: 59.39, high: 59.41, low: 58.79, close: 59.09 },
                //{ time: '2019-04-02T00:00:00', open: 59.22, high: 59.23, low: 58.34, close: 58.53 },
                //{ time: '2019-04-03T00:00:00', open: 58.78, high: 59.07, low: 58.41, close: 58.87 },
                //{ time: '2019-04-04T00:00:00', open: 58.84, high: 59.10, low: 58.77, close: 58.99 },
                //{ time: '2019-04-05T00:00:00', open: 59.02, high: 59.09, low: 58.82, close: 59.09 },
                //{ time: '2019-04-08T00:00:00', open: 59.02, high: 59.13, low: 58.72, close: 59.13 },
                //{ time: '2019-04-09T00:00:00', open: 58.37, high: 58.56, low: 58.04, close: 58.40 },
                //{ time: '2019-04-10T00:00:00', open: 58.40, high: 58.70, low: 58.36, close: 58.61 },
                //{ time: '2019-04-11T00:00:00', open: 58.65, high: 58.73, low: 58.20, close: 58.56 },
                //{ time: '2019-04-12T00:00:00', open: 58.75, high: 58.79, low: 58.52, close: 58.74 },
                //{ time: '2019-04-15T00:00:00', open: 58.91, high: 58.95, low: 58.59, close: 58.71 },
                //{ time: '2019-04-16T00:00:00', open: 58.79, high: 58.98, low: 58.66, close: 58.79 },
                //{ time: '2019-04-17T00:00:00', open: 58.40, high: 58.46, low: 57.64, close: 57.78 },
                //{ time: '2019-04-18T00:00:00', open: 57.51, high: 58.20, low: 57.28, close: 58.04 },
                //{ time: '2019-04-22T00:00:00', open: 58.14, high: 58.49, low: 57.89, close: 58.37 },
                //{ time: '2019-04-23T00:00:00', open: 57.62, high: 57.72, low: 56.30, close: 57.15 },
                //{ time: '2019-04-24T00:00:00', open: 57.34, high: 57.57, low: 56.73, close: 57.08 },
                //{ time: '2019-04-25T00:00:00', open: 56.82, high: 56.90, low: 55.75, close: 55.85 },
                //{ time: '2019-04-26T00:00:00', open: 56.06, high: 56.81, low: 55.83, close: 56.58 },
                //{ time: '2019-04-29T00:00:00', open: 56.75, high: 57.17, low: 56.71, close: 56.84 },
                //{ time: '2019-04-30T00:00:00', open: 56.99, high: 57.45, low: 56.76, close: 57.19 },
                //{ time: '2019-05-01T00:00:00', open: 57.23, high: 57.30, low: 56.52, close: 56.52 },
                //{ time: '2019-05-02T00:00:00', open: 56.81, high: 58.23, low: 56.68, close: 56.99 },
                //{ time: '2019-05-03T00:00:00', open: 57.15, high: 57.36, low: 56.87, close: 57.24 },
                //{ time: '2019-05-06T00:00:00', open: 56.83, high: 57.09, low: 56.74, close: 56.91 },
                //{ time: '2019-05-07T00:00:00', open: 56.69, high: 56.81, low: 56.33, close: 56.63 },
                //{ time: '2019-05-08T00:00:00', open: 56.66, high: 56.70, low: 56.25, close: 56.38 },
                //{ time: '2019-05-09T00:00:00', open: 56.12, high: 56.56, low: 55.93, close: 56.48 },
                //{ time: '2019-05-10T00:00:00', open: 56.49, high: 57.04, low: 56.26, close: 56.91 },
                //{ time: '2019-05-13T00:00:00', open: 56.72, high: 57.34, low: 56.66, close: 56.75 },
                //{ time: '2019-05-14T00:00:00', open: 56.76, high: 57.19, low: 56.50, close: 56.55 },
                //{ time: '2019-05-15T00:00:00', open: 56.51, high: 56.84, low: 56.17, close: 56.81 },
                //{ time: '2019-05-16T00:00:00', open: 57.00, high: 57.80, low: 56.82, close: 57.38 },
                //{ time: '2019-05-17T00:00:00', open: 57.06, high: 58.48, low: 57.01, close: 58.09 },
                //{ time: '2019-05-20T00:00:00', open: 59.15, high: 60.54, low: 58.00, close: 59.01 },
                //{ time: '2019-05-21T00:00:00', open: 59.10, high: 59.63, low: 58.76, close: 59.50 },
                //{ time: '2019-05-22T00:00:00', open: 59.09, high: 59.37, low: 58.96, close: 59.25 },
                //{ time: '2019-05-23T00:00:00', open: 59.00, high: 59.27, low: 58.54, close: 58.87 },
                //{ time: '2019-05-24T00:00:00', open: 59.07, high: 59.36, low: 58.67, close: 59.32 },
                //{ time: '2019-05-28T00:00:00', open: 59.21, high: 59.66, low: 59.02, close: 59.57 },
            ];

            candleStickData.setData(data);

            candleStickData.applyOptions({
                priceFormat: {
                    type: 'price',
                    precision: 5,
                },
            });


            window.addEventListener("resize", myFunction);

            window.onload = function () {
                console.log($("input#Ticker").val());
                getHistoricalData();
            };

            function myFunction() {
                var container = $("div#chartPlatform")[0];
                console.log("w : " + container.offsetWidth);
                console.log("h : " + container.offsetHeight);
                chart.resize(container.offsetWidth, container.offsetHeight);
            }

            //am4core.ready(function () {

            //    // Themes begin
            //    am4core.useTheme(am4themes_animated);
            //    // Themes end

            //    chartTrading = am4core.create("chartPlatform", am4charts.XYChart);
            //    chartTrading.paddingRight = 20;
            //    chartTrading.startDuration = 0;
            //    chartTrading.dateFormatter.inputDateFormat = "yyyy-MM-ddTHH:mm:ss";

            //    var dateAxis = chartTrading.xAxes.push(new am4charts.DateAxis());
            //    dateAxis.renderer.grid.template.location = 0;

            //    var valueAxis = chartTrading.yAxes.push(new am4charts.ValueAxis());
            //    valueAxis.tooltip.disabled = true;

            //    var series = chartTrading.series.push(new am4charts.CandlestickSeries());
            //    series.dataFields.dateX = "date";
            //    series.dataFields.valueY = "close";
            //    series.dataFields.openValueY = "open";
            //    series.dataFields.lowValueY = "low";
            //    series.dataFields.highValueY = "high";
            //    series.tooltipText = "Open:${openValueY.value}\nLow:${lowValueY.value}\nHigh:${highValueY.value}\nClose:${valueY.value}";

            //    // important!
            //    // candlestick series colors are set in states. 
            //    // series.riseFromOpenState.properties.fill = am4core.color("#00ff00");
            //    // series.dropFromOpenState.properties.fill = am4core.color("#FF0000");
            //    // series.riseFromOpenState.properties.stroke = am4core.color("#00ff00");
            //    // series.dropFromOpenState.properties.stroke = am4core.color("#FF0000");

            //    series.riseFromPreviousState.properties.fillOpacity = 1;
            //    series.dropFromPreviousState.properties.fillOpacity = 0;

            //    chartTrading.cursor = new am4charts.XYCursor();

            //    // a separate series for scrollbar
            //    var lineSeries = chartTrading.series.push(new am4charts.LineSeries());
            //    lineSeries.dataFields.dateX = "date";
            //    lineSeries.dataFields.valueY = "close";
            //    // need to set on default state, as initially series is "show"
            //    lineSeries.defaultState.properties.visible = false;

            //    // hide from legend too (in case there is one)
            //    lineSeries.hiddenInLegend = true;
            //    lineSeries.fillOpacity = 0.5;
            //    lineSeries.strokeOpacity = 0.5;

            //    var scrollbarX = new am4charts.XYChartScrollbar();
            //    scrollbarX.series.push(lineSeries);
            //    chartTrading.scrollbarX = scrollbarX;

            //    //chartTrading.data = [{
            //    //    "date": "2011-08-01",
            //    //    "open": "136.65",
            //    //    "high": "136.96",
            //    //    "low": "134.15",
            //    //    "close": "136.49"
            //    //}, {
            //    //    "date": "2011-08-02",
            //    //    "open": "135.26",
            //    //    "high": "135.95",
            //    //    "low": "131.50",
            //    //    "close": "131.85"
            //    //}, {
            //    //    "date": "2011-08-05",
            //    //    "open": "132.90",
            //    //    "high": "135.27",
            //    //    "low": "128.30",
            //    //    "close": "135.25"
            //    //}, {
            //    //    "date": "2011-08-06",
            //    //    "open": "134.94",
            //    //    "high": "137.24",
            //    //    "low": "132.63",
            //    //    "close": "135.03"
            //    //}, {
            //    //    "date": "2011-08-07",
            //    //    "open": "136.76",
            //    //    "high": "136.86",
            //    //    "low": "132.00",
            //    //    "close": "134.01"
            //    //}, {
            //    //    "date": "2011-08-08",
            //    //    "open": "131.11",
            //    //    "high": "133.00",
            //    //    "low": "125.09",
            //    //    "close": "126.39"
            //    //}, {
            //    //    "date": "2011-08-09",
            //    //    "open": "123.12",
            //    //    "high": "127.75",
            //    //    "low": "120.30",
            //    //    "close": "125.00"
            //    //}, {
            //    //    "date": "2011-08-12",
            //    //    "open": "128.32",
            //    //    "high": "129.35",
            //    //    "low": "126.50",
            //    //    "close": "127.79"
            //    //}, {
            //    //    "date": "2011-08-13",
            //    //    "open": "128.29",
            //    //    "high": "128.30",
            //    //    "low": "123.71",
            //    //    "close": "124.03"
            //    //}, {
            //    //    "date": "2011-08-14",
            //    //    "open": "122.74",
            //    //    "high": "124.86",
            //    //    "low": "119.65",
            //    //    "close": "119.90"
            //    //}, {
            //    //    "date": "2011-08-15",
            //    //    "open": "117.01",
            //    //    "high": "118.50",
            //    //    "low": "111.62",
            //    //    "close": "117.05"
            //    //}, {
            //    //    "date": "2011-08-16",
            //    //    "open": "122.01",
            //    //    "high": "123.50",
            //    //    "low": "119.82",
            //    //    "close": "122.06"
            //    //}, {
            //    //    "date": "2011-08-19",
            //    //    "open": "123.96",
            //    //    "high": "124.50",
            //    //    "low": "120.50",
            //    //    "close": "122.22"
            //    //}, {
            //    //    "date": "2011-08-20",
            //    //    "open": "122.21",
            //    //    "high": "128.96",
            //    //    "low": "121.00",
            //    //    "close": "127.57"
            //    //}, {
            //    //    "date": "2011-08-21",
            //    //    "open": "131.22",
            //    //    "high": "132.75",
            //    //    "low": "130.33",
            //    //    "close": "132.51"
            //    //}, {
            //    //    "date": "2011-08-22",
            //    //    "open": "133.09",
            //    //    "high": "133.34",
            //    //    "low": "129.76",
            //    //    "close": "131.07"
            //    //}, {
            //    //    "date": "2011-08-23",
            //    //    "open": "130.53",
            //    //    "high": "135.37",
            //    //    "low": "129.81",
            //    //    "close": "135.30"
            //    //}, {
            //    //    "date": "2011-08-26",
            //    //    "open": "133.39",
            //    //    "high": "134.66",
            //    //    "low": "132.10",
            //    //    "close": "132.25"
            //    //}, {
            //    //    "date": "2011-08-27",
            //    //    "open": "130.99",
            //    //    "high": "132.41",
            //    //    "low": "126.63",
            //    //    "close": "126.82"
            //    //}, {
            //    //    "date": "2011-08-28",
            //    //    "open": "129.88",
            //    //    "high": "134.18",
            //    //    "low": "129.54",
            //    //    "close": "134.08"
            //    //}, {
            //    //    "date": "2011-08-29",
            //    //    "open": "132.67",
            //    //    "high": "138.25",
            //    //    "low": "132.30",
            //    //    "close": "136.25"
            //    //}, {
            //    //    "date": "2011-08-30",
            //    //    "open": "139.49",
            //    //    "high": "139.65",
            //    //    "low": "137.41",
            //    //    "close": "138.48"
            //    //}, {
            //    //    "date": "2011-09-03",
            //    //    "open": "139.94",
            //    //    "high": "145.73",
            //    //    "low": "139.84",
            //    //    "close": "144.16"
            //    //}, {
            //    //    "date": "2011-09-04",
            //    //    "open": "144.97",
            //    //    "high": "145.84",
            //    //    "low": "136.10",
            //    //    "close": "136.76"
            //    //}, {
            //    //    "date": "2011-09-05",
            //    //    "open": "135.56",
            //    //    "high": "137.57",
            //    //    "low": "132.71",
            //    //    "close": "135.01"
            //    //}, {
            //    //    "date": "2011-09-06",
            //    //    "open": "132.01",
            //    //    "high": "132.30",
            //    //    "low": "130.00",
            //    //    "close": "131.77"
            //    //}, {
            //    //    "date": "2011-09-09",
            //    //    "open": "136.99",
            //    //    "high": "138.04",
            //    //    "low": "133.95",
            //    //    "close": "136.71"
            //    //}, {
            //    //    "date": "2011-09-10",
            //    //    "open": "137.90",
            //    //    "high": "138.30",
            //    //    "low": "133.75",
            //    //    "close": "135.49"
            //    //}, {
            //    //    "date": "2011-09-11",
            //    //    "open": "135.99",
            //    //    "high": "139.40",
            //    //    "low": "135.75",
            //    //    "close": "136.85"
            //    //}, {
            //    //    "date": "2011-09-12",
            //    //    "open": "138.83",
            //    //    "high": "139.00",
            //    //    "low": "136.65",
            //    //    "close": "137.20"
            //    //}, {
            //    //    "date": "2011-09-13",
            //    //    "open": "136.57",
            //    //    "high": "138.98",
            //    //    "low": "136.20",
            //    //    "close": "138.81"
            //    //}, {
            //    //    "date": "2011-09-16",
            //    //    "open": "138.99",
            //    //    "high": "140.59",
            //    //    "low": "137.60",
            //    //    "close": "138.41"
            //    //}, {
            //    //    "date": "2011-09-17",
            //    //    "open": "139.06",
            //    //    "high": "142.85",
            //    //    "low": "137.83",
            //    //    "close": "140.92"
            //    //}, {
            //    //    "date": "2011-09-18",
            //    //    "open": "143.02",
            //    //    "high": "143.16",
            //    //    "low": "139.40",
            //    //    "close": "140.77"
            //    //}, {
            //    //    "date": "2011-09-19",
            //    //    "open": "140.15",
            //    //    "high": "141.79",
            //    //    "low": "139.32",
            //    //    "close": "140.31"
            //    //}, {
            //    //    "date": "2011-09-20",
            //    //    "open": "141.14",
            //    //    "high": "144.65",
            //    //    "low": "140.31",
            //    //    "close": "144.15"
            //    //}, {
            //    //    "date": "2011-09-23",
            //    //    "open": "146.73",
            //    //    "high": "149.85",
            //    //    "low": "146.65",
            //    //    "close": "148.28"
            //    //}, {
            //    //    "date": "2011-09-24",
            //    //    "open": "146.84",
            //    //    "high": "153.22",
            //    //    "low": "146.82",
            //    //    "close": "153.18"
            //    //}, {
            //    //    "date": "2011-09-25",
            //    //    "open": "154.47",
            //    //    "high": "155.00",
            //    //    "low": "151.25",
            //    //    "close": "152.77"
            //    //}, {
            //    //    "date": "2011-09-26",
            //    //    "open": "153.77",
            //    //    "high": "154.52",
            //    //    "low": "152.32",
            //    //    "close": "154.50"
            //    //}, {
            //    //    "date": "2011-09-27",
            //    //    "open": "153.44",
            //    //    "high": "154.60",
            //    //    "low": "152.75",
            //    //    "close": "153.47"
            //    //}, {
            //    //    "date": "2011-09-30",
            //    //    "open": "154.63",
            //    //    "high": "157.41",
            //    //    "low": "152.93",
            //    //    "close": "156.34"
            //    //}, {
            //    //    "date": "2011-10-01",
            //    //    "open": "156.55",
            //    //    "high": "158.59",
            //    //    "low": "155.89",
            //    //    "close": "158.45"
            //    //}, {
            //    //    "date": "2011-10-02",
            //    //    "open": "157.78",
            //    //    "high": "159.18",
            //    //    "low": "157.01",
            //    //    "close": "157.92"
            //    //}, {
            //    //    "date": "2011-10-03",
            //    //    "open": "158.00",
            //    //    "high": "158.08",
            //    //    "low": "153.50",
            //    //    "close": "156.24"
            //    //}, {
            //    //    "date": "2011-10-04",
            //    //    "open": "158.37",
            //    //    "high": "161.58",
            //    //    "low": "157.70",
            //    //    "close": "161.45"
            //    //}, {
            //    //    "date": "2011-10-07",
            //    //    "open": "163.49",
            //    //    "high": "167.91",
            //    //    "low": "162.97",
            //    //    "close": "167.91"
            //    //}, {
            //    //    "date": "2011-10-08",
            //    //    "open": "170.20",
            //    //    "high": "171.11",
            //    //    "low": "166.68",
            //    //    "close": "167.86"
            //    //}, {
            //    //    "date": "2011-10-09",
            //    //    "open": "167.55",
            //    //    "high": "167.88",
            //    //    "low": "165.60",
            //    //    "close": "166.79"
            //    //}, {
            //    //    "date": "2011-10-10",
            //    //    "open": "169.49",
            //    //    "high": "171.88",
            //    //    "low": "153.21",
            //    //    "close": "162.23"
            //    //}, {
            //    //    "date": "2011-10-11",
            //    //    "open": "163.01",
            //    //    "high": "167.28",
            //    //    "low": "161.80",
            //    //    "close": "167.25"
            //    //}, {
            //    //    "date": "2011-10-14",
            //    //    "open": "167.98",
            //    //    "high": "169.57",
            //    //    "low": "163.50",
            //    //    "close": "166.98"
            //    //}, {
            //    //    "date": "2011-10-15",
            //    //    "open": "165.54",
            //    //    "high": "170.18",
            //    //    "low": "165.15",
            //    //    "close": "169.58"
            //    //}, {
            //    //    "date": "2011-10-16",
            //    //    "open": "172.69",
            //    //    "high": "173.04",
            //    //    "low": "169.18",
            //    //    "close": "172.75"
            //    //}];

            //}); // end am4core.ready()
        };




        var subscribeToTicker = function (hub) {
            var ticker = $("input#Ticker").val();
            hub.server.onSubscribe(ticker).done(function (result) {
                if (result)
                    toastr.success('Success', "Suscribed to " + ticker);
                else
                    toastr.error('Error', "Cannot subscribe to " + ticker);
            });
        };

        var updateBidAskButton = function (bid, ask) {
            var previous = $("span#bidInt").text() * 1 + $("span#bidFigure").text() / 10000 + $("span#bidPip").text() / 100000;
            $("span#bidInt").text(bid.toString().substring(0, 4));
            $("span#bidFigure").text(bid.toString().substring(4, 6));
            $("span#bidPip").text(bid.toString().substring(6, 7));
            var color = "white";
            if (bid < previous) {
                color = "red";
            }
            else if (bid > previous) {
                color = "green";
            }

            $("div#sellButton").css("background", "white");
            setTimeout(function () {
                $("div#sellButton").css("background", color);
            }, 500);

            previous = $("span#askInt").text() * 1 + $("span#askFigure").text() / 10000 + $("span#askPip").text() / 100000;
            $("span#askInt").text(ask.toString().substring(0, 4));
            $("span#askFigure").text(ask.toString().substring(4, 6));
            $("span#askPip").text(ask.toString().substring(6, 7));
            if (ask < previous) {
                color = "red";
            }
            else if (ask > previous) {
                color = "green";
            }

            $("div#buyButton").css("background", "white");
            setTimeout(function () {
                $("div#buyButton").css("background", color);
            }, 500);
        };



        var initPlatformHub = function () {
            var platformHub = $.connection.platformHub;
            // Create a function that the hub can call to broadcast messages.
            platformHub.client.onReceivedQuote = function (asset, bid, ask) {
                // toastr.info(asset, "Bid: " + bid + " Ask: " + ask);
                updateBidAskButton(bid, ask);
            };

            platformHub.client.onUpdatePnL = function (transaction) {
                updateTransactionAllByIdPnl(tableActualTransaction, transaction.Id, transaction.PnL);
            };


            platformHub.client.onReceivedDetailedQuote = function (symbol, bid, ask, dateTime, open, high, low, close, volume) {
                // toastr.info(symbol, "Bid: " + bid + " Ask: " + ask + " Date: " + dateTime + " Open: " + open);
                updateBidAskButton(bid, ask);
                addOrUpdateOHCL(open, high, low, close, dateTime);
            };
            $.connection.hub.start()
                .done(function () {
                    toastr.success('Success', "You're now connected");
                    subscribeToTicker(platformHub);
                })
                .fail(function () { toastr.error('Error', "Enable to connect"); });

            $.connection.hub.error(function (error) {
                console.log('SignalR error: ' + error)
            });
        };

        var addOrUpdateOHCL = function (open, high, low, close, date) {
            var temp = chart;
            if (date === undefined)
                return;
            var index = data.length - 1;
            if (index === -1) {
                addOHCL(open, high, low, close, date);
                return;
            }
            var lastdata = data[index];
            if (lastdata.time !== date) {
                addOHCL(open, high, low, close, date);
            }
            else {
                updateLastOHCL(open, high, low, close);
            }
        };

        var updateLastOHCL = function (open, high, low, close) {
            var index = candleStickData.length - 1;
            data[index].close = close;
            data[index].open = open;
            data[index].high = high;
            data[index].low = low;
            candleStickData.update(data[index]);
        };
        var addOHCL = function (open, high, low, close, date) {
            var newBar = {
                open: open,
                high: high,
                low: low,
                close: close,
                time: date,
            };
            candleStickData.update(newBar);
        };

        var getHistoricalData = function () {
            $.ajax({
                type: "GET",
                url: $("div#chartPlatform").attr("data-urlHistorical"),
                //data: { json: JSON.stringify($("input#Ticker").val()) },
                datatType: "json",
                data: { productName: $("input#Ticker").val() },
                success: function (data) {
                    if (data.isOk) {
                        toastr.success('Success', data.message);
                        candleStickData.setData(data.data);
                    }
                    else {
                        toastr.error('Error', data.message);
                    }
                },
                error: function (error) {
                    toastr.error('Error', error);
                }
            }).done(function () {

            });
        };


        var generateModelOpenDeal = function (ticker, volume, price, slippage) {
            var model = {
                Ticker: ticker,
                Quantity: volume,
                Price: price,
                Slippage: slippage
            };
            return model;
        };
        var generateModelCloseDeal = function (endPrice, dealId, productName, way) {
            var model = {
                EndPrice: endPrice,
                DealId: dealId,
                Status: "",
                ProductName: productName
            };
            return model;
        };

        var openDeal = function (way) {
            var volume = $("input#volumeDeal").val();
            if (volume === 0) {
                toastr.error("Volume is equal to 0", "Deal not created");
                return;
            }
            if (way === "Sell")
                volume = volume * -1;
            else
                volume = volume * 1;
            var ticker = $("input#Ticker").val();
            if ($.trim(ticker) === "") {
                toastr.error("Ticker not found", "Deal not created");
                return;
            }
            var model = generateModelOpenDeal(ticker, volume, 12.98, 0.1);
            $.ajax({
                type: "POST",
                url: $("table#actualTransactionTable").attr("data-openDeal"),
                data: { json: JSON.stringify(model) },
                datatType: "json",
                //data: { number: number },
                success: function (data) {
                    if (data.isOk) {
                        toastr.success('Success', data.message);
                        addTransactiontToActualTab(data.transaction);
                        addDealLine(data.transaction.StartPrice, data.transaction.Id);
                    }
                    else {
                        toastr.error('Error', data.message);
                    }
                },
                error: function (error) {
                    toastr.error('Error', error);
                }
            }).done(function () {

            });
        };

        var addRowTransctionToTable = function () {

        };


        var initEvent = function () {
         
            // On Click Sell button
            $("div#sellButton").on("click", function () {
                openDeal("Sell");
            });
            // On Click Buy Button 
            $("div#buyButton").on("click", function () {
                openDeal("Buy");
            });


            $("body").off("click", "button.buttonCloseDeal").on("click", "button.buttonCloseDeal", function () {
                var idDeal = $(this).attr("deal-id");
                if ($.trim(idDeal) === "") {
                    toastr.error("Unable to get id");
                    return;
                }
                toastr.info("Deal " + idDeal + " sent for closing");
                closeDeal(idDeal);
                // make ajax call

            });

        };

        var searchIndexInDataTableById = function (dataTableName, id) {
            var rowIndexes = [];
            var intString = parseInt(id);
            dataTableName.rows(function (idx, data, node) {
                if (data.id === intString) {
                    rowIndexes.push(idx);
                }
            });
            if (rowIndexes.length === 0)
                return -1;
            else
                return rowIndexes[0];
        };

        var closeDeal = function (idDeal) {
            var productName = $("input#Ticker").val();
            var model = generateModelCloseDeal(34.2, idDeal, productName);
            $.ajax({
                type: "POST",
                url: $("table#actualTransactionTable").attr("data-closeDeal"),
                data: { json: JSON.stringify(model) },
                dataType: "json",
                //data: { number: number },
                success: function (data) {
                    if (data.isOk) {
                        toastr.success('Success', data.message);
                        updateTransactionAllById(tableActualTransaction, idDeal, data.endDate, data.newEndPrice, data.newStatus);
                    }
                    else {
                        toastr.error('Error', data.message);
                    }
                },
                error: function (error) {
                    toastr.error('Error', error);
                }
            }).done(function () {

            });
        };
        var updateTransactionAllById = function (dataTableName, id, newEndDate, newEndPrice, newStatus) {
            var index = searchIndexInDataTableById(dataTableName, id);
            if (index === -1) {
                toastr.error("Unable to get deal id", "Update Transaction");
                return;
            }
            var data = dataTableName.row(index).data();
            data.status = newStatus;
            data.endDate = newEndDate;
            data.endPrice = newEndPrice;
            dataTableName.row(index).data(data).raw();
        };

        var updateTransactionAllByIdPnl = function (dataTableName, id, pnl) {
            var index = searchIndexInDataTableById(dataTableName, id);
            if (index === -1) {
                toastr.error("Unable to get deal id", "Update Transaction");
                return;
            }
            var data = dataTableName.row(index).data();
            data.pnl = pnl;
            dataTableName.row(index).data(data).draw();
        };


        var renderStatus = function (data, type, row) {
            if (data === 'Opened') {
                return '<label class="label label-success">Opened</label>';
            }
            if (data === 'Cancelled') {
                return '<label class="label label-warning">Cancelled</label>';
            }
            if (data === 'Closed') {
                return '<label class="label label-danger">Closed</label>';
            }
            return data;
        };

        var renderAction = function (data, type, row) {
            if ($.trim(data) !== "") {
                return '<button class="btn btn-danger btn-icon buttonTransaction buttonCloseDeal" deal-id="' + data + '"><i class="icofont icofont-ui-close"></i></button>';
            }
            return data;
        };


        var addTransactiontToActualTab = function (transac) {
            tableActualTransaction.row.add(
                {
                    "id": transac.Id,
                    "name": transac.Product.Name,
                    "startDate": transac.StartDate,
                    "endDate": transac.EndDate,
                    "startPrice": transac.StartPrice,
                    "endPrice": transac.EndPrice,
                    "pnl": transac.PnL,
                    "way": transac.Way,
                    "status": transac.Statuts,
                    "id": transac.Id
                }).draw();
        };



        var initDataTable = function () {
            $.fn.dataTable.ext.errMode = 'throw';

            tableActualTransaction = $('#actualTransactionTable').DataTable({
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                ajax: {
                    url: $("#actualTransactionTable").attr("data-url"),
                    type: 'POST',
                    // data: { ticker: $("input#Ticker").val() },
                    dataSrc: 'data'
                },
                responsive: true,
                columns: [
                    { "data": "id" },
                    { "data": "name" },
                    { "data": "startDate" },
                    { "data": "endDate" },
                    { "data": "startPrice" },
                    { "data": "endPrice" },
                    { "data": "pnl" },
                    { "data": "way" },
                    { "data": "status", render: renderStatus },
                    { "data": "id", render: renderAction }
                ]
            });

            tableLogUser = $('#LogTransactionTable').DataTable({
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                ajax: {
                    url: $("#LogTransactionTable").attr("data-mapping"),
                    type: 'GET',
                    data: { productName: $("input#Ticker").val() },
                    dataSrc: 'data'
                },
                responsive: true,
                columns: [
                    { "data": "hour" },
                    { "data": "type" },
                    { "data": "message" }
                ]
            });
        };

        var addDealLine = function (price, dealId) {
            var valueAxis = chartTrading.yAxes.values[0];
            if (valueAxis === undefined || valueAxis === null) {
                valueAxis = chartTrading.yAxes.push(new am4charts.ValueAxis());
            }
            var range = valueAxis.axisRanges.create();
            range.value = price;
            range.grid.stroke = am4core.color("#396478");
            range.grid.strokeWidth = 2;
            range.grid.strokeOpacity = 1;
            range.label.inside = true;
            range.label.text = "#" + dealId;
            range.label.fill = range.grid.stroke;
            range.label.align = "right";
            range.label.verticalCenter = "bottom";
        };



    };

    // ================
    var widget = new PlatformTrading();
    widget.initClass();

});

