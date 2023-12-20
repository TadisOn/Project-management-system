import React, { useState, useEffect } from 'react';
import axios from 'axios';
import AuthService from '../../Services/AuthService';
import { useParams, useNavigate } from 'react-router-dom';
import { 
  Typography, Paper, Container, Box, List, ListItem, ListItemText, 
  Divider, Button, Pagination, ListItemButton 
} from '@mui/material';
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField } from '@mui/material';

const ProjectDetails = () => {
  const { projectId } = useParams();
  const navigate = useNavigate();
  const [project, setProject] = useState(null);
  const [tasks, setTasks] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);
  const token = AuthService.getToken();
  const [openDeleteDialog, setOpenDeleteDialog] = useState(false);
  const [editDescription, setEditDescription] = useState('');
  const role = AuthService.getRole();

  useEffect(() => {
    // Fetch project details
    axios.get(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}`, {
      headers: {
        Authorization: 'Bearer ' + token
      }
    })
    .then(response => {
      setProject(response.data);
    })
    .catch(error => {
      console.error('Error fetching project details:', error);
    });

    // Fetch tasks for project
    const fetchTasks = (page) => {
      axios.get(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}/tasks?pageNumber=${page}&pageSize=4`, {
        headers: {
          Authorization: 'Bearer ' + token
        }
      })
      .then(response => {
        setTasks(response.data);
        const paginationHeader = response.headers['pagination'];
        if (paginationHeader) {
          const pagination = JSON.parse(paginationHeader);
          if (pagination) {
            setTotalPages(pagination.TotalPages);
            // Set other pagination states as needed
          }
        } else {
          // Handle the case where pagination header is not present
          console.warn('Pagination header is not present in the response');
        }
      })
      .catch(error => {
        console.error('Error fetching tasks:', error);
      });
    };

    fetchTasks(currentPage);
  }, [projectId, currentPage, token]);

  const handleChangePage = (event, newPage) => {
    setCurrentPage(newPage);
  };

  const handleDeleteClick = () => {
    setOpenDeleteDialog(true);
  };
  
  // Function to close the delete confirmation dialog
  const handleCloseDeleteDialog = () => {
    setOpenDeleteDialog(false);
  };
  
  // Function to handle the delete confirmation
  const handleDeleteConfirm = () => {
    axios.delete(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}`, {
      headers: {
        Authorization: 'Bearer ' + token
      }
    })
    .then(() => {
      // Handle the successful deletion, maybe redirect to the projects list
      navigate('/projects');
    })
    .catch(error => {
      console.error('Error deleting project:', error);
    });
  };
  
  // Function to handle the edit submission
  const handleEditSubmit = () => {
    axios.put(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}`, {
      Description: editDescription
    }, {
      headers: {
        Authorization: 'Bearer ' + token
      }
    })
    .then(() => {
      // Handle the successful update
      setProject({ ...project, description: editDescription }); // Update local state
      setEditDescription('');
    })
    .catch(error => {
      console.error('Error updating project:', error);
    });
  };

  return (
    <Container maxWidth="md" className='container'>
      <Box sx={{ my: 4 }}>

        <Paper elevation={3} sx={{ p: 3, mb: 3 }}>

        <Box sx={{ display: 'flex', flexDirection: 'row', gap: 2}}>
        <Typography variant="h4" gutterBottom>
            {project?.name || 'Loading project...'}
          </Typography>
        {role === 'Admin,PMSUser' && (
  <>
  <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end', gap: 2, ml: 50 }}>

    <Button variant="contained" color="error" size='small' sx={{ whiteSpace: 'nowrap', maxWidth:'150px',  }} onClick={handleDeleteClick}>
      Delete Project
    </Button>
    <Button variant="contained" color="primary" size='small' sx={{whiteSpace: 'nowrap', maxWidth:'150px' }} onClick={() => setEditDescription(project?.description)}>
      Edit Project
    </Button>
    </Box>
  </>
)}
          </Box>

        
          
          <Typography variant="subtitle1" gutterBottom>
            {project?.description || 'No description available.'}
          </Typography>
          

          

<Dialog
  open={openDeleteDialog}
  onClose={handleCloseDeleteDialog}
  aria-labelledby="alert-dialog-title"
  aria-describedby="alert-dialog-description"
>
  <DialogTitle id="alert-dialog-title">{"Confirm Delete"}</DialogTitle>
  <DialogContent>
    <DialogContentText id="alert-dialog-description">
      Are you sure you want to delete this project? This action cannot be undone.
    </DialogContentText>
  </DialogContent>
  <DialogActions>
    <Button onClick={handleCloseDeleteDialog} color="primary">
      Cancel
    </Button>
    <Button onClick={handleDeleteConfirm} color="primary" autoFocus>
      Confirm
    </Button>
  </DialogActions>
</Dialog>

<Dialog open={editDescription !== ''} onClose={() => setEditDescription('')}>
  <DialogTitle>Edit Project</DialogTitle>
  <DialogContent>
    <DialogContentText>
      Update the project's description below.
    </DialogContentText>
    <TextField
      autoFocus
      margin="dense"
      id="description"
      label="Project Description"
      type="text"
      fullWidth
      multiline
      rows={4}
      variant="standard"
      value={editDescription}
      onChange={(e) => setEditDescription(e.target.value)}
    />
  </DialogContent>
  <DialogActions>
    <Button onClick={() => setEditDescription('')}>Cancel</Button>
    <Button onClick={handleEditSubmit}>Update</Button>
  </DialogActions>
</Dialog>
        </Paper>

        <Paper elevation={3} sx={{ p: 3 }}>
          
          <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'flex-end', gap: 2 }}>
          <Typography variant="h5" gutterBottom>Tasks</Typography>
          <Box sx = {{ml: 75}}>
          <Button 
            variant="contained" 
            color="primary" 
            onClick={() => navigate(`/projects/${projectId}/create-task`)}
            sx={{whiteSpace: 'nowrap', maxWidth:'150px', display: 'flex', flexDirection: 'column', alignItems: 'flex-end' }}
          >
            Create Task
          </Button>
          </Box>
          </Box>
          <List>
  {tasks.length > 0 ? tasks.map((task, index) => (
    <React.Fragment key={task.id}>
      {index > 0 && <Divider />}
      <ListItemButton 
        sx={{ '&:hover': { bgcolor: 'action.hover' } }}
        onClick={() => navigate(`/projects/${projectId}/tasks/${task.id}`)}
      >
        <ListItemText 
          primary={task.name} 
          secondary={`Created on: ${new Date(task.creationDate).toLocaleDateString()}`} 
        />
      </ListItemButton>
    </React.Fragment>
  )) : (
    <ListItem>
      <ListItemText primary="No tasks available." />
    </ListItem>
  )}
</List>
          {totalPages > 1 && (
            <Box sx={{ display: 'flex', justifyContent: 'center', p: 2 }}>
              <Pagination 
                count={totalPages} 
                page={currentPage} 
                onChange={handleChangePage} 
                color="primary" 
              />
            </Box>
          )}
        </Paper>
      </Box>
    </Container>
  );
};

export default ProjectDetails;
