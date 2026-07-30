using System;
using TMPro;
using UnityEngine;

public class ButtonCode : MonoBehaviour
{
    public TextMeshProUGUI letter;
    public GameObject Cloud;
    private string transferLetter;
    private char justLetter;
    bool die = false;
    public void Update()
    {
        //transform.position = Vector3.MoveTowards();
        if(die == true)
        {
            Destroy(LetterManager.Clouds[0]);
        }
    }
    public void OnClick()
    {
        transferLetter = letter.text;
        justLetter = transferLetter[0];
        LetterManager.letterchoosen = justLetter;
        Debug.Log("This is the Letter Choosen " + justLetter);
        die = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Top"))
        {
            Destroy(Cloud);
        }
    }
}
