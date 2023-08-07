import {  useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useForm } from '../../../hooks';
import { getProductos, deleteProducto, getCategorias } from '../../../actions/ProductoAction';
import Pagination from 'react-bootstrap/Pagination';
import Container from 'react-bootstrap/Container';
import Alert from 'react-bootstrap/Alert';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Table from 'react-bootstrap/Table';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

import Swal from 'sweetalert2';


export const ProductoPage = () => {
    const navigate = useNavigate();
   
    const formDataFilter = { Name: null, Description: null, CategoryId: null, SortDirName: null, SortDirCategory: null, CurrentPage: 1, PageSize: 10, };
    const [, setRequestProductos] = useState(formDataFilter);
    const { formState, onInputChange } = useForm( formDataFilter, {} );
    const [categorias, setCategorias] = useState([]);
    const [listado, setListado] = useState([]);
    const [itemsPaginate, setItemsPaginate] = useState([]);
    
    const handleChange = (event, value) => 
    {
      setRequestProductos( (anterior) => ({ ...anterior, pageIndex: value }));
      getDataListado();
    }

    const serch = () => 
    {
      setRequestProductos(formState);
      getDataListado();
    }

    const editaProducto = (id) => 
    {
      navigate("/editar/" + id);
    }
    const crearProducto = () => 
    {
      navigate("/crear");
    }

    const eliminarProducto = (id) => 
    {
        Swal.fire({
          title: '¿Estas seguro?',
          text: "¿No podras reversar los cambios!",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: '¡si,eliminar!'
        }).then((result) => {
          if (result.isConfirmed) {
            deleteItem(id);
          }
        })
        
    }

    const deleteItem = async (id) => 
    {
        const response = await deleteProducto(id);           
        
        if(response.status === 200)
        {
          Swal.fire('Exitoso', "eliminado exitosamente", 'success');
          serch();
        }else{
          Swal.fire('Error', "error en la peticion", 'error');
        }
    }

    const generateItemsPaginate = (size, active)=>{
      
      var itemsPaginate = [];

      for (let number = 1; number <= size; number++) 
      {
        itemsPaginate.push(
          <Pagination.Item key={number} active={number === active} onClick={handleChange} >
            {number}
          </Pagination.Item>,
        );
      }

      setItemsPaginate(itemsPaginate);
    };

    const getDataListado = async ()=>
    {
      var dt_rq = Object.keys(formState).map(key => formState[key] ? `${key}=${encodeURIComponent(formState[key])}` : '' ).join('&');
      const response = await getProductos(dt_rq);
      setListado(response.data.Data.Results);
      generateItemsPaginate(response.data.Data.PageCount, response.data.Data.CurrentPage);
    };

    const init = async () => {
      getDataListado();
      const response = await getCategorias();
      setCategorias(response.data.Data);
    }

    useEffect( () => { init(); }, []);

  return (
    <Container>

      <h1 className='text-center text-primary mb-4'>
        Listado de productos
      </h1>

      <div className='card p-3 mb-4'>
        <Row className="justify-content-md-center">
          <Col>
            <Form.Group className="mb-3">
              <Form.Label>Buscar por nombre</Form.Label>
              <Form.Control type="text" name='Name' onChange={onInputChange} placeholder="Ingrese el nombre del producto" />
            </Form.Group>   
          </Col>
          <Col>
            <Form.Group className="mb-3">
              <Form.Label>Buscar por categoría</Form.Label>
              <Form.Select name='CategoryId' onChange={onInputChange} >
                <option>Seleccione una opción</option>
                { categorias.map((item) => (
                  <option key={item.Id} value={item.Id} >{item.Name}</option>
                ))}
              </Form.Select>
            </Form.Group> 
          </Col>
          <Col>
            <Form.Group className="mb-3">
              <Form.Label>Buscar por descripcion</Form.Label>
              <Form.Control type="text" name='Description' onChange={onInputChange} placeholder="Ingrese la descripcion del producto" />
            </Form.Group>   
          </Col>
          
        </Row>

        <Row className="justify-content-md-center">
          <Col>
            <Form.Group className="mb-3">
              <Form.Label>Orden por nombre</Form.Label>
              <div key="inline-radio" className="mb-3">
                <Form.Check inline type="radio" id="rb1" label="Asc" name='OrderName' onChange={onInputChange} value="DESC" />
                <Form.Check inline type="radio" id="rb2" label="Desc" name='OrderName' onChange={onInputChange} value="ASC" />
              </div>
            </Form.Group>   
          </Col>
          <Col>
            <Form.Group className="mb-3">
              <Form.Label>Orden por categoría</Form.Label>
              <div key="inline-radio" className="mb-3">
                <Form.Check inline type="radio" id="rb3" label="Asc" name='OrderCategory' onChange={onInputChange} value="DESC" />
                <Form.Check inline type="radio" id="rb4" label="Desc" name='OrderCategory' onChange={onInputChange} value="ASC" />
              </div>
            </Form.Group>  
          </Col>
          <Col>
            <div className="pt-4 d-grid gap-1">
              <button type="button" onClick={serch} className='btn btn-outline-primary' > Filtrar </button>
            </div>
          </Col>
          
        </Row>
      </div>

      <button type="button"  onClick={crearProducto} className='mx-1 btn btn-outline-success mb-2' >
          <i className="bi bi-plus"></i> agregar producto
      </button>
      <Table striped bordered hover>
        <thead>
            <tr>
                <th>NOMBRE</th>
                <th>CATEGORIA</th>
                <th>DESCRIPCION</th>
                <td style={{width: "120px"}} ></td>
            </tr>
        </thead>
            <tbody>
                { listado.map((producto) => (
                    <tr key={producto.Id} >

                        <td>{producto.Name}</td>
                        <td>{producto.CategoryName}</td>
                        <td>{producto.Description}</td>
                        <td>
                            <button type="button" className='mx-1 btn btn-outline-success' onClick={() => editaProducto(producto.Id)} >
                              <i className="bi bi-pencil-square"></i>
                            </button>
                            <button type="button" className='mx-1 btn btn-outline-danger' onClick={() => eliminarProducto(producto.Id)} >
                              <i className="bi bi-trash"></i>
                            </button>
                        </td>
                    </tr>
                ))}
            </tbody>
      </Table>
      {listado.length === 0 &&
        <Alert variant="info"> No hay resultados </Alert>
      }

      <div className='d-flex justify-content-center'>
        <Pagination>{itemsPaginate}</Pagination>
      </div>

    </Container>
  )
}