using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class EstadoEvadiendo : EstadosFinitos
{
    public GameObject pacmen;
    [SerializeField]
    private Vector3 direccion, cardinalidadDelFantasma, diffDePac = new Vector3(0, 0, 0);
    [SerializeField]
    private bool puntosLibres = false;
    public float speed;
    private Color colorOriginal;
    public override void Salir()
    {
        GetComponent<SpriteRenderer>().color = colorOriginal;
    }

    public override void Start()
    {
        base.Start();
        //aqui tengo que buscar a quien tengo que perseguir
        e.pacman = GameObject.Find("Pacman");
        //guardamos su color
        colorOriginal = GetComponent<SpriteRenderer>().color;
        //le cambiamos el color porque esta huyendo
        GetComponent<SpriteRenderer>().color = new Color(31, 30, 255);
        pacmen = e.pacman.transform.Find(transform.name).gameObject;
        speed = 1;
        buscarCardinalidadHaciaElObjetivo();
        //ignoramos las colisiones de todos los fantasmas
        GameObject[] listaDeEnemigos = GameObject.FindGameObjectsWithTag("EditorOnly").ToArray();
        foreach (GameObject e in listaDeEnemigos)
        {
            Physics2D.IgnoreCollision(e.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        anteriorPosicion = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            comidoPorPacman = true;
        }
    }
    public bool comidoPorPacman;
    public override Type VerficarTransiciones()
    {
        if (e.pacman.GetComponent<ControladorDeMovimientoPacman>().powerup <= 0)
        {
            return typeof(EstadoPersiguiendo);
        }
        else if (comidoPorPacman)
        {
            return typeof(EstadoMuerte);
        }
        else
        {
            return this.GetType();
        }
    }

    Vector2 anteriorPosicion;
    //vamos a contar cuantos frames son iguales
    public int contador;
    public override void Update()
    {
        if (LosPuntosEstanLibres())
        {
            buscarCardinalidadHaciaElObjetivo();
        }
        else
        {
            direccion = GirarHaciaDondeNoEstaEstrellado();
        }
        puntosLibres = LosPuntosEstanLibres();
        GetComponent<Rigidbody2D>().velocity = direccion*-1;
        VerificarCambios();
    }

    public string ConvertVectorToString(Vector2 vector)
    {
        if (vector == Vector2.up)
        {
            return "arriba";
        }
        if (vector == Vector2.down)
        {
            return "abajo";
        }
        if (vector == Vector2.left)
        {
            return "izqueirda";
        }
        if (vector == Vector2.right)
        {
            return "derecha";
        }
        return "centro";
    }

    private Vector3 GirarHaciaDondeNoEstaEstrellado()
    {
        if (cardinalidadDelFantasma == Vector3.up)
        {
            //los de arriba encendidos
            if (transform.Find("IzqSup").gameObject.GetComponent<BuscadorPacmen>().estaEstrellado)
            {
                //vamos para la derecha
                return transform.TransformDirection(Vector3.right);
            }
            else if (transform.Find("DerSup").gameObject.GetComponent<BuscadorPacmen>().estaEstrellado)
            {
                //vamos para la izquierda
                return transform.TransformDirection(Vector3.left);
            }
        }
        if (cardinalidadDelFantasma == Vector3.down)
        {
            //los de abajo encendidos
            if (transform.Find("IzqInf").gameObject.GetComponent<BuscadorPacmen>().estaEstrellado)
            {
                //vamos para la derecha
                return transform.TransformDirection(Vector3.right);
            }
            else if (transform.Find("DerInf").gameObject.GetComponent<BuscadorPacmen>().estaEstrellado)
            {
                //vamos para la izquierda
                return transform.TransformDirection(Vector3.left);
            }
        }
        if (cardinalidadDelFantasma == Vector3.right)
        {
            //los de abajo encendidos
            if (transform.Find("DerSup").gameObject.GetComponent<BuscadorPacmen>().estaEstrellado)
            {
                //vamos para la abajo
                return transform.TransformDirection(Vector3.down);
            }
            else if (transform.Find("DerInf").gameObject.GetComponent<BuscadorPacmen>().estaEstrellado)
            {
                //vamos para la arriba
                return transform.TransformDirection(Vector3.up);
            }
        }
        if (cardinalidadDelFantasma == Vector3.left)
        {
            //los de abajo encendidos
            if (transform.Find("IzqSup").gameObject.GetComponent<BuscadorPacmen>().estaEstrellado)
            {
                //vamos para la abajo
                return transform.TransformDirection(Vector3.down);
            }
            else if (transform.Find("IzqInf").gameObject.GetComponent<BuscadorPacmen>().estaEstrellado)
            {
                //vamos para la arriba
                return transform.TransformDirection(Vector3.up);
            }
        }
        return new Vector3();
    }
    private bool LosPuntosEstanLibres()
    {
        return !(transform.Find("IzqSup").GetComponent<BuscadorPacmen>().estaEstrellado ||
            transform.Find("DerSup").GetComponent<BuscadorPacmen>().estaEstrellado ||
            transform.Find("IzqInf").GetComponent<BuscadorPacmen>().estaEstrellado ||
            transform.Find("DerInf").GetComponent<BuscadorPacmen>().estaEstrellado);

    }

    private List<string> BuscandoLasPosiblesPosiciones(Vector3 diff)
    {
        List<string> posicionesPosibles = new List<string>();
        if (diff.x < 0)
        {
            //derecha
            posicionesPosibles.Add("izquierda");
        }
        if (diff.x > 0)
        {
            posicionesPosibles.Add("derecha");
        }
        if (diff.y > 0)
        {
            posicionesPosibles.Add("arriba");
        }
        if (diff.y < 0)
        {
            posicionesPosibles.Add("abajo");
        }
        return posicionesPosibles;
    }

    private void buscarCardinalidadHaciaElObjetivo()
    {
        string resultado;
        Vector3 diff = pacmen.transform.position - transform.position;
        diffDePac = diff;
        //Tenemos vector para calcular
        List<string> posicionesPosibles = BuscandoLasPosiblesPosiciones(diff);

        resultado = "";
        float distancia = 0;
        List<string> direccionesPosibles = new List<string>();
        foreach (string item in posicionesPosibles)
        {
            if (item == "arriba")
            {
                direccion = transform.TransformDirection(Vector3.up);
                distancia = Mathf.Abs(direccion.y);
            }
            if (item == "abajo")
            {
                direccion = transform.TransformDirection(Vector3.down);
                distancia = Mathf.Abs(direccion.y);
            }
            if (item == "derecha")
            {
                direccion = transform.TransformDirection(Vector3.right);
                distancia = Mathf.Abs(direccion.x);
            }
            if (item == "izquierda")
            {
                direccion = transform.TransformDirection(Vector3.left);
                distancia = Mathf.Abs(direccion.x);
            }
            Debug.DrawRay(transform.position, (direccion), Color.yellow);
            //+ (direccion / 2)
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direccion, distancia);

            if (hit.collider == null)
            {
                direccionesPosibles.Add(item);
            }
            else if (hit.transform.CompareTag("Player"))
            {
                direccionesPosibles.Add(item);
            }
        }
        if (direccionesPosibles.Count == 2)
        {
            //es porque son dos direcciones
            //solo comparamos su X y Y para ver quien esta mas lejos
            if (Mathf.Abs(diff.x) >= Mathf.Abs(diff.y))
            {
                resultado = diff.x < 0 ? "izquierda" : "derecha";
            }
            else
            {
                resultado = diff.y < 0 ? "abajo" : "arriba";
            }
        }
        if (direccionesPosibles.Count == 1)
        {
            //la respuesta es el primero
            resultado = direccionesPosibles[0];
        }
        if (direccionesPosibles.Count == 0)
        {
            //tenemos que pensar que hacer en este caso
            if (Mathf.Abs(diff.x) <= Mathf.Abs(diff.y))
            {
                resultado = diff.x > 0 ? "izquierda" : "derecha";
            }
            else
            {
                resultado = diff.y > 0 ? "abajo" : "arriba";
            }
        }
        //resultado sabemos hacia donde debe de dirigirse el fantasma
        ActivarRayosRaycvast(resultado);
        Moverlo(resultado);
    }

    private void ActivarRayosRaycvast(string resultado)
    {

        transform.Find("IzqSup").gameObject.GetComponent<BuscadorPacmen>().comprobar = false;
        transform.Find("DerSup").gameObject.GetComponent<BuscadorPacmen>().comprobar = false;
        transform.Find("IzqInf").gameObject.GetComponent<BuscadorPacmen>().comprobar = false;
        transform.Find("DerInf").gameObject.GetComponent<BuscadorPacmen>().comprobar = false;

        if (resultado == "arriba")
        {
            transform.Find("IzqSup").gameObject.GetComponent<BuscadorPacmen>().comprobar = true;
            transform.Find("DerSup").gameObject.GetComponent<BuscadorPacmen>().comprobar = true;
        }
        if (resultado == "abajo")
        {
            transform.Find("IzqInf").gameObject.GetComponent<BuscadorPacmen>().comprobar = true;
            transform.Find("DerInf").gameObject.GetComponent<BuscadorPacmen>().comprobar = true;
        }
        if (resultado == "derecha")
        {
            transform.Find("DerInf").gameObject.GetComponent<BuscadorPacmen>().comprobar = true;
            transform.Find("DerSup").gameObject.GetComponent<BuscadorPacmen>().comprobar = true;
        }
        if (resultado == "izquierda")
        {
            transform.Find("IzqInf").gameObject.GetComponent<BuscadorPacmen>().comprobar = true;
            transform.Find("IzqSup").gameObject.GetComponent<BuscadorPacmen>().comprobar = true;
        }
    }

    private void Moverlo(string resultado)
    {
        if (resultado == "arriba")
        {
            direccion = transform.TransformDirection(Vector3.up);
        }
        if (resultado == "abajo")
        {
            direccion = transform.TransformDirection(Vector3.down);
        }
        if (resultado == "derecha")
        {
            direccion = transform.TransformDirection(Vector3.right);
        }
        if (resultado == "izquierda")
        {
            direccion = transform.TransformDirection(Vector3.left);
        }
        //CambioDeCardinalidad (direccion);
        cardinalidadDelFantasma = direccion * speed;
    }


    public bool bIsOnTheMove = false;
    public int vecesTrabajo;
    private IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(0.01f);
        Vector3 finalPos = transform.position;
        float magnitudeDeMovimiento = (Math.Abs(startPos.magnitude) - Math.Abs(finalPos.magnitude));
        if (Math.Abs(magnitudeDeMovimiento) == 0)
        {
            //esta trabado completamente
            //lo mandamos para donde sea
            //TODO: lo mandamos a volar a otro lado
            MandarloOtroLado();
        }
    }
    [SerializeField]
    bool autonomo;
    private void MandarloOtroLado()
    {
        //necesitamos saber a donde se puede mover
        List<Vector2> listaDeCaminosLibres = new List<Vector2>();
        if (Physics2D.Raycast(transform.position, Vector2.left, transform.localScale.x).collider == null)
        {
            listaDeCaminosLibres.Add(Vector2.left);
        }
        if (Physics2D.Raycast(transform.position, Vector2.right, transform.localScale.x).collider == null)
        {
            listaDeCaminosLibres.Add(Vector2.right);
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.x).collider == null)
        {
            listaDeCaminosLibres.Add(Vector2.down);
        }
        if (Physics2D.Raycast(transform.position, Vector2.up, transform.localScale.x).collider == null)
        {
            listaDeCaminosLibres.Add(Vector2.up);
        }
        int cardinalidadAleatoria = UnityEngine.Random.Range(0, listaDeCaminosLibres.Count);
        direccion = listaDeCaminosLibres[cardinalidadAleatoria];
        autonomo = true;
        //mandarlo a ese lado hasta que chique con una pared

    }
}
