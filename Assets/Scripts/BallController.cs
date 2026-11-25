using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] GameManager manager;

    public float force = 1f;
    public float MAX_Y = 2.5f;
    public float MIN_Y = -2.5f;

    public float delay = 6f;
    const float MIN_ANG = 25.0f;
    const float MAX_ANG = 40.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();        
        int directionX = Random.Range(0,2) == 0 ? -1 : 1;
        StartCoroutine(LanzarPelota(directionX));
        
    }
    
    IEnumerator LanzarPelota(int directionX)
    {
        yield return new WaitForSeconds(delay);

        // Calculo vector lanzamento
        float posLanzamiento = Random.Range(MIN_Y, MAX_Y);
        gameObject.transform.position = new Vector3(0,posLanzamiento,0);


        // Calculo vector impulso
        float anguloLanzamiento = Random.Range(MIN_ANG, MAX_ANG) * Mathf.Deg2Rad;
        float x = Mathf.Cos(anguloLanzamiento) * directionX;

        int directionY = Random.Range(0,2) == 0 ? -1 : 1;
        float y = Mathf.Sin(anguloLanzamiento) * directionY;

        rb2d.linearVelocity = Vector2.zero;
        Vector2 impulso = new Vector2(x,y);
        rb2d.AddForce(impulso * force, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {        
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == "Porteria1")
        {
            Debug.Log("Gol en Porteria 1");
            StartCoroutine(LanzarPelota(1));
            manager.AddPointP2();
        }
        else if (tag == "Porteria2")
        {
            Debug.Log("Gol en Porteria 2");
            StartCoroutine(LanzarPelota(-1));
            manager.AddPointP1();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Pala1" || tag == "Pala2")
        {
            Debug.Log("Pala Collision");
            
            if (manager.mecanicaVelocidadBola.active)
            {                                
                rb2d.AddForce(rb2d.linearVelocity.normalized * manager.mecanicaVelocidadBola.fuerzaExtra, ForceMode2D.Impulse);
            }
        }
    }
}
