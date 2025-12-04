using UnityEngine;
using UnityEngine.InputSystem; // Necesario para leer el mando

public class SimpleHandAnimator : MonoBehaviour
{
    [Header("Input")]
    public InputActionProperty triggerAction; // Referencia al gatillo (0 a 1)
    public InputActionProperty gripAction;    // Referencia al grip (0 a 1)

    [Header("Configuración de Dedos")]
    // Arrastra aquí los huesos "base" de cada dedo (Proximal)
    public Transform[] dedos;

    [Header("Ajustes de Rotación")]
    public Vector3 rotacionAbierta;  // Rotación cuando la mano está abierta
    public Vector3 rotacionCerrada;  // Rotación cuando cierras el puño
    public float velocidad = 10f;

    void Update()
    {
        // 1. Leer cuánto estamos apretando (El mayor entre Grip y Trigger)
        float valorGrip = gripAction.action != null ? gripAction.action.ReadValue<float>() : 0;
        float valorTrigger = triggerAction.action != null ? triggerAction.action.ReadValue<float>() : 0;

        // Usamos el valor más alto para que cierre la mano con cualquiera de los dos
        float targetValue = Mathf.Max(valorGrip, valorTrigger);

        // 2. Calcular la rotación actual (Lerp)
        // Mezclamos entre abierto y cerrado según el valor (0 a 1)
        Vector3 rotacionObjetivo = Vector3.Lerp(rotacionAbierta, rotacionCerrada, targetValue);

        // 3. Aplicar a cada dedo
        foreach (Transform dedo in dedos)
        {
            if (dedo != null)
            {
                // Movemos suavemente hacia la posición objetivo
                dedo.localRotation = Quaternion.Lerp(dedo.localRotation, Quaternion.Euler(rotacionObjetivo), Time.deltaTime * velocidad);

                // TRUCO: Si tus dedos tienen hijos (falanges), también deberíamos rotarlos
                // para que el dedo se enrolle completo, no solo la base.
                foreach (Transform falange in dedo)
                {
                    falange.localRotation = Quaternion.Lerp(falange.localRotation, Quaternion.Euler(rotacionObjetivo), Time.deltaTime * velocidad);
                }
            }
        }
    }
}