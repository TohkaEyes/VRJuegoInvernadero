using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EquipmentSocket : MonoBehaviour
{
    public enum TipoEquipo { Mascara, Traje, Guantes }
    public TipoEquipo tipoDeSocket;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    void Start()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }
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

        GameObject objetoEntrante = args.interactableObject.transform.gameObject;

        Destroy(objetoEntrante);
    }
}
