import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useForm } from '../../../hooks';
import { crearProducto, getCategorias } from '../../../actions/ProductoAction';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Swal from 'sweetalert2';

export const ProductoCrearPage = () => {

      const navigate = useNavigate();
      const formData = { Name: null, Description: null, CategoryId: null, Img: null };
      const [categorias, setCategorias] = useState([]);
      
      const formValidations = {
        Name: [ (value) => value, 'El nombre es obligatorio'],
        Description: [ (value) => value, 'El descripcion es obligatorio.'],
        CategoryId: [ (value) => value, 'El categoria es obligatorio.'],
      }
      const { formState, isFormValid, onInputChange, onInputChangeFile } = useForm( formData, formValidations );
      const [,setFormSubmitted] = useState(false);

      const onSubmit = ( event ) => 
      {
        event.preventDefault();
        setFormSubmitted(true);
    
        if ( !isFormValid ) return;
         guardarProducto();
      }

      const guardarProducto = async() =>
      {
        const input = document.getElementById('upload-photo');
        const resultado = await crearProducto(formState, input);
       
        if(resultado.status === 200)
        {
          Swal.fire({
            title: 'El registro se ha guardado exitosamente',
            type: 'success',
            showCancelButton: false,
          })
          .then((result) => {
            navigate("/");
          });           
        }
        else{
          Swal.fire('Error', "error al realizar la petición", 'error');
        }
      }

      useEffect( () => {   
        const init = () => {
          getCategorias()
          .then((data)=>{
            setCategorias(data.data.Data);
          });
        }

        init();

    }, []);
    
  return (
    
    <Form onSubmit={ onSubmit } >

      <h1 className='text-center text-primary'>
        Crear producto
      </h1>

      <Form.Group className="mb-3">
        <Form.Label>Nombre</Form.Label>
        <Form.Control type="text" name='Name' onChange={onInputChange} placeholder="Ingrese el nombre del producto" />
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label>Categoría</Form.Label>
        <Form.Select name='CategoryId' onChange={onInputChange} >
          <option>Seleccione una opción</option>
          { categorias.map((item) => (
            <option key={item.Id} value={item.Id} >{item.Name}</option>
          ))}
        </Form.Select>
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label>Descripcion</Form.Label>
        <Form.Control as="textarea" name='Description' onChange={onInputChange} placeholder='Ingrese la descripción del producto' rows={4} />
      </Form.Group>

      <Form.Group controlId="formData.img" className="mb-3">
        <Form.Label>Imagen</Form.Label>
        <Form.Control type="file" name='Img' onChange={onInputChangeFile} />
      </Form.Group>

      <div className="text-center">
        <Button type="submit" variant="success" size="lg"> Guardar producto </Button>
      </div>

    </Form>
  );
}