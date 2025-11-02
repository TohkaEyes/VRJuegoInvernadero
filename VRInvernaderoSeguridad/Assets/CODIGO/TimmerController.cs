using TMPro;
using UnityEngine;

public class TimmerController : MonoBehaviour
{
    public float tiempoInicial = 90f; 
    public float tiempoRestante = 90f; 
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText; 

    void Start()
    {
        tiempoRestante = tiempoInicial;
        timerIsRunning = true; 
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                DisplayTime(tiempoRestante);
            }
            else
            {
                Debug.Log("Timmer Finalizado");
                tiempoRestante = 0;
                timerIsRunning = false;
                DisplayTime(tiempoRestante);

                //TODO iniciar evento de timmer Finalizado
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        //timeToDisplay += 1; // To ensure 0:00 is displayed when time runs out
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
