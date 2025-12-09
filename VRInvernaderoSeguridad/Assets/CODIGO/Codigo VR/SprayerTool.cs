using UnityEngine;
using UnityEngine.XR;
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
    public float sensibilidadGatillo = 0.3f;

    // Variables internas
    private XRGrabInteractable grabInteractable;
    private bool estaSiendoAgarrado = false;
    private XRNode manoActual = XRNode.RightHand;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(AlAgarrar);
        grabInteractable.selectExited.AddListener(AlSoltar);
    }

    void AlAgarrar(SelectEnterEventArgs args)
    {
        estaSiendoAgarrado = true;
        string nombreMano = args.interactorObject.transform.name;
        string tagMano = args.interactorObject.transform.tag;
        if (nombreMano.Contains("Left") || tagMano.Contains("Left"))
        {
            manoActual = XRNode.LeftHand;
        }
        else
        {
            manoActual = XRNode.RightHand;
        }
    }

    void AlSoltar(SelectExitEventArgs args)
    {
        estaSiendoAgarrado = false;
        DetenerEfectos();
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
        InputDevice device = InputDevices.GetDeviceAtXRNode(manoActual);
        if (device.isValid)
        {
            float valorGatillo = 0;
            device.TryGetFeatureValue(CommonUsages.trigger, out valorGatillo);

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
        if (particulasGas != null && !particulasGas.isPlaying) particulasGas.Play();
        if (sonidoSpray != null && !sonidoSpray.isPlaying) sonidoSpray.Play();
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
        if (SafetyManager.Instance != null)
        {
            protegido = SafetyManager.Instance.tieneMascara &&
                        SafetyManager.Instance.tieneTraje &&
                        SafetyManager.Instance.tieneGuantes;
        }
        if (!protegido)
        {
            if (PlayerHealth.Instance != null)
            {
                PlayerHealth.Instance.RecibirDano(danoPorSegundo * Time.deltaTime);
            }
        }
    }
}