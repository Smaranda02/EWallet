function getSpendingDataToDelete(id) {
    var modalForm = document.getElementById("deleteForm");
    deleteDiv_ = document.getElementById("deleteDiv")
    if (deleteDiv_)
        modalForm.removeChild(deleteDiv_);
    fetch(`Delete?id=${id}`, {
        method: "get",
    })
        .then(response => response.json())
        .then(data => {
            ConfirmDeleteSpendingForm(data, id)
        })
}

function ConfirmDeleteSpendingForm(data, id) {

    createButton = (type, id, text, classes) => {
        var button = document.createElement("button");
        button.type = type;
        button.id = id;
        button.textContent = text;
        for (var index = 0; index < classes.length; index++) {
            button.classList.add(classes[index]);
        }

        return button;
    }

    var createDiv = (parent, divText) => {
        var div = document.createElement("div");
        div.innerHTML = divText;
        div.classList.add("form-control");
        parent.appendChild(div);
    }

    var divButtons = document.createElement("div");
    divButtons.id = "deleteDiv";
    divButtons.classList.add("modal-footer");
    divButtons.classList.add("form-group");


    submitButton = createButton("submit", "submitDeleteButton", "Submit", ["btn", "btn-primary"]);
    var cancelButton = createButton("button", "cancelDeleteButton", "Cancel", ["btn", "btn-cancel"]);  
    cancelButton.setAttribute('data-dismiss', 'modal');

    submitButton.addEventListener('click', function (e) {
        e.preventDefault();
        postDeletedSpending(id);
    });


    divButtons.appendChild(cancelButton);
    divButtons.appendChild(submitButton);

    form = document.getElementById("deleteForm");
    form.appendChild(divButtons);
  
    
    var postDeletedSpending = (id) => {
        fetch(`/Spending/Delete?id=${id}`, {
            method: "post",
           
        }).then(location.reload())

    }
}