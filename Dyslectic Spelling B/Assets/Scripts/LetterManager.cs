using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEditor.SceneView;
using Random = System.Random;
public class LetterManager : MonoBehaviour
{
    public static char letterchoosen;
    public GameObject[] slotList;
    private GameObject selectedSlot;
    private GameObject[] letteredSlots;
    public GameObject Camera;
    int Spelled = 0;
    Words word;
    private char[] wordletters;
    public static char letter;
    public GameObject CloudUI;
    public GameObject SpeachUI;
    public GameObject FailedUI;
    public GameObject ContinueUI;
    string combine;
    bool TimerS;
    int Timer;
    bool spawning;
    public List<LevelWords> levelWords = new List<LevelWords>();
    private List<UnityEngine.Object> usedWords = new List<UnityEngine.Object>();
    public AudioSource audioSource;
    public static List<GameObject> Clouds = new List<GameObject>();
    public GameObject CloudPrefab;
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
        if (spawning == true)
        {
            StartCoroutine(WaitAnime());
        }
        if (TimerS == true) 
        {
            if(Timer > 0)
            {
                Timer -= (int)Time.deltaTime;
            }
            else
            {
                Check();
            }
        }
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
        wordletters = word.BreakingWord();
        audioSource.clip = word.GetAudio();
        audioSource.Play();
    }
    //Checks Word
    public void Check()
    {
        TimerS = false;
        spawning = false;
        CloudUI.SetActive(false);
        SpeachUI.SetActive(true);
        for (int i = 0; i < slotList.Length; i++)
        {
            slotList[i].SetActive(true);
        }
        Camera.GetComponent<Animator>().Play("CameraToTeacher");
        if (combine == word.GetWord())
        {
            Spelled++;
            ChooseWord();
            SetTimer();
            for (int i = 0; i < slotList.Length; i++)
            {
                if (i > wordletters.Length)
                {
                    slotList[i].SetActive(false);
                }
            }
        }
        else
        {
            FailedUI.SetActive(true);
        }
    }
    //Go to Spelling
    public void NowSpell()
    {
        CloudUI.SetActive(true);
        SpeachUI.SetActive(false);
        Camera.GetComponent<Animator>().Play("RCameraToTeacher");
        StartCoroutine(WaitAnime());
    }
    // Checks if a slot is filled and gets the Letter for that slot
    public void CurrentSlot()
    {
        for (int i = 0; i < wordletters.Length; i++) 
        {
            if (slotList[i].transform.GetComponent<TextMeshProUGUI>().text == " ")
            {
                selectedSlot = slotList[i];
                letter = wordletters[i];
                break;
            }
        }
    }
    public void AddtoSlot()
    {
        selectedSlot.GetComponent<TextMeshProUGUI>().text = letterchoosen.ToString();
    }
    public void Sabatoge(GameObject slot)
    {
        if (Spelled <= 10)
        {
            slot.GetComponent<SlotCode>().floatLetters();
        }
        else if (Spelled > 10 && Spelled <= 20) 
        {
            slot.GetComponent<SlotCode>().flipLetters();
        }
        else
        {
            slot.GetComponent<SlotCode>().dispearLetters();
        }
    }
    public void ChooseSlot()
    {
        for (int i = 0; i < wordletters.Length; i++) 
        {
            if (slotList[i].transform.GetComponent<TextMeshProUGUI>().text != " ")
            {
                letteredSlots[i] = slotList[i];
            }
        }
    }
    public void CloudSpawn()
    {
        if (spawning == true) 
        {
            GameObject Cloud = Instantiate(CloudPrefab, new Vector3(463, 66, 0), Quaternion.identity);
            GameObject Cloud2 = Instantiate(CloudPrefab, new Vector3(802.9f, 109, 0), Quaternion.identity);
            GameObject Cloud3 = Instantiate(CloudPrefab, new Vector3(623, 25, 0), Quaternion.identity);
            Clouds.Add(Cloud);
            Clouds.Add(Cloud2);
            Clouds.Add(Cloud3);
        }
    }
    IEnumerator WaitAnime()
    {
        yield return new WaitForSeconds(1);
        spawning = true;
        CloudSpawn();
        TimerS = true;
    }
    private void SetTimer()
    {
        if (Spelled <= 10)
        {
            Timer = 120;
        }
        else if (Spelled > 10 && Spelled <= 20)
        {
            Timer = 90;
        }
        else if (Spelled > 20)
        {
            Timer = 60;
        }
    }
}
