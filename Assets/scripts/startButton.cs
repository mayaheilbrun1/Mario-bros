using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickOnSpace()
    {
        SceneManager.LoadScene(1);
    }
}
