$(document).ready(
    function () {
        $(".menuItem").click(
            function () {
                $.ajax({
                    url: "http://server:port/Controller/MethodName/?id=42",
                    method: "GET",
                    dataType: "html",
                    beforeSend: function () {
                        alert("beforeSend");
                    },
                    error: function () {
                        alert("wrror");
                    },
                    success: function (data) {
                        $("#content").html(data);
                    },
                    complete: function () {
                        // your code here;
                    }
                }
                );
            }
        );

        $(".arrowdown").click(
            function () {
                $(".authorFilter").scrollIntoView();
                }
            )
      


    });




