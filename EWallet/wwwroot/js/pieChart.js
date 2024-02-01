function drawPieChart(spendings) {
    var labels = [];
    var data = []; 
    spendings.forEach((spending) => {
        labels.push(spending["CategoryName"]);
        data.push(spending["Appearances"]);
    })
 
    colors = ['red', 'yellow', 'purple', 'pink', 'green', 'beige', 'black', 'white', 'blue', 'brown', 'orange']

    var canvas = document.getElementById('spendingPieChart');
    var ctx = canvas.getContext('2d');

    if (spendings.length != 0) {
        canvas.style.display = "block";
        var myPieChart = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    backgroundColor: colors,

                }]
            },
            options: {

                title: {
                    display: true,
                    text: "Distribution of spendings based on spending categories"
                }

            }
        });
    }

}
