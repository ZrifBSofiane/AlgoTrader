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
                            toastr.error('Error', 'Enable to get data');
                        }
                    },
                    error: function (error) {
                        toastr.error('Error', error);
                    }
                }).done(function () {

                });
            });


            $("button.block[type=button]").on("click", function () {
                $.ajax({
                    type: "POST",
                    url: $(this).attr("data-url"),
                    //data: { number: number },
                    success: function (data) {
                        if (data.isOk) {
                            toastr.success('Success', data.message);
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
            });
        };
    };

    // ================
    var widget = new AdminUser();
    widget.initClass();

});