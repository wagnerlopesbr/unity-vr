using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterScript : MonoBehaviour
{
    public void LeaveButton() {
        SceneManager.LoadScene("startingMenu");
    }
}
