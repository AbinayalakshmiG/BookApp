// Bar Chart – Top 5 Rated Books
new Chart(document.getElementById("barChart"), {
    type: "bar",
    data: {
        labels: ["The Alchemist", "Atomic Habits", "Rich Dad Poor Dad", "1984", "To Kill a Mockingbird"],
        datasets: [{
            label: "Rating",
            data: [4.9, 4.8, 4.7, 4.6, 4.5],
            backgroundColor: "rgba(54, 162, 235, 0.6)"
        }]
    },
    options: {
        responsive: true
    }
});

// Pie Chart – Genre Distribution
new Chart(document.getElementById("pieChart"), {
    type: "pie",
    data: {
        labels: ["Fiction", "Self-help", "Drama", "Thriller", "Fantasy"],
        datasets: [{
            label: "Genre Count",
            data: [3, 7, 4, 4, 2], // Adjust if needed
            backgroundColor: [
                "rgba(255, 99, 132, 0.6)",
                "rgba(75, 192, 192, 0.6)",
                "rgba(255, 206, 86, 0.6)",
                "rgba(153, 102, 255, 0.6)",
                "rgba(255, 159, 64, 0.6)"
            ]
        }]
    },
    options: {
        responsive: true
    }
});

// Line Chart – Monthly Review Counts
new Chart(document.getElementById("lineChart"), {
    type: "line",
    data: {
        labels: ["Jan", "Feb", "Mar", "Apr", "May"],
        datasets: [{
            label: "Monthly Reviews",
            data: [10, 15, 25, 18, 22],
            fill: false,
            borderColor: "rgba(255, 99, 132, 1)",
            tension: 0.3
        }]
    },
    options: {
        responsive: true
    }
});

