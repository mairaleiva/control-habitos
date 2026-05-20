import type { Habito } from "../types";
import type { Dispatch } from "react";
import type { HabitosActions  } from "../reducers/habitosReducer";

type FormularioProps = {
    nombre: Habito['nombre'],
    setNombre: (nombre: string) => void,
    agregarHabito: () => void,
    habitoIdActividad: number | null,
    dispatch: Dispatch<HabitosActions>
}


function FormularioHabito({nombre, setNombre, agregarHabito, habitoIdActividad, dispatch} : FormularioProps){
    
    return (
            <div className="bg-white shadow-lg rounded-2xl p-6 space-y-4">
                <label className="block text-sm font-bold text-slate-700">Habito: </label>
                <input 
                    className="w-full border border-slate-300 rounded-xl p-3 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                    type="text" 
                    value={nombre} 
                    onChange={(e) => setNombre(e.target.value)}
                >
                </input>
                <button 
                    className={`w-full font-bold py-3 rounded-xl transition ${
                                    nombre.trim() !== ""
                                    ? "bg-indigo-600 hover:bg-indigo-700 text-white"
                                    : "bg-slate-400 opacity-50 cursor-not-allowed text-white"
                                }`}
                    onClick={agregarHabito}
                    disabled={nombre.trim() === ""}
                >
                    {habitoIdActividad == null ? 'Agregar' : 'Editar'}
                </button>

                {habitoIdActividad !== null && (
                    <button 
                        className="w-full bg-indigo-600 hover:bg-indigo-700 text-white font-bold py-3 rounded-xl transition"
                        onClick={() => dispatch({type: 'cancelar-edicion'})}
                    >
                        Cancelar Edición
                    </button>
                )}

            </div>
    )
}


export default FormularioHabito;