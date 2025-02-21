using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using System;

public class StateController : MonoBehaviour
{
    public string[] States = {
        "Fire",
        "Water",
        "Wind"
    };
    public  Sprite[] StatesImages = {};
    void Update()
    {
        int index = Array.IndexOf(States,playerController.State);
        GetComponent<SpriteRenderer>().sprite = StatesImages[index];
    }
}
