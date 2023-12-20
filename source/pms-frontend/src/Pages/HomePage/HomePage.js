import React from 'react';
import './HomePage.css'; // Import the CSS file
import AuthService from '../../Services/AuthService';
import Carousel from './Carousel'
import { Typography } from '@mui/material';

const HomePage = () => {
  const isLoggedIn = AuthService.isLoggedIn();

  return (
    <div className="container">
      {isLoggedIn ? (
        <main>
          <section className="hero">
          <Typography 
              variant="h6" 
              sx={{ 
                fontFamily: "'Rubik Lines', sans-serif", 
                textAlign: 'center', 
                justifyContent:'center',
                flexShrink: 0,
                fontSize: { xs: '4vw', sm: '3vw', md: '2vw', lg: '1.5vw', xl: '1.25vw' },
                color:'black'
              }}
            >
              Welcome to Project Managment System.
            </Typography>
            <p style={{justifyContent:'center', textAlign:'center'}}>To browse projects, head to the projects tab.</p>
            <div className='container'>
            <Carousel style={{justifyContent:'center', textAlign:'center'}}/>
          </div>
          </section>
          
        </main>
      ) : (
        <main>
          <section className="hero">
            <h1>Please Log In to Continue</h1>
          </section>
        </main>
      )}
    </div>
  );
};

export default HomePage;