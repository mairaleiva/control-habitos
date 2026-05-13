import type { Dispatch } from "react";
import type { HabitosActions  } from "../reducers/habitosReducer";
import type { FiltroOpciones  } from "../types";

type FiltroHabitosProps = {
    dispatch: Dispatch<HabitosActions>,
    filtroActual: FiltroOpciones
}


function FiltroHabitos({dispatch, filtroActual} : FiltroHabitosProps){


    return (
        <div>
            <button 
            onClick={() => dispatch({type: 'cambiar-filtro', payload: {filtro: "todos"}})}
            className={filtroActual === "todos" ? "bg-indigo-600 text-white" : "bg-slate-200 text-slate-700"}
            >
                Todos
            </button>

            <button 
            onClick={() => dispatch({type: 'cambiar-filtro', payload: {filtro: "completo"}})}
            className={filtroActual === "completo" ? "bg-indigo-600 text-white" : "bg-slate-200 text-slate-700"}
            >
                Completo
            </button>
            
            <button 
            onClick={() => dispatch({type: 'cambiar-filtro', payload: {filtro: "pendiente"}})}
            className={filtroActual === "pendiente" ? "bg-indigo-600 text-white" : "bg-slate-200 text-slate-700"}
            >
                Pendiente
            </button>
        </div>
    )


}


export default FiltroHabitos;