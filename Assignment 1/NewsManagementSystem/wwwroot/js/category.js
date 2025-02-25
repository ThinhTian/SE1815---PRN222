document.addEventListener("DOMContentLoaded", function () {
    const deleteBtns = document.querySelectorAll('.delete-btn');

    deleteBtns.forEach(btn => {
        btn.addEventListener('click', function (e) {
            const categoryId = e.target.getAttribute('data-id');
            const categoryName = e.target.getAttribute('data-name');

            document.getElementById('categoryName').textContent = categoryName;

            document.getElementById('categoryId').value = categoryId;

            $('#deleteModal').modal('show');
        });
    });
});
