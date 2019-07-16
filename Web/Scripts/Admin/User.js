$(function () {

    var AdminUser = function () {

        this.initClass = function () {
            initEvent();
        };

        var initEvent = function () {
            $('body').off("click", "a#addOption").on("click", "a#addOption", function () {
                var trAdd = $(this).closest("tr").clone();
                $(this).closest("tr").remove();
                var number = parseInt($("tbody#optionList tr").last().attr("id")) + 1;
                if (isNaN(number))
                    number = 0;
               
            });


            $("button.viewUser[type=button]").on("click", function () {
                $.ajax({
                    type: "GET",
                    url: $(this).attr("data-url"),
                    //data: { number: number },
                    success: function (data) {
                        if (data !== null || $.trim(data) !== "") {
                            $("div.modal").find("div.modal-body").empty();
                            $("div.modal").find("div.modal-body").prepend(data);
                            $("div.modal").modal("show");
                        }
                        else {
                            toastr.error("A problem happened to add your oder into the list, please refresh the page.");
                        }
                    },
                    error: function () {
                        toastr.error("A problem happened.");
                    }
                }).done(function () {

                });


                // end
                var model = [];
                var rowOptions = $("tbody#optionList tr");
                var i = 0;
                var interestRate = $("input#interest").val();
                var spot = $("input#spot-price").val();
                var volatility = $("input#volatility").val();
                for (i = 0; i < rowOptions.length; i++) {
                    var e = $(rowOptions[i]);
                    if (e.attr('id') !== "-1") {
                        var modelTemp = {
                            Position: e.find("input#position").val(),
                            Expiration: e.find("input#expiration").val(),
                            Strike: e.find("input#strike").val(),
                            Type: e.find("select#type :selected").text(),
                            Price: e.find("td#price").text(),
                            PnL: e.find("td#pnl").text(),
                            Delta: e.find("td#delta").text(),
                            Gamma: e.find("td#gamma").text(),
                            Theta: e.find("td#theta").text(),
                            Vega: e.find("td#vega").text(),
                            Rho: e.find("td#rho").text(),
                            Id: e.attr("id"),
                            InterestRate: interestRate,
                            Spot: spot,
                            Volatility: volatility,
                        };
                        model.push(modelTemp);
                    }
                }
                $.ajax({
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    url: $("input[type=hidden][id=urlToComputeStrategy]").val(),
                    data: JSON.stringify(model),
                    success: function (data) {
                        if (data !== null || $.trim(data) !== "") {
                            var options = data.option;
                            var i = 0;
                            for (i = 0; i < options.length; i++) {
                                var e = options[i];
                                var tr = $('tbody#optionList tr#' + e[5][0]);
                                tr.find("td#price").text(e[4][0]);
                                tr.find("td#delta").text(e[4][1]);
                                tr.find("td#gamma").text(e[4][2]);
                                tr.find("td#vega").text(e[4][3]);
                                tr.find("td#theta").text(e[4][0]);
                                tr.find("td#rho").text(e[4][0]);
                            }
                            var payoff = data.payoffString;
                            $("div#payoff").html(payoff);
                            var chartPayoff = data.chartPayoff;
                            var i = 0;
                            payOffChart.addSeries({
                                name: 'PayOff',
                            });
                            payOffChart.series[0].setData([]);
                            for (i = 0; i < chartPayoff.length; i++) {
                                var e = chartPayoff[i];
                                payOffChart.series[0].addPoint([e.Item1, e.Item2]);
                            }
                        }
                        else {
                            toastr.error("A problem happened to add your oder into the list, please refresh the page.");
                        }
                    },
                    error: function () {
                        toastr.error("A problem happened.");
                    }
                }).done(function () {

                });


            });




        }

    }

    // ================
    var widget = new AdminUser();
    widget.initClass();

});