using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
