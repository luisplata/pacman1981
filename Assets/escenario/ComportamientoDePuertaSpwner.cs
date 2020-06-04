using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoDePuertaSpwner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //hacemos que ignore la colicion con el enemigo
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("enemigo");
        foreach(GameObject e in enemigos)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), e.GetComponent<Collider2D>());
        }
    }

}
