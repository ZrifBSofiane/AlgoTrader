$(function () {

    var FxWatchList = function () {


        this.initClass = function () {
            initEvent();
            initDataTable();

            initPlatformHub();
          //  initChart();
        };


        //var initChart = function () {
        //    var container = $("div#chartPlatform")[0];
        //    chart = LightweightCharts.createChart(container, {
        //        width: container.offsetWidth,
        //        height: container.offsetHeight,
        //        crosshair: {
        //            mode: LightweightCharts.CrosshairMode.Normal,
        //        },
        //        timeScale: {
        //            timeVisible: true,
        //            secondsVisible: true,
        //        },
        //        localization: {
        //            dateFormat: 'yyyy-MM-dd',
        //        },
        //    });
        //    candleStickData = chart.addCandlestickSeries();

        //    data = [

        //    ];

        //    candleStickData.setData(data);

        //    candleStickData.applyOptions({
        //        priceFormat: {
        //            type: 'price',
        //            precision: 5,
        //        },
        //    });


        //    window.addEventListener("resize", myFunction);

        //    window.onload = function () {
        //        console.log($("input#Ticker").val());
        //        getHistoricalData();
        //    };

        //    function myFunction() {
        //        var container = $("div#chartPlatform")[0];
        //        console.log("w : " + container.offsetWidth);
        //        console.log("h : " + container.offsetHeight);
        //        chart.resize(container.offsetWidth, container.offsetHeight);
        //    }

        //    //am4core.ready(function () {

        //    //    // Themes begin
        //    //    am4core.useTheme(am4themes_animated);
        //    //    // Themes end

        //    //    chartTrading = am4core.create("chartPlatform", am4charts.XYChart);
        //    //    chartTrading.paddingRight = 20;
        //    //    chartTrading.startDuration = 0;
        //    //    chartTrading.dateFormatter.inputDateFormat = "yyyy-MM-ddTHH:mm:ss";

        //    //    var dateAxis = chartTrading.xAxes.push(new am4charts.DateAxis());
        //    //    dateAxis.renderer.grid.template.location = 0;

        //    //    var valueAxis = chartTrading.yAxes.push(new am4charts.ValueAxis());
        //    //    valueAxis.tooltip.disabled = true;

        //    //    var series = chartTrading.series.push(new am4charts.CandlestickSeries());
        //    //    series.dataFields.dateX = "date";
        //    //    series.dataFields.valueY = "close";
        //    //    series.dataFields.openValueY = "open";
        //    //    series.dataFields.lowValueY = "low";
        //    //    series.dataFields.highValueY = "high";
        //    //    series.tooltipText = "Open:${openValueY.value}\nLow:${lowValueY.value}\nHigh:${highValueY.value}\nClose:${valueY.value}";

        //    //    // important!
        //    //    // candlestick series colors are set in states. 
        //    //    // series.riseFromOpenState.properties.fill = am4core.color("#00ff00");
        //    //    // series.dropFromOpenState.properties.fill = am4core.color("#FF0000");
        //    //    // series.riseFromOpenState.properties.stroke = am4core.color("#00ff00");
        //    //    // series.dropFromOpenState.properties.stroke = am4core.color("#FF0000");

        //    //    series.riseFromPreviousState.properties.fillOpacity = 1;
        //    //    series.dropFromPreviousState.properties.fillOpacity = 0;

        //    //    chartTrading.cursor = new am4charts.XYCursor();

        //    //    // a separate series for scrollbar
        //    //    var lineSeries = chartTrading.series.push(new am4charts.LineSeries());
        //    //    lineSeries.dataFields.dateX = "date";
        //    //    lineSeries.dataFields.valueY = "close";
        //    //    // need to set on default state, as initially series is "show"
        //    //    lineSeries.defaultState.properties.visible = false;

        //    //    // hide from legend too (in case there is one)
        //    //    lineSeries.hiddenInLegend = true;
        //    //    lineSeries.fillOpacity = 0.5;
        //    //    lineSeries.strokeOpacity = 0.5;

        //    //    var scrollbarX = new am4charts.XYChartScrollbar();
        //    //    scrollbarX.series.push(lineSeries);
        //    //    chartTrading.scrollbarX = scrollbarX;

        //    //    //chartTrading.data = [{
        //    //    //    "date": "2011-08-01",
        //    //    //    "open": "136.65",
        //    //    //    "high": "136.96",
        //    //    //    "low": "134.15",
        //    //    //    "close": "136.49"
        //    //    //}, {
        //    //    //    "date": "2011-08-02",
        //    //    //    "open": "135.26",
        //    //    //    "high": "135.95",
        //    //    //    "low": "131.50",
        //    //    //    "close": "131.85"
        //    //    //}, {
        //    //    //    "date": "2011-08-05",
        //    //    //    "open": "132.90",
        //    //    //    "high": "135.27",
        //    //    //    "low": "128.30",
        //    //    //    "close": "135.25"
        //    //    //}, {
        //    //    //    "date": "2011-08-06",
        //    //    //    "open": "134.94",
        //    //    //    "high": "137.24",
        //    //    //    "low": "132.63",
        //    //    //    "close": "135.03"
        //    //    //}, {
        //    //    //    "date": "2011-08-07",
        //    //    //    "open": "136.76",
        //    //    //    "high": "136.86",
        //    //    //    "low": "132.00",
        //    //    //    "close": "134.01"
        //    //    //}, {
        //    //    //    "date": "2011-08-08",
        //    //    //    "open": "131.11",
        //    //    //    "high": "133.00",
        //    //    //    "low": "125.09",
        //    //    //    "close": "126.39"
        //    //    //}, {
        //    //    //    "date": "2011-08-09",
        //    //    //    "open": "123.12",
        //    //    //    "high": "127.75",
        //    //    //    "low": "120.30",
        //    //    //    "close": "125.00"
        //    //    //}, {
        //    //    //    "date": "2011-08-12",
        //    //    //    "open": "128.32",
        //    //    //    "high": "129.35",
        //    //    //    "low": "126.50",
        //    //    //    "close": "127.79"
        //    //    //}, {
        //    //    //    "date": "2011-08-13",
        //    //    //    "open": "128.29",
        //    //    //    "high": "128.30",
        //    //    //    "low": "123.71",
        //    //    //    "close": "124.03"
        //    //    //}, {
        //    //    //    "date": "2011-08-14",
        //    //    //    "open": "122.74",
        //    //    //    "high": "124.86",
        //    //    //    "low": "119.65",
        //    //    //    "close": "119.90"
        //    //    //}, {
        //    //    //    "date": "2011-08-15",
        //    //    //    "open": "117.01",
        //    //    //    "high": "118.50",
        //    //    //    "low": "111.62",
        //    //    //    "close": "117.05"
        //    //    //}, {
        //    //    //    "date": "2011-08-16",
        //    //    //    "open": "122.01",
        //    //    //    "high": "123.50",
        //    //    //    "low": "119.82",
        //    //    //    "close": "122.06"
        //    //    //}, {
        //    //    //    "date": "2011-08-19",
        //    //    //    "open": "123.96",
        //    //    //    "high": "124.50",
        //    //    //    "low": "120.50",
        //    //    //    "close": "122.22"
        //    //    //}, {
        //    //    //    "date": "2011-08-20",
        //    //    //    "open": "122.21",
        //    //    //    "high": "128.96",
        //    //    //    "low": "121.00",
        //    //    //    "close": "127.57"
        //    //    //}, {
        //    //    //    "date": "2011-08-21",
        //    //    //    "open": "131.22",
        //    //    //    "high": "132.75",
        //    //    //    "low": "130.33",
        //    //    //    "close": "132.51"
        //    //    //}, {
        //    //    //    "date": "2011-08-22",
        //    //    //    "open": "133.09",
        //    //    //    "high": "133.34",
        //    //    //    "low": "129.76",
        //    //    //    "close": "131.07"
        //    //    //}, {
        //    //    //    "date": "2011-08-23",
        //    //    //    "open": "130.53",
        //    //    //    "high": "135.37",
        //    //    //    "low": "129.81",
        //    //    //    "close": "135.30"
        //    //    //}, {
        //    //    //    "date": "2011-08-26",
        //    //    //    "open": "133.39",
        //    //    //    "high": "134.66",
        //    //    //    "low": "132.10",
        //    //    //    "close": "132.25"
        //    //    //}, {
        //    //    //    "date": "2011-08-27",
        //    //    //    "open": "130.99",
        //    //    //    "high": "132.41",
        //    //    //    "low": "126.63",
        //    //    //    "close": "126.82"
        //    //    //}, {
        //    //    //    "date": "2011-08-28",
        //    //    //    "open": "129.88",
        //    //    //    "high": "134.18",
        //    //    //    "low": "129.54",
        //    //    //    "close": "134.08"
        //    //    //}, {
        //    //    //    "date": "2011-08-29",
        //    //    //    "open": "132.67",
        //    //    //    "high": "138.25",
        //    //    //    "low": "132.30",
        //    //    //    "close": "136.25"
        //    //    //}, {
        //    //    //    "date": "2011-08-30",
        //    //    //    "open": "139.49",
        //    //    //    "high": "139.65",
        //    //    //    "low": "137.41",
        //    //    //    "close": "138.48"
        //    //    //}, {
        //    //    //    "date": "2011-09-03",
        //    //    //    "open": "139.94",
        //    //    //    "high": "145.73",
        //    //    //    "low": "139.84",
        //    //    //    "close": "144.16"
        //    //    //}, {
        //    //    //    "date": "2011-09-04",
        //    //    //    "open": "144.97",
        //    //    //    "high": "145.84",
        //    //    //    "low": "136.10",
        //    //    //    "close": "136.76"
        //    //    //}, {
        //    //    //    "date": "2011-09-05",
        //    //    //    "open": "135.56",
        //    //    //    "high": "137.57",
        //    //    //    "low": "132.71",
        //    //    //    "close": "135.01"
        //    //    //}, {
        //    //    //    "date": "2011-09-06",
        //    //    //    "open": "132.01",
        //    //    //    "high": "132.30",
        //    //    //    "low": "130.00",
        //    //    //    "close": "131.77"
        //    //    //}, {
        //    //    //    "date": "2011-09-09",
        //    //    //    "open": "136.99",
        //    //    //    "high": "138.04",
        //    //    //    "low": "133.95",
        //    //    //    "close": "136.71"
        //    //    //}, {
        //    //    //    "date": "2011-09-10",
        //    //    //    "open": "137.90",
        //    //    //    "high": "138.30",
        //    //    //    "low": "133.75",
        //    //    //    "close": "135.49"
        //    //    //}, {
        //    //    //    "date": "2011-09-11",
        //    //    //    "open": "135.99",
        //    //    //    "high": "139.40",
        //    //    //    "low": "135.75",
        //    //    //    "close": "136.85"
        //    //    //}, {
        //    //    //    "date": "2011-09-12",
        //    //    //    "open": "138.83",
        //    //    //    "high": "139.00",
        //    //    //    "low": "136.65",
        //    //    //    "close": "137.20"
        //    //    //}, {
        //    //    //    "date": "2011-09-13",
        //    //    //    "open": "136.57",
        //    //    //    "high": "138.98",
        //    //    //    "low": "136.20",
        //    //    //    "close": "138.81"
        //    //    //}, {
        //    //    //    "date": "2011-09-16",
        //    //    //    "open": "138.99",
        //    //    //    "high": "140.59",
        //    //    //    "low": "137.60",
        //    //    //    "close": "138.41"
        //    //    //}, {
        //    //    //    "date": "2011-09-17",
        //    //    //    "open": "139.06",
        //    //    //    "high": "142.85",
        //    //    //    "low": "137.83",
        //    //    //    "close": "140.92"
        //    //    //}, {
        //    //    //    "date": "2011-09-18",
        //    //    //    "open": "143.02",
        //    //    //    "high": "143.16",
        //    //    //    "low": "139.40",
        //    //    //    "close": "140.77"
        //    //    //}, {
        //    //    //    "date": "2011-09-19",
        //    //    //    "open": "140.15",
        //    //    //    "high": "141.79",
        //    //    //    "low": "139.32",
        //    //    //    "close": "140.31"
        //    //    //}, {
        //    //    //    "date": "2011-09-20",
        //    //    //    "open": "141.14",
        //    //    //    "high": "144.65",
        //    //    //    "low": "140.31",
        //    //    //    "close": "144.15"
        //    //    //}, {
        //    //    //    "date": "2011-09-23",
        //    //    //    "open": "146.73",
        //    //    //    "high": "149.85",
        //    //    //    "low": "146.65",
        //    //    //    "close": "148.28"
        //    //    //}, {
        //    //    //    "date": "2011-09-24",
        //    //    //    "open": "146.84",
        //    //    //    "high": "153.22",
        //    //    //    "low": "146.82",
        //    //    //    "close": "153.18"
        //    //    //}, {
        //    //    //    "date": "2011-09-25",
        //    //    //    "open": "154.47",
        //    //    //    "high": "155.00",
        //    //    //    "low": "151.25",
        //    //    //    "close": "152.77"
        //    //    //}, {
        //    //    //    "date": "2011-09-26",
        //    //    //    "open": "153.77",
        //    //    //    "high": "154.52",
        //    //    //    "low": "152.32",
        //    //    //    "close": "154.50"
        //    //    //}, {
        //    //    //    "date": "2011-09-27",
        //    //    //    "open": "153.44",
        //    //    //    "high": "154.60",
        //    //    //    "low": "152.75",
        //    //    //    "close": "153.47"
        //    //    //}, {
        //    //    //    "date": "2011-09-30",
        //    //    //    "open": "154.63",
        //    //    //    "high": "157.41",
        //    //    //    "low": "152.93",
        //    //    //    "close": "156.34"
        //    //    //}, {
        //    //    //    "date": "2011-10-01",
        //    //    //    "open": "156.55",
        //    //    //    "high": "158.59",
        //    //    //    "low": "155.89",
        //    //    //    "close": "158.45"
        //    //    //}, {
        //    //    //    "date": "2011-10-02",
        //    //    //    "open": "157.78",
        //    //    //    "high": "159.18",
        //    //    //    "low": "157.01",
        //    //    //    "close": "157.92"
        //    //    //}, {
        //    //    //    "date": "2011-10-03",
        //    //    //    "open": "158.00",
        //    //    //    "high": "158.08",
        //    //    //    "low": "153.50",
        //    //    //    "close": "156.24"
        //    //    //}, {
        //    //    //    "date": "2011-10-04",
        //    //    //    "open": "158.37",
        //    //    //    "high": "161.58",
        //    //    //    "low": "157.70",
        //    //    //    "close": "161.45"
        //    //    //}, {
        //    //    //    "date": "2011-10-07",
        //    //    //    "open": "163.49",
        //    //    //    "high": "167.91",
        //    //    //    "low": "162.97",
        //    //    //    "close": "167.91"
        //    //    //}, {
        //    //    //    "date": "2011-10-08",
        //    //    //    "open": "170.20",
        //    //    //    "high": "171.11",
        //    //    //    "low": "166.68",
        //    //    //    "close": "167.86"
        //    //    //}, {
        //    //    //    "date": "2011-10-09",
        //    //    //    "open": "167.55",
        //    //    //    "high": "167.88",
        //    //    //    "low": "165.60",
        //    //    //    "close": "166.79"
        //    //    //}, {
        //    //    //    "date": "2011-10-10",
        //    //    //    "open": "169.49",
        //    //    //    "high": "171.88",
        //    //    //    "low": "153.21",
        //    //    //    "close": "162.23"
        //    //    //}, {
        //    //    //    "date": "2011-10-11",
        //    //    //    "open": "163.01",
        //    //    //    "high": "167.28",
        //    //    //    "low": "161.80",
        //    //    //    "close": "167.25"
        //    //    //}, {
        //    //    //    "date": "2011-10-14",
        //    //    //    "open": "167.98",
        //    //    //    "high": "169.57",
        //    //    //    "low": "163.50",
        //    //    //    "close": "166.98"
        //    //    //}, {
        //    //    //    "date": "2011-10-15",
        //    //    //    "open": "165.54",
        //    //    //    "high": "170.18",
        //    //    //    "low": "165.15",
        //    //    //    "close": "169.58"
        //    //    //}, {
        //    //    //    "date": "2011-10-16",
        //    //    //    "open": "172.69",
        //    //    //    "high": "173.04",
        //    //    //    "low": "169.18",
        //    //    //    "close": "172.75"
        //    //    //}];

        //    //}); // end am4core.ready()
        //};




        var subscribeToTicker = function (hub) {
            var products = $("div.button-above-watchlist");
            products.each(function (index) {
                var ticker = $(this).attr("data-Name");
                hub.server.onSubscribe(ticker).done(function (result) {
                    if (result)
                        toastr.success('Success', "Suscribed to " + ticker);
                    else
                        toastr.error('Error', "Cannot subscribe to " + ticker);
                });
            });
        };

        var updateBidAskButton = function (symbol, bid, ask) {
            var button = $("div.button-above-watchlist." + symbol);
            var previous = $(button).find("span#bidInt").text() * 1 + $(button).find("span#bidFigure").text() / 10000 + $(button).find("span#bidPip").text() / 100000;
            $(button).find("span#bidInt").text(bid.toString().substring(0, 4));
            $(button).find("span#bidFigure").text(bid.toString().substring(4, 6));
            $(button).find("span#bidPip").text(bid.toString().substring(6, 7));
            var color = "white";
            if (bid < previous) {
                color = "red";
                console.log("bid down red");
            }
            else if (bid > previous) {
                color = "green";
                console.log("bid up green");
            }
            else {
                color = "orange";
                console.log("bid equal");
            }

            //$(button).find("div.sellButton").css("background", "white");
            setTimeout(function () {
                $(button).find("div.sellButton").css("background", color);
            }, 50);

            previous = $(button).find("span#askInt").text() * 1 + $(button).find("span#askFigure").text() / 10000 + $(button).find("span#askPip").text() / 100000;
            $(button).find("span#askInt").text(ask.toString().substring(0, 4));
            $(button).find("span#askFigure").text(ask.toString().substring(4, 6));
            $(button).find("span#askPip").text(ask.toString().substring(6, 7));
            if (ask < previous) {
                color = "red";
                console.log("ask down red");
            }
            else if (ask > previous) {
                color = "green";
                console.log("ask up green");
            }
            else {
                color = "orange";
                console.log("ask equal");
            }

           // $(button).find("div.buyButton").css("background", "white");
            setTimeout(function () {
                $(button).find("div.buyButton").css("background", color);
            }, 50);
        };



        var initPlatformHub = function () {
            var platformHub = $.connection.platformHub;
            // Create a function that the hub can call to broadcast messages.
            platformHub.client.onReceivedQuote = function (asset, bid, ask) {
                updateBidAskButton(asset, bid, ask);
            };

            platformHub.client.onReceivedDetailedQuote = function (symbol, bid, ask, dateTime, open, high, low, close, volume) {
                updateBidAskButton(symbol,bid, ask);
            };
            $.connection.hub.start()
                .done(function () {
                    toastr.success('Success', "You're now connected");
                    subscribeToTicker(platformHub);
                })
                .fail(function () { toastr.error('Error', "Enable to connect"); });

            $.connection.hub.error(function (error) {
                console.log('SignalR error: ' + error);
            });
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

        var openDeal = function (way, object) {
            
            var button = $(object).closest("div.button-above-watchlist");
            var volume = $("input#volumeDeal").val();
            if (volume === 0) {
                toastr.error("Volume is sequal to 0", "Deal not created");
                return;
            }
            if (way === "Sell")
                volume = volume * -1;
            else
                volume = volume * 1;
            var ticker = $(button).attr("data-name");
            if ($.trim(ticker) === "") {
                toastr.error("Ticker not found", "Deal not created");
                return;
            }
            var model = generateModelOpenDeal(ticker, volume, 12.98, 0.1);
            $.ajax({
                type: "POST",
                url: $("div.page-body").attr("data-openDeal"),
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
            $("div.sellButton").on("click", function () {
                openDeal("Sell", this);
            });
            // On Click Buy Button 
            $("div.buyButton").on("click", function () {
                openDeal("Buy", this);
            });


            $("body").off("click", "button.buttonCloseDeal").on("click", "button.buttonCloseDeal", function () {
                var idDeal = $(this).attr("deal-id");
                if ($.trim(idDeal) === "") {
                    toastr.error("Unable to get id");
                    return;
                }
                toastr.info("Deal " + idDeal + " sent for closing");
                closeDeal(idDeal);
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
    var widget = new FxWatchList();
    widget.initClass();

});

