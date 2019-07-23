$(function () {

    var Context = function () {

        this.initClass = function () {
            initContextHub();
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

