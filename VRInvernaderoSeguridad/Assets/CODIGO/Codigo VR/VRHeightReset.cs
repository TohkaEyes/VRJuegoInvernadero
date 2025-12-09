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

        //ROTACIÓN
        float rotacionY = rig.Camera.transform.localEulerAngles.y;
        rig.transform.Rotate(0, -rotacionY, 0);

        //ALTURA
        float alturaFisicaHMD = rig.Camera.transform.localPosition.y;

        float nuevoOffset = alturaDeseada - alturaFisicaHMD;

        cameraOffset.transform.localPosition = new Vector3(posActual.x, nuevoOffset, posActual.z);

        Debug.Log($"Calibrado! Altura Física: {alturaFisicaHMD}, Nuevo Suelo: {nuevoOffset}");
    }
}