let deleteButton = document.getElementById('deleteButton');
deleteButton.onclick = function () {
    if (confirm("Вы действительно хотите удалить выбранный элемент?")) {
        let id = $(this).data('id');

        if (id) {
            $.post("/Carts/Delete", { id: id }, function (data) {
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