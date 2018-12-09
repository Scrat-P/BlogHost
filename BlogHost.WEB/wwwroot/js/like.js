"use strict";

const likeButton = document.getElementById("likeButton");
let isLiked = $("#isLiked").val() === "True" ? true : false;

if (isLiked) {
    likeButton.classList.add("text-danger");
} else {
    likeButton.classList.add("text-info");
}

likeHubConnection.on("Like",
    function (postId, isPostLiked) {
        if (postId !== parseInt($("#PostId").val())) return;
        const countElement = document.getElementById("likesCountTop");
        const count = parseInt(countElement.innerHTML);

        if (isPostLiked) {
            countElement.innerHTML = count - 1;
            likeButton.classList.remove("text-danger");
            likeButton.classList.add("text-info");
        } else {
            countElement.innerHTML = count + 1;
            likeButton.classList.remove("text-info");
            likeButton.classList.add("text-danger");
        }
    });

likeButton.addEventListener("click",
    function (event) {
        const id = parseInt($("#PostId").val());
        likeHubConnection.invoke("Like", id, isLiked);
        isLiked = !isLiked;
        event.preventDefault();
    });