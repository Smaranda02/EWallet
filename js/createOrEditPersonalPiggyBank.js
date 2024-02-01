function createOrEditPiggyBank(id, incomes) {
    if (id) {
        fetch(`Edit?id=${id}`, {
            method: "get",
        })
            .then(response => response.json())
            .then(data => {

                createPiggyBankForm(data, {}, "/PiggyBank/Edit", id, incomes);
            })
    } else {
        createPiggyBankForm({}, {}, "/PiggyBank/Create", "", incomes);
    }


    function createPiggyBankForm(data, attributes, postDataPath, id, incomes) {

        var modalForm = document.getElementById("piggyBankForm");
        var editDiv = document.getElementById("createOrEditPiggyBankDiv");
        pbButtons = document.getElementById("pbButtons");

        if (modalForm) {
            editDiv.removeChild(modalForm);
        }
        var incomeForm = document.getElementById("incomeDetails");
        if (incomeForm) {
            incomeForm.innerHTML = "";
        }

        if (pbButtons) {
            editDiv.parentElement.removeChild(pbButtons);
        }

        var errorsDiv = document.getElementById("errors");
        if (errorsDiv) {
            errorsDiv.innerHTML = "";
        }
       

        form = utils.createForm("piggyBankForm", attributes, ["backgroundColor"]);

        var date;
        if (data["dueDate"]) {
            date = data["dueDate"];
            date = date.split("T")[0];
        }
        else date = "";
        utils.createInput(form, data["targetSum"] ?? "", "Target Sum:", "piggyBankAmount", "text", ["col-md-3"]);
        utils.createInput(form, data["piggyBankDescription"] ?? "", "Description:", "piggyBankDescription", "text", ["col-md-6"]);
        utils.createInput(form, date, "Due Date:", "piggyBankDueDate", "date", ["col-md-4"]);
        utils.createInput(form, data["savingPriority"] ?? "", "Saving Priority:", "piggyBankPriority", "number" , ["col-md-2"]);

        var incomesDiv = document.getElementById("incomeDetails");
        var incomesTitle = utils.createDiv(incomesDiv, "Income details");
        incomesTitle.id = "incomeDetailsTitle";



        createIncomeButton = utils.createButton("button", "createIncomeButton", "Add income", ["btn", "btn-primary"]);
        incomesDiv.appendChild(createIncomeButton);

        var associatedIncomes = document.createElement("div");
        incomesDiv.appendChild(associatedIncomes);
        associatedIncomes.id = "associatedIncomes";

        var length;
        if (Object.keys(data).length === 0) {
            length = 0;  
        }
        else {
            length = data["piggyBanksIncomes"].length;
        }
        for (var index = 0; index < length; index++) {
            income = document.createElement("div");
            income.id = "pbIncomeDiv";
            incomeId = data["piggyBanksIncomes"][index]["incomeId"];
            sum = data["piggyBanksIncomes"][index]["allocatedIncomeAmount"];
            incomeName = data["piggyBanksIncomes"][index]["incomeName"];

            income.setAttribute("incomeId", incomeId);
            income.setAttribute("incomeSum", sum);
            income.setAttribute("incomeName", incomeName);

            income.textContent = `${incomeName} :  ${sum}$`;
            income.classList.add("newIncomeDiv");
            associatedIncomes.appendChild(income);

            closeButton = document.createElement("span");
            closeButton.innerHTML = "x";
            income.appendChild(closeButton);
            closeButton.classList.add("close");

            closeButton.addEventListener("click", function (e) {

                var closeButton = e.target;
                var incomeDiv = closeButton.parentElement;
                associatedIncomes.removeChild(incomeDiv);
            });

        }


        createIncomeButton.addEventListener('click', function (e) {
            if (!document.getElementById("addIncomeForm")) {
                //incomeForm = utils.createForm("addIncomeForm");
                incomeForm = document.createElement("div");
                incomeForm.id = "addIncomeForm";
                var selectIncome = utils.createSelect(incomeForm, incomes, "", "Choose associated incomes", "incomesForPiggyBank");
                var sum = utils.createInput(incomeForm, "", "Income Sum To Allocate:", "allocatedIncomeSum", "text");
                selectIncome.id = "incomesPB";
                sum.parentElement.id = "incomeSumPB";

                var addIncomeButton = utils.createButton("submit", "", "✓", ["btn", "btn-primary"]);
                var cancelAddIncome = utils.createButton("button", "", "X", ["btn", "btn-cancel"]);
                cancelAddIncome.id = "cancelIncomePB";

                var divIncomeButtons = document.createElement("div");
                divIncomeButtons.appendChild(cancelAddIncome);
                divIncomeButtons.appendChild(addIncomeButton);
                divIncomeButtons.id = "incomeButtonsPB";

                incomeForm.appendChild(divIncomeButtons);
                incomesDiv.appendChild(incomeForm);

                cancelAddIncome.addEventListener('click', function (e) {
                    incomesDiv.removeChild(incomeForm);
                });

                addIncomeButton.addEventListener('click', function (e) {
                    e.preventDefault();

                    var newIncomeDiv = document.createElement("div");
                    newIncomeDiv.classList.add("newIncomeDiv");

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
                    associatedIncomes.appendChild(newIncomeDiv);

                    closeButton = document.createElement("span");
                    newIncomeDiv.appendChild(closeButton);
                    closeButton.classList.add("close");
                    closeButton.innerHTML = "x";

                    closeButton.addEventListener("click", function (e) {

                        var closeButton = e.target;
                        var incomeDiv = closeButton.parentElement;
                        associatedIncomes.removeChild(incomeDiv);
                    });

                    incomesDiv.removeChild(incomeForm);

                });
            }

        });


        var divButtons = document.createElement("div");

        var submitButton = utils.createButton("submit", "submitPiggyBankButton", "Submit", ["btn", "btn-primary"]);
        submitButton.addEventListener('click', function (e) {
            e.preventDefault();

            var amount = document.getElementById("piggyBankAmount").value;
            if (!amount) {
                amount = 0.00;
            }
            var description = document.getElementById("piggyBankDescription").value;
           
            var date = document.getElementById("piggyBankDueDate").value;
            if (!date) {
                date = new Date(0);
            }
           
            var priority = document.getElementById("piggyBankPriority").value;
            if (!priority) {
                priority = 0;
            }

            var newData = {};
            if (id) {
                newData.Id = id;
            }


            var creatorId = data.creatorId;
        
            newData.TargetSum = amount,
            newData.PiggyBankDescription = description;
            newData.DueDate = date;
            newData.SavingPriority = priority;
            newData.CreatorId = creatorId;


            var associatedIncomes = [];
            var incomes = document.getElementsByClassName("newIncomeDiv");


            var pbId;
            if (id) {
                pbId = id;
            }

            for (var i = 0; i < incomes.length; i++) {
                var sum = incomes[i].getAttribute("incomeSum");
                if (!sum) {
                    sum = 0;
                }
                var incomeId = incomes[i].getAttribute("incomeId");

                associatedIncomes.push({
                    AllocatedIncomeAmount: sum,
                    IncomeId: incomeId,
                    PiggyBankId:pbId
                });
            }

            if (incomes.length) {
                newData.PiggyBanksIncomes = associatedIncomes;
            }

            postPiggyBankData(newData, postDataPath);
        });

        var cancelButton = utils.createButton("button", "cancelPiggyBankButton", "Cancel", ["btn", "btn-dark"]);
        cancelButton.setAttribute('data-dismiss', 'modal');


        divButtons.appendChild(cancelButton);
        divButtons.appendChild(submitButton);
        divButtons.id = "pbButtons";

        createDiv = document.getElementById("createOrEditPiggyBankDiv")
        createDiv.appendChild(form);
        createDiv.parentElement.appendChild(divButtons);

    }


    function postPiggyBankData(model, path) {
        var errorsDiv = document.getElementById("errors");
        if (!errorsDiv) {
            errorsDiv = document.createElement("div");
            errorsDiv.id = "errors";
        }
        else {
            errorsDiv.innerHTML = "";
        }
        fetch(path, {
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