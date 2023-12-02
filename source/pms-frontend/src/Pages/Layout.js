import { Outlet, Link } from "react-router-dom";

const Layout = () => {
  return (
    <>
      <header>
        <nav>
          <ul>
            <li><a href="/">Home</a></li>
            <li><a href="/about">About</a></li>
            <li><a href="/contact">Contact</a></li>
            <li><a href= "/login">Login</a></li> 
          </ul>
        </nav>
      </header>

      <footer>
        <p>&copy; 2023 Tadas Jutkus, IFF-0/6</p>
      </footer>
      <Outlet />
    </>
  )
};

export default Layout;
