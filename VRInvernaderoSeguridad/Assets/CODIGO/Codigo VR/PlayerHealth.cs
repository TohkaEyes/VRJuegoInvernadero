using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [Header("Configuración")]
    public float vidaMaxima = 100f;
    public float vidaActual;
    public Slider barraDeVida;

    private void Awake()
    {
        Instance = this;
        vidaActual = vidaMaxima;
        ActualizarBarra();
    }

    public void RecibirDano(float cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;

        ActualizarBarra();
    }

    public void Curar()
    {
        vidaActual = vidaMaxima;
        ActualizarBarra();
    }

    void ActualizarBarra()
    {
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaActual / vidaMaxima;
        }
    }
}