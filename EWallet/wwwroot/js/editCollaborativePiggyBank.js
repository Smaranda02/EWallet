function editCollaborativePiggyBank(id, incomes) {
    fetch(`EditCollaborativePiggyBank?id=${id}`, {
            method: "get",
        })
            .then(response => response.json())
            .then(data => {

                createCollabPiggyBankForm(data,id,incomes);
            })
   


function createCollabPiggyBankForm(data, id,  incomes) {

        var parentDiv = document.getElementById("editCollaborativePiggyBankDiv");
        var incomesDiv = document.getElementById("collabIncomes");
        var pbButtons = document.getElementById("collabButtons");
        var createButton = document.getElementById("createIncomeButton");
        
        if (pbButtons) {
           parentDiv.removeChild(pbButtons);
        }

        if (incomesDiv) {
            parentDiv.removeChild(incomesDiv);
         }

        var errorsDiv = document.getElementById("errors");
        if (errorsDiv) {
            errorsDiv.innerHTML = "";
         }

     


        var incomesDiv = document.createElement("div");
        incomesDiv.id = "collabIncomes";
        parentDiv.appendChild(incomesDiv);
        var createIncomeButton = utils.createButton("button", "createIncomeButton", "Add income", ["btn", "btn-primary"]);
        incomesDiv.appendChild(createIncomeButton);


        var length;
        if (Object.keys(data).length === 0) {
            length = 0;
        }
        else {
            length = data["collaborativePiggyBankIncomes"].length;
        }
        for (var index = 0; index < length; index++) {
            income = document.createElement("div");
            incomeId = data["collaborativePiggyBankIncomes"][index]["incomeId"];
            sum = data["collaborativePiggyBankIncomes"][index]["allocatedIncomeAmount"];
            incomeName = data["collaborativePiggyBankIncomes"][index]["incomeName"];

            income.setAttribute("incomeId", incomeId);
            income.setAttribute("incomeSum", sum);
            income.setAttribute("incomeName", incomeName);

            income.textContent = `${incomeName} :  ${sum}\$`;
            income.classList.add("newCollabIncomeDiv");
            incomesDiv.appendChild(income);

            closeButton = document.createElement("span");
            closeButton.innerHTML = "x";
            income.appendChild(closeButton);
            closeButton.classList.add("close");

            closeButton.addEventListener("click", function (e) {

                var closeButton = e.target;
                
                incomesDiv.removeChild(income);
            });

        }


        createIncomeButton.addEventListener('click', function (e) {

            incomeForm = utils.createForm("addIncomeForm");
      
            var selectIncome = utils.createSelect(incomeForm, incomes, "", "Choose associated incomes", "incomesForPiggyBank");
            selectIncome.classList.add("col-md-4");
            var sum = utils.createInput(incomeForm, "", "Income Sum To Allocate:", "allocatedIncomeSum", "text");
            sum.parentElement.classList.add("col-md-4");

            var addIncomeButton = utils.createButton("submit", "", "✓", ["btn", "btn-primary"]);
            var cancelAddIncome = utils.createButton("button", "", "X", ["btn", "btn-cancel"]);

            var divIncomeButtons = document.createElement("div");
            divIncomeButtons.appendChild(cancelAddIncome);
            divIncomeButtons.appendChild(addIncomeButton);

            incomeForm.appendChild(divIncomeButtons);
            incomesDiv.appendChild(incomeForm);

            cancelAddIncome.addEventListener('click', function (e) {
                incomesDiv.removeChild(incomeForm);
            });

            addIncomeButton.addEventListener('click', function (e) {
                e.preventDefault();

                var newIncomeDiv = document.createElement("div");
                newIncomeDiv.classList.add("newCollabIncomeDiv");

                incomeValue = document.getElementById("incomesForPiggyBank").value;

                for (var index = 0; index < incomes.length; index++) {
                    if (incomes[index]["Value"] == incomeValue) {
                        incomeName = incomes[index]["Text"];
                        newIncomeDiv.setAttribute("incomeName", incomeName);
                        newIncomeDiv.setAttribute("incomeId", incomeValue);
                        break;
                    }
                }

                var incomeSum = document.getElementById("allocatedIncomeSum").value;
                newIncomeDiv.setAttribute("incomeSum", incomeSum);
                newIncomeDiv.textContent = `${incomeName} :   ${incomeSum}\$`;
                incomesDiv.appendChild(newIncomeDiv);

                closeButton = document.createElement("span");
                newIncomeDiv.appendChild(closeButton);
                closeButton.classList.add("close");
                closeButton.innerHTML = "x";

                closeButton.addEventListener("click", function (e) {

                    var closeButton = e.target;
                    var incomeDiv = closeButton.parentElement;
                    incomesDiv.removeChild(incomeDiv);
                });

                incomesDiv.removeChild(incomeForm);

            });


        });




    var divButtons = document.createElement("div");
    var submitButton = utils.createButton("submit", "submitPiggyBankButton", "Submit", ["btn", "btn-primary"]);
    var cancelButton = utils.createButton("button", "cancelPiggyBankButton", "Cancel", ["btn", "btn-dark"]);
    cancelButton.setAttribute('data-dismiss', 'modal');


    divButtons.appendChild(cancelButton);
    divButtons.appendChild(submitButton);
    divButtons.id = "collabButtons";

    parentDiv.appendChild(divButtons);

    submitButton.addEventListener('click', function (e) {
        e.preventDefault();

        model = {}
        model.Id = id;


        var associatedIncomes = [];
        var incomes = document.getElementsByClassName("newCollabIncomeDiv");


        for (var i = 0; i < incomes.length; i++) {
            var sum = incomes[i].getAttribute("incomeSum");
            if (!sum) {
                sum = 0;
            }
            var incomeId = incomes[i].getAttribute("incomeId");

            associatedIncomes.push({
                AllocatedIncomeAmount: sum,
                IncomeId: incomeId,
                PiggyBankId: id
            });
        }

        if (incomes.length) {
            model.CollaborativePiggyBankIncomes = associatedIncomes;
        }

        postPiggyBankData(model);
    });

    
    }


    function postPiggyBankData(model) {
        var errorsDiv = document.getElementById("errors");
        if (!errorsDiv) {
            errorsDiv = document.createElement("div");
            errorsDiv.id = "errors";
        }
        else {
            errorsDiv.innerHTML = "";
        }
        fetch(`/PiggyBank/EditCollaborativePiggyBank`, {
            method: "post",
            body: JSON.stringify(model),
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(response => response.json())
            .then(res => {
                if (res && res.errors && res.errors.length > 0) {

                    createDiv.insertBefore(errorsDiv, form);
                    res.errors.forEach(err => {
                        newError = document.createElement("div");
                        newError.classList.add("text-danger");
                        newError.textContent = err.errorMessage;
                        errorsDiv.appendChild(newError);
                    });

                } else {
                    location.reload();
                }
            });
    }



}