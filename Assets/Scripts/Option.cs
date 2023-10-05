using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    
    public int optionNumber;

    [NonSerialized] 
    public bool selected;
    [NonSerialized]
    public Image optionImage;

    private void Awake()
    {
        optionImage = GetComponent<Image>();
    }
}
