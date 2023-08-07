import { Navigate, Route, Routes } from "react-router-dom"
import { ProductoPage, ProductoCrearPage, ProductoEditarPage } from "./pages"


export const ProductoRoutes = () => {
  return (
    <Routes>
        <Route path="crear" element={<ProductoCrearPage />} />
        <Route path="editar/:id" element={<ProductoEditarPage />} />
        <Route path="/" element={ <ProductoPage /> } />

        <Route path="/*" element={ <Navigate to="/" /> } />
    </Routes>
  )
}
