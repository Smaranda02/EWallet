function createOrEditSpending(reccurenceTypes, spendingCategories, id) {
    if (id) {
        fetch(`Edit?id=${id}`, {
            method: "get",
        })
            .then(response => response.json())
            .then(data => {

                createIncomeForm(data, reccurenceTypes, spendingCategories, {}, "/Spending/Edit",id);
            })
    } else {
        createIncomeForm({}, reccurenceTypes, spendingCategories, {}, "/Spending/Create");
    }

    function createIncomeForm(data, recurrenceTypes, spendingCategories, attributes, postDataPath,id) {

        var editDiv = document.getElementById("createOrEditSpendingDiv")
        editDiv.innerHTML = "";

        var title = document.createElement("div");
        title.classList.add("spendingTitle");
        if (!id) {
            title.innerHTML = "Create your new spending";
        }
        else {
            title.innerHTML = "Edit your spending";
        }
        editDiv.appendChild(title);

        form = utils.createForm("spendingForm", attributes, ["backgroundColor"])

        utils.createInput(form, data["amount"] ?? "", "Spending Amount:", "spendingAmount", "text", ["col-md-5"]);
        utils.createInput(form, data["spendingDescription"] ?? "", "Description:", "spendingDescription", "text", ["col-md-5"]);
        utils.createSelect(form, spendingCategories, data["spendingCategoryId"] ?? "", "Spending Categories", "spendingCategories", ["col-md-5"]);
        utils.createSelect(form, recurrenceTypes, data["recurrenceTypeId"] ?? "", "Recurrence Type:", "recurrenceTypes", ["col-md-5"]);
        utils.createInput(form, data["recurringNumber"] ?? "", "Day:", "spendingRecurrenceNumber", "number", ["col-md-5"]);


        var divButtons = document.createElement("div");
        divButtons.classList.add("modal-footer");

        var submitButton = utils.createButton("submit", "submitSpendingButton", "Submit", ["btn", "btn-primary"]);
        submitButton.addEventListener('click', function (e) {
            e.preventDefault();

            var recurrenceTypeId, recurrenceName, recurringNumber;
            recurrenceTypeId = document.getElementById("recurrenceTypes").value;
            recurringNumber = document.getElementById("spendingRecurrenceNumber").value;
            if (recurrenceTypeId == 0) {
                recurrenceTypeId = null;
                recurringNumber = null;
                recurrenceName = "One Time"
            }


            var spendingCategoryId;
            spendingCategoryId = document.getElementById("spendingCategories").value;


            var amount = document.getElementById("spendingAmount").value;
            if (!amount) {
                amount = 0.00;
            }

            var model = {
                
                Amount: amount,
                SpendingDescription: document.getElementById("spendingDescription").value,
                SpendingCategoryId: spendingCategoryId,
                RecurrenceTypeId: recurrenceTypeId,
                RecurringNumber: recurringNumber,
                RecurrenceTypeName: recurrenceName

            };

            if (id)
                model.Id=id

            postSpendingData(model, postDataPath);
        });

        var cancelButton = utils.createButton("button", "cancelSpendingButton", "Cancel", ["btn", "btn-cancel"]);
        cancelButton.setAttribute('data-dismiss', 'modal');


        divButtons.appendChild(cancelButton);
        divButtons.appendChild(submitButton);
        form.appendChild(divButtons);

        createDiv = document.getElementById("createOrEditSpendingDiv")
        createDiv.appendChild(form);
    }


    function postSpendingData(model, path) {
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
