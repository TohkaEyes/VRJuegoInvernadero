using UnityEngine;
using UnityEngine.UI; // Necesario para la UI

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance; // Singleton para acceso fácil

    [Header("Configuración")]
    public float vidaMaxima = 100f;
    public float vidaActual;
    public Slider barraDeVida; // Arrastra tu Slider aquí

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

    public void Curar() // Por si quieres reiniciar
    {
        vidaActual = vidaMaxima;
        ActualizarBarra();
    }

    void ActualizarBarra()
    {
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaActual / vidaMaxima; // Normalizamos a 0-1
        }
    }
}