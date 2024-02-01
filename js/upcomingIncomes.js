fetch(`/Income/GetUpcomingIncomes`, {
    method: "get",
})
    .then(response => response.json())
    .then(data => {
        var container = document.getElementById("upcomingIncomes");
        for (index = 0; index < data.length; index++) {
            div = document.createElement("div");
            nameSpan = document.createElement("span");
            nameSpan.classList.add("upcomingIncomeName");
            nameSpan.textContent = data[index]["incomeDescription"];
            sumSpan = document.createElement("span");
            sumSpan.textContent = `+${data[index]["incomeSum"]}\$`;
            sumSpan.classList.add("upcomingIncomeSum")
            div.appendChild(nameSpan);
            div.appendChild(sumSpan);
            div.classList.add("upcomingIncomes");

            container.appendChild(div);
        }
    })