using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class ControladorScore : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreGlobal;
    public bool estaVivo;
    // Start is called before the first frame update
    void Start()
    {
        estaVivo = true;
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
            //Debug.Log(listadeComida.Length);
            if (listadeComida.Length <= 0)
            {
                //guardamos el score el los pref
                PlayerPrefs.SetInt("score", score);
                GameObject.Find("vidas").GetComponent<ControladorDeVidas>().AgregarVida();
                //aumento de nivel
                PlayerPrefs.SetInt("nivel", PlayerPrefs.GetInt("nivel")+1);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("enemigo"))
        {
            if (gameObject.GetComponent<ControladorDeMovimientoPacman>().powerup >= 1)
            {
                collision.gameObject.GetComponent<EstadoEvadiendo>().comidoPorPacman = true;
                score += 200;
                scoreGlobal.text = string.Format("{0}", score);
            }
            else
            {
                //porque mato a pacman
                //mandamos a los enemigos al centro
                estaVivo = false;
                //demoramos a pacman unos segudos para que aparezca en su ubicacion original
                GetComponent<Animator>().SetBool("muerto",true);
                gameObject.GetComponent<ControladorDeMovimientoPacman>().enabled = false;
                gameObject.GetComponent<ControladorDeMovimientoPacman>().ultimaDesicion = Vector2.zero;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.GetComponent<Collider2D>().enabled = false;
                //ControladorDeMovimientoPacman lo removemos y se lo colocamos despues del tiempo
                //le quitamos una vida
                PlayerPrefs.SetInt("score", score);
                GameObject.Find("vidas").GetComponent<ControladorDeVidas>().QuitarVida();
            }
        }
    }
    public void PacmanRevive()
    {
        estaVivo = true;
        gameObject.GetComponent<ControladorDeMovimientoPacman>().enabled = true;
        GetComponent<Animator>().SetBool("muerto", false);
        gameObject.GetComponent<Collider2D>().enabled = true;
        transform.position = GameObject.Find("OrigenPlayer").transform.position;
    }
    IEnumerator PowerUp()
    {
        gameObject.GetComponent<ControladorDeMovimientoPacman>().powerup = 1;
        yield return new WaitForSeconds(10);
        gameObject.GetComponent<ControladorDeMovimientoPacman>().powerup = 0;
    }
    public void JugarDeNuevo()
    {
        SceneManager.LoadScene("02_Pacman");
    }
}
