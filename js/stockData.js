
    fetch('https://www.alphavantage.co/query?function=TOP_GAINERS_LOSERS&apikey=demo')
        .then(response => response.json())
        .then(data => {
            const mostActivelyTradedStocks = data.most_actively_traded.slice(0, 10);;

            const stockDataSection = document.getElementById('stockDataSection');

            mostActivelyTradedStocks.forEach(stock => {
                const stockCard = document.createElement('tr');
                stockCard.classList.add('stock-card');

                const changeAmount = parseFloat(stock.change_amount);
                if (changeAmount > 0) {
                    stockCard.classList.add('grew');
                } else if (changeAmount < 0) {
                    stockCard.classList.add('dropped');
                }

                ticker_ = createTd(stock.ticker);
                price = createTd(`$${stock.price}`);
                change = createTd(`${stock.change_amount} (${stock.change_percentage})`);
                volume = createTd(`${stock.volume}`);

                stockCard.appendChild(ticker_);
                stockCard.appendChild(price);
                stockCard.appendChild(change);
                stockCard.appendChild(volume);

                stockDataSection.appendChild(stockCard);
            });
        })
        .catch(error => {
            console.log('Error fetching data:', error);
        });



function createTd(text) {
    td = document.createElement("td");
    td.textContent = text;
    return td;
}