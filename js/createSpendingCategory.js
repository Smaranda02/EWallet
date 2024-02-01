function createSpendingCategory() {


    var modalForm = document.getElementById("createSpendingCategoryForm");
    editDiv = document.getElementById("createSpendingCategoryDiv");
    if (modalForm)
        editDiv.removeChild(modalForm);

    form = utils.createForm("createSpendingCategoryForm");
    form.classList.add("col-md-6");


    utils.createInput(form, "", "Spending Category:", "createSpendingCategory","text");
   

    var divButtons = document.createElement("div");
    divButtons.id = "spendingCategoryButtons";

    submitButton = utils.createButton("submit", "submitSpendingCategoryButton", "Submit", ["btn", "btn-primary"]);
    var cancelButton = utils.createButton("button", "cancelSpendingCategoryButton", "Cancel", ["btn", "btn-cancel"]);
    cancelButton.setAttribute('data-dismiss', 'modal');

    createDiv = document.getElementById("createSpendingCategoryDiv");
    var formDiv = document.createElement("div");
    formDiv.appendChild(form);
    formDiv.id = "spendingCategoryFormDiv";
    createDiv.appendChild(formDiv);

    divButtons.appendChild(cancelButton);
    divButtons.appendChild(submitButton);
    editDiv.appendChild(divButtons);

    submitButton.addEventListener('click', function (e) {
        e.preventDefault();
        var data = {
            CategoryName: document.getElementById("createSpendingCategory").value
        };
        postCreatedSpendingCategoryData(data);

    });

 



    var postCreatedSpendingCategoryData = (model) => {
        fetch(`/Spending/CreateSpendingCategory`, {
            method: "post",
            body: JSON.stringify(model),
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(response => response.json())
            .then(res => {
                if (res && res.errors && res.errors.length > 0) {
                    var errorsDiv = document.createElement("div");
                    errorsDiv.id = "errors";
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

    };


}