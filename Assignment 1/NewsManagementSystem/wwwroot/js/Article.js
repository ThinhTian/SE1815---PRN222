document.addEventListener("DOMContentLoaded", function () {
    const deleteBtns = document.querySelectorAll('.delete-btn');

    deleteBtns.forEach(btn => {
        btn.addEventListener('click', function (e) {
            const categoryId = e.target.getAttribute('data-id');
            const categoryName = e.target.getAttribute('data-name');

            document.getElementById('NewsTitle').textContent = categoryName;

            document.getElementById('NewsArticleId').value = categoryId;

            $('#deleteModal').modal('show');
        });
    });
});
