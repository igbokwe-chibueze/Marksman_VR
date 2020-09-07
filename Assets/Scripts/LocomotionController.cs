using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    [Header("Teleportation")]

    [Tooltip("The left teleportation ray")]
    public XRController leftTeleportRay;

    [Tooltip("The right teleportation ray")]
    public XRController rightTeleportRay;

    [Tooltip("Button that handles teleportation")]
    public InputHelpers.Button teleportActivationButton;

    [Tooltip("How hard the teleportation button needs to be pressed to trigger it.")]
    public float activationThreshold = 0.1f;

    public bool enableLeftTeleport { get; set; } = true;
    public bool enableRightTeleport { get; set; } = true;


    [Header("Interaction")]

    [Tooltip("The left interaction ray")]
    public XRRayInteractor leftInteractorRay;

    [Tooltip("The left interaction ray")]
    public XRRayInteractor rightInteractorRay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 pos = new Vector3();
        //Vector3 nom = new Vector3();
        //int index = 0;
        //bool validTarget = false;

        if (leftTeleportRay)
        {
            //bool isLeftInteratorRayHovering = leftInteractorRay.TryGetHitInfo(ref pos, ref nom, ref index, ref validTarget);
            //leftTeleportRay.gameObject.SetActive(enableLeftTeleport && CheckIfActivated(leftTeleportRay) && !isLeftInteratorRayHovering);
        }

        if (rightTeleportRay)
        {
            //bool isrightInteratorRayHovering = rightInteractorRay.TryGetHitInfo(ref pos, ref nom, ref index, ref validTarget);
            //rightTeleportRay.gameObject.SetActive(enableRightTeleport && CheckIfActivated(rightTeleportRay) && !isrightInteratorRayHovering);
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
