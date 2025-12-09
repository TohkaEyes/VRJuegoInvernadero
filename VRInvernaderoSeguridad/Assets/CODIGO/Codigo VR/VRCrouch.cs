using UnityEngine;
using UnityEngine.XR; 
using Unity.XR.CoreUtils;

public class VRCrouch : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject cameraOffset; 

    [Header("Configuración")]
    public float distanciaAgachado = 0.6f;

   
    private bool estabaPresionado = false;
    private bool estoyAgachado = false;

    void Update()
    {
       
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (device.isValid)
        {
            
            bool estaPresionadoAhora = false;
            device.TryGetFeatureValue(CommonUsages.primaryButton, out estaPresionadoAhora);

            
            if (estaPresionadoAhora && !estabaPresionado)
            {
                AlternarAgachado();
            }

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