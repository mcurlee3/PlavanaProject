using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public MenuController menuController;
    public int pose;

    public void OnClick()
    {
        menuController.SetPose(pose);
    }
}
