function deletePiggyBankCollaboration(id) {
    var modal = document.getElementById("deleteCollab");
    var deleteDiv_ = document.getElementById("deleteCollabDiv");
    if (deleteDiv_) {
        modal.removeChild(deleteDiv_);
    }
    ConfirmDeletePiggyBankForm(id);
}

function ConfirmDeletePiggyBankForm(id) {

    var divButtons = document.createElement("div");
    divButtons.id="deleteCollabDiv";
    divButtons.classList.add("form-group");


    submitButton = utils.createButton("submit", "submitButton", "Submit", ["btn", "btn-primary"]);
    var cancelButton = utils.createButton("button", "cancelButton", "Cancel", ["btn", "btn-cancel"]);
    cancelButton.setAttribute('data-dismiss', 'modal');


    divButtons.appendChild(cancelButton);
    divButtons.appendChild(submitButton);

    var modal = document.getElementById("deleteCollab");
    modal.appendChild(divButtons);


    submitButton.addEventListener('click', function (e) {
        e.preventDefault();
        postDeletedCollab(id);
    });

 


    var postDeletedCollab= (id) => {
        fetch(`/PiggyBank/DeleteCollaborationById?piggyBankId=${id}`, {
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