import type { Dispatch } from "react";
import type { HabitosActions  } from "../reducers/habitosReducer";
import type { FiltroOpciones  } from "../types";

type FiltroHabitosProps = {
    dispatch: Dispatch<HabitosActions>,
    filtroActual: FiltroOpciones
}


function FiltroHabitos({dispatch, filtroActual} : FiltroHabitosProps){


    return (
        <div className="bg-white shadow-md rounded-2xl p-3 flex gap-3 justify-center">
            <button
            onClick={() => dispatch({type: 'cambiar-filtro', payload: {filtro: "todos"}})}
            className={`
                            px-4 py-2 rounded-xl font-bold transition
                            ${filtroActual === "todos"
                                ? "bg-indigo-600 text-white"
                                : "bg-slate-100 text-slate-700 hover:bg-slate-200"}
                    `}
            >
                Todos
            </button>

            <button 
            onClick={() => dispatch({type: 'cambiar-filtro', payload: {filtro: "completo"}})}
            className={`
                        px-4 py-2 rounded-xl font-bold transition
                        ${filtroActual === "completo"
                            ? "bg-indigo-600 text-white"
                            : "bg-slate-100 text-slate-700 hover:bg-slate-200"}
                        `}
            >
                Completo
            </button>
            
            <button 
            onClick={() => dispatch({type: 'cambiar-filtro', payload: {filtro: "pendiente"}})}
            className={`
                            px-4 py-2 rounded-xl font-bold transition
                            ${filtroActual === "pendiente"
                                ? "bg-indigo-600 text-white"
                                : "bg-slate-100 text-slate-700 hover:bg-slate-200"}
                        `}
            >
                Pendiente
            </button>
        </div>
    )


}


export default FiltroHabitos;