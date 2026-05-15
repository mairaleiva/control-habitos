import { useReducer, useState, useEffect } from "react"
import { habitosReducer, initialState } from "../reducers/habitosReducer"

export function useHabitos(){

    const estadoInicialConStorage = {
            ...initialState,
            habitos: obtenerHabitosStorage()
        }

    const [state, dispatch] = useReducer(habitosReducer, estadoInicialConStorage);
    const [nombre, setNombre] = useState('');


    useEffect(() => {
        localStorage.setItem("habitos", JSON.stringify(state.habitos));
    }, [state.habitos])


    const agregarHabito = () => {
        if(nombre.trim() === '')
            return 

        dispatch({type: 'guardar', 
                payload: {
                            nuevoHabito: {
                            id: Date.now(),
                            nombre: nombre,
                            completo: false}
                            }
                }
                )

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