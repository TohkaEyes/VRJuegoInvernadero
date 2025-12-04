using UnityEngine;
using UnityEngine.XR; // Necesario para hablar directo con el control
using Unity.XR.CoreUtils;

public class VRCrouch : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject cameraOffset; // Arrastra el "Camera Offset"

    [Header("Configuración")]
    public float distanciaAgachado = 0.6f;

    // Variables para controlar el estado del botón (para que no parpadee)
    private bool estabaPresionado = false;
    private bool estoyAgachado = false;

    void Update()
    {
        // 1. Obtener el control derecho (Right Hand)
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (device.isValid)
        {
            // 2. Preguntar por el botón A (PrimaryButton)
            bool estaPresionadoAhora = false;
            device.TryGetFeatureValue(CommonUsages.primaryButton, out estaPresionadoAhora);

            // 3. Detectar el momento exacto del "Click" (Solo cuando baja el dedo)
            if (estaPresionadoAhora && !estabaPresionado)
            {
                Debug.Log("¡BOTÓN A (Derecha) DETECTADO!"); // Debug para consola
                AlternarAgachado();
            }

            // Actualizamos el estado para el siguiente frame
            estabaPresionado = estaPresionadoAhora;
        }
    }

    void AlternarAgachado()
    {
        if (cameraOffset == null) return;

        if (estoyAgachado)
        {
            // LEVANTARSE
            cameraOffset.transform.localPosition += new Vector3(0, distanciaAgachado, 0);
            estoyAgachado = false;
        }
        else
        {
            // AGACHARSE
            cameraOffset.transform.localPosition -= new Vector3(0, distanciaAgachado, 0);
            estoyAgachado = true;
        }
    }
}