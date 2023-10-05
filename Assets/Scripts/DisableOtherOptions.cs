using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableOtherOptions : MonoBehaviour
{
    [SerializeField]
    private Button[] optionsToDisable;

    public void OtherOptions()
    {
        for (int i = 0; i < optionsToDisable.Length; i++)
        {
            optionsToDisable[i].enabled = false;
        }
    }
}
