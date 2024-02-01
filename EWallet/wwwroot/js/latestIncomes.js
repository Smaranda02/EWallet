fetch(`/Income/GetLatestIncomes`, {
    method: "get",
})
    .then(response => response.json())
    .then(data => {
        var container = document.getElementById("incomeHistoryContainer");
        for (index = 0; index < data.length; index++) {
            div = document.createElement("div");
            nameSpan = document.createElement("span");
            nameSpan.classList.add("latestIncomeName");
            nameSpan.textContent = data[index]["incomeDescription"];
            sumSpan = document.createElement("span");
            sumSpan.textContent = `+${data[index]["incomeSum"]}$`;
            sumSpan.classList.add("latestIncomeSum")
            div.appendChild(nameSpan);
            div.appendChild(sumSpan);
            div.classList.add("latestIncomes");

            container.appendChild(div);
        }
    })