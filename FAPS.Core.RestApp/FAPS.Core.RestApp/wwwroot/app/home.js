$(document).ready(function () {
    $("#PaymentType").change(function () {
        return GetPaymentJson($(this).val(), $("#RequestType").val());
    });
});

function GetPaymentJson(paymentType, requestType) {
    if (paymentType != "" && requestType != "") {

        //var myJSONString = JSON.stringify(requstJson);

        //var splitData = requstJson.split('\n');
        //var singlestring = "";
        //for (var i = 0; i <= splitData.length - 1; i++) {
        //    singlestring = singlestring + splitData[i].trim();
        //}
        //singlestring = "'" + singlestring + "'";

        //var jsonObjects = { id: 1, name: "amit" };
        //var stringJson = JSON.stringify(jsonObjects);
        //window.location.href = window.location.origin + "/RestApi/GetPaymentBaseJson?" + "paymentType=" + paymentType + "requstJson=" + requstJson;
        $.ajax({
            type: "GET",
            url: "/RestApi/GetPaymentBaseJson",
            data: { paymentType: paymentType, requestType: requestType },
            dataType: "json",
            success: function (result) {
                $("#TransationRequest").val(result);
            },
            error: function (xhr, status, error) {
                alert(error);
            }
        });
    }
    else {
        alert("false");
        return false;
    }
}