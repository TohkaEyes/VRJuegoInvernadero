using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleHandAnimator : MonoBehaviour
{
    [Header("Input")]
    public InputActionProperty triggerAction;
    public InputActionProperty gripAction;

    [Header("Configuración de Dedos")]
    public Transform[] dedos;

    [Header("Ajustes de Rotación")]
    public Vector3 rotacionAbierta;
    public Vector3 rotacionCerrada;
    public float velocidad = 10f;

    void Update()
    {
        float valorGrip = gripAction.action != null ? gripAction.action.ReadValue<float>() : 0;
        float valorTrigger = triggerAction.action != null ? triggerAction.action.ReadValue<float>() : 0;
        float targetValue = Mathf.Max(valorGrip, valorTrigger);
        Vector3 rotacionObjetivo = Vector3.Lerp(rotacionAbierta, rotacionCerrada, targetValue);
        foreach (Transform dedo in dedos)
        {
            if (dedo != null)
            {
                dedo.localRotation = Quaternion.Lerp(dedo.localRotation, Quaternion.Euler(rotacionObjetivo), Time.deltaTime * velocidad);
                foreach (Transform falange in dedo)
                {
                    falange.localRotation = Quaternion.Lerp(falange.localRotation, Quaternion.Euler(rotacionObjetivo), Time.deltaTime * velocidad);
                }
            }
        }
    }
}