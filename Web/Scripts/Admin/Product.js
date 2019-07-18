$(function () {

    var AdminProduct = function () {

        this.initClass = function () {
            initEvent();
            initDataTable();
        };

        function format(d) {
            // `d` is the original data object for the row
            if (d.type === "FOREX") {
                return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                    '<tr>' +
                    '<td>Name:</td>' +
                    '<td>' + d.name + '</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td>Base:</td>' +
                    '<td>' + d.product.Base + '</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td>Asset:</td>' +
                    '<td>' + d.product.Asset + '</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td>Pip value:</td>' +
                    '<td>' + d.product.Pip + ' pip unit</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td>Margin:</td>' +
                    '<td>' + d.product.MarginPercentage + '%</td>' +
                    '</tr>' +
                    '</table>';
            }

        }



        var initEvent = function () {

        };

        var initDataTable = function () {
            var table = $('#dataTableProduct').DataTable({
                dom: 'Bfrtip',
                ajax: {
                    url: $("#dataTableProduct").attr("data-url"),
                    type: 'POST',
                    dataSrc: 'data'
                },
                columns: [
                    {
                        "className": 'details-control',
                        "orderable": false,
                        "data": null,
                        "defaultContent": ''
                    },
                    { "data": "type" },
                    { "data": "name" },
                    { "data": "market" }
                ],
                buttons: [
                    {
                        text: 'Add Product',
                        action: function (e, dt, node, config) {
                            addProduct();
                        }
                    }
                ]
            });

            $('#dataTableProduct').on('click', 'td.details-control', function () {
                var tr = $(this).closest('tr');
                var row = table.row(tr);

                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                    tr.removeClass('shown');
                }
                else {
                    // Open this row
                    row.child(format(row.data())).show();
                    tr.addClass('shown');
                }
            });
        };

        var addProduct = function () {
            $.ajax({
                type: "GET",
                url: $('#dataTableProduct').attr("data-addProduct"),
                data: { productType: "FOREX" },
                //data: { number: number },
                success: function (data) {
                    if (data !== null || $.trim(data) !== "") {
                        $("div#addProductModal").find("div.modal-body").empty();
                        $("div#addProductModal").find("div.modal-body").prepend(data);
                        $("div#addProductModal").modal("show");
                    }
                    else {
                        toastr.error('Error', 'Enable to get data');
                    }
                },
                error: function (error) {
                    toastr.error('Error', error);
                }
            }).done(function () {

            });

        };
    };

    // ================
    var widget = new AdminProduct();
    widget.initClass();

});

