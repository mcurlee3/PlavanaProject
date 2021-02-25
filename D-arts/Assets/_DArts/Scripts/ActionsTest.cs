using RootMotion.Demos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ActionsTest : MonoBehaviour
{/*
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean callibrateAction;
    public SteamVR_Action_Boolean selectUIAction;
    public SteamVR_Action_Single interactUIAction;

    public GameObject callibrator;

    // Update is called once per frame
    void Update()
    { 
        if (GetCallibrate())
        {
            print("Callibrate " + handType);
            callibrator.GetComponent<VRIKCalibrationController>().setCallibrate(true);
        }

        if (GetInteractUI())
        {
            //print("Interact UI " + handType);
        }

        if (GetSelectUI())
        {
            print("UI Select " + handType);
        }
    }

    //GetStateDown gets if it was just activated
    //GetState gets if it is currently activated

    public bool GetCallibrate()
    {
        return callibrateAction.GetStateDown(handType);
    }

    public bool GetInteractUI()
    {
        float trigger = interactUIAction.GetAxis(handType);
        if (trigger > 0.0f)
            return true;
        return false;
    }

    public bool GetSelectUI()
    {
        return selectUIAction.GetStateDown(handType);
    }*/
}
