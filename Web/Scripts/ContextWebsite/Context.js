$(function () {

    var Context = function () {

        this.initClass = function () {
           // initContextHub();
            initEvent();
        };

        var initEvent = function () {
            $("a#tradeBlotterLink").on("click", function () {
                var winFeature = 'location=no,toolbar=no,menubar=no,scrollbars=yes,resizable=yes';
                window.open($("a#tradeBlotterLink").attr("data-url"), 'null', winFeature);  
            });
            
        };

        var initContextHub = function () {
            var contextHub = $.connection.contextHub;
            // Create a function that the hub can call to broadcast messages.
            contextHub.client.receiveNewConnectionToAll = function (user) {
                toastr.info("New Connection", user + " is now connected");
            };

            $.connection.hub.start().done(function () {
                toastr.success('Success', "Context Connected");
            });
        };
    };

    // ================
    var widget = new Context();
    widget.initClass();

});

