using UnityEngine;
using UnityEngine.XR; // Necesario para leer el gatillo directamente
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SprayerTool : MonoBehaviour
{
    [Header("Efectos")]
    public ParticleSystem particulasGas;
    public AudioSource sonidoSpray;

    [Header("Configuración")]
    public float danoPorSegundo = 10f;
    [Range(0.1f, 1f)]
    public float sensibilidadGatillo = 0.3f; // Qué tanto hay que apretar (30%)

    // Variables internas
    private XRGrabInteractable grabInteractable;
    private bool estaSiendoAgarrado = false;
    private XRNode manoActual = XRNode.RightHand; // ¿Qué mano lo sostiene?

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Nos suscribimos a los eventos de agarrar/soltar
        grabInteractable.selectEntered.AddListener(AlAgarrar);
        grabInteractable.selectExited.AddListener(AlSoltar);
    }

    // Se activa cuando una mano lo toma
    void AlAgarrar(SelectEnterEventArgs args)
    {
        estaSiendoAgarrado = true;

        // DETECCIÓN AUTOMÁTICA DE MANO:
        // Verificamos el nombre del objeto que nos agarró para saber si es la Izquierda
        string nombreMano = args.interactorObject.transform.name;
        string tagMano = args.interactorObject.transform.tag;

        // Si el nombre o el tag dice "Left", asumimos que es la mano izquierda
        if (nombreMano.Contains("Left") || tagMano.Contains("Left"))
        {
            manoActual = XRNode.LeftHand;
        }
        else
        {
            manoActual = XRNode.RightHand;
        }
    }

    // Se activa cuando lo sueltas
    void AlSoltar(SelectExitEventArgs args)
    {
        estaSiendoAgarrado = false;
        DetenerEfectos(); // Apagamos todo por seguridad
    }

    void Update()
    {
        if (estaSiendoAgarrado)
        {
            LeerGatillo();
        }
    }

    void LeerGatillo()
    {
        // 1. Conectamos con la mano que nos sostiene
        InputDevice device = InputDevices.GetDeviceAtXRNode(manoActual);

        if (device.isValid)
        {
            // 2. Leemos el valor del gatillo (float de 0 a 1)
            float valorGatillo = 0;
            device.TryGetFeatureValue(CommonUsages.trigger, out valorGatillo);

            // 3. Si apretamos más fuerte que la sensibilidad... ¡DISPARAR!
            if (valorGatillo > sensibilidadGatillo)
            {
                Disparar();
            }
            else
            {
                DetenerEfectos();
            }
        }
    }

    void Disparar()
    {
        // Activamos efectos visuales si no están ya activos
        if (particulasGas != null && !particulasGas.isPlaying) particulasGas.Play();
        if (sonidoSpray != null && !sonidoSpray.isPlaying) sonidoSpray.Play();

        // Lógica de Daño y Seguridad
        VerificarSeguridad();
    }

    void DetenerEfectos()
    {
        if (particulasGas != null && particulasGas.isPlaying) particulasGas.Stop();
        if (sonidoSpray != null && sonidoSpray.isPlaying) sonidoSpray.Stop();
    }

    void VerificarSeguridad()
    {
        bool protegido = false;

        // Consultamos al SafetyManager
        if (SafetyManager.Instance != null)
        {
            protegido = SafetyManager.Instance.tieneMascara &&
                        SafetyManager.Instance.tieneTraje &&
                        SafetyManager.Instance.tieneGuantes;
        }

        // Si NO está protegido, aplicamos daño
        if (!protegido)
        {
            if (PlayerHealth.Instance != null)
            {
                PlayerHealth.Instance.RecibirDano(danoPorSegundo * Time.deltaTime);
            }
        }
    }
}