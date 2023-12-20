import React, { useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import { 
  Container, Typography, TextField, Button, Box
} from '@mui/material';
import AuthService from '../../Services/AuthService';

const TaskCreation = () => {
  const { projectId } = useParams();
  const navigate = useNavigate();
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const token = AuthService.getToken();

  const handleCreateTask = async () => {
    try {
      await axios.post(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}/tasks`, {
        Name: name,
        Description: description
      }, {
        headers: {
          Authorization: 'Bearer ' + token
        }
      });
      // Task created successfully, navigate back to the project details
      navigate(`/projects/${projectId}`);
    } catch (error) {
      console.error('Error creating task:', error);
    }
  };

  return (
    <Container maxWidth="sm" className='container'>
      <Box sx={{ mt: 4 }}>
        <Typography variant="h4" gutterBottom>Create Task</Typography>
        <Box component="form" noValidate sx={{ mt: 2 }}>
          <TextField
            required
            fullWidth
            id="name"
            label="Task Name"
            margin="normal"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <TextField
            required
            fullWidth
            id="description"
            label="Task Description"
            margin="normal"
            multiline
            rows={4}
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
          <Button
            type="button"
            fullWidth
            variant="contained"
            color="primary"
            sx={{ mt: 3, mb: 2 }}
            onClick={handleCreateTask}
          >
            Create Task
          </Button>
        </Box>
      </Box>
    </Container>
  );
};

export default TaskCreation;