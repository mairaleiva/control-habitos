import type { Habito, FiltroOpciones } from "../types"


//inicializar el state
export const initialState : HabitosState = {
    habitos: [],
    filtro: "todos"
}

//un tipo para state
export type HabitosState ={
    habitos: Habito[],
    filtro: FiltroOpciones
}

//un tipo para action
export type HabitosActions = 
    {type: 'guardar', payload: {nuevoHabito: Habito}} |
    {type: 'eliminar', payload: {id: Habito['id']}} |
    {type: 'toggle', payload: {id: Habito['id']}} |
    {type: 'cambiar-filtro', payload: {filtro: FiltroOpciones}}

//una funcion
export function habitosReducer(state: HabitosState, action: HabitosActions){

    switch(action.type){
        case 'guardar':
            return{
                ...state,
                habitos: [...state.habitos, action.payload.nuevoHabito]
            }
        case 'eliminar':
            return{
                ...state,
                habitos:  state.habitos.filter(x => x.id !== action.payload.id)
            }
        case 'toggle':
            return{
                ...state,
                habitos: state.habitos.map(x => x.id === action.payload.id ? 
                    {
                        ...x, 
                        completo: !x.completo
                    } : x)
            }
        case 'cambiar-filtro':
            return{
                ...state,
                filtro: action.payload.filtro
            }
    }
    
    return state;
}