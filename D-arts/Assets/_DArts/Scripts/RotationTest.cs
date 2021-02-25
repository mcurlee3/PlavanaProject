using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    public Transform cube1RightHand;
    public Transform cube2RightHand;

    public Transform cube1LeftHand;
    public Transform cube2LeftHand;

    //private Vector3 cube1RotRight;
    //private Vector3 cube1RotLeft;

    //private Vector3 cube2RotRight;
    //private Vector3 cube2RotLeft;


    private void Update()
    {
        /*
        cube1RotRight = cube1RightHand.eulerAngles;
        cube2RotRight = cube2RightHand.eulerAngles;
        cube1RotLeft = cube1LeftHand.eulerAngles;
        cube2RotLeft= cube2LeftHand.eulerAngles;

        cube2RotRight.y = 360 - 180 - cube2RotRight.y;
        cube2RotRight.z = 360 - cube2RotRight.z;
        if (cube2RotLeft.y > 180)
            cube2RotLeft.y = cube2RotLeft.y - 180;
        else
            cube2RotLeft.y = 180 - cube2RotLeft.y;
        cube2RotLeft.y = 360 - cube2RotLeft.y;
        cube2RotLeft.z = 360 - cube2RotLeft.z;

        if (cube2RotRight.z == 360) cube2RotRight.z = 0;
        if (cube2RotLeft.z == 360) cube2RotLeft.z = 0;
        */

        Quaternion rightHandPlayer = Quaternion.Euler(cube2RightHand.eulerAngles);
        Quaternion leftHandPlayer = Quaternion.Euler(cube2LeftHand.eulerAngles);

        Quaternion rightHandInstructor = Quaternion.Euler(cube1RightHand.eulerAngles);
        Quaternion leftHandInstructor = Quaternion.Euler(cube1LeftHand.eulerAngles);

        /* TESTING INVERSION
        Debug.Log("Non-inverted Player Right Hand: " + rightHandPlayer);
        Debug.Log("Non-inverted Player Left Hand: " + leftHandPlayer);

        rightHandPlayer = Quaternion.Inverse(rightHandPlayer);
        leftHandPlayer = Quaternion.Inverse(leftHandPlayer);

        Debug.Log("Inverted Player Right Hand: " + rightHandPlayer);
        Debug.Log("Inverted Player Left Hand: " + leftHandPlayer);
        */

        /* TESTING NORMALIZE
        Debug.Log("Non-normalized Player Right Hand: " + rightHandPlayer);
        Debug.Log("Non-normalized Player Leftt Hand: " + leftHandPlayer);
        rightHandPlayer.Normalize();
        leftHandPlayer.Normalize();
        Debug.Log("Normalized Player Right Hand: " + rightHandPlayer);
        Debug.Log("Normalized Player Left Hand: " + leftHandPlayer);
        */

        /*Switching the coordinates
         */
        //Debug.Log("Original Player Right Hand: " + rightHandPlayer);
        //Debug.Log("Original Player Leftt Hand: " + leftHandPlayer);
        rightHandPlayer = new Quaternion(rightHandPlayer.z, -rightHandPlayer.w, rightHandPlayer.x, -rightHandPlayer.y);
        leftHandPlayer = new Quaternion(leftHandPlayer.z, -leftHandPlayer.w, leftHandPlayer.x, -leftHandPlayer.y);
        //Debug.Log("Normalized Player Right Hand: " + rightHandPlayer);
        //Debug.Log("Normalized Player Left Hand: " + leftHandPlayer);



        float angleRight = Quaternion.Angle(rightHandPlayer, leftHandInstructor);
        float angleLeft = Quaternion.Angle(leftHandPlayer, rightHandInstructor);

        bool sameRotationRight = Mathf.Abs(angleRight) < 1e-3f;
        bool sameRotationLeft = Mathf.Abs(angleLeft) < 1e-3f;

        /*
        Debug.Log("Instructor Right Rotation: " + rightHandInstructor);
        Debug.Log("Player Right Rotation: " + rightHandPlayer);
        Debug.Log("Instructor Left Rotation: " + leftHandInstructor);
        Debug.Log("Player Left Rotation: " + leftHandPlayer);

        Debug.Log("Difference Between Left Instructor and right Player: " + angleRight);
        Debug.Log("Difference Between Right Instructor and left Player: " + angleLeft);

        Debug.Log("Same Right Rotation: " + sameRotationRight);
        Debug.Log("Same Left Rotation: " + sameRotationLeft);
        */
    }
}
