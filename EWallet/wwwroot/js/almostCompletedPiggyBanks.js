fetch(`/PiggyBank/GetAlmostCompletedPiggyBanks`, {
    method: "get",
})
    .then(response => response.json())
    .then(data => {

        var container = document.getElementById("notificationsContainer");
        var stocksAndAdvice = document.getElementById("stocksAndAdvice");

        var div = document.createElement("div");
        div.id = "advice";
        var adviceTitle = document.createElement("div");
        adviceTitle.id = "adviceTitle";
        adviceTitle.innerHTML = "But before managing your finances bear in mind these pieces of advice from professionals ";
        var icon = document.createElement("span");
        icon.classList.add("bi");
        icon.classList.add("bi-megaphone-fill");
        adviceTitle.appendChild(icon);
        div.appendChild(adviceTitle);
        stocksAndAdvice.appendChild(div);

        var icons = []

        var list = utils.createList(div, ["Pay With Cash, Not Credit",
            "Start an Emergency Fund",
            "Never let your expenses exceed your income, and watch where your money goes ",
            "Save for Retirement Now",
            "Don’t wait to apply for health insurance",
            "Monitor Your Taxes"
        ], "list", classes = [["bi", "bi-cash"], ["bi", "bi-exclamation-triangle-fill"], ["bi", "bi-binoculars"], ["bi", "bi-piggy-bank"], ["bi", "bi-hospital"],
        ["bi", "bi-wallet2"]]);
    


        if (Object.keys(data).length === 0) {
            title = document.createElement("h3");
            title.innerHTML = "No upcoming events to show. Keep exploring!";
            title.id = "upcomingEvents";
            container.appendChild(title);
        }

           

        else {

            title = document.createElement("h3");
            title.innerHTML = "Your upcoming events";
            container.appendChild(title);
            title.id = "upcomingEvents";


            title2 = document.createElement("h4");
            title2.innerHTML = "Your almost completed Piggy Banks. Keep going you're doing great!";
            container.appendChild(title2);
            title2.id = "almostDonePB";


            for (index = 0; index < data.length; index++) {
                div = document.createElement("div");
                div.classList.add("pbEvent");
                numberSpan = document.createElement("span");
                numberSpan.classList.add("leftSum"); 
                numberSpan.textContent = data[index]["targetSum"] - data[index]["currentBalance"];
                nameSpan = document.createElement("span");
                nameSpan.classList.add("pbName");
                nameSpan.textContent = data[index]["piggyBankDescription"];
                div.innerHTML = `Only ${numberSpan.innerHTML}$ to save until you complete
                your ${nameSpan.innerHTML} Piggy Bank`;
                container.appendChild(div);
            }
        }
    })
    .then(
        fetch(`/PiggyBank/GetNearDueDatePiggyBanks`, {
            method: "get",
        })
            .then(resp => resp.json())
            .then(data2 => {

                var container = document.getElementById("notificationsContainer");
                var lenght;

                if (!Object.keys(data2).length === 0) {
                

                    for (index = 0; index < data2.length; index++) {
                        div = document.createElement("div");
                        container.appendChild(div);
                    }
                }
            })
       )

