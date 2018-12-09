"use strict";

const likeButton = document.getElementById("likeButton");
let isLiked = $("#isLiked").val() === "True" ? true : false;
let isAuthenticated = $("#isAuthenticated").val() === "True" ? true : false;

if (isAuthenticated) {
    if (isLiked) {
        likeButton.classList.add("text-danger");
    } 
}

likeHubConnection.on("Like",
    function (postId, isPostLiked) {
        if (postId !== parseInt($("#PostId").val())) return;

        const countElement = document.getElementById("likesCountTop");
        const count = parseInt(countElement.innerHTML);

        if (isPostLiked) {
            countElement.innerHTML = count - 1;
        } else {
            countElement.innerHTML = count + 1;
        }

        if (!isAuthenticated) return;
        if (isPostLiked) {
            likeButton.classList.remove("text-danger");
        } else {
            likeButton.classList.add("text-danger");
        }
    }
);

likeButton.addEventListener("click",
    function (event) {
        if (!isAuthenticated) return;
        const id = parseInt($("#PostId").val());
        likeHubConnection.invoke("Like", id, isLiked);
        isLiked = !isLiked;
        event.preventDefault();
    }
);