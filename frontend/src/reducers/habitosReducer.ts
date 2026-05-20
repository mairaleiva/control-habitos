import type { Habito, FiltroOpciones } from "../types"


//inicializar el state
export const initialState : HabitosState = {
    habitos: [],
    filtro: "todos",
    habitoIdActividad: null
}

//un tipo para state
export type HabitosState = {
    habitos: Habito[],
    filtro: FiltroOpciones,
    habitoIdActividad: number | null
}

//un tipo para action
export type HabitosActions = 
    {type: 'guardar', payload: {nuevoHabito: Habito}} |
    {type: 'editar', payload: {id: Habito['id']}} |
    {type: 'eliminar', payload: {id: Habito['id']}} |
    {type: 'toggle', payload: {id: Habito['id']}} |
    {type: 'cambiar-filtro', payload: {filtro: FiltroOpciones}} |
    {type: 'cancelar-edicion'} |
    {type: 'cargar-habitos', payload: {habito: Habito[]}} 

//una funcion
export function habitosReducer(state: HabitosState, action: HabitosActions){

    switch(action.type){
        case 'guardar':
                if (state.habitoIdActividad !== null) {
                return {
                    ...state,
                    habitos: state.habitos.map(x => x.id === state.habitoIdActividad
                            ? {
                                ...x,
                                nombre: action.payload.nuevoHabito.nombre
                            }
                            : x
                    ),

                    habitoIdActividad: null
                }
            }

            return {
                ...state,
                habitos: [...state.habitos, action.payload.nuevoHabito]
            }
            
        case 'editar':
            return{
                ...state,
                habitoIdActividad: action.payload.id
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
        case 'cancelar-edicion':
            return {
                ...state,
                habitoIdActividad: null
            }
        case 'cargar-habitos':
            return{
                ...state,
                habitos: action.payload.habito
            }
    }
    
    return state;
}