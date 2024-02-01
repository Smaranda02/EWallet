function addFriend() {
    var username = document.getElementById("addFriendInput").value;

    fetch(`/User/AddFriend`, {
        method: "post",
        body: JSON.stringify(username),
        headers: {
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(res => {
            if (res && res.length > 0) {
                var errorsDiv = document.getElementById("usernameError");
                errorsDiv.innerHTML = res[0].errorMessage;              
                }

            else {
                location.reload();
                
            }
        })
}