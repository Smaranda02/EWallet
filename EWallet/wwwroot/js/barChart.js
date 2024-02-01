
function drawBarChart(model) {

    transactionStats = {
        "Spendings": model["SpendingsNumber"],
        "Incomes": model["IncomesNumber"],
        "Savings": model["SavingsNumber"]
    }


    var labels = Object.keys(transactionStats);
    var data = Object.values(transactionStats);
    var colors = ['red', 'yellow', 'green', 'pink', 'green', 'beige', 'black', 'white', 'blue', 'brown', 'orange']
;


    const canvas = document.getElementById('barChart');
    const ctx = canvas.getContext('2d');
    if (model["SpendingsNumber"] || model["IncomesNumber"] || model["SavingsNumber"]) {
        canvas.style.display = "block";
        const myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{

                    data: data,
                    backgroundColor: colors,
                    borderColor: "black",
                    borderWidth: 1
                }]
            },
            options: {
                legend: { display: false },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                title: {
                    display: true,
                    text: "The distribution of Spendings, Savings and Incomes"
                }
            }
        });
    }
}