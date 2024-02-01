var pageNumber = 1;
var itemNumber = 5;
function loadContent(recurrenceTypes, spendingCategories) {
    var container = document.getElementById("spendingsContainer");
    var parent = container.parentElement;
    if (container.scrollTop >= parent.clientHeight - container.clientHeight) {
        loading = true;
         pageNumber++;
        fetch(`/Spending/Index?pageNumber=${pageNumber}`, {
            method: "get",
        })
            .then(response => response.json())
            .then(data => {

                allData = data;
                data = data["spendingViewModels"];
                for (let  index = 0; index < data.length; index++) {

                    itemNumber++;

                    spendingDiv = utils.createDiv(container, "", ["spendingContainer"]);
                    amount = utils.createDiv(spendingDiv, "", ["spendingAmount"]);
                    amountSpan = utils.createSpan(amount, `-${data[index]["amount"]}\$`, ["spendingAmountSpan"]);

                    description = utils.createDiv(spendingDiv,"", ["spendingDescription"]);
                    descriptionSpan = utils.createSpan(description, data[index]["spendingDescription"]);

                    category = utils.createDiv(spendingDiv, "Category : ");
                    categorySpan = utils.createSpan(category, data[index]["spendingCategoryName"]);

                    recurrence = utils.createDiv(spendingDiv, "Recurrence : ");
                    if (data[index]["recurrenceTypeId"] != null) {
                        recurrenceSpan = utils.createSpan(recurrence, data[index]["recurrenceTypeId"]);
                    }

                    else {
                        recurrenceSpan = utils.createSpan(recurrence, "One Time");

                    }



                    var spendingInfo = document.createElement("div");
                    spendingInfo.classList.add("spendingInfo");
                    spendingDiv.appendChild(spendingInfo);

                    var spendingDetails = document.createElement("div");
                    spendingDetails.classList.add("spendingDetails");
                    spendingDiv.appendChild(spendingDetails);
                    spendingDetails.appendChild(category);
                    spendingDetails.appendChild(recurrence);


                    spendingInfo.appendChild(description);
                    spendingInfo.appendChild(amount);


                    buttonsDiv = document.createElement("div");
                    buttonsDiv.classList.add("spendingButtons");

                    //DELETE
                    deleteDiv = document.createElement("div");
                    deleteDiv.classList.add("deleteButtonDiv");
                    deleteButton = utils.createButton("button", `delete${index}`, "", ["btn", "btn-primary", "deleteButton"],
                        attributes = [
                        {
                            "key": "data-toggle",
                            "value" : "modal"
                        },
                        {
                            "key": "data-target",
                            "value": ".deleteSpending"
                        }
                    ]);

                    deleteDiv.appendChild(deleteButton);

                    deleteButton.addEventListener("click", function () {
                        getSpendingDataToDelete(data[index]["id"]);
                    });
                    var trashIcon = document.createElement("span");
                    trashIcon.classList.add("bi");
                    trashIcon.classList.add("bi-trash");

                    deleteButton.appendChild(trashIcon);
                    buttonsDiv.appendChild(deleteDiv);                   



                    //EDIT 
                    editDiv = document.createElement("div");
                    editDiv.classList.add("editButtonDiv");   
                    editButton = utils.createButton("button","", "", ["btn", "btn-primary", "editButton"],

                        attributes = [
                            {
                                "key": "data-toggle",
                                "value": "modal"
                            },
                            {
                                "key": "data-target",
                                "value": ".createOrEditSpending"
                            }
                        ]);

                    editDiv.appendChild(editButton);
                    editButton.addEventListener("click",() => {
                        createOrEditSpending(recurrenceTypes, spendingCategories, data[index].id);
                    });

                    penIcon = document.createElement("span");
                    penIcon.classList.add("bi");
                    penIcon.classList.add("bi-pencil-fill");

                    editButton.appendChild(penIcon);

                    buttonsDiv.appendChild(editDiv);

                    spendingDiv.appendChild(buttonsDiv);



                }

                loading = false;
            })
            .catch(error => {
                console.error("Error fetching data:", error);
                loading = false;
            });
    }
}
