using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeTransportadores : MonoBehaviour
{
    public GameObject objetivo;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    //vamos a desactivar el controlador de movimiento si pasa por el triger
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.position = objetivo.transform.position;
    }
}
