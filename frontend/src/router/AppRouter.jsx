import { Route, Routes } from 'react-router-dom';
import { ProductoRoutes } from '../pages/producto/ProductoRoutes';


export const AppRouter = () => {
  return (
    <Routes>

        {/* ProductosApp */}
        <Route path="/*" element={ <ProductoRoutes /> } />

    </Routes>
  )
}
