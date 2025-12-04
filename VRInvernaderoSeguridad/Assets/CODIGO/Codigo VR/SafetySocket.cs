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
        // Obtenemos el objeto que entró en el socket (la máscara/guante)
        GameObject objetoEntrante = args.interactableObject.transform.gameObject;

        // --- VALIDACIÓN DE TAG ---
        if (tagRequerido != "Untagged" && !objetoEntrante.CompareTag(tagRequerido))
        {
            Debug.LogWarning("Este socket esperaba un objeto con tag: " + tagRequerido);
            return;
        }

        // --- LÓGICA DE SEGURIDAD ---
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

        // --- LIMPIEZA FINAL ---

        // 1. Eliminamos el objeto físico (el guante/máscara que tenías en la mano)
        Destroy(objetoEntrante);

        // 2. NUEVO: Eliminamos este mismo Socket para que desaparezca de la escena
        // "gameObject" (con g minúscula) se refiere al objeto donde está puesto este script
        Destroy(this.gameObject);
    }
}