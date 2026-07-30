using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class lettershift : MonoBehaviour
{
    //chance rate can be changed based on how long you last and what level your on
    public static List<int> wrongLetter = new List<int> { 4 };
    public static List<int> correctLetter = new List<int> { 1, 2, 3 };
    bool correct;
    public TextMeshProUGUI slotLetter;
    int cnum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WrongorRight();
        if (correct == true)
        {
            //the correct letter
            slotLetter.GetComponent<TextMeshProUGUI>().text = (LetterManager.letter).ToString();
        }
        else
        {
            //incorrect letter
            cnum = LettertoNum(LetterManager.letter);
            slotLetter.GetComponent<TextMeshProUGUI>().text = WrongLetterPicker(cnum);
        }
    }
    //correct letter possibliy
    public void WrongorRight()
    {
        int chance;
        Random random = new Random();
        chance = random.Next(1, 5);
        foreach (int i in wrongLetter) 
        {
            if (chance == wrongLetter[i])
            {
                correct = false;
                break;
            }
        }
        foreach(int i in correctLetter)
        {
            if(chance == correctLetter[i])
            {
                correct = true;
                break;
            }
        }
    }
    //randomly selects wrong letter.
    public string WrongLetterPicker(int correctNum)
    {
        int pickedNum;
        string pickedLetter;
        Random rand = new Random();
        pickedNum = rand.Next(1, 27 - correctNum); //exlcudes the correct number in the random selection.
        pickedLetter = NumtoLetter(pickedNum); //converts the picked number to a letter
            return pickedLetter;
    }
    //converting letters to numbers
    public int LettertoNum(char L)
    {
        char upper = char.ToUpper(L);
        return upper - 'A' + 1;
    }
    //converting numbers to letters
    public string NumtoLetter(int Num)
    {
        string letter = "";
        switch (Num)
        {
            case 1:
                letter = "A";
                break;
            case 2:
                letter = "B";
                break;
            case 3:
                letter = "C";
                break;
            case 4:
                letter = "D";
                break;
            case 5:
                letter = "E";
                break;
            case 6:
                letter = "F";
                break;
            case 7:
                letter = "G";
                break;
            case 8:
                letter = "H";
                break;
            case 9:
                letter = "I";
                break;
            case 10:
                letter = "J";
                break;
            case 11:
                letter = "K";
                break;
            case 12:
                letter = "L";
                break;
            case 13:
                letter = "M";
                break;
            case 14:
                letter = "N";
                break;
            case 15:
                letter = "O";
                break;
            case 16:
                letter = "P";
                break;
            case 17:
                letter = "Q";
                break;
            case 18:
                letter = "R";
                break;
            case 19:
                letter = "S";
                break;
            case 20:
                letter = "T";
                break;
            case 21:
                letter = "U";
                break;
            case 22:
                letter = "V";
                break;
            case 23:
                letter = "W";
                break;
            case 24:
                letter = "X";
                break;
            case 25:
                letter = "Y";
                break;
            case 26:
                letter = "Z";
                break;
        }
        return letter;
    }
}
