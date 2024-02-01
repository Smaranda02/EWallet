function getPiggyBankDataToDelete(id) {
    var modal = document.getElementById("deletePersonalPb");
    var _deleteDiv = document.getElementById("deletePiggyBankDiv");
    if (_deleteDiv)
        modal.removeChild(_deleteDiv);
    fetch(`Delete?id=${id}`, {
        method: "get",
    })
        .then(response => response.json())
        .then(data => {
            ConfirmDeletePersonalPiggyBankForm(data, id)
        })
}

function ConfirmDeletePersonalPiggyBankForm(data, id) {

    var divButtons = document.createElement("div");
    divButtons.id = "deletePiggyBankDiv";
    divButtons.classList.add("form-group");


    submitButton = utils.createButton("submit", "submitDeletePiggyBankButton", "Submit", ["btn", "btn-primary"]);
    var cancelButton = utils.createButton("button", "cancelDeletePiggyBankButton", "Cancel", ["btn", "btn-cancel"]);
    cancelButton.setAttribute('data-dismiss', 'modal');

    submitButton.addEventListener('click', function (e) {
        e.preventDefault();
        postDeletedPiggyBank(id);
    });


    divButtons.appendChild(cancelButton);
    divButtons.appendChild(submitButton);

    form = document.getElementById("deletePersonalPb");
    form.appendChild(divButtons);


    var postDeletedPiggyBank = (id) => {
        fetch(`/PiggyBank/Delete?id=${id}`, {
            method: "post",

         
        }).then(response => response.json())
            .then(res => {
                if (res && res.errors && res.errors.length > 0) {

                    //createDiv.insertBefore(errorsDiv, form);
                    //res.errors.forEach(err => {
                    //    newError = document.createElement("div");
                    //    newError.classList.add("text-danger");
                    //    newError.textContent = err.errorMessage;
                    //    errorsDiv.appendChild(newError);
                    //});

                } else {
                    location.reload();
                }
            });
    }
}