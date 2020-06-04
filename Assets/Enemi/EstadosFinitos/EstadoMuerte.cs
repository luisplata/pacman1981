using UnityEngine;
using System.Collections;
using System;

public class EstadoMuerte : EstadosFinitos
{
    public override void Salir()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    Vector2 origen;
    public override void Start()
    {
        base.Start();
        //lo mandamos al origen sin importan nada
        origen = GameObject.Find("OrigenDeEnemigo").transform.position;
        //desactivamos el colisionador para que atraviese todo
        GetComponent<Collider2D>().enabled = false;
        //buscamos el vector resultante hacia el objeivo
        //Vector2 diff = origen - (Vector2)transform.position;
        GetComponent<Rigidbody2D>().MovePosition(origen);
    }
    bool llegoAlOrigen;
    public override Type VerficarTransiciones()
    {
        if (llegoAlOrigen)
        {
            return typeof(EstadoSaliento);
        }
        else
        {
            return this.GetType();
        }
    }
    public override void Update()
    {
        //no salimos hasta que no estemos en el origen
        llegoAlOrigen = (Vector2)transform.position == origen;
        VerificarCambios();
    }
}
