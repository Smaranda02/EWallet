function createOrEditIncome(reccurenceTypes, id) {
    if (id) {
        fetch(`Edit?id=${id}`, {
            method: "get",
        })
            .then(response => response.json())
            .then(data => {

                createIncomeForm(data, reccurenceTypes, {}, "/Income/Edit", id);
            })
    } else {
        createIncomeForm({}, reccurenceTypes, {}, "/Income/Create");
    }

    function createIncomeForm(data, recurrenceTypes, attributes, postDataPath, id) {

        editDiv = document.getElementById("incomeDiv")
        editDiv.innerHTML = "";

        var title = document.createElement("div");
        title.classList.add("spendingTitle");
        if (!id) {
            title.innerHTML = "Create your new income";
        }
        else {
            title.innerHTML = "Edit your income";
        }
        editDiv.appendChild(title);

        form = utils.createForm("incomeForm", attributes, ["backgroundColor"]);

        utils.createInput(form, data["incomeSum"] ?? "", "Income Amount:", "incomeAmount", "text",["col-md-5"]);
        utils.createInput(form, data["incomeDescription"] ?? "", "Description:", "incomeDescription", "text", ["col-md-5"]);
        utils.createSelect(form, recurrenceTypes, data["recurrenceTypeId"] ?? "", "Recurrence Type:", "recurrenceTypes", ["col-md-5"]);
        utils.createInput(form, data["recurringNumber"] ?? "", "Day:", "incomeRecurrenceNumber", "number", ["col-md-5"]);


        var divButtons = document.createElement("div");
        divButtons.classList.add("modal-footer");

        var submitButton = utils.createButton("submit", "submitIncomeButton", "Submit", ["btn", "btn-primary"]);
        submitButton.addEventListener('click', function (e) {
            e.preventDefault();

            var recurrenceTypeId,recurrenceName,recurringNumber;
            recurrenceTypeId = document.getElementById("recurrenceTypes").value;
            recurringNumber = document.getElementById("incomeRecurrenceNumber").value;
            if (recurrenceTypeId == 0) {
                recurrenceTypeId = null;
                recurringNumber = null;
                recurrenceName="One Time"
            }

            if (!recurringNumber) {
                recurringNumber = null;

            }

            var amount = document.getElementById("incomeAmount").value;
            if (!amount) {
                amount = 0.00;
            }

            var model = {

                IncomeSum: amount,
                IncomeDescription: document.getElementById("incomeDescription").value,
                RecurringNumber: recurringNumber,
                RecurrenceTypeId: recurrenceTypeId,
                RecurrenceTypeName : recurrenceName
            };

            if (id)
                model.Id = id;
            postIncomeData(model, postDataPath);
        });

        var cancelButton = utils.createButton("button", "cancelIncomeButton", "Cancel", ["btn", "btn-cancel"]);
        cancelButton.setAttribute('data-dismiss', 'modal');


        divButtons.appendChild(cancelButton);
        divButtons.appendChild(submitButton);
        form.appendChild(divButtons);

        createDiv = document.getElementById("incomeDiv")


        createDiv.style.backgroundColor = utils.secondaryColor;
        createDiv.appendChild(form);
    }


    function postIncomeData(model, path) {
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

                    