using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeVidas : MonoBehaviour
{
    public GameObject prefabDeVida;
    [SerializeField]
    private int vidas;
    private List<GameObject> vidasCargadas = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //lo que vamos a hacer al inicio es darle 3 vidas al PJ y colocarlas en pantalla
        vidas = 3;
        PintarVidas();
    }
    private void PintarVidas()
    {
        for (int i = 1; i <= vidas; i++)
        {
            vidasCargadas.Add(Instantiate(prefabDeVida, (Vector2)transform.position + (Vector2.right * i), prefabDeVida.transform.rotation, transform));
        }
    }
    //metodos publicos para agregar o quitar vida
    public void QuitarVida()
    {
        Debug.Log("Quito");
        vidas--;
        //PintarVidas();
    }

    public void AgregarVida()
    {
        Debug.Log("Agrego");
        vidas++;
        //PintarVidas();
    }
}
