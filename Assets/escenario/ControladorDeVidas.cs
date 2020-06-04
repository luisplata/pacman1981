using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (PlayerPrefs.HasKey("vidas"))
        {
            vidas = PlayerPrefs.GetInt("vidas");
        }
        else
        {
            vidas = 2;
        }
        PintarVidas();
    }
    private void Update()
    {
        if(vidas < 0)
        {
            SceneManager.LoadScene("03_Creditos");
        }
    }
    private void DestruirLasVidas()
    {
        foreach(GameObject v in vidasCargadas)
        {
            Destroy(v);
        }
        vidasCargadas = new List<GameObject>();
    }
    private void PintarVidas()
    {
        DestruirLasVidas();
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
        PintarVidas();
        GuardarVidas();
    }

    public void AgregarVida()
    {
        Debug.Log("Agrego");
        vidas++;
        PintarVidas();
        GuardarVidas();
    }

    public void GuardarVidas()
    {
        PlayerPrefs.SetInt("vidas", vidas);
    }
}
