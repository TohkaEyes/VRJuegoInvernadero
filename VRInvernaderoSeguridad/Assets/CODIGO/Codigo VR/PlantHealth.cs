using UnityEngine;
using UnityEngine.UI; // Necesario para el Slider

public class PlantHealth : MonoBehaviour
{
    [Header("Configuración Visual")]
    public Renderer meshRenderer; // El modelo 3D de la planta
    public Color colorEnfermo = Color.red;
    public Color colorSano = Color.white; // Blanco = color original de la textura

    [Header("Barra de Progreso")]
    public Slider barraProgreso; // Arrastra el Slider aquí
    public GameObject canvasBarra; // El objeto padre del slider (para ocultarlo al final)

    [Header("Configuración de Curación")]
    public float velocidadCuracion = 0.2f; // Qué tan rápido se cura (0.1 a 1.0)
    private float saludActual = 0f; // 0 = Enferma, 1 = Sana

    void Start()
    {
        if (meshRenderer == null) meshRenderer = GetComponent<Renderer>();

        // Inicializamos todo en modo "Enfermo"
        ActualizarEstado();
    }

    // Esta función mágica detecta cuando las partículas del spray tocan la planta
    void OnParticleCollision(GameObject other)
    {
        // Solo curamos si no está completa
        if (saludActual < 1f)
        {
            // Aumentamos la salud
            // Usamos Time.deltaTime para que sea suave y no dependa de los FPS
            saludActual += velocidadCuracion * Time.deltaTime;

            // Limitamos para que no pase de 1
            saludActual = Mathf.Clamp01(saludActual);

            ActualizarEstado();

            if (saludActual >= 1f)
            {
                PlantaCurada();
            }
        }
    }

    void ActualizarEstado()
    {
        // 1. Cambiar el Color (Mezcla entre Rojo y Normal)
        if (meshRenderer != null)
        {
            meshRenderer.material.color = Color.Lerp(colorEnfermo, colorSano, saludActual);
        }

        // 2. Actualizar la Barra
        if (barraProgreso != null)
        {
            barraProgreso.value = saludActual;
        }
    }

    void PlantaCurada()
    {
        // Opcional: Ocultar la barra cuando termine para que se vea limpio
        if (canvasBarra != null)
        {
            // canvasBarra.SetActive(false); // Descomenta si quieres que desaparezca la barra
        }
        Debug.Log("¡Planta completamente sana!");
    }
}