$('.deleteButton').on('click', function () {
    if (confirm("Вы действительно хотите удалить выбранный элемент?")) {
        let id = $(this).data('id');
        if (id) {
            $.post("/Goods/Delete", {id: id}, function (data) {
                if (data === "OK") {
                    alert("Данные успешно удалены");
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

$('.addToCartButton').on('click', function () {
    let id = $(this).data('id');
    if (id) {
        $.post("/Goods/AddToCart", {id: id}, function (data) {
            if (data === "OK") {
                alert("Товар добавлен в корзину");
                location.reload();
            } else {
                alert("Что-то пошло не так...");
            }
        }).fail(function () {
            alert("Что-то пошло не так...");
        });
    }
});