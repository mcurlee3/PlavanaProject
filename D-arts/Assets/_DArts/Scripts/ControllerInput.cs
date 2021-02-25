using RootMotion.Demos;
using UnityEngine;
using VRTK;

public class ControllerInput : MonoBehaviour
{
    public VRIKCalibrationController callibration;

    private void Start()
    {
        GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(DoGripPressed);
    }

    private void DoGripPressed(object sender, ControllerInteractionEventArgs e)
    {
        callibration.setCallibrate(true);
    }
}
