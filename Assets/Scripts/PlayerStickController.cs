using UnityEngine;

public class PlayerStickController : MonoBehaviour
{
    public const float MAX_Y = 4.2f;
    public const float MIN_Y = -4.2f;
    [SerializeField] GameManager manager;


    Vector3 originalScale;
    Vector3 originalPosition;

    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;
        manager.onStartNewPoint.AddListener(ResetStick);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Pala1"))
        {
            if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < MAX_Y)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow)  && transform.position.y > MIN_Y)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
        }
        if (gameObject.CompareTag("Pala2"))
        {
            if (Input.GetKey(KeyCode.W) && transform.position.y < MAX_Y)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S)  && transform.position.y > MIN_Y)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            Debug.Log("PRUEBA");
            if (manager.mecanicaReduccion.active)
            {
                Vector3 actualScale = gameObject.transform.localScale;
                Vector3 newScale = new Vector3(actualScale.x, actualScale.y - (actualScale.y * manager.mecanicaReduccion.scaleReduc), actualScale.z);
                
                gameObject.transform.localScale = newScale;
            }
        }
    }


    void ResetStick()
    {
        transform.localScale = originalScale;
        transform.position = originalPosition;
    }
}
