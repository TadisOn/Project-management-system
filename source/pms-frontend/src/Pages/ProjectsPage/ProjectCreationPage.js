import React, { useState } from 'react';
import axios from 'axios';
import AuthService from '../../Services/AuthService';
import { TextField, Button, Paper, Typography, Container, CircularProgress } from '@mui/material';

const ProjectCreationPage = ({ history }) => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const token = AuthService.getToken();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);

    try {
      const response = await axios.post('https://lionfish-app-f5xc6.ondigitalocean.app/api/projects', {
        Name: name,
        Description: description
      }, {
        headers: {
          Authorization: 'Bearer ' + token
        }
      });

      window.location.href="/projects";
    } catch (error) {
      console.error('Error creating project:', error);
      setIsLoading(false);
    }
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 8 }} className='container'>
      <Paper sx={{ p: 4 }}>
        <Typography variant="h4" align="center" gutterBottom>
          Create a Project
        </Typography>
        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: 16 }}>
          <TextField
            label="Project Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
            fullWidth
          />
          <TextField
            label="Description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
            fullWidth
            multiline
            rows={4}
          />
          <Button
            type="submit"
            variant="contained"
            sx={{ mt: 2, backgroundColor: '#0056b3', ':hover': { backgroundColor: '#004494' } }}
            disabled={isLoading}
          >
            {isLoading ? <CircularProgress size={24} /> : 'Create Project'}
          </Button>
        </form>
      </Paper>
    </Container>
  );
};

export default ProjectCreationPage;
