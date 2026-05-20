import { useMemo } from "react";
import type { Habito } from "../types";


type EstadisticasHabitosProps = {
    habitosFiltrados: Habito[]
}


function EstadisticasHabitos({habitosFiltrados} : EstadisticasHabitosProps){

    const estadisticas = useMemo(() => {

        const total = habitosFiltrados.length;
        const completos = habitosFiltrados.filter(x => x.completo).length
        const pendientes = habitosFiltrados.filter(x => !x.completo).length

        return {total, completos, pendientes}

    }, [habitosFiltrados])

    return(
        <>
            <div className="bg-white shadow-md rounded-2xl p-4">
                <h2 className="text-xl font-black text-slate-800 mb-3">
                    Estadísticas
                </h2>

                <div className="grid grid-cols-3 gap-2">
                    <div className="bg-slate-100 rounded-xl p-3 text-center">
                        <p className="text-2xl font-black text-indigo-600">
                            {estadisticas.completos}
                        </p>
                        <p className="text-xs font-bold text-slate-600">
                            Completos
                        </p>
                    </div>

                    <div className="bg-slate-100 rounded-xl p-3 text-center">
                        <p className="text-2xl font-black text-indigo-600">
                            {estadisticas.pendientes}
                        </p>
                        <p className="text-xs font-bold text-slate-600">
                            Pendientes
                        </p>
                    </div>

                    <div className="bg-slate-100 rounded-xl p-3 text-center">
                        <p className="text-2xl font-black text-indigo-600">
                            {estadisticas.total}
                        </p>
                        <p className="text-xs font-bold text-slate-600">
                            Total
                        </p>
                    </div>
                </div>
        </div>
        </>
    )
}


export default EstadisticasHabitos;