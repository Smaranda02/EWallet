function getPiggyBankToBreak(id) {
    var modalForm = document.getElementById("breakPiggyBankForm");
    breakDiv_ = document.getElementById("breakPiggyBankDiv")
    if (breakDiv_)
        modalForm.removeChild(breakDiv_);
    fetch(`Break?id=${id}`, {
        method: "get",
    })
        .then(response => response.json())
        .then(data => {
            ConfirmBreakPiggyBankForm(data, id)
        })
}

function ConfirmBreakPiggyBankForm(data, id) {

    var divButtons = document.createElement("div");
    divButtons.id = "breakPiggyBankDiv";
    divButtons.classList.add("modal-footer");
    divButtons.classList.add("form-group");


    submitButton = utils.createButton("submit", "submitBreakPiggyBankButton", "Submit", ["btn", "btn-primary"]);
    var cancelButton = utils.createButton("button", "cancelBreakPiggyBankButton", "Cancel", ["btn", "btn-cancel"]);
    cancelButton.setAttribute('data-dismiss', 'modal');

    submitButton.addEventListener('click', function (e) {
        e.preventDefault();
        postBrokenPiggyBank(id);
    });


    divButtons.appendChild(cancelButton);
    divButtons.appendChild(submitButton);

    form = document.getElementById("breakPiggyBankForm");
    form.appendChild(divButtons);


    var postBrokenPiggyBank = (id) => {
        fetch(`/PiggyBank/Break?id=${id}`, {
            method: "post",


        }).then(location.reload())
    }
}