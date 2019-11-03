$(function () {

    var PortalHome = function () {
        var tableListTrade;

        this.initClass = function () {
            initDataTable();
            initEvent();
        };

        var initDataTable = function () {
            $.fn.dataTable.ext.errMode = 'throw';

            tableListTrade = $('table#listTrade').DataTable({
                "scrollY": "700px",
                "scrollCollapse": true,
                "paging": true,
            });
        };

        var initEvent = function () {

        };

 

 
    };

    // ================
    var widget = new PortalHome();
    widget.initClass();

});

