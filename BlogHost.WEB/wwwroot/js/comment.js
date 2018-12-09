"use strict";

function createCommentBody(commentText, userName) {
    const userNameBold = document.createElement("b");
    userNameBold.innerHTML = userName + "&nbsp;";

    const userNameLink = document.createElement("a");
    userNameLink.classList.add("name");
    userNameLink.appendChild(userNameBold);

    const commentDate = document.createElement("h6");
    commentDate.classList.add("date");
    commentDate.innerHTML = "on " + dateFormat(new Date(), "dd.mm.yy HH:MM:ss");

    const infoDiv = document.createElement("div");
    infoDiv.classList.add("pull-left");
    infoDiv.appendChild(userNameLink);
    infoDiv.appendChild(commentDate);

    const postInfoDiv = document.createElement("div");
    postInfoDiv.classList.add("post-info");
    postInfoDiv.appendChild(infoDiv);

    const commentTextParagraph = document.createElement("b");
    commentTextParagraph.innerHTML = commentText;

    const comment = document.createElement("div");
    comment.classList.add("comment");
    comment.appendChild(postInfoDiv);
    comment.appendChild(commentTextParagraph);

    const commentArea = document.createElement("div");
    commentArea.classList.add("commnets-area");
    commentArea.appendChild(comment);

    return commentArea;
}

commentHubConnection.on("Add",
    function (postId, commentText, userName) {
        alert(1)
        if (postId !== parseInt($("#PostId").val())) return;
        alert(2)

        if (isAuthenticated) {
            const commentTextElement = document.getElementById("Text");
            commentTextElement.innerHTML = "";
        }

        const commentCountElement = document.getElementById("commentCount");
        const count = parseInt(commentCountElement.innerHTML) + 1;
        commentCountElement.innerHTML = count;

        const commentCountTopElement = document.getElementById("commentsCountTop");
        const countTop = parseInt(commentCountTopElement.innerHTML) + 1;
        commentCountTopElement.innerHTML = countTop;

        const element = createCommentBody(commentText, userName);

        document.getElementById("commentList").insertBefore(element, document.getElementById("lastComment"));
    }
);

if (isAuthenticated) {
    const addCommentButton = document.getElementById("addCommentButton");
    addCommentButton.addEventListener("click",
        function (event) {
            commentHubConnection.invoke("Add", parseInt($("#PostId").val()), $("#Text").val());
            event.preventDefault();
        }
    );
}
