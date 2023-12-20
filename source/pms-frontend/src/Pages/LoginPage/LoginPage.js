import React, { useState } from 'react';
import './LoginPage.css'; // Corresponding CSS file
import axios from 'axios'
import Cookies from 'js-cookie';
import AuthService from '../../Services/AuthService';




const LoginPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  
  const handleLogin = (e) => {
    e.preventDefault();

    
      // Perform login API call and get tokens
      const requestOptions ={
        method:"POST",
        headers: {"Content-Type":"application/json"},
        body: JSON.stringify({
          userName:username,
          password:password
        }),
      };
  
      fetch('https://lionfish-app-f5xc6.ondigitalocean.app/api/login',requestOptions)
      .then(response=>response.json())
      .then(data=>{
        const {accessToken, refreshToken} = data;
        const signIn = AuthService.login(accessToken,refreshToken);
        
        
          window.location.href="/";
        

      })
      .catch(error=>{
        if (error.response) {
          // The request was made, but the server responded with a status code that falls out of the range of 2xx
          console.log(error.response.data);
          console.log(error.response.status);
          console.log(error.response.headers);
        } else if (error.request) {
          // The request was made but no response was received
          console.log(error.request);
        } else {
          // Something happened in setting up the request that triggered an error
          console.log('Error', error.message);
        }
        console.log(error.config);
      })

  };
  return (
    <div className="login-page">
      <form onSubmit={handleLogin}>
        <h2>Login</h2>
        <div className="input-group">
          <label htmlFor="username">Username</label>
          <input
            type="text"
            id="username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
        </div>
        <div className="input-group">
          <label htmlFor="password">Password</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <button type="submit">Login</button>
      </form>
    </div>
  );
};

export default LoginPage;
