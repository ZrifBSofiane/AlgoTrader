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
        var generateModelAddedProduct = function (type) {
            var div = $("div#divDataProduct");
            if ($.trim(type) === "FOREX") {
                var model = {
                    Name: div.find("input#Product_Name").val(),
                    Market: div.find("input#Product_Market").val(),
                    Type: type,
                    Param1: div.find("input#Forex_Asset").val(),
                    Param2: div.find("input#Forex_Base").val(),
                    Param3: div.find("input#Forex_Pip").val(),
                    Param4: div.find("input#Forex_MarginPercentage").val()
                };
                return model;
            }
        };



        var initEvent = function () {

            //CLose addProduct Modal on click
            $("button#closeModalAddProduct").on("click", function () {
                //$("div#addProductModal").toggle();
            });
            // Add product modal on radio type click
            $("form#productTypeButtonRadio input[type=radio]").on("change", function () {
                var type = $("form#productTypeButtonRadio input[type=radio]:checked").val();
                getPartialViewProductAdded(type);
            });

            // Save and add the product on click button (Modal)
            $("button#addProductButton").on("click", function () {
                var type = $("form#productTypeButtonRadio input[type=radio]:checked").val();
                var model = generateModelAddedProduct(type);
                $.ajax({
                    type: "POST",
                    url: $(this).attr("data-addProductUrl"),
                    data: { model: model },
                    //data: { number: number },
                    success: function (data) {
                        if (data.isOk) {
                            toastr.success('Success', data.message);
                            $("div#addProductModal").modal("toggle");
                        }
                        else {
                            toastr.error('Error', data.message);
                            $("div#addProductModal").modal("toggle");
                        }
                    },
                    error: function (error) {
                        toastr.error('Error', error);
                    }
                }).done(function () {

                });
            });

        };

        var initDataTable = function () {
            // Init the DataTable
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
                            showAddProductModal();
                        }
                    }
                ]
            });
            // Show specific info inside the datatable depending on the product
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

        // On click Add Product event => show modal
        var showAddProductModal = function () {
            $("div#addProductModal").modal("show");
        };
       
        // Get the partial View depending on the product type
        var getPartialViewProductAdded = function (typeProduct) {
            $.ajax({
                type: "GET",
                url: $('#dataTableProduct').attr("data-addProduct"),
                data: { productType: typeProduct },
                //data: { number: number },
                success: function (data) {
                    if (data !== null || $.trim(data) !== "") {
                        $("div#addProductModal").find("div.bodyProduct").empty();
                        $("div#addProductModal").find("div.bodyProduct").prepend(data);
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

