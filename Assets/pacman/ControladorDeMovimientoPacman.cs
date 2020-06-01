using UnityEngine;

public class ControladorDeMovimientoPacman : MonoBehaviour
{
    public float speed;
    private bool quitarControl = false;
    private Vector2 ultimaDesicion = Vector2.zero;
    /* blink va al centro (0,0)
     * pinky adelante (0,1)
     * inky va a la derecha (1,0)
     * clyde va a la izquierda (-1,0)
     */
    public GameObject blink, pinky, inky, clyde;
    public int powerup;

    // Update is called once per frame
    void Update()
    {
        Vector2 cardinalidad = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Debug.Log("X"+Input.GetAxis("Horizontal"));
        //Debug.Log("Y"+Input.GetAxis("Vertical"));
        //acomodamos los objetos que persiguen los fantasmas para que se coloquen en la cardinalidad que corresponde
        //primero miramos quien es mas fuerte x o y
        if(Mathf.Abs(cardinalidad.x) == Mathf.Abs(cardinalidad.y))
        {
            ColocandoFantasmas("centro");
        }
        else
        {
            if (Mathf.Abs( cardinalidad.x) > Mathf.Abs(cardinalidad.y))
            {
                //va por el eje x ahora buscamos para donde va
                if(cardinalidad.x > 0)
                {
                    //va para la derecha
                    ColocandoFantasmas("derecha");
                }
                else
                {
                    //va para la izquierda
                    ColocandoFantasmas("izquierda");
                }
            }
            else
            {
                //va por el eje y ahora buscamos para donde va
                if (cardinalidad.y > 0)
                {
                    //va para la arriba
                    ColocandoFantasmas("arriba");
                }
                else
                {
                    //va para la abajo
                    ColocandoFantasmas("abajo");
                }
            }
        }
    }

    public void DesabilitarMovimiento()
    {
        quitarControl = !quitarControl;
    }

    private void ColocandoFantasmas(string cardinalidad)
    {
        //Vamos a hacer correr a pacman hacia la direccion donde tenga la cardinalidad tambien
        Vector2 vector = Vector2.zero;
        //vamos a usar la escala de pacman para posicionarlos
        float distancia = gameObject.transform.localScale.x * 4;
        switch (cardinalidad)
        {
            case "arriba":
                /* pinky: 0,1
                 * inky: 1,0
                 * clyde: -1,0
                 * blink: 0,0
                 */
                blink.transform.position = (Vector2)transform.position+ new Vector2(0, 0);
                pinky.transform.position = (Vector2)transform.position + new Vector2(0, distancia);
                inky.transform.position = (Vector2)transform.position + new Vector2(distancia, 0);
                clyde.transform.position = (Vector2)transform.position + new Vector2(-distancia, 0);
                vector = Vector2.up;
                break;
            case "abajo":
                /* pinky: 0,-1
                 * inky: -1,0
                 * clyde: 1,0
                 * blink: 0,0
                 */
                pinky.transform.position = (Vector2)transform.position + new Vector2(0, -distancia);
                inky.transform.position = (Vector2)transform.position + new Vector2(-distancia, 0);
                clyde.transform.position = (Vector2)transform.position + new Vector2(distancia, 0);
                blink.transform.position = (Vector2)transform.position + new Vector2(0, 0);
                vector = Vector2.down;
                break;
            case "derecha":
                /* pinky: 1,0
                 * inky: 0,-1
                 * clyde: 0,1
                 * blink: 0,0
                 */
                pinky.transform.position = (Vector2)transform.position + new Vector2(distancia, 0);
                inky.transform.position = (Vector2)transform.position + new Vector2(-distancia, 0);
                clyde.transform.position = (Vector2)transform.position + new Vector2(distancia, 0);
                blink.transform.position = (Vector2)transform.position + new Vector2(0, 0);
                vector = Vector2.right;
                //flipeamos el sprite
                GetComponent<SpriteRenderer>().flipX = true;
                break;
            case "izquierda":
                /* pinky: -1,0
                 * inky: 0,1
                 * clyde: 0,-1
                 * blink: 0,0
                 */
                pinky.transform.position = (Vector2)transform.position + new Vector2(-distancia, 0);
                inky.transform.position = (Vector2)transform.position + new Vector2(0, distancia);
                clyde.transform.position = (Vector2)transform.position + new Vector2(0, -distancia);
                blink.transform.position = (Vector2)transform.position + new Vector2(0, 0);
                vector = Vector2.left;
                GetComponent<SpriteRenderer>().flipX = false;
                break;
            default:
                vector = ultimaDesicion;
                break;
        }
        //Antes de dejarlo girar en una esquina, vamos a verificar si esta libre, ya que en el juego original esto pasa
        //vamos a utilizar el raycast para eso
        Debug.DrawRay(transform.position, vector*0.7f);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, vector,(vector.magnitude*0.7f));
        if(hit.collider == null || hit.collider.transform.CompareTag("comida") || hit.collider.transform.CompareTag("powerup"))
        {
            GetComponent<Rigidbody2D>().velocity = vector * (speed + powerup);
            ultimaDesicion = vector;
        }
    }
}
