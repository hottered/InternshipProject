function toggleSearchButton() {
    var searchInput = document.getElementById("searchInput");
    var searchButton = document.getElementById("searchButton");
    searchButton.disabled = searchInput.value.trim() === "";
}
document.addEventListener("DOMContentLoaded", function () {
    toggleSearchButton();
    document.getElementById("searchInput").addEventListener("input", toggleSearchButton);
});