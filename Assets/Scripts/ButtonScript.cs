using System;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public String[] ButtonsNames = {"w","a","s","d","q","e"};
    public Sprite[] ButtonsImages = {};
    [SerializeField] private GameObject Visual;
    private System.Random random = new System.Random();
    private int ButtonNumber;

    void Start()
    {
        GenerateButton();
    }
    private void GenerateButton(){
        ButtonNumber = random.Next(0,ButtonsNames.Length);
        Visual = transform.GetChild(0).gameObject;
        Visual.GetComponent<SpriteRenderer>().sprite = ButtonsImages[ButtonNumber];
        transform.parent.GetComponent<pressButtons>().ButtonsNames.Add(ButtonsNames[ButtonNumber]);
    }
}
