using UnityEngine;
using UnityEngine.UI; 

public class PlantHealth : MonoBehaviour
{
    [Header("Configuración Visual")]
    public Renderer meshRenderer; 
    public Color colorEnfermo = Color.red;
    public Color colorSano = Color.white; 

    [Header("Barra de Progreso")]
    public Slider barraProgreso;
    public GameObject canvasBarra;

    [Header("Configuración de Curación")]
    public float velocidadCuracion = 0.2f; 
    private float saludActual = 0f; 

    void Start()
    {
        if (meshRenderer == null) meshRenderer = GetComponent<Renderer>();

        ActualizarEstado();
    }

    void OnParticleCollision(GameObject other)
    {
        if (saludActual < 1f)
        {
            saludActual += velocidadCuracion * Time.deltaTime;

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
        if (meshRenderer != null)
        {
            meshRenderer.material.color = Color.Lerp(colorEnfermo, colorSano, saludActual);
        }

        if (barraProgreso != null)
        {
            barraProgreso.value = saludActual;
        }
    }

    void PlantaCurada()
    {
       
        Debug.Log("¡Planta completamente sana!");
    }
}