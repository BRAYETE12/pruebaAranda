import { createSlice } from "@reduxjs/toolkit";

export const productoSlice = createSlice({
    name:'producto',
    initialState: {
        pageNumber: 1,
        nombre:'',
        categoria:'',
        descripcion:'',
        ascNombre:null,
        ascCategoria:null,
        pageSize:10,
        totalPage:1,
        productos: [],
        isLoading: false,
    },
    reducers: {
        startLoadingProductos: (state, /* action */ ) => {
            state.isLoading = true;
        },
        setProductos: ( state, action ) => {
            state.isLoading = false;
            state.pageNumber = action.payload.pageNumber;
            state.productos = action.payload.items;
            state.totalPage = action.payload.totalPages;
        }
    }
});