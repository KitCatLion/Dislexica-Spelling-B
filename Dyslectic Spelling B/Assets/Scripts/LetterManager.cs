using UnityEngine;
using Random = System.Random;
using System;
using System.Collections.Generic;
using TMPro;
public class LetterManager : MonoBehaviour
{
    public static char letterchoosen;
    public GameObject[] slotList;
    int Spelled = 0;
    GameObject wordMang;
    GameObject CloudUI;
    GameObject Speach;
    GameObject FailedUI;
    string combine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FailedUI.SetActive(false);
        CloudUI.SetActive(false);
        Speach.SetActive(true);
        ChooseWord();
        //wordMang.wordSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Choose word for spelling
    private void ChooseWord()
    {
        Random random = new Random();
        int num = random.Next(0, 5);
        if (Spelled == 10)
        {
            //wordMang = ;
        }
        else if(Spelled == 20)
        {
            //wordMang = 3Level.Word[num];
        }
        else
        {
            //wordMang = 1Level.Word[num];
        }
    }
    //Checks Word
    public void Check()
    {/*
        CloudUI.SetActive(false);
        Speach.SetActive(true);
        if (combine == wordMang.word)
        {
            Spelled++;
            ChooseWord();
            wordMang.wordSound.Play();
        }
        else
        {
            FailedUI.SetActive(true);
        }
        */
    }
    //Go to Spelling
    public void NowSpell()
    {
        CloudUI.SetActive(true);
        Speach.SetActive(false);
        //Start Spawning Clouds
    }
    // Checks if a slot is filled
    public void CurrentSlot()
    {/*
        for (int i = 0; i < wordMang.brokenWord.Length; i++) 
        {
            
        }
        */
    }
}
