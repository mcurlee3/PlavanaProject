using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject model;
    Animator animator;

    private void Start()
    {
        animator = model.GetComponent<Animator>();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void SetPose(int pose)
    {
        animator.SetInteger("pose", pose);
        Debug.Log(pose.ToString());
    }

    public void SetSpeed(float speed)
    {
        animator.SetInteger("pose", 0);
        animator.speed = speed;
    }

    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
