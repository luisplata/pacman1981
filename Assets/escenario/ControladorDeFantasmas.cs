using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeFantasmas : MonoBehaviour
{
    //aqui vamos a soltar a cada fantasma cada cierta cantidad de tiempo
    float deltatimelocal = 0;
    public List<GameObject> enemigos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deltatimelocal += Time.deltaTime;
        if(deltatimelocal >= 5 && enemigos.Count != 0)
        {
            //soltamos al primero
            enemigos[0].AddComponent(typeof(EstadoSaliento));
            enemigos.RemoveAt(0);
            deltatimelocal = 0;
        }
    }
}
