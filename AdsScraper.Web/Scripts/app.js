var Car = (function () {
    function Car() {
    }
    return Car;
}());
window.onload = function () {
    $("#models-list").attr("disabled", "disabled");
    $("#manufacturers-list").on("change", function (e) {
        var manufacturer = $(e.target).val();
        $("#models-list").attr("disabled", "disabled");
        $.get("/api/Cars/" + manufacturer, function (data, xhr) {
            $("#models-list").empty();
            var list = data;
            list.forEach(function (x) {
                $("#models-list").append($("<option></option>")
                    .attr("value", x.id)
                    .text(x.value));
            });
            $("#models-list").removeAttr("disabled");
        });
    });
    $(".input-sm,.checkbox").on("click change", function (e) {
        $("#price").hide();
    });
    $("#fetch-btn").click(function (e) {
        var car = new Car();
        car.manufacturer = $("#manufacturers-list option:selected").text();
        car.modelId = Number($("#models-list option:selected").val());
        car.ownersCountId = Number($("#owners-list option:selected").val());
        car.year = Number($("#years-list option:selected").val());
        car.kilometers = Number($("#kilometers").val());
        car.isRegistred = Boolean($("#is-reg").is(":checked"));
        car.engineTypeId = Number($("#engines-list option:selected").val());
        car.volume = Number($("#volume").val());
        car.power = Number($("#power").val());
        car.gearboxTypeId = Number($("#years-list option:selected").val());
        car.gearsCount = Number($("#gears-list option:selected").val());
        car.hasGasInstallation = Boolean($("#has-gas").is(":checked"));
        $.post("/api/Cars", car, function (data, xhr) {
            $("#price").html("Cijena: " + parseFloat(data).toFixed(2) + " \u20AC").show();
        });
    });
};
//# sourceMappingURL=app.js.map