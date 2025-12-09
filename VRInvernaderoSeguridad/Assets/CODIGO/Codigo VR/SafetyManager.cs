using UnityEngine;

public class SafetyManager : MonoBehaviour
{
    public static SafetyManager Instance;

    [Header("Modelos del Personaje")]
    public GameObject modeloCivil;
    public GameObject modeloProtegido;
    public GameObject modeloMano1;
    public GameObject modeloMano2;
    [Header("Estado del Equipo")]
    public bool tieneMascara = false;
    public bool tieneTraje = false;
    public bool tieneGuantes = false;

    private void Awake()
    {
        Instance = this;
        ActualizarPersonaje();
    }
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
        if (tieneMascara && tieneTraje && tieneGuantes)
        {
            modeloCivil.SetActive(false);
            modeloProtegido.SetActive(true);
            modeloMano1.SetActive(true);
            modeloMano2.SetActive(true);
            Debug.Log("¡EQUIPO COMPLETO! Cambiando modelo.");
        }
    }
    public void ActualizarPersonaje()
    {
        modeloCivil.SetActive(true);
        modeloProtegido.SetActive(false);
    }
}