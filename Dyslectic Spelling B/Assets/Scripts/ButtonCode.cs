using System;
using TMPro;
using UnityEngine;

public class ButtonCode : MonoBehaviour
{
    public TextMeshProUGUI letter;
    public GameObject Cloud;
    private string transferLetter;
    private char justLetter;
    public void OnClick()
    {
        transferLetter = letter.text;
        justLetter = transferLetter[0];
        LetterManager.letterchoosen = justLetter;
        Debug.Log("This is the Letter Choosen " + justLetter);
        Destroy(Cloud);
    }
}
