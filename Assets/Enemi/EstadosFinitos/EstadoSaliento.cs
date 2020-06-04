using UnityEngine;
using UnityEditor;
using System;

public class EstadoSaliento : EstadosFinitos
{
    private bool salio;
    public override void Salir()
    {
        //cuando salga del estado vuelve a tener la colision para que no entre de nuevo por accidente
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GameObject.Find("spawner_up").GetComponent<Collider2D>(), false);
    }

    public override void Start()
    {
        Debug.Log("Esta saliendo");
        base.Start();
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GameObject.Find("spawner_up").GetComponent<Collider2D>());
    }

    public override Type VerficarTransiciones()
    {
        if (salio)
        {
            return typeof(EstadoPersiguiendo);
        }
        else
        {
            return this.GetType();
        }
    }

    public override void Update()
    {
        //va a ir hacia adelante Arriba del mapa para salir del cajo
        GetComponent<Rigidbody2D>().velocity = Vector2.up;
        VerificarCambios();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //coliciona con el triger de la casa
        if (collision.gameObject.CompareTag("salida"))
        {
            Debug.Log("choco con salida");
            salio = true;
        }
        else
        {
            Debug.Log("no era salida, era "+collision.gameObject.name);
        }
    }
}