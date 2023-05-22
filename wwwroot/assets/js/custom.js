let dbServiceCount = $("#dbServiceCount").val()

$("#btnloadMore").on("click", () => {
    let serviceCount = $("#services").children().length

    $.ajax("Service/LoadMore", {
        method: "GET",
        data: {
            skip: serviceCount,
            take:8
        },
        success: (data) => {
            $("#services").append(data)
            serviceCount = $("#services").children().length
            if (serviceCount >= dbServiceCount) {
                $("#btnloadMore").remove()
            }
        }
    })
})