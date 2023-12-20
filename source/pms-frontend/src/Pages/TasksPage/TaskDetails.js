import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import { Box, Button, Container, Paper, Typography, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField, List, ListItem, ListItemText} from '@mui/material';
import AuthService from '../../Services/AuthService';

const TaskDetails = () => {
    const { projectId, taskId } = useParams();
    const navigate = useNavigate();
    const [task, setTask] = useState({ name: '', description: '' });
    const [openEditDialog, setOpenEditDialog] = useState(false);
    const [openDeleteDialog, setOpenDeleteDialog] = useState(false);
    const [editName, setEditName] = useState('');
    const [editDescription, setEditDescription] = useState('');
    const [workers, setWorkers] = useState([]);
    const token = AuthService.getToken();
    const role = AuthService.getRole();

  useEffect(() => {
    const fetchTask = async () => {
      try {
        const response = await axios.get(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}/tasks/${taskId}`, {
          headers: {
            Authorization: 'Bearer ' + token
          }
        });
        setTask(response.data);
      } catch (error) {
        console.error('Error fetching task details:', error);
      }
    };

    fetchTask();
  }, [projectId, taskId, token]);
  
  useEffect(() =>{
  const fetchWorkers = async () => {
    try {
      const response = await axios.get(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}/tasks/${taskId}/workers`, {
        headers: {
          Authorization: 'Bearer ' + token
        }
      });
      setWorkers(response.data);
    } catch (error) {
      console.error('Error fetching workers:', error);
    }
  };

  fetchWorkers();
}, [projectId, taskId, token]);


const handleDeleteWorker = async (workerId) => {
    try {
      await axios.delete(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}/tasks/${taskId}/workers/${workerId}`, {
        headers: {
          Authorization: 'Bearer ' + token
        }
      });
      // Remove the worker from the local state to update the UI
      setWorkers(workers.filter((worker) => worker.id !== workerId));
    } catch (error) {
      console.error('Error removing worker from task:', error);
    }
  };

  const handleEditTask = async (editedDescription) => {
    try {
      const response = await axios.put(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}/tasks/${taskId}`, {
        Description: editedDescription
      }, {
        headers: {
          Authorization: 'Bearer ' + token
        }
      });
      setTask({ ...task, description: editedDescription });
      // Close any open dialog here if necessary
    } catch (error) {
      console.error('Error updating task:', error);
    }
  };

  

  const handleOpenEditDialog = () => {
    setEditName(task.name);
    setEditDescription(task.description);
    setOpenEditDialog(true);
  };

  // Handle closing the edit task dialog
  const handleCloseEditDialog = () => {
    setOpenEditDialog(false);
  };

  const handleEditTaskSubmit = async () => {
    try {
      const response = await axios.put(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}/tasks/${taskId}`, {
        Name: editName,
        Description: editDescription
      }, {
        headers: {
          Authorization: 'Bearer ' + token
        }
      });
      setTask(response.data);
      handleCloseEditDialog();
    } catch (error) {
      console.error('Error updating task:', error);
    }
  };

  const handleDeleteTask = async () => {
    try {
      await axios.delete(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects/${projectId}/tasks/${taskId}`, {
        headers: {
          Authorization: 'Bearer ' + token
        }
      });
      navigate(`/projects/${projectId}`);
    } catch (error) {
      console.error('Error deleting task:', error);
    }
  };

  

  // Handle opening the delete confirmation dialog
  const handleOpenDeleteDialog = () => {
    setOpenDeleteDialog(true);
  };

  // Handle closing the delete confirmation dialog
  const handleCloseDeleteDialog = () => {
    setOpenDeleteDialog(false);
  };

  const ConfirmationDialog = ({ open, onClose, onConfirm, title, description }) => {
    return (
      <Dialog open={open} onClose={onClose}>
        <DialogTitle>{title}</DialogTitle>
        <DialogContent>
          <DialogContentText>{description}</DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose} color="primary">Cancel</Button>
          <Button onClick={onConfirm} color="primary">Confirm</Button>
        </DialogActions>
      </Dialog>
    );
  };

  return (
    <Container maxWidth="sm" className='container'>
      <Paper elevation={3} sx={{ p: 3, my: 4 }}>

      <Box sx={{ display: 'flex', flexDirection: 'row', gap: 20}}>
      <Typography variant="h5">{task?.name}</Typography>
      {role === 'Admin,PMSUser' && (
          <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end', gap: 2 }}>
            <Button variant="contained" color="error" size='small' sx={{ whiteSpace: 'nowrap', maxWidth:'150px',  }} onClick={handleOpenDeleteDialog}>
              Delete Task
            </Button>
            <Button variant="contained" color="primary" size='small' sx={{whiteSpace: 'nowrap', maxWidth:'150px' }} onClick={handleOpenEditDialog} >
              Edit Task
            </Button>

           
          </Box>
        )}

<Dialog open={openEditDialog} onClose={handleCloseEditDialog}>
          <DialogTitle>Edit Task</DialogTitle>
          <DialogContent>
            <TextField
              margin="dense"
              id="description"
              label="Task Description"
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
            <Button onClick={handleCloseEditDialog}>Cancel</Button>
            <Button onClick={handleEditTaskSubmit}>Save</Button>
          </DialogActions>
        </Dialog>
        

        <ConfirmationDialog
      open={openDeleteDialog}
      onClose={handleCloseDeleteDialog}
      onConfirm={handleDeleteTask}
      title="Confirm Delete"
      description="Are you sure you want to delete this task? This action cannot be undone."
    />
        </Box>

        
        <Typography variant="body1" sx={{ mb: 2 }}>
          {task?.description}
        </Typography>
        
      </Paper>

      <Paper>
      <Typography variant="h6" gutterBottom>
          Workers Assigned
        </Typography>
        {role==='Admin,PMSUser' &&(
            <Button variant="contained" color="primary" onClick={() => navigate(`/projects/${projectId}/tasks/${taskId}/assign-workers`)}>
          Assign Workers
        </Button>
        )}
        
        <List>
          {workers.map((worker) => (
            
            <ListItem key={worker.id} secondaryAction={
                
              <Button
                variant="contained"
                color="error"
                onClick={() => handleDeleteWorker(worker.id)}
              >
                Delete Worker
              </Button>
              
            }>
              <ListItemText primary={`${worker.firstName} ${worker.lastName}`} />
            </ListItem>
          ))}
        </List>
      </Paper>
    </Container>
  );
};

export default TaskDetails;
