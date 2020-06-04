﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EstadosFinitos : MonoBehaviour
{
    public Enemigo e;
    public abstract void Salir();
    private Vector2 cardinalidadDelFantasma;
    public virtual void Start()
    {
        e = GetComponent<ComportamientoEnemigo>().enemigo;
    }

    public abstract void Update();

    public abstract Type VerficarTransiciones();

    public virtual Vector2 GetCardinalidad()
    {
        return this.cardinalidadDelFantasma;
    }


    public void VerificarCambios()
    {
        Type estadoACambiar = VerficarTransiciones();
        if(estadoACambiar != this.GetType())
        {
            CambiarEstado(estadoACambiar);
        }
    }

    public void CambiarEstado(Type nuevoEstado)
    {
        //salir del estado actual
        Salir();
        //agregamos el componente
        gameObject.AddComponent(nuevoEstado);
        //destuimos el estado viejo
        Destroy(this);
    }

}
