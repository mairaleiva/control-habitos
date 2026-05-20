import { useReducer, useState, useEffect } from "react"
import { habitosReducer, initialState } from "../reducers/habitosReducer"

export function useHabitos(){

    const estadoInicialConStorage = {
            ...initialState,
            habitos: obtenerHabitosStorage()
        }

    const [state, dispatch] = useReducer(habitosReducer, estadoInicialConStorage);
    const [nombre, setNombre] = useState('');

    // ========================================================

    useEffect(() => {
        localStorage.setItem("habitos", JSON.stringify(state.habitos));
    }, [state.habitos])


    //editando
    useEffect(() => {
        const habito = state.habitos.find(x => x.id === state.habitoIdActividad);

        if(habito){
            setNombre(habito.nombre)
        }else{
            setNombre('')
        }

    }, [state.habitoIdActividad])


    useEffect(() => {
        const url = 'http://localhost:5150/api/habitos';

        fetch(url)
        .then(result => result.json())
        .then(datos => dispatch({type: 'cargar-habitos', payload: {habito: datos}}))
    }, [])

    // ========================================================
    const agregarHabito = async () => {

        if(nombre.trim() === '')
            return

        const nuevoHabito =  {  id: state.habitoIdActividad !== null ? state.habitoIdActividad : Date.now(),
                                nombre: nombre,
                                completo: false
                            }

        if(state.habitoIdActividad !== null){
            const resp = await fetch(`http://localhost:5150/api/habitos/${state.habitoIdActividad}`, {
                method: 'PUT',
                headers: {
                            'Content-Type': 'application/json'
                        },
                body: JSON.stringify(nuevoHabito)
            })

            const dataPUT = await resp.json();

            dispatch({type: 'guardar',
                    payload: {
                                nuevoHabito: dataPUT
                            }
                    }
                    )
            
        }else{
            const response = await fetch('http://localhost:5150/api/habitos', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(nuevoHabito)
                    })

            const data = await response.json();

            dispatch({type: 'guardar',
                    payload: {
                                nuevoHabito: data
                            }
                    }
                    )
            }

        setNombre('')
    }

    return {
        nombre,
        setNombre,
        state,
        dispatch,
        agregarHabito
    }
}

function obtenerHabitosStorage(){
    const guardado =  localStorage.getItem("habitos");

    if(guardado !== null){
        return JSON.parse(guardado)
    }

    return []
}