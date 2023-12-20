import Cookies from 'js-cookie';
import { jwtDecode } from 'jwt-decode'

const AuthService = {
  login: (accessToken, refreshToken) => {

    
        const decodedToken = jwtDecode(accessToken);
  
        Cookies.set('accessToken', accessToken);
        Cookies.set('refreshToken', refreshToken);
        Cookies.set('userRole', decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
        Cookies.set('userId', decodedToken.sub);
        Cookies.set('userName', decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']);       
        
          console.log("Signed In.")
    
  },

  logout: () => {
    // Clear cookies on logout
    Cookies.remove('accessToken');
    Cookies.remove('refreshToken');
    Cookies.remove('userRole');
    Cookies.remove('userId');
    Cookies.remove('userName');
    window.location.href = "/";

  },

  isLoggedIn: () => {
    // Check if tokens exist in cookies
    return !!Cookies.get('accessToken') && !!Cookies.get('refreshToken');
  },

  getUsername: () => {
    // Get username from cookies if logged in
    return Cookies.get('userName') || '';
  },

  getRole: () => {
    return Cookies.get('userRole') || '';
  },
  getToken: () => {
    return Cookies.get('accessToken') || '';
  }
};

export default AuthService;