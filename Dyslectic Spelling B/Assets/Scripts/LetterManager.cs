using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEditor.SceneView;
using Random = System.Random;
public class LetterManager : MonoBehaviour
{
    public static char letterchoosen;
    public GameObject[] slotList;
    public GameObject Camera;
    int Spelled = 0;
    GameObject wordMang;
    Words word;
    public GameObject CloudUI;
    public GameObject SpeachUI;
    public GameObject FailedUI;
    public GameObject ContinueUI;
    string combine;


    public List<LevelWords> levelWords = new List<LevelWords>();
    private List<UnityEngine.Object> usedWords = new List<UnityEngine.Object>();
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FailedUI.SetActive(false);
        CloudUI.SetActive(false);
        SpeachUI.SetActive(true);
        ChooseWord();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Choose word for spelling
    private void ChooseWord()
    {
        LevelWords currentLevelWord = levelWords[Spelled % 10];
        //Picking new word from level
        int newWord = UnityEngine.Random.Range(0, currentLevelWord.word.Length);
        word = currentLevelWord.word[newWord];

        if (!currentLevelWord.word.Contains(usedWords[0]))
        {
            usedWords.Clear();
        }

        while (usedWords.Contains(word))
        {
            //Keep rolling until it doesnt contain word
            newWord = UnityEngine.Random.Range(0, currentLevelWord.word.Length);
            word = currentLevelWord.word[newWord];
        }
        usedWords.Add(word);

        audioSource.clip = word.GetAudio();
        audioSource.Play();
    }
    //Checks Word
    public void Check()
    {
        CloudUI.SetActive(false);
        SpeachUI.SetActive(true);
        Camera.GetComponent<Animator>().Play("CameraToTeacher");
        /*
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
        SpeachUI.SetActive(false);
        Camera.GetComponent<Animator>().Play("RCameraToTeacher");
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
