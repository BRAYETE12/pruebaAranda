import { configureStore} from '@reduxjs/toolkit'
import { productoSlice } from './producto';

export const store = configureStore({
    reducer: {
        producto: productoSlice.reducer
    },
});