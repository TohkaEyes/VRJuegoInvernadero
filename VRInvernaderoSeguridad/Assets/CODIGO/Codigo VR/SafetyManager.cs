using UnityEngine;

public class SafetyManager : MonoBehaviour
{
    // Singleton para poder llamarlo desde cualquier lado facilmente
    public static SafetyManager Instance;

    [Header("Modelos del Personaje")]
    public GameObject modeloCivil;       // Tu modelo actual (Ropa normal)
    public GameObject modeloProtegido;   // Tu modelo con el traje completo
    public GameObject modeloMano1;   // Tu modelo con el traje completo
    public GameObject modeloMano2;   // Tu modelo con el traje completo
    [Header("Estado del Equipo")]
    public bool tieneMascara = false;
    public bool tieneTraje = false;
    public bool tieneGuantes = false;

    private void Awake()
    {
        Instance = this;
        ActualizarPersonaje(); // Asegurar que empezamos bien
    }

    // Funciones para registrar cada pieza
    public void RegistrarMascara()
    {
        tieneMascara = true;
        Debug.Log("Mascara equipada");
        VerificarSetCompleto();
    }

    public void RegistrarTraje()
    {
        tieneTraje = true;
        Debug.Log("Traje equipado");
        VerificarSetCompleto();
    }

    public void RegistrarGuantes()
    {
        tieneGuantes = true;
        Debug.Log("Guantes equipados");
        VerificarSetCompleto();
    }

    private void VerificarSetCompleto()
    {
        // Si tenemos las 3 cosas, cambiamos el modelo
        if (tieneMascara && tieneTraje && tieneGuantes)
        {
            modeloCivil.SetActive(false);
            modeloProtegido.SetActive(true);
            modeloMano1.SetActive(true);
            modeloMano2.SetActive(true);
            Debug.Log("¡EQUIPO COMPLETO! Cambiando modelo.");
        }
    }

    // Por si necesitas reiniciar en pruebas
    public void ActualizarPersonaje()
    {
        modeloCivil.SetActive(true);
        modeloProtegido.SetActive(false);
    }
}