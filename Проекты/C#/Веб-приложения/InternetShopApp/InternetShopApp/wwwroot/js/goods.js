let deleteButton = document.getElementById('deleteButton');
deleteButton.onclick = function () {
    if (confirm("Вы действительно хотите удалить выбранный элемент?")) {
        let id = $(this).data('id');
        if (id) {
            $.post("/Good/Delete", { id: id }, function (data) {
                if (data === "OK") {
                    alert("Данные успешно удалены");
                    location.reload();
                }
                else {
                    alert("Что-то пошло не так...");
                }
            }).fail(function () {
                alert("Что-то пошло не так...");
            });
        }
    }
};

let addToCartButton = document.getElementById('addToCartButton');
addToCartButton.onclick = function () {
    let id = $(this).data('id');
    if (id) {
        $.post("/Good/AddToCart", { id: id }, function (data) {
            if (data === "OK") {
                alert("Товар добавлен в корзину");
                location.reload();
            }
            else {
                alert("Что-то пошло не так...");
            }
        }).fail(function () {
            alert("Что-то пошло не так...");
        });
    }
};