using UnityEngine;
using UnityEngine.XR;

public class VRArmsAndHands : MonoBehaviour
{
    [Header("Dedos mano izquierda")]
    public Transform[] leftFingers;
    [Header("Dedos mano derecha")]
    public Transform[] rightFingers;

    [Header("Controladores VR")]
    public XRNode leftHandNode = XRNode.LeftHand;
    public XRNode rightHandNode = XRNode.RightHand;

    [Range(0f, 10f)] public float fingerLerpSpeed = 15f;

    void LateUpdate()
    {
        InputDevice leftDevice = InputDevices.GetDeviceAtXRNode(leftHandNode);
        InputDevice rightDevice = InputDevices.GetDeviceAtXRNode(rightHandNode);

        if (leftDevice.TryGetFeatureValue(CommonUsages.grip, out float leftGrip))
            AnimateFingers(leftFingers, leftGrip);

        if (rightDevice.TryGetFeatureValue(CommonUsages.grip, out float rightGrip))
            AnimateFingers(rightFingers, rightGrip);
    }

    void AnimateFingers(Transform[] fingers, float grip)
    {
        if (fingers == null) return;

        foreach (var finger in fingers)
        {
            finger.localRotation = Quaternion.Slerp(
                finger.localRotation,
                Quaternion.Euler(grip * 90f, 0f, 0f),
                Time.deltaTime * fingerLerpSpeed
            );
        }
    }
}
