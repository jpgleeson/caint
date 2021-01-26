const uri = 'api/Comments';
let todos = [];

function getItems() {
  fetch(uri + "/admin")
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get comments.', error));
}

function getThread(threadId) {
  fetch(uri + "/thread/" + threadId)
    .then(response => response.json())
    .then(data => _displayThread(data))
    .catch(error => console.error('Unable to get comments.', error));
}

function addItem() {
  const commenterNameTextbox = document.getElementById('commenterName');
  const commentBodyTextbox = document.getElementById('commentBody');

  const item = {
    approved: false,
    name: commenterNameTextbox.value.trim(),
    body: commentBodyTextbox.value.trim(),
    threadId: 1,
  };

  fetch(uri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
    .then(response => response.json())
    .then(() => {
      getItems();
      commenterNameTextbox.value = '';
    })
    .catch(error => console.error('Unable to add comment.', error));
}

function deleteItem(id) {
  fetch(`${uri}/${id}`, {
    method: 'DELETE'
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to delete item.', error));
}

function approveItem(id) {
    fetch(`${uri}/admin/approve/${id}`, {
        method: 'POST'
      })
      .then(() => getItems())
      .catch(error => console.error('Unable to approve comments.', error));
}

function displayEditForm(id) {
  const item = todos.find(item => item.id === id);
  
  document.getElementById('edit-name').value = item.name;
  document.getElementById('edit-id').value = item.id;
  document.getElementById('edit-isComplete').checked = item.isComplete;
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const itemId = document.getElementById('edit-id').value;
  const item = {
    id: parseInt(itemId, 10),
    isComplete: document.getElementById('edit-isComplete').checked,
    name: document.getElementById('edit-name').value.trim()
  };

  fetch(`${uri}/${itemId}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to update item.', error));

  closeInput();

  return false;
}

function closeInput() {
  document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
  const name = (itemCount === 1) ? 'comment' : 'comments';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
  const tBody = document.getElementById('comments');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {
    let isCompleteCheckbox = document.createElement('input');
    isCompleteCheckbox.type = 'checkbox';
    isCompleteCheckbox.disabled = true;
    isCompleteCheckbox.checked = item.isComplete;

    let approveButton = button.cloneNode(false);
    approveButton.innerText = 'Approve';
    approveButton.setAttribute('onclick', `approveItem(${item.id})`);

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    td1.appendChild(isCompleteCheckbox);

    let td2 = tr.insertCell(1);
    let threadIdNode = document.createTextNode(item.threadId);
    td2.appendChild(threadIdNode);

    let td3 = tr.insertCell(2);
    let commenterNode = document.createTextNode(item.name);
    td3.appendChild(commenterNode);

    let td4 = tr.insertCell(3);
    let bodyNode = document.createTextNode(item.body);
    td4.appendChild(bodyNode);

    let td5 = tr.insertCell(4);
    td5.appendChild(approveButton);

    let td6 = tr.insertCell(5);
    td6.appendChild(editButton);

    let td7 = tr.insertCell(6);
    td7.appendChild(deleteButton);
  });

  comments = data;
}

function _displayThread(data) {
  const threadBody = document.getElementById('commentThread');
  threadBody.setAttribute('class', 'commentThread');
  threadBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  var x = 0;

  data.forEach(item => {
    var commentDiv = document.createElement('div');
    var commentName = document.createElement('h3');
    var commentBody = document.createElement('p');

    commentDiv.setAttribute('class', 'comment');

    commentName.setAttribute('class', 'commenterName');
    commentBody.setAttribute('class', 'commentBody');

    commentName.innerHTML = item.name;
    commentBody.innerHTML = item.body;

    commentDiv.appendChild(commentName);
    commentDiv.appendChild(commentBody);

    threadBody.appendChild(commentDiv);
  });

  comments = data;
}