using UnityEngine;

public class ProceduralVRAvatar : MonoBehaviour
{
    [Header("Bones")]
    public Transform pelvis;
    public Transform spine;
    public Transform chest;
    public Transform head;
    public Transform leftUpperArm;
    public Transform leftLowerArm;
    public Transform leftHand;
    public Transform rightUpperArm;
    public Transform rightLowerArm;
    public Transform rightHand;
    public Transform leftUpperLeg;
    public Transform leftLowerLeg;
    public Transform leftFoot;
    public Transform rightUpperLeg;
    public Transform rightLowerLeg;
    public Transform rightFoot;

    [Header("Targets")]
    public Transform headTarget;
    public Transform leftHandTarget;
    public Transform rightHandTarget;

    [Header("Settings")]
    public LayerMask groundLayer;
    public float footRayDistance = 1f;
    public float bodySmooth = 10f;

    private Vector3 pelvisOffset;

    void Start()
    {
        pelvisOffset = pelvis.position - headTarget.position;
    }

    void Update()
    {
        Vector3 targetPelvisPos = headTarget.position + pelvisOffset;
        pelvis.position = Vector3.Lerp(pelvis.position, targetPelvisPos, Time.deltaTime * bodySmooth);
        Vector3 lookDir = headTarget.forward;
        lookDir.y = 0;
        if (lookDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            spine.rotation = Quaternion.Slerp(spine.rotation, targetRot, Time.deltaTime * bodySmooth);
            chest.rotation = Quaternion.Slerp(chest.rotation, targetRot, Time.deltaTime * bodySmooth);
        }

        // Cabeza
        head.position = headTarget.position;
        head.rotation = headTarget.rotation;

        // Brazos
        ApplyIKArm(leftUpperArm, leftLowerArm, leftHand, leftHandTarget);
        ApplyIKArm(rightUpperArm, rightLowerArm, rightHand, rightHandTarget);

        // Piernas
        ApplyIKLeg(leftUpperLeg, leftLowerLeg, leftFoot);
        ApplyIKLeg(rightUpperLeg, rightLowerLeg, rightFoot);
    }

    void ApplyIKArm(Transform upper, Transform lower, Transform handBone, Transform target){
        Vector3 upperToTarget = target.position - upper.position;
        Vector3 axis = Vector3.Cross((lower.position - upper.position).normalized, upperToTarget.normalized);
        Quaternion rot = Quaternion.LookRotation(upperToTarget, axis);
        upper.rotation = Quaternion.Slerp(upper.rotation, rot, Time.deltaTime * bodySmooth);
        lower.rotation = Quaternion.Slerp(lower.rotation, target.rotation, Time.deltaTime * bodySmooth);
        handBone.rotation = Quaternion.Slerp(handBone.rotation, target.rotation, Time.deltaTime * bodySmooth);
    }

    void ApplyIKLeg(Transform upper, Transform lower, Transform foot)
    {
        Vector3 rayStart = foot.position + Vector3.up * 0.5f;
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, footRayDistance + 0.5f, groundLayer))
        {
            Vector3 targetPos = hit.point;
            targetPos.y += 0.05f;
            foot.position = Vector3.Lerp(foot.position, targetPos, Time.deltaTime * bodySmooth);

            Quaternion footRot = Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation;
            foot.rotation = Quaternion.Slerp(foot.rotation, footRot, Time.deltaTime * bodySmooth);
        }
    }
}
