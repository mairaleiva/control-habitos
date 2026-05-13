import type { Dispatch } from "react";
import type { HabitosActions  } from "../reducers/habitosReducer";
import type { Habito } from "../types";


type ListaHabitosProps = {
    dispatch: Dispatch<HabitosActions>,
    habitos: Habito[]
}

function ListaHabitos({dispatch, habitos} : ListaHabitosProps){

    if(habitos.length === 0){
        return <p className="text-gray-500">No hay hábitos registrados.</p>;
    }

    return(

        <div>
            {habitos.map(x => (

            <div
                className="bg-white shadow-md rounded-2xl p-5 flex items-center justify-between gap-4" 
                key={x.id} 
            >
                <span
                className={x.completo ? "line-through text-slate-400 font-semibold" : "text-slate-800 font-semibold"}
                >{x.nombre}</span>
                <button 
                className="bg-red-100 text-red-600 px-3 py-2 rounded-lg font-bold hover:bg-red-200 transition"
                onClick={() => dispatch({type: 'eliminar', payload: {id: x.id}})} 
                >
                Eliminar
                </button>

                <button
                className={x.completo 
                            ? "bg-green-100 text-green-700 px-3 py-2 rounded-lg font-bold hover:bg-green-200 transition"
                            : "bg-yellow-100 text-yellow-700 px-3 py-2 rounded-lg font-bold hover:bg-yellow-200 transition"
                        }
                onClick={() => dispatch({type: 'toggle', payload: {id: x.id}})}
                >
                {x.completo ? 'Completo' : 'Pendiente'}
                </button>
            </div>
        ))}
        </div>
    )
}



export default ListaHabitos;