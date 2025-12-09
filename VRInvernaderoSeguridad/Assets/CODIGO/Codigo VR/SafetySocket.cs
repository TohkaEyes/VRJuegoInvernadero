using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// IMPORTANTE: El archivo debe llamarse SafetySocket.cs
public class SafetySocket : MonoBehaviour
{
    [Header("1. Arrastra aquí a tu Personaje")]
    public SafetyManager safetyManager;

    [Header("2. Configuración del Socket")]
    public TipoEquipo tipoDeEquipo;

    [Header("3. (Opcional) Restricción por Tag")]
    public string tagRequerido = "Untagged";

    public enum TipoEquipo { Mascara, Traje, Guantes }

    public void OnObjetoCapturado(SelectEnterEventArgs args)
    {
        GameObject objetoEntrante = args.interactableObject.transform.gameObject;
        if (tagRequerido != "Untagged" && !objetoEntrante.CompareTag(tagRequerido))
        {
            Debug.LogWarning("Este socket esperaba un objeto con tag: " + tagRequerido);
            return;
        }
        if (safetyManager != null)
        {
            switch (tipoDeEquipo)
            {
                case TipoEquipo.Mascara:
                    safetyManager.RegistrarMascara();
                    break;
                case TipoEquipo.Traje:
                    safetyManager.RegistrarTraje();
                    break;
                case TipoEquipo.Guantes:
                    safetyManager.RegistrarGuantes();
                    break;
            }
            Debug.Log("Pieza equipada: " + tipoDeEquipo);
        }
        else
        {
            Debug.LogError("¡Falta asignar el SafetyManager en el Inspector!");
        }
        Destroy(objetoEntrante);
        Destroy(this.gameObject);
    }
}