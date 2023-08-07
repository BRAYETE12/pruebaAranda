import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import { AppRouter } from './router/AppRouter';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

function App() {
  return (
    <main>

      <Navbar expand="lg" bg="primary" data-bs-theme="dark" >
        <Container>
          <Navbar.Brand href="#home">Prueba tecnica Aranda</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link href="/">Productos</Nav.Link>
              <Nav.Link href="/crear">Crear producto</Nav.Link>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>

      <Container className='bg-white shadow-sm p-5 my-4' >
        <AppRouter />
      </Container>

    </main>
  );
}

export default App;
