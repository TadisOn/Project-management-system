import logo from './logo.svg';
import './App.css';
import HomePage from './Pages/HomePage/HomePage';

import {BrowserRouter as Router, Route, Routes, BrowserRouter } from 'react-router-dom';
import LoginPage from './Pages/Login/Login';
import Layout from './Pages/Layout';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout/>}>
          <Route index element ={<HomePage/>} />
          <Route path="login" element={<LoginPage/>}/>
        </Route>
      </Routes>
    </BrowserRouter>

  );
}

export default App;
