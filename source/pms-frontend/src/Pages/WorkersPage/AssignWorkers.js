import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Checkbox, FormControlLabel, FormGroup, Button, Typography } from '@mui/material';
import axios from 'axios';
import AuthService from '../../Services/AuthService';

const AssignWorkers = () => {
  const { projectId, taskId } = useParams();
  const navigate = useNavigate();
  const [users, setUsers] = useState([]);
  const [selectedUsers, setSelectedUsers] = useState(new Set());
  const token = AuthService.getToken();

  useEffect(() => {
    axios.get('https://lionfish-app-f5xc6.ondigitalocean.app/api/getUsers')
      .then(response => {
        setUsers(response.data);
      })
      .catch(error => {
        console.error('Error fetching users:', error);
      });
  }, []);

  const handleSelectUser = (username) => {
    const newSelectedUsers = new Set(selectedUsers);
    if (newSelectedUsers.has(username)) {
      newSelectedUsers.delete(username);
    } else {
      newSelectedUsers.add(username);
    }
    setSelectedUsers(newSelectedUsers);
  };

  const handleSubmit = async () => {
    try {
      for (const username of selectedUsers) {
        const user = users.find(u => u.username === username);
        const userData = { FirstName: user.firstName, LastName: user.lastName, Username: user.username };
  
        await axios.post(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}/tasks/${taskId}/workers`, userData, {
          headers: {
            Authorization: `Bearer ${token}` // Replace YOUR_TOKEN with the actual token
          }
        });
      }
  
      navigate(`/projects/${projectId}/tasks/${taskId}`);
    } catch (error) {
      console.error('Error assigning workers:', error);
      // Handle the error here. For example, show an error message
    }
  };

  return (
    <Container maxWidth="sm" className='container'>
      <Typography variant="h4">Assign Workers</Typography>
      <FormGroup>
        {users.map(user => (
          <FormControlLabel
            key={user.username}
            control={<Checkbox onChange={() => handleSelectUser(user.username)} />}
            label={`${user.firstName} ${user.lastName}`}
          />
        ))}
      </FormGroup>
      <Button variant="contained" color="primary" onClick={handleSubmit}>
        Assign Selected Workers
      </Button>
    </Container>
  );
};

export default AssignWorkers;
