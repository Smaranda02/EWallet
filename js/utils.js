var utils = {};
(function () {
    this.createButton = (type, id, text, classes,attributes=[]) => {
        var button = document.createElement("button");
        button.type = type;
        button.id = id;
        button.textContent = text;
        for (var index = 0; index < classes.length; index++) {
            button.classList.add(classes[index]);
        }

        for (var index = 0; index < attributes.length; index++) {
            button.setAttribute(attributes[index]["key"], attributes[index]["value"]);
        }

        return button;
    }

    this.createSpan = (parent, text, classes = []) => {
        var span = document.createElement("span");
        span.innerHTML = text;

        for (var index = 0; index < classes.length; index++) {
            span.classList.add(classes[index]);
        }
        parent.appendChild(span);

    }

    this.createDiv = (parent, divText, classes=[]) => {
        var div = document.createElement("div");
        div.innerHTML = divText;

        for (var index = 0; index < classes.length; index++) {
            div.classList.add(classes[index]);
        }
        parent.appendChild(div);
        return div;
    }

    this.createInput = (parent, value, labelText, id, type,classes=[]) => {

        var input = document.createElement("input");
        input.type = type;
        if (type == "number") {
            input.setAttribute("min", "1");
        }
        input.value = value;
        inputLabel = document.createElement("label");
        inputLabel.textContent = labelText;
        inputLabel.classList.add("control-label");
        input.id = id;
        input.classList.add("form-control");
       
        if (id == "allocatedIncomeSum") {
            inputLabel.id = "assocSum";
        }

        if (id == "createSpendingCategory") {
            inputLabel.id = "categoryLabel";
        }

        if (id == "searchCollaborator") {
            inputLabel.id = "searchCollaboratorLabel";
        }
        inputLabel.setAttribute("for", id);

        for (var index = 0; index < classes.length; index++) {
            input.classList.add(classes[index]);
        }


        var divForInput = document.createElement("div");
        divForInput.classList.add("form-group");

        divForInput.appendChild(inputLabel);
        divForInput.appendChild(input);

        parent.appendChild(divForInput);

        return input;
    }

    this.createDefaultOption = (parent, text,isDisabled) => {
        option = document.createElement("option");
        option.value = 0;
        option.textContent = text;
        option.disabled = isDisabled;
        option.selected = "true";
        parent.appendChild(option);
    }

    this.createIcon = (parent, classes) => {
        var span = document.createElement("span");
        parent.appendChild(span);
        for (var index = 0; index < classes.length; index++) {
            span.classList.add(classes[index]);

        }
    }


    this.createList = (parent, lis, id,classes) => {
        var list = document.createElement("ul");
        parent.appendChild(list);
        list.id = id;

        for (var index = 0; index < lis.length; index++) {
            var option = document.createElement("li");
            list.appendChild(option);
            option.innerHTML = lis[index];

            utils.createIcon(option, classes[index]);
        }


    }


    this.createSelect = (parent, options, selectedValue, labelText, id,classes=[]) => {

        selectDiv = document.createElement("div");
        selectDiv.classList.add("form-group");

        selectLabel = document.createElement("label");
        selectLabel.classList.add("control-label");
        selectLabel.textContent = labelText;
        select = document.createElement("select");
        select.classList.add("form-select");
        select.id = id;
        if (id == "incomesForPiggyBank") {
            selectLabel.id = "assocIncomes"
        }

        if (id == "recurrenceTypes") {  
            this.createDefaultOption(select, "One Time",false);
        }

        else if (id == "spendingCategories") {
            this.createDefaultOption(select, "Choose a category",true);

        }

        else if (id == "piggyBanks") {
            this.createDefaultOption(select, "Choose Piggy Banks",true);
        }

        for (var index = 0; index < options.length; index++) {
            option = document.createElement("option");
            option.value = options[index]["Value"];
            option.textContent = options[index]["Text"];

            if (options[index]["Value"] == selectedValue)
                option.selected = "true";

            select.appendChild(option);
        }

        for (var index = 0; index < classes.length; index++) {
            select.classList.add(classes[index]);
        }


        selectDiv.appendChild(selectLabel);
        selectDiv.appendChild(select);
        parent.appendChild(selectDiv);

        return selectDiv;

    }


    this.createForm = (id, attributes = [], classes = []) => {
        var form = document.createElement("form");

        form.id = id
        for (var index = 0; index < attributes.length; index++) {
            form.setAttribute(attributes[index]["key"], attributes[index]["value"]);
        }

        for (var index = 0; index < classes.length; index++) {
            form.classList.add(classes[index]);
        }


        return form;
    }



}).call(utils);