import FormularioHabito from "./components/FormularioHabito"
import ListaHabitos from "./components/ListaHabitos"
import FiltroHabitos from "./components/FiltroHabitos"
import { useHabitos } from "./components/Hooks/useHabitos"
import { useMemo } from "react"
import type { Habito } from "./types"

function App() {

    const {state, dispatch, nombre, setNombre, agregarHabito} = useHabitos();

    const habitosFiltrados: Habito[] = useMemo(() => {
      if(state.filtro === "todos")
        return state.habitos;

      if(state.filtro === "completo")
        return state.habitos.filter(x => x.completo);

      if(state.filtro === "pendiente")
        return state.habitos.filter(x => !x.completo);

      return []

    }, [state.habitos, state.filtro]);
  
  return (
    <>
      <div className="max-w-2xl mx-auto mt-10 p-5 space-y-8">
          <h1 className="text-5xl font-black text-center text-slate-800">Control de Hábitos</h1>

          <FormularioHabito
            nombre={nombre}
            setNombre={setNombre}
            agregarHabito={agregarHabito}
          />

          <ListaHabitos
            dispatch={dispatch}
            habitosFiltrados={habitosFiltrados}
          />

          <FiltroHabitos
            dispatch={dispatch}
            filtroActual={state.filtro}
          />
          
      </div>
    </>
  )
}

export default App
