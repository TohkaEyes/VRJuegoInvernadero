using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EquipmentSocket : MonoBehaviour
{
    // Definimos qué tipo de socket es este
    public enum TipoEquipo { Mascara, Traje, Guantes }
    public TipoEquipo tipoDeSocket;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    void Start()
    {
        // Obtenemos el componente Socket automáticamente
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }

    // Esta función la conectaremos al evento "Select Entered" del Socket
    public void OnObjetoEncajado(SelectEnterEventArgs args)
    {
        // 1. Avisar al SafetyManager qué nos pusimos
        switch (tipoDeSocket)
        {
            case TipoEquipo.Mascara:
                SafetyManager.Instance.RegistrarMascara();
                break;
            case TipoEquipo.Traje:
                SafetyManager.Instance.RegistrarTraje();
                break;
            case TipoEquipo.Guantes:
                SafetyManager.Instance.RegistrarGuantes();
                break;
        }

        // 2. Hacer "desaparecer" el objeto que encajamos (destruirlo)
        // El objeto que entró está en args.interactableObject
        GameObject objetoEntrante = args.interactableObject.transform.gameObject;

        Destroy(objetoEntrante);
    }
}
