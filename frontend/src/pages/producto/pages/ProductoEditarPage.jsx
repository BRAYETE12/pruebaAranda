import {  useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useForm } from '../../../hooks';
import { getProducto, actualizarProducto, getCategorias } from '../../../actions/ProductoAction';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Swal from 'sweetalert2';

export const ProductoEditarPage = (props) => {

    const {id} = useParams();
    const navigate = useNavigate();
    const formData = { Name: null, Description: null, CategoryId: null, Img: null };
    
    const formValidations = {
      Name: [ (value) => value, 'El nombre es obligatorio'],
      Description: [ (value) => value, 'El descripcion es obligatorio.'],
      CategoryId: [ (value) => value, 'El categoria es obligatorio.'],
    }
    const { formState, isFormValid, onInputChange, onInputChangeFile, onResetForm } = useForm( formData, formValidations );
    const [categorias, setCategorias] = useState([]);
    const [formSubmitted, setFormSubmitted] = useState(false);

    const onSubmit = ( event ) => 
    {
        event.preventDefault();
        setFormSubmitted(true);
    
        if ( !isFormValid ) return;
         guardarProducto();
    }

    const guardarProducto = async() =>
    {  
      const resultado = await actualizarProducto( id , formState);

      if(resultado.status === 200)
      {
        Swal.fire({
          title: 'El registro se ha guardado exitosamente',
          type: 'success',
          showCancelButton: false,
        })
        .then((result) => {
          navigate("/");
        })
      }
      else{
        Swal.fire('Error', "error en la peticion", 'error');
      }
    }

    useEffect( () => {   
        const init = async() => 
        {
          const responseC = await getCategorias();
          setCategorias(responseC.data.Data);

          const responseP = await getProducto(id);
          onResetForm(responseP.data.Data);
        }

        init();

    }, [])

  return (
    <Form onSubmit={ onSubmit } >

      <h1 className='text-center text-primary'>
        Editar producto
      </h1>

      <Form.Group className="mb-3">
        <Form.Label>Nombre</Form.Label>
        <Form.Control type="text" name='Name' value={formState.Name} onChange={onInputChange} placeholder="Ingrese el nombre del producto" />
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label>Categoría</Form.Label>
        <Form.Select name='CategoryId' value={formState.CategoryId} onChange={onInputChange} >
          <option>Seleccione una opción</option>
          { categorias.map((item) => (
            <option key={item.Id} value={item.Id} >{item.Name}</option>
          ))}
        </Form.Select>
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label>Descripcion</Form.Label>
        <Form.Control as="textarea" name='Description' value={formState.Description} onChange={onInputChange} placeholder='Ingrese la descripción del producto' rows={4} />
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