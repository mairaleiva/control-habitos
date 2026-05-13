import type { Habito } from "../types";


type FormularioProps = {
    nombre: Habito['nombre'],
    setNombre: (nombre: string) => void,
    agregarHabito: () => void
}


function FormularioHabito({nombre, setNombre, agregarHabito} : FormularioProps){
    
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
                    className="w-full bg-indigo-600 hover:bg-indigo-700 text-white font-bold py-3 rounded-xl transition"
                    onClick={agregarHabito}
                >Agregar
                </button>
            </div>
    )
}


export default FormularioHabito;