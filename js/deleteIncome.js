function getIncomeDataToDelete(id) {
    var modalForm = document.getElementById("deleteIncomeForm");
    deleteDiv_ = document.getElementById("deleteIncomeDiv")
    if (deleteDiv_)
        modalForm.removeChild(deleteDiv_);
    fetch(`Delete?id=${id}`, {
        method: "get",
    })
        .then(response => response.json())
        .then(data => {
            ConfirmDeleteIncomeForm(data, id)
        })
}

function ConfirmDeleteIncomeForm(data, id) {

    divButtons = document.createElement("div");
    divButtons.id = "deleteIncomeDiv";
    divButtons.classList.add("modal-footer");
    divButtons.classList.add("delete");

    divButtons.classList.add("form-group");


    submitButton = utils.createButton("submit", "submitDeleteIncomeButton", "Submit", ["btn", "btn-primary"]);
    cancelButton = utils.createButton("button", "cancelDeleteIncomeButton", "Cancel", ["btn", "btn-cancel"]);
    cancelButton.setAttribute('data-dismiss', 'modal');

    submitButton.addEventListener('click', function (e) {
        e.preventDefault();
        postDeletedIncome(id);
    });


    divButtons.appendChild(cancelButton);
    divButtons.appendChild(submitButton);

    form = document.getElementById("deleteIncomeForm");
    form.appendChild(divButtons);


    var postDeletedIncome = (id) => {
        fetch(`/Income/Delete?id=${id}`, {
            method: "post"
            
        }).then(location.reload())

    }
}