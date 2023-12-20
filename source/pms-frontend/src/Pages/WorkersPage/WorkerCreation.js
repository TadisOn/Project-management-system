import React, { useState } from 'react';
import { Formik, Form, Field } from 'formik';
import * as Yup from 'yup';
import { Button, TextField, Container, Typography, Snackbar } from '@mui/material';
import axios from 'axios';
import { useNavigate } from 'react-router-dom'; // Import useNavigate

const WorkerCreationSchema = Yup.object().shape({
  email: Yup.string().email('Invalid email').required('Required'),
  firstName: Yup.string().required('Required'),
  lastName: Yup.string().required('Required'),
  username: Yup.string().required('Required'),
  password: Yup.string()
    .matches(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/,
      "Must Contain 8 Characters, One Uppercase, One Lowercase, One Number and One Special Case Character"
    )
    .required('Required'),
});

const WorkerCreation = () => {
    const [openSnackbar, setOpenSnackbar] = useState(false);
    const [snackbarMessage, setSnackbarMessage] = useState('');
    const navigate = useNavigate(); // Initialize useNavigate
  
    const handleCloseSnackbar = (event, reason) => {
      if (reason === 'clickaway') {
        return;
      }
      setOpenSnackbar(false);
    };
  
    const handleSubmit = (values, { setSubmitting, resetForm }) => { // Add resetForm to parameters
      axios.post('https://lionfish-app-f5xc6.ondigitalocean.app/api/register', values)
        .then(response => {
          console.log(response);
          setSnackbarMessage('Worker created successfully!');
          setOpenSnackbar(true);
          resetForm(); // Reset the form after successful submission
          navigate('/'); // Navigate to desired path
        })
        .catch(error => {
          console.error('Error registering worker:', error);
          setSnackbarMessage('Error registering worker.');
          setOpenSnackbar(true);
        })
        .finally(() => setSubmitting(false));
    };

  return (
    <Container maxWidth="sm" className='container'>
      <Typography variant="h4" gutterBottom>
        Worker Creation
      </Typography>
      <Formik
        initialValues={{ email: '', firstName: '', lastName: '', username: '', password: '' }}
        validationSchema={WorkerCreationSchema}
        onSubmit={handleSubmit}
      >
        {({ errors, touched, isSubmitting }) => (
          <Form>
            <Field as={TextField} name="email" label="Email" fullWidth margin="normal" error={touched.email && !!errors.email} helperText={touched.email && errors.email} />
            <Field as={TextField} name="firstName" label="First Name" fullWidth margin="normal" error={touched.firstName && !!errors.firstName} helperText={touched.firstName && errors.firstName} />
            <Field as={TextField} name="lastName" label="Last Name" fullWidth margin="normal" error={touched.lastName && !!errors.lastName} helperText={touched.lastName && errors.lastName} />
            <Field as={TextField} name="username" label="Username" fullWidth margin="normal" error={touched.username && !!errors.username} helperText={touched.username && errors.username} />
            <Field as={TextField} type="password" name="password" label="Password" fullWidth margin="normal" error={touched.password && !!errors.password} helperText={touched.password && errors.password} />
            <Button type="submit" variant="contained" color="primary" disabled={isSubmitting} fullWidth>
              Create Worker
            </Button>
          </Form>
        )}
      </Formik>
      <Snackbar
        open={openSnackbar}
        autoHideDuration={6000}
        onClose={handleCloseSnackbar}
        message={snackbarMessage}
      />
    </Container>
  );
};

export default WorkerCreation;
