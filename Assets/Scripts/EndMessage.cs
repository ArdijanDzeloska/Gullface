using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndMessage : MonoBehaviour
{

    public TextMeshProUGUI EndText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (GlobalScript.Won)
        {
            EndText.text = "Congrats!";
        }
        else
        {
            EndText.text = "Try again!";
        }
    }

}
