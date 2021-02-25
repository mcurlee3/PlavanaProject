using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPositionChecking : MonoBehaviour
{
    #region Public Variables

    [Header("Player")]
    public Transform playerHead;
    public Transform playerLeftHand;
    public Transform playerRightHand;
    public Transform playerLeftFoot;
    public Transform playerRightFoot;

    [Header("Instructor")]
    public Transform instructorHead;
    public Transform instructorLeftHand;
    public Transform instructorRightHand;
    public Transform instructorLeftFoot;
    public Transform instructorRightFoot;

    #endregion

    #region Private Variables

    private Transform playerRoot;
    private Transform instructorRoot;

    #endregion

    private void Start()
    {
        //Get root (head) of player and instructor
        playerRoot = playerHead;
        instructorRoot = instructorHead;
    }

    private void Update()
    {
        CheckPoses();
    }

    private void CheckPoses()
    {
        //CheckPosition();
        CheckRotation();
    }

    private void CheckPosition()
    {
        /*Get coordinates of hands based off of root parent position DOESN'T WORK
        Vector3 playerLeftHandPosition = playerRoot.InverseTransformPoint(playerLeftHand.position);
        Vector3 playerRightHandPosition = playerRoot.InverseTransformPoint(playerRightHand.position);

        Vector3 instructorLeftHandPosition = instructorRoot.InverseTransformPoint(instructorLeftHand.position);
        Vector3 instructorRightHandPosition = instructorRoot.InverseTransformPoint(instructorRightHand.position);
        */
        //Draw Debugs
        Debug.DrawLine(playerRoot.position, playerLeftHand.position, Color.red);
        Debug.DrawLine(instructorRoot.position, instructorRightHand.position, Color.green);

        //Distance from root 
        float playerLeftHandDistance = Vector3.Distance(playerRoot.position, playerLeftHand.position);
        float instructorRightHandDistance = Vector3.Distance(instructorRoot.position, instructorRightHand.position);

        Debug.Log("Player Distance: " + playerLeftHandDistance);
        Debug.Log("Instructor Distance: " + instructorRightHandDistance);
    }

    private void CheckRotation()
    {
        //Debugs
        print(playerLeftHand.rotation);
        print(instructorLeftHand.rotation);     

        //Try to multiply x and q by -1... maybe
    }
}
