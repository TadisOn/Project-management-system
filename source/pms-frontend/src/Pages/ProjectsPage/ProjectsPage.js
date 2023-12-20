import React, { useState, useEffect } from 'react';
import axios from 'axios';
import AuthService from '../../Services/AuthService';
import { Link } from 'react-router-dom';
import { 
  Paper, Typography, Button, Container, Box, List, ListItem, ListItemText, 
  Divider, Pagination 
} from '@mui/material';

const ProjectsList = () => {
  const [projects, setProjects] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);
  const token = AuthService.getToken();
  const role = AuthService.getRole();

  useEffect(() => {
    const fetchProjects = (page) => {
      axios.get(`https://lionfish-app-f5xc6.ondigitalocean.app/api/projects?pageNumber=${page}&pageSize=5`, {
        headers: {
            Authorization: 'Bearer ' + token
        }
      })
      .then(response => {
        // Set the projects or tasks from the response body
        setProjects(response.data);
      
        // Check if the 'Pagination' header is present and has a value
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
        console.error('Error fetching data:', error);
      });
    };

    fetchProjects(currentPage);
  }, [currentPage, token]);

  const handleChangePage = (event, newPage) => {
    setCurrentPage(newPage);
  };

  const ListItemLink = (props) => {
    return <ListItem button component={Link} {...props} />;
  };

  return (
    <Container maxWidth="md" className='container'>
      <Box sx={{ my: 4, textAlign: 'center' }}>
        <Typography variant="h4" gutterBottom>Projects</Typography>
        {role === 'Admin,PMSUser' && (
          <Box sx={{ mb: 2 }}>
            <Button variant="contained" color="primary" component={Link} to="/create-project">
              Create a new project
            </Button>
          </Box>
        )}
        <Paper elevation={3}>
          <List>
            {projects.length > 0 ? projects.map((project, index) => (
              <React.Fragment key={project.id}>
                {index > 0 && <Divider />}
                <ListItemLink to={`/projects/${project.id}`} sx={{ '&:hover': { bgcolor: 'action.hover' } }}>
                  <ListItemText primary={project.name} secondary={`Created on: ${new Date(project.creationDate).toLocaleDateString()}`} />
                </ListItemLink>
              </React.Fragment>
            )) : (
              <ListItem>
                <ListItemText primary="No projects available." />
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

export default ProjectsList;
