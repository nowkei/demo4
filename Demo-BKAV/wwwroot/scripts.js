document.addEventListener('DOMContentLoaded', function () {
    fetchStudents();

    const form = document.getElementById('student-form');
    form.addEventListener('submit', function (event) {
        event.preventDefault();
        const name = document.getElementById('student-name').value;
        const age = document.getElementById('student-age').value;
        addStudent({ name, age });
    });

    const updateForm = document.getElementById('update-student-form');
    updateForm.addEventListener('submit', function (event) {
        event.preventDefault();
        const id = document.getElementById('update-student-id').value;
        const name = document.getElementById('update-student-name').value;
        const age = document.getElementById('update-student-age').value;
        updateStudent({ id, name, age });
    });

    const deleteForm = document.getElementById('delete-student-form');
    deleteForm.addEventListener('submit', function (event) {
        event.preventDefault();
        const id = document.getElementById('delete-student-id').value;
        deleteStudent(id);
    });
});

function fetchStudents() {
    fetch('/api/student')
        .then(response => response.json())
        .then(data => {
            const studentList = document.getElementById('student-list');
            studentList.innerHTML = '';
            data.forEach(student => {
                const studentItem = document.createElement('div');
                studentItem.className = 'student-item';
                studentItem.textContent = `${student.idStudent} - ${student.name} - Age: ${student.age}`;
                studentList.appendChild(studentItem);
            });
        })
        .catch(error => console.error('Error fetching students:', error));
}

function addStudent(student) {
    fetch('/api/student', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(student)
    })
        .then(response => response.json())
        .then(() => {
            fetchStudents();
            document.getElementById('student-form').reset();
        })
        .catch(error => console.error('Error adding student:', error));
}

function updateStudent(student) {
    fetch(`/api/student/${student.id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(student)
    })
        .then(() => {
            fetchStudents();
            document.getElementById('update-student-form').reset();
        })
        .catch(error => console.error('Error updating student:', error));
}

function deleteStudent(id) {
    fetch(`/api/student/${id}`, {
        method: 'DELETE'
    })
        .then(() => {
            fetchStudents();
            document.getElementById('delete-student-form').reset();
        })
        .catch(error => console.error('Error deleting student:', error));
}
