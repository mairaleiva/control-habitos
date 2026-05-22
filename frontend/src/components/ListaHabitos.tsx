import type { Dispatch } from "react";
import type { HabitosActions  } from "../reducers/habitosReducer";
import type { Habito } from "../types";


type ListaHabitosProps = {
    dispatch: Dispatch<HabitosActions>,
    habitosFiltrados: Habito[]
}

function ListaHabitos({dispatch, habitosFiltrados} : ListaHabitosProps){

    if(habitosFiltrados.length === 0){
        return <p className="text-gray-500">No hay hábitos registrados.</p>;
    }

    const EliminarHabito = async (id) => {

        const resp = await fetch(`http://localhost:5150/api/habitos/${id}`, {
            method: 'DELETE'
        })

        if(resp.ok){
            dispatch({type: 'eliminar',
                    payload: {id: id}
                    });
        }
    }

    const ToggleHabito = async (habito : Habito) => {

        const resp = await fetch(`http://localhost:5150/api/habitos/${habito.id}`, {
            method: 'PUT',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({
                id: habito.id,
                nombre: habito.nombre,
                completo: !habito.completo
            })

        })

        if(resp.ok){
            dispatch({type: 'toggle', payload: {id: habito.id}})
        }
    }

    return(

        <div>
            {habitosFiltrados.map(x => (

            <div
                className="bg-white shadow-md rounded-2xl p-5 flex items-center justify-between gap-4" 
                key={x.id} 
            >
                <span
                className={x.completo ? "line-through text-slate-400 font-semibold" : "text-slate-800 font-semibold"}
                >{x.nombre}</span>
                <button 
                className="bg-red-100 text-red-600 px-3 py-2 rounded-lg font-bold hover:bg-red-200 transition"
                onClick={() => EliminarHabito(x.id)} 
                >
                Eliminar
                </button>

                <button 
                className="bg-red-100 text-green-600 px-3 py-2 rounded-lg font-bold hover:bg-red-200 transition"
                onClick={() => dispatch({type: 'editar', payload: {id: x.id}})}
                >
                Editar
                </button>

                <button
                className={x.completo 
                            ? "bg-green-100 text-green-700 px-3 py-2 rounded-lg font-bold hover:bg-green-200 transition"
                            : "bg-yellow-100 text-yellow-700 px-3 py-2 rounded-lg font-bold hover:bg-yellow-200 transition"
                        }
                onClick={() => ToggleHabito(x)}
                >
                {x.completo ? 'Completo' : 'Pendiente'}
                </button>
            </div>
        ))}
        </div>
    )
}



export default ListaHabitos;