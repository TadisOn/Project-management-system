import logo from './logo.svg';
import './App.css';
import HomePage from './Pages/HomePage/HomePage';
import {BrowserRouter as Router, Route, Routes, BrowserRouter } from 'react-router-dom';
import  LoginPage  from './Pages/LoginPage/LoginPage';
import Layout from './Pages/Layout';
import ProjectsPage from './Pages/ProjectsPage/ProjectsPage';
import ProjectCreationPage from './Pages/ProjectsPage/ProjectCreationPage';
import ProjectDetails from './Pages/ProjectsPage/ProjectDetails';
import TaskCreation from './Pages/TasksPage/TaskCreation';
import TaskDetails from './Pages/TasksPage/TaskDetails';
import WorkerCreation from './Pages/WorkersPage/WorkerCreation';
import AssignWorkers from './Pages/WorkersPage/AssignWorkers';


function App() {
  return (
    
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout/>}>
          <Route index element ={<HomePage/>} />
          <Route path="login" element={<LoginPage/>}/>
          <Route path="projects" element={<ProjectsPage/>}/>
          <Route path ="create-project" element={<ProjectCreationPage/>}/>
          <Route path ="projects/:projectId" element={<ProjectDetails/>}/>
          <Route path="/projects/:projectId/create-task" element={<TaskCreation/>} />
          <Route path="/projects/:projectId/tasks/:taskId" element={<TaskDetails />} />
          <Route path="/projects/:projectId/tasks/:taskId/assign-workers" element={<AssignWorkers/>}/>
          <Route path="create-worker" element={<WorkerCreation/>}/>

        </Route>
      </Routes>
      </BrowserRouter>

  );
}

export default App;