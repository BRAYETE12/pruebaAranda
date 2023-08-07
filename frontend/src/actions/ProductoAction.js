import HttpCliente from '../servicios/HttpCliente';

export const actualizarProducto = async (id, producto) => {

    const formData = new FormData();
   
    for(let nb in producto)
        formData.append(nb, producto[nb]);
    
    return HttpCliente.put("/api/Produts", formData);
}

export const crearProducto = async (producto)=>
{
    const formData = new FormData();

    for(let nb in producto)
        formData.append(nb, producto[nb]);
    
    return HttpCliente.post("/api/Produts", formData);
}

export const getProductos = (request) => {
    return HttpCliente.get(`/api/Produts?${request}`);
};

export const getProducto =  id => {
    return HttpCliente.get(`/api/Produts/${id}`);
};

export const deleteProducto =  id => {
    return HttpCliente.delete(`/api/Produts/${id}`);
};

export const getCategorias =  id => {
    return HttpCliente.get('/api/ReferenceTable/Category');
};