using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscadorPacmen : MonoBehaviour {
    private EstadosFinitos controladorDeMovimientoFantasma;
    public bool estaEstrellado = false;
    public bool comprobar;
    private void Update () {
        if (!comprobar) {
            return;
        }
        controladorDeMovimientoFantasma = transform.parent.gameObject.GetComponent<EstadosFinitos>();
        Debug.DrawRay (transform.position, (controladorDeMovimientoFantasma.GetCardinalidad () / 10), Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast (transform.position, (controladorDeMovimientoFantasma.GetCardinalidad () / 10), (transform.parent.gameObject.transform.localScale.x / 2));
        if (hit.collider != null && !hit.transform.CompareTag ("Player")) {
            estaEstrellado = true;
        } else {
            StartCoroutine ("QuitarElEstrellado");
            //estaEstrellado = false;
        }
    }

    IEnumerator QuitarElEstrellado () {
        yield return new WaitForSeconds (0.02f);
        estaEstrellado = false;
    }
}