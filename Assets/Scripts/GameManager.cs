using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int scoreP1;
    int scoreP2;

    [SerializeField] Text uiScoreP1;
    [SerializeField] Text uiScoreP2;

    [SerializeField] GameObject ball;

    public UnityEvent onStartNewPoint;
    public void AddPointP1() { 
        scoreP1++; 
        onStartNewPoint.Invoke();
    }
    public void AddPointP2() { 
        scoreP2++;
        onStartNewPoint.Invoke();
    }

    public MecanicaReduccion mecanicaReduccion;
    public MecanicaVelocidadBola mecanicaVelocidadBola;


    bool running = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible=false;
        mecanicaReduccion = new MecanicaReduccion();
        mecanicaVelocidadBola = new MecanicaVelocidadBola();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            mecanicaReduccion.active = true;
            Debug.Log("Activada reduccion pala");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            mecanicaVelocidadBola.active = true;
            Debug.Log("Activada mecanica Velocidad Bola");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            mecanicaReduccion.active = false;
            mecanicaVelocidadBola.active = false;
            Debug.Log("Desctivada reduccion pala");
            Debug.Log("Desctivada mecanica Velocidad Bola");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(!running && Input.GetKeyDown(KeyCode.Space))
        {
            running = true;
            ball.SetActive(true);
        }
    }

    void OnGUI()
    {
        uiScoreP1.text = scoreP1.ToString();
        uiScoreP2.text = scoreP2.ToString();
    }
}
