fetch(`/Spending/GetUpcomingspendings`, {
    method: "get",
})
    .then(response => response.json())
    .then(data => {
        var container = document.getElementById("upcomingSpendings");
        for (index = 0; index < data.length; index++) {
            div = document.createElement("div");
            nameSpan = document.createElement("span");
            nameSpan.classList.add("upcomingSpendingName");
            nameSpan.textContent = data[index]["spendingDescription"];
            sumSpan = document.createElement("span");
            sumSpan.textContent = `-${data[index]["amount"]}\$`;
            sumSpan.classList.add("upcomingSpendingSum")
            div.appendChild(nameSpan);
            div.appendChild(sumSpan);
            div.classList.add("upcomingSpendings");

            container.appendChild(div);
        }
    })