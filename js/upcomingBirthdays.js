fetch(`/User/GetUpcomingBirthdays`, {
    method: "get",
})
    .then(response => response.json())
    .then(data => {

        var container = document.getElementById("friendsContainer");

        if (Object.keys(data).length != 0) {

            title = document.createElement("div");
            title.innerHTML = "Upcoming birthdays";
            container.appendChild(title);
            title.id = "birthdaysTitle";
            var icon = document.createElement("span");
            icon.classList.add("bi");
            icon.classList.add("bi-balloon-fill");  

            var title2 = document.createElement("div");
            title2.innerHTML = "Consider saving some money for their presents";
            container.appendChild(title2);
            title2.id = "birthdaysTitle2";
            var icon2 = document.createElement("span");
            icon2.classList.add("bi");
            icon2.classList.add("bi-gift-fill");
            title2.appendChild(icon2);


            for (index = 0; index < data.length; index++) {
                div = document.createElement("div");
                var name = document.createElement("span");
                name.classList.add("birthday");
                name.textContent = data[index]["username"];          
                div.appendChild(name);
                name.appendChild(icon);
                container.appendChild(div);
            }

           

        }

    });
   