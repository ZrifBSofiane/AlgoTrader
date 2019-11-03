$(function () {

    var TradeBlotter = function () {

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

        };

        var initPlatformHub = function () {
            var platformHub = $.connection.platformHub;
          
            platformHub.client.onUpdatePnL = function (transaction) {
                updateTransactionAllByIdPnl(tableActualTransaction, transaction.Id, transaction.PnL);
            };


            $.connection.hub.start()
                .done(function () {
                    toastr.success('Success', "You're now connected");
                })
                .fail(function () { toastr.error('Error', "Enable to connect"); });

            $.connection.hub.error(function (error) {
                console.log('SignalR error: ' + error);
            });
        };

        var initEvent = function () {
          
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

        var generateModelCloseDeal = function (endPrice, dealId, productName, way) {
            var model = {
                EndPrice: endPrice,
                DealId: dealId,
                Status: "",
                ProductName: productName
            };
            return model;
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

    };

    // ================
    var widget = new TradeBlotter();
    widget.initClass();

});

