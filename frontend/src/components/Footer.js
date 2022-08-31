import { Link } from 'react-router-dom'

const Footer = ()=>{
  return(
    <footer className="container mt-2">
      <nav className="nav bg-light p-3">
          <Link to="/" className="nav-link">
            Home
          </Link>
          <Link to="/keyboards"  className="nav-link">
            Keyboards
          </Link>
      </nav>
    </footer>
  )
}

export default Footer;