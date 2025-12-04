using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class VRHeightReset : MonoBehaviour
{
    [Header("Referencias")]
    public XROrigin rig;
    public GameObject cameraOffset;

    [Header("Input")]
    public InputActionProperty botonReset;

    [Header("Configuración")]
    public float alturaDeseada = 1.75f;

    void OnEnable()
    {
        if (botonReset.action != null) botonReset.action.Enable();
    }

    void Update()
    {
        if (botonReset.action != null && botonReset.action.WasPressedThisFrame())
        {
            CalibrarAbsoluto();
        }
    }

    void CalibrarAbsoluto()
    {
        if (rig == null || cameraOffset == null) return;

        // 1. ROTACIÓN (Mirar al frente)
        // Giramos el rig para que coincida con la rotación de la cámara
        float rotacionY = rig.Camera.transform.localEulerAngles.y;
        rig.transform.Rotate(0, -rotacionY, 0);

        // 2. ALTURA (MATEMÁTICA CORREGIDA)
        // Obtenemos solo la altura física de tus gafas (HMD)
        // Esto es cuánto mides tú en la vida real en este instante
        float alturaFisicaHMD = rig.Camera.transform.localPosition.y;

        // Calculamos el nuevo suelo:
        // Si quiero medir 1.75 y mis gafas están a 1.00 (sentado), el suelo debe bajar 0.75.
        // Si quiero medir 1.75 y mis gafas están a 1.70 (de pie), el suelo debe bajar 0.05.
        float nuevoOffset = alturaDeseada - alturaFisicaHMD;

        // APLICAMOS DIRECTAMENTE (Usamos = en vez de +=)
        // Esto borra cualquier error anterior y pone la altura perfecta.
        Vector3 posActual = cameraOffset.transform.localPosition;
        cameraOffset.transform.localPosition = new Vector3(posActual.x, nuevoOffset, posActual.z);

        Debug.Log($"Calibrado! Altura Física: {alturaFisicaHMD}, Nuevo Suelo: {nuevoOffset}");
    }
}