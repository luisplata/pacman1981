using UnityEngine;
using System.Collections;

public class ComportamientoEnemigo : MonoBehaviour
{
    public Enemigo enemigo = new Enemigo();

    // Use this for initialization
    void Start()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("enemigo");
        foreach (GameObject e in enemigos)
        {
            if(e != this)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), e.GetComponent<Collider2D>());
            }
            else
            {
                Debug.Log("Es igual a este");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
