$('.cancelButton').on('click', function () {
    if (confirm("Вы действительно хотите отменить заказ?")) {
        let id = $(this).data('id');
        if (id) {
            $.post("/Orders/Cancel", {id: id}, function (data) {
                if (data === "OK") {
                    alert("Заказ успешно отменен");
                    location.reload();
                } else {
                    alert("Что-то пошло не так...");
                }
            }).fail(function () {
                alert("Что-то пошло не так...");
            });
        }
    }
});

$('.completeButton').on('click', function () {
    if (confirm("Вы действительно хотите завершить заказ?")) {
        let id = $(this).data('id');
        if (id) {
            $.post("/Orders/Complete", {id: id}, function (data) {
                if (data === "OK") {
                    alert("Заказ успешно завершен");
                    location.reload();
                } else {
                    alert("Что-то пошло не так...");
                }
            }).fail(function () {
                alert("Что-то пошло не так...");
            });
        }
    }
});

$('.returnButton').on('click', function () {
    if (confirm("Вы действительно хотите вернуть заказ?")) {
        let id = $(this).data('id');
        if (id) {
            $.post("/Orders/Return", {id: id}, function (data) {
                if (data === "OK") {
                    alert("Заявка на возврат отправлена");
                    location.reload();
                } else {
                    alert("Что-то пошло не так...");
                }
            }).fail(function () {
                alert("Что-то пошло не так...");
            });
        }
    }
});