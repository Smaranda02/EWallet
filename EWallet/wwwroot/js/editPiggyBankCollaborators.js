function editCollaborators(id) {

    createForm(id);
    getCollaborators(id);


}


function getCollaborators(id) {

    fetch(`GetCollaborators?piggyBankId=${id}`, {
        method: "get",
    })
        .then(response => response.json())
        .then(collabs => {
            addCollabs(collabs,id);
        });
}

function addCollabs(collaborators, id) {

    addDiv = document.getElementById("addFriendModalDiv");
    var oldCollaborators = document.getElementById("currentCollaborators");
    if (oldCollaborators) {
        addDiv.removeChild(oldCollaborators);
    }
    var existsTitle = false;
    if (document.getElementById("collabTitle")) {
        var existsTitle = true;
    }
    var currentCollaborators = document.createElement("div");
    addDiv.insertBefore(currentCollaborators, addDiv.firstChild);
    currentCollaborators.id = "currentCollaborators";
    if (existsTitle == false) {
        var title = document.createElement("span");
        title.innerHTML = "Your collaborators";
        title.id = "collabTitle";
        currentCollaborators.appendChild(title);
    }
   


    collaborators.forEach(c => {

        var name = document.createElement("div");
        name.classList.add("collaborators");
        name.innerHTML = c["username"];
        name.setAttribute("userId", c["userId"]);
        var close = document.createElement("span");
        close.classList.add("close");
        close.innerHTML = "x";
        name.appendChild(close);
        currentCollaborators.appendChild(name);
        var select = document.getElementById("collabSelect");

        close.addEventListener("click", function (e) {
            select.innerHTML = "";
            var closeButton = e.target;
            var parent = closeButton.parentElement;
            var uid = parent.getAttribute("userid");
            currentCollaborators.removeChild(parent);

            var model = {
                PiggyBankId: id,
                UserId: uid
            };

            postDeleteCollaboration(model);
        });

    })

}


function createForm(id) {

    var addDiv = document.getElementById("addFriendModalDiv");
    addDiv.innerHTML = "";


    form = document.createElement("div");
    form.id = "formCollaborators";
    form.classList.add("col-md-6");

    var input = utils.createInput(form, "", "Search for a collaborator", "searchCollaborator", "text", ["col-md-8"]);
    input.setAttribute("placeholder", "Search a friend...")

    var selectDiv = document.createElement("div");
    selectDiv.classList.add("form-group");
    selectDiv.id = "collabSuggestions";

    var select = document.createElement("select");
    select.classList.add("form-select");
    select.id = "collabSelect";

    selectDiv.appendChild(select);
    form.appendChild(selectDiv);


    var addButton = document.createElement("button");
    addButton.classList.add("btn");
    addButton.classList.add("btn-primary");
    addButton.textContent = "+";
    addButton.id = "addCollab";
    selectDiv.appendChild(addButton);


    var divButtons = document.createElement("div");
    divButtons.id = "cancelCollabButton";
    var cancelButton = utils.createButton("button", "cancelPBFriendButton", "Cancel", ["btn", "btn-cancel"]);
    cancelButton.setAttribute('data-dismiss', 'modal');

    divButtons.appendChild(cancelButton);
    addDiv.appendChild(form);
    addDiv.appendChild(divButtons);


    addButton.onclick = function () {

        var userId = select.value;
        var model = {
            PiggyBankId: id,
            UserId: userId
        };
        postAddCollaboration(model);

    };


    getNonCollaborators(id);
    let timeoutId;
    input.addEventListener("input", function () {

        clearTimeout(timeoutId);
        
        var text = input.value;
        select.innerHTML = "";
        timeoutId = setTimeout(() => {
           getNonCollaborators(id, text);
        }, 3000 )
        
        
    })

}



function getNonCollaborators(id, text="") {

        fetch(`GetNonCollaborators?piggyBankId=${id}&name=${text}`, {
            method: "get",
        })
            .then(resp => resp.json())
            .then(options => {

                var select = document.getElementById("collabSelect");

                for (var index = 0; index < options.length; index++) {
                    option = document.createElement("option");
                    option.value = options[index]["value"];
                    option.textContent = options[index]["text"];
                    select.appendChild(option);

                }

            });
}


function postDeleteCollaboration(model) {

        fetch(`/PiggyBank/DeleteCollaboration`, {
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
                    var modal = document.getElementById("addFriendModalDiv");
                    var form = document.getElementById("formCollaborators");
                    modal.insertBefore(errorsDiv, form);
                    res.errors.forEach(err => {
                        newError = document.createElement("div");
                        newError.classList.add("text-danger");
                        newError.textContent = err.errorMessage;
                        errorsDiv.appendChild(newError);
                    });

                }
                else {
                    getNonCollaborators(model.PiggyBankId);
                }
            });

    };





function postAddCollaboration(model) {

    var errorsDiv = document.getElementById("errors");
    if (!errorsDiv) {
        errorsDiv = document.createElement("div");
        errorsDiv.id = "errors";
    }
    else {
        errorsDiv.innerHTML = "";
    }

        fetch(`/PiggyBank/AddCollaborator`, {
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
                    var modal = document.getElementById("addFriendModalDiv");
                    var form = document.getElementById("formCollaborators");
                    modal.insertBefore(errorsDiv, form);
                    res.errors.forEach(err => {
                        newError = document.createElement("div");
                        newError.classList.add("text-danger");
                        newError.textContent = err.errorMessage;
                        errorsDiv.appendChild(newError);
                    });

                }
                else {
                    getCollaborators(model.PiggyBankId);                   
                }

            });

    }