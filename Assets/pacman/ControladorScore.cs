using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class ControladorScore : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreGlobal;
    // Start is called before the first frame update
    void Start()
    {
        //buscamos el score de los playerpref
        if (PlayerPrefs.HasKey("score"))
        {
            score = PlayerPrefs.GetInt("score");
        }
        else
        {
            score = 0;
        }
        scoreGlobal.text = string.Format("{0}", score);
    }
    private bool comio = false;
    private void Update()
    {
        if (comio)
        {
            //verificamos que no se haya comido todo o si no lo mandamos de nuevo al game
            GameObject[] listadeComida = GameObject.FindGameObjectsWithTag("comida");
            Debug.Log(listadeComida.Length);
            if (listadeComida.Length <= 0)
            {
                //guardamos el score el los pref
                PlayerPrefs.SetInt("score", score);
                //recargamos el mapa
                JugarDeNuevo();
            }
            comio = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("comida"))
        {
            score++;
            //destuimos la comida
            Destroy(collision.gameObject);
            //actualizamos la UI
            scoreGlobal.text = string.Format("{0}", score);
            comio = true;
        }
        if (collision.transform.CompareTag("powerup"))
        {
            StartCoroutine(PowerUp());
            score +=10;
            //destuimos la comida
            Destroy(collision.gameObject);
            //actualizamos la UI
            scoreGlobal.text = string.Format("{0}", score);
        }
    }

    IEnumerator PowerUp()
    {
        gameObject.GetComponent<ControladorDeMovimientoPacman>().powerup = 1;
        yield return new WaitForSeconds(10);
        gameObject.GetComponent<ControladorDeMovimientoPacman>().powerup = 0;
    }
    public void JugarDeNuevo()
    {
        SceneManager.LoadScene("Pacman");
    }
}
