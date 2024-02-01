fetch(`/User/GetCurrentBalance`, {
    method: "get",
})
    .then(response => response.json())
    .then(data => {
       
        var currentBalance = document.getElementById("currentBalance") ?? "";
        if (currentBalance) {
            var dataDiv = document.getElementById("currentBalanceData");
            div = document.createElement("div");
            div.textContent = "Current Balance";
            div.id = "currentBalanceText";
            currentBalance.insertBefore(div, dataDiv);
            var span = document.createElement("span");
            dataDiv.appendChild(span);
            span.textContent = `${data}$`;
            span.id = "currentBalanceSpan";
         
        }
        
    })
    .then(
        fetch(`/User/GetTotalSavings`, {
            method: "get",
        })
            .then(res => res.json())
            .then(data2 => {

                var span = document.createElement("span");
                var savings = document.getElementById("currentSavings") ?? "";
                if (savings) {
                    var savingsContent = document.getElementById("savingsContent");
                    div = document.createElement("div");
                    div.textContent = "Savings";
                    div.id = "currentSavingsText";
                    savings.insertBefore(div, savingsContent);
                    span.textContent = `${data2}$`;
                    span.id = "currentSavingsSpan"
                    savingsContent.appendChild(span);
                }
               


            })
    )