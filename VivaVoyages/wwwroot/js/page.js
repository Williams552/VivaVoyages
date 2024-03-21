document.addEventListener('DOMContentLoaded', function() {
    // Lấy danh sách các tour từ trang
    var tours = document.querySelectorAll('.tour-item');
    // Số tour trên mỗi trang
    var itemsPerPage = 6;
    // Tính số trang dựa trên số tour và số tour trên mỗi trang
    var totalPages = Math.ceil(tours.length / itemsPerPage);
    // Mặc định hiển thị trang đầu tiên
    showPage(1);

    // Hàm hiển thị trang
    function showPage(pageNumber) {
        // Tính chỉ số bắt đầu và kết thúc của các tour trong trang
        var startIndex = (pageNumber - 1) * itemsPerPage;
        var endIndex = Math.min(startIndex + itemsPerPage, tours.length);

        // Ẩn tất cả các tour
        tours.forEach(function(tour) {
            tour.style.display = 'none';
        });

        // Hiển thị các tour của trang hiện tại
        for (var i = startIndex; i < endIndex; i++) {
            tours[i].style.display = 'block';
        }

        // Hiển thị các tour của trang hiện tại
    }
    
    // Tạo các nút phân trang
    var paginationContainer = document.getElementById('pagination-container');
    for (var i = 1; i <= totalPages; i++) {
        var button = document.createElement('button');
        button.textContent = i;
        button.addEventListener('click', function() {
            var pageNumber = parseInt(this.textContent);
            showPage(pageNumber);
            // Di chuyển lên đầu trang
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });
        paginationContainer.appendChild(button);
    }
});
