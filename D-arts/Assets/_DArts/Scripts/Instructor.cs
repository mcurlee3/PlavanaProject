using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class Instructor : MonoBehaviour
{
    #region Public Variables

    [Header("Controllers")]
    public GameObject leftController;
    public GameObject rightController;

    [Header("Player Attributes")]
    public Transform player;
    public Transform playerHead;
    public Transform playerLeftHand;
    public Transform playerRightHand;
    public Transform playerLeftFoot;
    public Transform playerRightFoot;

    [Header("Instructor Attributes")]
    public Transform instructorHead;
    public Transform instructorLeftHand;
    public Transform instructorRightHand;
    public Transform instructorLeftFoot;
    public Transform instructorRightFoot;

    [Header("Visual Feedback")]
    public Image rightArm;
    public Image leftArm;
    public Image rightLeg;
    public Image leftLeg;

    [Header("Results")]
    public GameObject summaryPanel;
    public Text RH1, LH1, RF1, LF1;
    public Text RH2, LH2, RF2, LF2;
    public Text RH3, LH3, RF3, LF3;
    public Text RH4, LH4, RF4, LF4;
    public Text RH5, LH5, RF5, LF5;
    public Text RH6, LH6, RF6, LF6;

    public GameObject MenuObj;

    #endregion

    #region Private Variables

    //Root Transforms
    private Transform playerRoot;
    private Transform instructorRoot;

    private Transform body;
    
    private float accuracy = 0.075f;

    private bool leftHand = false;
    private bool rightHand = false;
    private bool leftFoot = false;
    private bool rightFoot = false;
    private bool head = false;

    private bool checkPoses= false;

    private float[,,] finalPos = new float[4, 6, 2];
    private float rightHandDistance, rightHandAngle = 500;
    private float leftHandDistance, leftHandAngle = 500;
    private float rightFootDistance, rightFootAngle = 500;
    private float leftFootDistance, leftFootAngle = 500;
    private int pose;

    private Color orange = new Color(255, 165, 0);

    #endregion

    #region Debugging Variables
    [Header("Debug Variables")]
    public Text testingL; //left position difference
    public Text testingR; //right position difference

    public Text testingIL; //instructor left position
    public Text testingIR; //instructor right position

    public Text testingPL; //player left position
    public Text testingPR; //player right position

    public Text testingPLR; //player left rotation
    public Text testingPRR; //player right rotation

    public Text testingILR; //instructor left rotation
    public Text testingIRR; //instructor right rotation

    public Text testingLR; //left rotation difference
    public Text testingRR; //right rotation difference
    #endregion
    
    private void Start()
    {
        //Parent nodes
        instructorRoot = instructorHead;
        playerRoot = playerHead;
    }

    private void Update()
    {
        if (checkPoses) //Check each joint
        {
            CheckPositionAndRotation();
        }
    }

    public void SetCheck(bool val)
    {
        checkPoses = val;
        Debug.Log("Set Check: " + checkPoses);
    }

    public void SaveValues()
    {
        finalPos[0, pose, 0] = rightHandDistance;
        finalPos[0, pose, 1] = rightHandAngle;
        finalPos[1, pose, 0] = leftHandDistance;
        finalPos[1, pose, 1] = leftHandAngle;
        finalPos[2, pose, 0] = rightFootDistance;
        finalPos[2, pose, 1] = rightFootAngle;
        finalPos[3, pose, 0] = leftFootDistance;
        finalPos[3, pose, 1] = leftFootAngle;

        pose++;

        rightHandDistance = rightHandAngle = leftHandDistance = leftHandAngle = 1;
        rightFootDistance = rightFootAngle = leftFootDistance = leftFootAngle = 1;
    }
    
    private void CheckPositionAndRotation()
    {
        // Draw Debugs
        Debug.DrawLine(playerRoot.position, playerLeftHand.position, Color.red);
        Debug.DrawLine(playerRoot.position, playerRightHand.position, Color.red);
        Debug.DrawLine(instructorRoot.position, instructorRightHand.position, Color.green);
        Debug.DrawLine(instructorRoot.position, instructorLeftHand.position, Color.green);
        
        /// Position Checking
        // Calculate each check point distance from root player
        float playerLeftHandDistance = Vector3.Distance(playerRoot.position, playerLeftHand.position);
        float playerRightHandDistance = Vector3.Distance(playerRoot.position, playerRightHand.position);
        float playerRightFootDistance = Vector3.Distance(playerRoot.position, playerRightFoot.position);
        float playerLeftFootDistance = Vector3.Distance(playerRoot.position, playerLeftFoot.position);

        // Calculate each check point distance from root instructor
        float instructorRightHandDistance = Vector3.Distance(instructorRoot.position, instructorRightHand.position);
        float instructorLeftHandDistance = Vector3.Distance(instructorRoot.position, instructorLeftHand.position);
        float instructorLeftFootDistance = Vector3.Distance(instructorRoot.position, instructorLeftFoot.position);
        float instructorRightFootDistance = Vector3.Distance(instructorRoot.position, instructorRightFoot.position);

        // Calculate Accuracy
        float rightHandPosDifference = playerRightHandDistance / instructorLeftHandDistance; //mirrored so compare right to left
        if (rightHandPosDifference > 1) rightHandPosDifference = 1 - (rightHandPosDifference - 1);

        float leftHandPosDifference = playerLeftHandDistance / instructorRightHandDistance; //mirrored so compare left to right
        if (leftHandPosDifference > 1) leftHandPosDifference = 1 - (leftHandPosDifference - 1);

        float leftFootPosDifference = playerLeftFootDistance / instructorRightFootDistance; //mirrored so compare left to right
        if (leftFootPosDifference > 1) leftFootPosDifference = 1 - (leftFootPosDifference - 1);

        float rightFootPosDifference = playerRightFootDistance / instructorLeftFootDistance; //mirrored so compare right to left
        if (rightFootPosDifference > 1) rightFootPosDifference = 1 - (rightFootPosDifference - 1);

        // Debugs
        /*
        Debug.Log("Player Right Hand Distance From Root: " + playerRightHandDistance);
        Debug.Log("Player Left Hand Distance From Root: " + playerLeftHandDistance);
        Debug.Log("Player Left Foot Distance From Root: " + playerLeftFootDistance);
        Debug.Log("Player Right Foot Distance From Root: " + playerRightFootDistance);

        Debug.Log("Instructor Right Hand Distance From Root: " + instructorRightHandDistance);
        Debug.Log("Instructor Left Hand Distance From Root: " + instructorLeftHandDistance);
        Debug.Log("Instructor Left Foot Distance From Root: " + instructorLeftFootDistance);
        Debug.Log("Instructor Right Foot Distance From Root: " + instructorRightFootDistance);
        */

        ///Rotations
        //Calculate the angle between root and child rotations
        float instructorLeftHandRot = Quaternion.Angle(instructorRoot.rotation, instructorLeftHand.rotation);
        float instructorRightHandRot = Quaternion.Angle(instructorRoot.rotation, instructorRightHand.rotation);
        float instructorLeftFootRot = Quaternion.Angle(instructorRoot.rotation, instructorLeftFoot.rotation);
        float instructorRightFootRot = Quaternion.Angle(instructorRoot.rotation, instructorRightFoot.rotation);

        float playerRightHandRot = Quaternion.Angle(playerRoot.rotation, playerRightHand.rotation);
        float playerLeftHandRot = Quaternion.Angle(playerRoot.rotation, playerLeftHand.rotation);
        float playerRightFootRot = Quaternion.Angle(playerRoot.rotation, playerRightFoot.rotation);
        float playerLeftFootRot = Quaternion.Angle(playerRoot.rotation, playerLeftFoot.rotation);

        //Quaternion playerLeftHandDifference = instructorRightHand.rotation * Quaternion.Inverse(playerLeftHand.rotation);
        //float angle = Quaternion.Angle(instructorRightHand.rotation, playerLeftHand.rotation);

        //Debugs
        testingPL.text = "Player Left Hand: " + playerLeftHandDistance;
        testingPR.text = "Player Right Hand: " + playerRightHandDistance;
        testingIL.text = "Instructor Left Hand: " + instructorLeftHandDistance;
        testingIR.text = "Instructor Right Hand: " + instructorRightHandDistance;

        testingPLR.text = "Player Left Hand Rotation: " + playerLeftHandRot;
        testingPRR.text = "Player Right Hand Rotation: " + playerRightHandRot;
        testingILR.text = "Instructor Left Hand Rotation: " + instructorLeftHandRot;
        testingIRR.text = "Instructor Right Hand Rotation: " + instructorRightHandRot;

        /*
        // Get the difference between player and instructor rotations
        float rightHandRotDifference = 180 - Math.Abs(Math.Abs(45 - 270) - 180); //mirrored so right compare to left
        float leftHandRotDifference = 180 - Math.Abs(Math.Abs(playerLeftHandRot - instructorRightHandRot) - 180); //mirrored so left compare to right
        float rightFootRotDifference = 180 - Math.Abs(Math.Abs(playerRightFootRot - instructorLeftFootRot) - 180); //mirrored so right compare to left
        float leftFootRotDifference = 180 - Math.Abs(Math.Abs(playerLeftFootRot - instructorRightFootRot) - 180); //mirrored so left compare to right
        */
        float rightHandRotDifference;
        if (playerRightHandRot < instructorLeftHandRot)
            rightHandRotDifference = playerRightHandRot / instructorLeftHandRot; //mirrored so compare right to left
        else
            rightHandRotDifference = instructorLeftHandRot / playerRightHandRot;
        if (rightHandRotDifference < 1) rightHandRotDifference *= -1;

        float leftHandRotDifference;
        if (playerLeftHandRot < instructorRightHandRot)
            leftHandRotDifference = playerLeftHandRot / instructorRightHandRot; //mirrored so compare right to left
        else
            leftHandRotDifference = instructorRightHandRot / playerLeftHandRot;
        if (leftHandRotDifference < 1) leftHandRotDifference *= -1;

        float rightFootRotDifference;
        if (playerRightFootRot < instructorLeftFootRot)
            rightFootRotDifference = playerRightFootRot / instructorLeftFootRot; //mirrored so compare right to left
        else
            rightFootRotDifference = instructorLeftFootRot / playerRightFootRot;
        if (rightFootRotDifference < 1) rightFootRotDifference *= -1;

        float leftFootRotDifference;
        if (playerLeftFootRot < instructorRightFootRot)
            leftFootRotDifference = playerLeftFootRot / instructorRightFootRot; //mirrored so compare right to left
        else
            leftFootRotDifference = instructorRightFootRot / playerLeftFootRot;
        if (leftFootRotDifference < 1) leftFootRotDifference *= -1;

        Debug.Log("Right hand pos difference: " + rightHandPosDifference);
        Debug.Log("Right hand pos percentage: " + instructorLeftHandRot/playerRightHandRot);

        //float rightHandRotAccuracy = playerRightHandRot / instructorLeftHandRot;
        //if (rightHandRotAccuracy > 1) rightHandRotAccuracy = 1 - (rightHandRotAccuracy - 1);

        //Debugs
        testingLR.text = "Player Left Angle: " + leftHandRotDifference;

        // Calculate Accuracy of Player Movement
        float rightHandAccuracy = rightHandPosDifference;// + rightHandRotDifference / 2; //average out the two accuracies
        float leftHandAccuracy = leftHandPosDifference;// + leftHandRotDifference / 2;
        float rightFootAccuracy = rightFootPosDifference; // + rightFootRotDifference / 2;
        float leftFootAccuracy = leftFootPosDifference;// + leftFootRotDifference / 2;

        // Update Variables to Save
        rightHandDistance = rightHandAccuracy;
        leftHandDistance = leftHandAccuracy;
        rightFootDistance = rightFootAccuracy;
        leftFootDistance = leftFootAccuracy;

        // Debugs
        Debug.Log("Player Right Hand Accuracy: " + rightHandAccuracy);
        Debug.Log("Player Left Hand Accuracy: " + leftHandAccuracy);
        Debug.Log("Player Right Foot Accuracy: " + rightFootAccuracy);
        Debug.Log("Player Left Foot Accuracy: " + leftFootAccuracy);

        testingL.text = "Left Distance: " + leftHandAccuracy;
        testingR.text = "Right Distance: " + rightHandAccuracy;

        // Update Menu
        rightArm.color = DetermineColor(rightHandAccuracy, 0f);
        leftArm.color = DetermineColor(leftHandAccuracy, 0f);
        rightLeg.color = DetermineColor(rightFootAccuracy, 0f);
        leftLeg.color = DetermineColor(leftFootAccuracy, 0f);
    }

    private void CheckMovement()
    {
        //Set up Nodes for Instructor and Player

        #region Position
        //Relative coordinates from parent to left hand
        Vector3 leftHandRelativePos = instructorHead.InverseTransformPoint(instructorLeftHand.position);
        Vector3 playerLeftHandPos = playerHead.InverseTransformPoint(playerLeftHand.position);

        Vector3 leftFootRelativePos = instructorHead.InverseTransformPoint(instructorLeftFoot.position);
        Vector3 playerLeftFootPos = playerHead.InverseTransformPoint(playerLeftFoot.position);

        //Relative coordinates from parent to right hand
        Vector3 rightHandRelativePos = instructorHead.InverseTransformPoint(instructorRightHand.position);
        Vector3 playerRightHandPos = playerHead.InverseTransformPoint(playerRightHand.position);

        Vector3 rightFootRelativePos = instructorHead.InverseTransformPoint(instructorLeftFoot.position);
        Vector3 playerRightFootPos = playerHead.InverseTransformPoint(playerRightFoot.position);

        //Offset
        playerRightHandPos.x -= player.position.x;
        playerLeftHandPos.x -= player.position.x;
        playerRightHandPos.z -= player.position.z;
        playerLeftHandPos.z -= player.position.z;
        playerRightFootPos.x -= player.position.x;
        playerLeftFootPos.x -= player.position.x;
        playerRightFootPos.z -= player.position.z;
        playerLeftFootPos.z -= player.position.z;

        //Mirror the instructors hands to compensate for mirroring effect
        rightHandRelativePos.z *= -1;
        leftHandRelativePos.z *= -1;
        rightFootRelativePos.z *= -1;
        leftFootRelativePos.z *= -1;
        #endregion

        #region Rotation
        Quaternion rightHandPlayer = Quaternion.Euler(playerRightHand.eulerAngles);
        Quaternion leftHandPlayer = Quaternion.Euler(playerLeftHand.eulerAngles);

        Quaternion rightHandInstructor = Quaternion.Euler(instructorRightHand.eulerAngles);
        Quaternion leftHandInstructor = Quaternion.Euler(instructorLeftHand.eulerAngles);

        Quaternion rightFootPlayer = Quaternion.Euler(playerRightFoot.eulerAngles);
        Quaternion leftFootPlayer = Quaternion.Euler(playerLeftFoot.eulerAngles);

        Quaternion rightFootInstructor = Quaternion.Euler(instructorRightFoot.eulerAngles);
        Quaternion leftFootInstructor = Quaternion.Euler(instructorLeftFoot.eulerAngles);

        //Corrections
        rightHandPlayer = new Quaternion(rightHandPlayer.z, -rightHandPlayer.w, rightHandPlayer.x, -rightHandPlayer.y);
        leftHandPlayer = new Quaternion(leftHandPlayer.z, -leftHandPlayer.w, leftHandPlayer.x, -leftHandPlayer.y);

        #endregion

        #region Debugs
        //Messages
        //Debug.Log("Instructor Left Hand Position: " + leftHandRelativePos);
        //Debug.Log("Player Right Hand Position: " + playerRightHandPos);
        //Debug.Log("Distance between instructor and player right: " + (Vector3.Distance(playerRightHandPos, leftHandRelativePos)));

        //Debug.Log("Instructor Right Hand Position: " + rightHandRelativePos);
        //Debug.Log("Player Left Hand Position: " + playerLeftHandPos);
        //Debug.Log("Distance between instructor and player left: " + (Vector3.Distance(playerLeftHandPos, rightHandRelativePos)));

        //Debug.Log("Player Left Hand Rotation: " + playerLeftHandRot);
        //Debug.Log("Instructor Right Hand Rotation: " + instructorRightHandRot);
        //Debug.Log("Player Right Hand Rotation: " + playerRightHandRot);
        //Debug.Log("Instructor Left Hand Rotation: " + instructorLeftHandRot);

        //Debug.Log("Difference between Player Left and Instructor Right: " + Quaternion.Angle(playerLeftHand.rotation, instructorRightHand.rotation));

        //Draws
        Debug.DrawLine(playerHead.position, playerLeftHandPos, Color.black);
        Debug.DrawLine(playerHead.position, playerRightHandPos, Color.black);

        Debug.DrawLine(instructorHead.position, instructorLeftHand.position, Color.green);
        Debug.DrawLine(instructorHead.position, instructorRightHand.position, Color.green);

        //Coordinates
        testingIL.text = "Instructor Left Hand: " + leftHandRelativePos;
        testingIR.text = "Instructor Right Hand: " + rightHandRelativePos;

        testingPL.text = "Player Left Hand: " + playerLeftHandPos;
        testingPR.text = "Player Right Hand: " + playerRightHandPos;

        testingILR.text = "Instructor Left Rotation: " + leftHandInstructor;
        testingIRR.text = "Instructor Right Rotation: " + rightHandInstructor;

        testingPLR.text = "Player Left Rotation: " + leftHandPlayer;
        testingPRR.text = "Player Right Rotation: " + rightHandPlayer;

        #endregion

        //Checks
        CheckLeftHand(playerLeftHandPos, rightHandRelativePos, leftHandPlayer, rightHandInstructor);
        CheckRightHand(playerRightHandPos, leftHandRelativePos, rightHandPlayer, leftHandInstructor);
        CheckLeftFoot(playerLeftFootPos, rightFootRelativePos, leftFootPlayer, rightFootInstructor);
        CheckRightFoot(playerRightFootPos, leftFootRelativePos, rightFootPlayer, leftFootInstructor);
    }

    //Check position of left hand with right hand of instructor
    private void CheckLeftHand(Vector3 playerLeft, Vector3 instructorRight, Quaternion playerLeftRot, Quaternion instructorRightRot)
    {
        //Get distance from instructor hand to player hand
        float distance = Vector3.Distance(playerLeft, instructorRight);
        testingL.text = "Left Distance: " + distance;

        if (distance < leftHandDistance)
            leftHandDistance = distance;

        //Get angle between instructor and player
        float angle = Quaternion.Angle(playerLeftRot, instructorRightRot);
        testingLR.text = "Left Angle: " + angle;

        if (angle < leftHandAngle)
            leftHandAngle = angle;

        //Checking accuracy
        if (distance < 0.20f && angle < 0.30f)
            leftArm.color = Color.green;
        else if (distance < 0.30f && angle < 0.40f)
            leftArm.color = orange;
        else
            leftArm.color = Color.red;

        #region Debugging

        if (distance < 0.20f)
            testingPL.color = Color.green;
        else
            testingPL.color = Color.black;

        if (angle < 30f)
            testingPLR.color = Color.green;
        else
            testingPLR.color = Color.black;

        #endregion
    }

    //Check position of right hand with left hand of instructor
    private void CheckRightHand(Vector3 playerRight, Vector3 instructorLeft, Quaternion playerRightRot, Quaternion instructorLeftRot)
    {
        //Get distance from instructor hand to player hand
        float distance = Vector3.Distance(playerRight, instructorLeft);
        testingR.text = "Right Distance: " + distance;

        //Get angle between instructor and player
        float angle = Quaternion.Angle(playerRightRot, instructorLeftRot);
        testingRR.text = "Right Angle: " + angle;

        if (distance < rightHandDistance)
            rightHandDistance = distance;

        if (angle < rightHandAngle)
            rightHandAngle = angle;

        if (distance < 0.20f && angle < 0.30f)
            rightArm.color = Color.green;
        else if (distance < 0.30f && angle < 0.40f)
            rightArm.color = orange;
        else
            rightArm.color = Color.red;

        #region debugging
        if (distance < 0.20f)
            testingPR.color = Color.green;
        else
            testingPR.color = Color.black;

        if (angle < 30f)
            testingPRR.color = Color.green;
        else
            testingPRR.color = Color.black;
        #endregion
    }

    //Check position of left foot with right foot of instructor
    private void CheckLeftFoot(Vector3 playerLeft, Vector3 instructorRight, Quaternion playerLeftRot, Quaternion instructorRightRot)
    {
        //Get distance from instructor foot to player foot
        float distance = Vector3.Distance(playerLeft, instructorRight);
        float angle = Quaternion.Angle(playerLeftRot, instructorRightRot);

        if (distance < leftFootDistance)
            leftFootDistance = distance;

        if (angle < leftFootAngle)
            leftFootAngle = angle;

        if (distance < 0.20f && angle < 0.30f)
            leftLeg.color = Color.green;
        else if (distance < 0.30f && angle < 0.40f)
            leftLeg.color = orange;
        else
            leftLeg.color = Color.red;
    }

    //Check position of right foot with left foot of instructor
    private void CheckRightFoot(Vector3 playerRight, Vector3 instructorLeft, Quaternion playerRightRot, Quaternion instructorLeftRot)
    {
        //Get distance from instructor foot to player foot
        float distance = Vector3.Distance(playerRight, instructorLeft);
        float angle = Quaternion.Angle(playerRightRot, instructorLeftRot);

        if (distance < rightFootDistance)
            rightFootDistance = distance;

        if (angle < rightFootAngle)
            rightFootAngle = angle;

        rightLeg.color = DetermineColor(distance, angle);
    }

    private Color DetermineColor(float accuracy, float num)
    {
        Color col;
        if (accuracy > 0.90f)
            col = Color.green;
        else if (accuracy > 0.80f)
            col = orange;
        else
            col = Color.red;

        return col;
    }

    private void EnableControllers()
    {
        //Enable laser pointers
        leftController.GetComponent<VRTK_Pointer>().enabled = true;
        leftController.GetComponent<VRTK_StraightPointerRenderer>().enabled = true;
        leftController.GetComponent<VRTK_UIPointer>().enabled = true;

        rightController.GetComponent<VRTK_Pointer>().enabled = true;
        rightController.GetComponent<VRTK_StraightPointerRenderer>().enabled = true;
        rightController.GetComponent<VRTK_UIPointer>().enabled = true;
    }

    public void ShowResults()
    {
        //Show menus
        summaryPanel.SetActive(true);
        MenuObj.SetActive(true);

        EnableControllers();

        //Set Colors Pose 1
        RH1.color = DetermineColor(finalPos[0, 0, 0], finalPos[0, 0, 1]);
        LH1.color = DetermineColor(finalPos[1, 0, 0], finalPos[1, 0, 1]);
        RF1.color = DetermineColor(finalPos[2, 0, 0], finalPos[2, 0, 1]);
        LF1.color = DetermineColor(finalPos[3, 0, 0], finalPos[3, 0, 1]);

        //Set Colors Pose 2
        RH2.color = DetermineColor(finalPos[0, 1, 0], finalPos[0, 1, 1]);
        LH2.color = DetermineColor(finalPos[1, 1, 0], finalPos[1, 1, 1]);
        RF2.color = DetermineColor(finalPos[2, 1, 0], finalPos[2, 1, 1]);
        LF2.color = DetermineColor(finalPos[3, 1, 0], finalPos[3, 1, 1]);

        //Set Colors Pose 3
        RH3.color = DetermineColor(finalPos[0, 2, 0], finalPos[0, 2, 1]);
        LH3.color = DetermineColor(finalPos[1, 2, 0], finalPos[1, 2, 1]);
        RF3.color = DetermineColor(finalPos[2, 2, 0], finalPos[2, 2, 1]);
        LF3.color = DetermineColor(finalPos[3, 2, 0], finalPos[3, 2, 1]);

        //Set Colors Pose 4
        RH4.color = DetermineColor(finalPos[0, 3, 0], finalPos[0, 3, 1]);
        LH4.color = DetermineColor(finalPos[1, 3, 0], finalPos[1, 3, 1]);
        RF4.color = DetermineColor(finalPos[2, 3, 0], finalPos[2, 3, 1]);
        LF4.color = DetermineColor(finalPos[3, 3, 0], finalPos[3, 3, 1]);

        //Set Colors Pose 5
        RH5.color = DetermineColor(finalPos[0, 4, 0], finalPos[0, 4, 1]);
        LH5.color = DetermineColor(finalPos[1, 4, 0], finalPos[1, 4, 1]);
        RF5.color = DetermineColor(finalPos[2, 4, 0], finalPos[2, 4, 1]);
        LF5.color = DetermineColor(finalPos[3, 4, 0], finalPos[3, 4, 1]);

        //Set Colors Pose 6
        RH6.color = DetermineColor(finalPos[0, 5, 0], finalPos[0, 5, 1]);
        LH6.color = DetermineColor(finalPos[1, 5, 0], finalPos[1, 5, 1]);
        RF6.color = DetermineColor(finalPos[2, 5, 0], finalPos[2, 5, 1]);
        LF6.color = DetermineColor(finalPos[3, 5, 0], finalPos[3, 5, 1]);
    }
}

