using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{/*
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Single interactUIAction;
    public SteamVR_Action_Boolean selectUIAction;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn a new laser and save a reference in laser
        laser = Instantiate(laserPrefab);
        // Store the transform component
        laserTransform = laser.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // if interact UI action is activated
        if (interactUIAction.GetAxis(handType) > 0.0f)
        {
            RaycastHit hit;

            // shoot a ray from controller. If it hits something, store the point and show the laser
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                
                if (selectUIAction.GetState(handType))
                {
                    //hit.collider.gameObject.GetComponent<Button>().OnPointerClick();
                    print(hit.collider.name);
                    hit.collider.gameObject.GetComponent<Button>().OnClick();
                }
            }
        }
        else // Hide the laser when the teleport action deactivates
        {
            //print("disactivating laser");
            laser.SetActive(false);
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true); //show laser
        //position the laser between the controller and the point where the raycast hits
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f); //.5f = middle point
        laserTransform.LookAt(hitPoint); //Point laser at position where raycast hit
        //scale laser so it fits perfectly between controller and hit point
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }*/
}
