let books = [];

function generateDummyBooks() {
const titles = [
    "The Alchemist", "Atomic Habits", "Rich Dad Poor Dad", "1984", "To Kill a Mockingbird",
    "The Great Gatsby", "The Power of Now", "The Hobbit", "Becoming", "Sapiens",
    "Deep Work", "Educated", "Think and Grow Rich", "Start With Why", "Can't Hurt Me",
    "The Psychology of Money", "The Silent Patient", "Dune", "The Book Thief", "The Catcher in the Rye"
];
titles.forEach((title, i) => {
    books.push({
    id: i + 1,
    title,
    author: `Author ${i + 1}`,
    genre: ["Fiction", "Self-help", "Drama", "Thriller", "Fantasy"][i % 5],
    rating: Math.ceil(Math.random() * 5),
    review: "Sample review for " + title,
    liked: Math.random() > 0.5
    });
});
}

function renderBooks() {
const container = document.getElementById("bookList");
container.innerHTML = books.map(book => `
    <div class="col-md-4 mb-4">
    <div class="card card-custom">
        <div class="card-body">
        <h5 class="card-title">${book.title}</h5>
        <p class="card-text">
            <small><strong>Author:</strong> ${book.author}</small><br>
            <small><strong>Genre:</strong> ${book.genre}</small><br>
            <small><strong>Rating:</strong> ${'<i class="fas fa-star text-warning"></i>'.repeat(book.rating)}</small><br>
            <small><strong>Review:</strong> ${book.review}</small><br>
            <small><strong>Liked:</strong> ${book.liked ? '<i class="fas fa-heart text-danger"></i>' : '<i class="far fa-heart text-muted"></i>'}</small>
        </p>
        <button class="btn btn-sm btn-warning me-2" onclick='editBook(${book.id})'>Edit</button>
        <button class="btn btn-sm btn-danger" onclick='deleteBook(${book.id})'>Delete</button>
        </div>
    </div>
    </div>
`).join("");
}

function openAddModal() {
document.getElementById("bookForm").reset();
document.getElementById("bookId").value = "";
const modal = new bootstrap.Modal(document.getElementById('bookModal'));
modal.show();
}

function editBook(id) {
const book = books.find(b => b.id === id);
if (book) {
    document.getElementById("bookId").value = book.id;
    document.getElementById("title").value = book.title;
    document.getElementById("author").value = book.author;
    document.getElementById("genre").value = book.genre;
    document.getElementById("rating").value = book.rating;
    document.getElementById("review").value = book.review;
    document.getElementById("liked").checked = book.liked;
    const modal = new bootstrap.Modal(document.getElementById('bookModal'));
    modal.show();
}
}

function deleteBook(id) {
if (confirm("Are you sure you want to delete this book?")) {
    books = books.filter(b => b.id !== id);
    renderBooks();
}
}

document.getElementById("bookForm").addEventListener("submit", function(e) {
e.preventDefault();
const id = document.getElementById("bookId").value;
const bookData = {
    id: id ? Number(id) : Date.now(),
    title: document.getElementById("title").value,
    author: document.getElementById("author").value,
    genre: document.getElementById("genre").value,
    rating: Number(document.getElementById("rating").value),
    review: document.getElementById("review").value,
    liked: document.getElementById("liked").checked
};

if (id) {
    const index = books.findIndex(b => b.id == id);
    books[index] = bookData;
} else {
    books.push(bookData);
}
const modal = bootstrap.Modal.getInstance(document.getElementById('bookModal'));
modal.hide();
renderBooks();
});

generateDummyBooks();
renderBooks();
