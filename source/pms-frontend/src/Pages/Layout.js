import React, { useState } from 'react';
import { Outlet, NavLink, useNavigate } from 'react-router-dom';
import AuthService from '../Services/AuthService';
import { AppBar, Toolbar, Typography, Button, Box, Container, IconButton, Drawer, List, ListItem, ListItemText } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { styled, useTheme } from '@mui/material/styles';
import useMediaQuery from '@mui/material/useMediaQuery';
import LogoutIcon from '../Images/logout.svg';

const LayoutLink = styled(NavLink)(({ theme }) => ({
  color: theme.palette.common.white,
  textDecoration: 'none',
  marginLeft: theme.spacing(2),
  '&.active': {
    textDecoration: 'underline',
  },
  '&:hover': {
    color: theme.palette.grey[300],
  },
}));

const Layout = () => {
  const [drawerOpen, setDrawerOpen] = useState(false);
  const isLoggedIn = AuthService.isLoggedIn();
  const username = AuthService.getUsername();
  const role = AuthService.getRole();
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
  const navigate = useNavigate();

  const handleDrawerToggle = () => {
    setDrawerOpen(!drawerOpen);
  };

  const handleLogout = () => {
    AuthService.logout();
    navigate('/login');
  };

  const drawerWidth = isMobile ? '100vw' : 250;

  const drawer = (
    <Box onClick={handleDrawerToggle} sx={{ textAlign: 'center', width: drawerWidth }}>
      <List>
        <ListItem button component={LayoutLink} to="/" exact>
          <ListItemText primary="Home" />
        </ListItem>
        {isLoggedIn && (
          <>
            <ListItem button component={LayoutLink} to="/projects">
              <ListItemText primary="Projects" />
            </ListItem>
            {(role === 'Admin,PMSUser') && (
              <ListItem button component={LayoutLink} to="/create-worker">
                <ListItemText primary="Create Worker" />
              </ListItem>
            )}
          </>
        )}
      </List>
    </Box>
  );

  return (
    <>
      <AppBar position="static">
        <Toolbar>
          
          <Box sx={{ 
            display: 'flex', 
            flexGrow: 1, 
            justifyContent:'space-evenly',
            alignItems: 'center', 
            flexWrap: 'wrap', 
            padding: '0 2vw' 
            }}>
              {isMobile && (
            <IconButton
              color="inherit"
              aria-label="open drawer"
              edge="start"
              onClick={handleDrawerToggle}
              sx={{ mr: 2 }}
            >
              <MenuIcon />
            </IconButton>
          )}
            {!isMobile && (
              <>
                <Typography variant="h6" component="div" >
                  <LayoutLink to="/" exact>Home</LayoutLink>
                </Typography>
                {isLoggedIn && (
                  <>
                    <Typography variant="h6" component="div">
                      <LayoutLink to="/projects">Projects</LayoutLink>
                    </Typography>
                    {(role === 'Admin,PMSUser') && (
                      <Typography variant="h6" component="div">
                        <LayoutLink to="/create-worker">Create Worker</LayoutLink>
                      </Typography>
                    )}
                  </>
                )}
              </>
            )}
            
            {isLoggedIn ? (
              <Box sx={{ display: 'flex', alignItems: 'flex-end' }}>
                <Typography variant="subtitle1" sx={{ marginRight: 2 }}>
                  Welcome back, {username}
                </Typography>
                <IconButton onClick={handleLogout} sx={{ padding: 0, display: 'center' }}>
                <img src={LogoutIcon} alt="Logout" style={{ width: '24px', height: '24px' }} />
                </IconButton>
              </Box>
            ) : (
              <Button color="inherit" component={LayoutLink} to="/login">
                Login
                </Button>
            )}
          </Box>
        </Toolbar>
      </AppBar>
      <Drawer
        variant={isMobile ? 'temporary' : 'persistent'}
        anchor="left"
        open={drawerOpen}
        onClose={handleDrawerToggle}
        ModalProps={{
          keepMounted: true,
        }}
        sx={{
          '& .MuiDrawer-paper': { width: drawerWidth, boxSizing: 'border-box' },
        }}
      >
        {drawer}
      </Drawer>

      <Container component="main" maxWidth="xl">
        <Outlet />
      </Container>

      <Box component="footer" sx={{ bgcolor: 'primary.main', p: 3, mt: 5, color: 'white', textAlign: 'center' }}>
        <Typography variant="body1">
          &copy; 2023 Tadas Jutkus, IFF-0/6
        </Typography>
      </Box>
    </>
  );
};

export default Layout;
