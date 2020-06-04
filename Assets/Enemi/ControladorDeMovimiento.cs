using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControladorDeMovimiento : MonoBehaviour {
    public GameObject pacmen;
    [SerializeField]
    private Vector3 direccion, cardinalidadDelFantasma, diffDePac = new Vector3 (0, 0, 0);
    [SerializeField]
    private bool puntosLibres = false;
    public float timescale, speed;

    //controlador de puntos de rayos raycast
    private void Start () {
        buscarCardinalidadHaciaElObjetivo ();
        //ignoramos las colisiones de todos los fantasmas
        GameObject[] listaDeEnemigos = GameObject.FindGameObjectsWithTag("EditorOnly").ToArray();
        foreach(GameObject e in listaDeEnemigos)
        {
            Physics2D.IgnoreCollision(e.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        // Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    // Update is called once per frame
    void Update () {
        Time.timeScale = timescale;
        if (LosPuntosEstanLibres ()) {
            buscarCardinalidadHaciaElObjetivo ();
        } else {
            direccion = GirarHaciaDondeNoEstaEstrellado ();
        }
        puntosLibres = LosPuntosEstanLibres ();
        GetComponent<Rigidbody2D> ().velocity = direccion;
    }

    private Vector3 GirarHaciaDondeNoEstaEstrellado () {
        if (cardinalidadDelFantasma == Vector3.up) {
            //los de arriba encendidos
            if (transform.Find ("izqSup").gameObject.GetComponent<BuscadorPacmen> ().estaEstrellado) {
                //vamos para la derecha
                return transform.TransformDirection (Vector3.right);
            } else if (transform.Find ("derSup").gameObject.GetComponent<BuscadorPacmen> ().estaEstrellado) {
                //vamos para la izquierda
                return transform.TransformDirection (Vector3.left);
            }
        }
        if (cardinalidadDelFantasma == Vector3.down) {
            //los de abajo encendidos
            if (transform.Find ("izqInf").gameObject.GetComponent<BuscadorPacmen> ().estaEstrellado) {
                //vamos para la derecha
                return transform.TransformDirection (Vector3.right);
            } else if (transform.Find ("derInf").gameObject.GetComponent<BuscadorPacmen> ().estaEstrellado) {
                //vamos para la izquierda
                return transform.TransformDirection (Vector3.left);
            }
        }
        if (cardinalidadDelFantasma == Vector3.right) {
            //los de abajo encendidos
            if (transform.Find ("derSup").gameObject.GetComponent<BuscadorPacmen> ().estaEstrellado) {
                //vamos para la abajo
                return transform.TransformDirection (Vector3.down);
            } else if (transform.Find ("derInf").gameObject.GetComponent<BuscadorPacmen> ().estaEstrellado) {
                //vamos para la arriba
                return transform.TransformDirection (Vector3.up);
            }
        }
        if (cardinalidadDelFantasma == Vector3.left) {
            //los de abajo encendidos
            if (transform.Find ("izqSup").gameObject.GetComponent<BuscadorPacmen> ().estaEstrellado) {
                //vamos para la abajo
                return transform.TransformDirection (Vector3.down);
            } else if (transform.Find ("izqInf").gameObject.GetComponent<BuscadorPacmen> ().estaEstrellado) {
                //vamos para la arriba
                return transform.TransformDirection (Vector3.up);
            }
        }
        return new Vector3 ();
    }
    private bool LosPuntosEstanLibres () {
        return !(transform.Find ("izqSup").GetComponent<BuscadorPacmen> ().estaEstrellado ||
            transform.Find ("derSup").GetComponent<BuscadorPacmen> ().estaEstrellado ||
            transform.Find ("izqInf").GetComponent<BuscadorPacmen> ().estaEstrellado ||
            transform.Find ("derInf").GetComponent<BuscadorPacmen> ().estaEstrellado);

    }

    private List<string> BuscandoLasPosiblesPosiciones (Vector3 diff) {
        List<string> posicionesPosibles = new List<string> ();
        if (diff.x < 0) {
            //derecha
            posicionesPosibles.Add ("izquierda");
        }
        if (diff.x > 0) {
            posicionesPosibles.Add ("derecha");
        }
        if (diff.y > 0) {
            posicionesPosibles.Add ("arriba");
        }
        if (diff.y < 0) {
            posicionesPosibles.Add ("abajo");
        }
        return posicionesPosibles;
    }

    private void buscarCardinalidadHaciaElObjetivo () {
        string resultado;
        Vector3 diff = pacmen.transform.position - transform.position;
        diffDePac = diff;
        //Tenemos vector para calcular
        List<string> posicionesPosibles = BuscandoLasPosiblesPosiciones (diff);

        resultado = "";
        float distancia = 0;
        List<string> direccionesPosibles = new List<string> ();
        foreach (string item in posicionesPosibles) {
            if (item == "arriba") {
                direccion = transform.TransformDirection (Vector3.up);
                distancia = Mathf.Abs (direccion.y);
            }
            if (item == "abajo") {
                direccion = transform.TransformDirection (Vector3.down);
                distancia = Mathf.Abs (direccion.y);
            }
            if (item == "derecha") {
                direccion = transform.TransformDirection (Vector3.right);
                distancia = Mathf.Abs (direccion.x);
            }
            if (item == "izquierda") {
                direccion = transform.TransformDirection (Vector3.left);
                distancia = Mathf.Abs (direccion.x);
            }
            Debug.DrawRay (transform.position, (direccion), Color.yellow);
            //+ (direccion / 2)
            RaycastHit2D hit = Physics2D.Raycast (transform.position, direccion, distancia);

            if (hit.collider == null) {
                direccionesPosibles.Add (item);
            } else if (hit.transform.CompareTag ("Player")) {
                direccionesPosibles.Add (item);
            }
        }
        if (direccionesPosibles.Count == 2) {
            //es porque son dos direcciones
            //solo comparamos su X y Y para ver quien esta mas lejos
            if (Mathf.Abs (diff.x) >= Mathf.Abs (diff.y)) {
                resultado = diff.x < 0 ? "izquierda" : "derecha";
            } else {
                resultado = diff.y < 0 ? "abajo" : "arriba";
            }
        }
        if (direccionesPosibles.Count == 1) {
            //la respuesta es el primero
            resultado = direccionesPosibles[0];
        }
        if (direccionesPosibles.Count == 0) {
            //tenemos que pensar que hacer en este caso
            if (Mathf.Abs (diff.x) <= Mathf.Abs (diff.y)) {
                resultado = diff.x > 0 ? "izquierda" : "derecha";
            } else {
                resultado = diff.y > 0 ? "abajo" : "arriba";
            }
        }
        //resultado sabemos hacia donde debe de dirigirse el fantasma
        ActivarRayosRaycvast (resultado);
        Moverlo (resultado);
    }

    private void ActivarRayosRaycvast (string resultado) {

        transform.Find ("izqSup").gameObject.GetComponent<BuscadorPacmen> ().comprobar = false;
        transform.Find ("derSup").gameObject.GetComponent<BuscadorPacmen> ().comprobar = false;
        transform.Find ("izqInf").gameObject.GetComponent<BuscadorPacmen> ().comprobar = false;
        transform.Find ("derInf").gameObject.GetComponent<BuscadorPacmen> ().comprobar = false;

        if (resultado == "arriba") {
            transform.Find ("izqSup").gameObject.GetComponent<BuscadorPacmen> ().comprobar = true;
            transform.Find ("derSup").gameObject.GetComponent<BuscadorPacmen> ().comprobar = true;
        }
        if (resultado == "abajo") {
            transform.Find ("izqInf").gameObject.GetComponent<BuscadorPacmen> ().comprobar = true;
            transform.Find ("derInf").gameObject.GetComponent<BuscadorPacmen> ().comprobar = true;
        }
        if (resultado == "derecha") {
            transform.Find ("derInf").gameObject.GetComponent<BuscadorPacmen> ().comprobar = true;
            transform.Find ("derSup").gameObject.GetComponent<BuscadorPacmen> ().comprobar = true;
        }
        if (resultado == "izquierda") {
            transform.Find ("izqInf").gameObject.GetComponent<BuscadorPacmen> ().comprobar = true;
            transform.Find ("izqSup").gameObject.GetComponent<BuscadorPacmen> ().comprobar = true;
        }
    }

    private void Moverlo (string resultado) {
        if (resultado == "arriba") {
            direccion = transform.TransformDirection (Vector3.up);
        }
        if (resultado == "abajo") {
            direccion = transform.TransformDirection (Vector3.down);
        }
        if (resultado == "derecha") {
            direccion = transform.TransformDirection (Vector3.right);
        }
        if (resultado == "izquierda") {
            direccion = transform.TransformDirection (Vector3.left);
        }
        //CambioDeCardinalidad (direccion);
        cardinalidadDelFantasma = direccion * speed;
    }

    public Vector3 GetCardinalidad () {
        return this.cardinalidadDelFantasma;
    }
}