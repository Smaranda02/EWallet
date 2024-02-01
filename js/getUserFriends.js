fetch(`/User/GetFriends`, {
    method: "get",
})
    .then(response => response.json())
    .then(data => {

        parent = document.getElementById("friends");
        title = document.createElement("div");
        parent.appendChild(title);
        title.id = "friendsTitle";
        


        if (data.length==0) {          
            title.innerHTML = "Think about adding some friends to your list (❁´◡`❁)";     
            parent.appendChild(title);
        }

        else {
            var container = document.getElementById("friendsList");
            title.innerHTML = "Your friends";
            var icon = document.createElement("span");
            icon.classList.add("bi");
            icon.classList.add("bi-person-fill");
            title.appendChild(icon);
            container.appendChild(title);
           
            for (index = 0; index < data.length; index++) {
                div = document.createElement("div");
                friendName = document.createElement("span");
                friendName.classList.add("friendName");
                friendName.innerHTML = data[index].username;
                div.appendChild(friendName);
                container.appendChild(div);
            }
        }
        
        
    })