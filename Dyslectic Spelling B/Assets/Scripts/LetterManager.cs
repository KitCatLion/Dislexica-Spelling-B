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
    private static GameObject selectedSlot;
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
    public TextMeshProUGUI TimerUI;
    public GameObject Cloudpos1;
    public GameObject Cloudpos2;
    public GameObject Cloudpos3;
    bool TimerS;
    int Timer = 120;
    bool spawning;
    public List<LevelWords> levelWords = new List<LevelWords>();
    private List<UnityEngine.Object> usedWords = new List<UnityEngine.Object>();
    public AudioSource audioSource;
    public static List<GameObject> Clouds = new List<GameObject>();
    public GameObject CloudPrefab;
    public static bool choosen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FailedUI.SetActive(false);
        CloudUI.SetActive(false);
        SpeachUI.SetActive(true);
        ResetSlots();
        TimerUI.text = Timer.ToString();
        ChooseWord();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawning == true)
        {
            CurrentSlot();
            StartCoroutine(WaitAnime());
        }
        if (choosen == true)
        {
            CloudKill();
        }
        if (TimerS == true) 
        {
            if(Timer > 0)
            {
                Timer -= (int)Time.deltaTime;
                TimerUI.text = Timer.ToString();
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
        LevelWords currentLevelWord = levelWords[(int)Spelled / 10];
        //Picking new word from level
        int newWord = UnityEngine.Random.Range(0, currentLevelWord.word.Length);
        word = currentLevelWord.word[newWord];
        while (usedWords.Contains(word))
        {
            //Keep rolling until it doesnt contain word
            newWord = UnityEngine.Random.Range(0, currentLevelWord.word.Length);
            word = currentLevelWord.word[newWord];
        }
        wordletters = word.BreakingWord();
        audioSource.clip = word.GetAudio();
        audioSource.Play();
        for (int i = 0; i < slotList.Length; i++)
        {
            if (i > wordletters.Length - 1)
            {
                slotList[i].SetActive(false);
            }
        }
        usedWords.Add(word);
        if (!currentLevelWord.word.Contains(usedWords[0]))
        {
            usedWords.Clear();
        }
    }
    #region GamePlayButtons
    //Checks Word
    public void Check()
    {
        TimerS = false;
        spawning = false;
        CloudUI.SetActive(false);
        SpeachUI.SetActive(true);
        ResetSlots();
        Camera.GetComponent<Animator>().Play("CameraToTeacher");
        if (combine == word.GetWord())
        {
            Spelled++;
            ChooseWord();
            SetTimer();
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
    public void ListenAgain()
    {
        audioSource.clip = word.GetAudio();
        audioSource.Play();
    }
    #endregion
    #region SlotCode
    // Checks if a slot is filled and gets the Letter for that slot
    public void CurrentSlot()
    {
        for (int i = 0; i < wordletters.Length; i++) 
        {
            if (slotList[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text == " " && slotList[i].activeInHierarchy == true)
            {
                selectedSlot = slotList[i].transform.GetChild(1).gameObject;
                letter = wordletters[i];
                break;
            }
        }
    }
    public static void AddtoSlot(string Letter)
    {
        selectedSlot.GetComponent<TextMeshProUGUI>().text = Letter;
        choosen = true;
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
            if (slotList[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text != " ")
            {
                letteredSlots[i] = slotList[i].transform.GetChild(1).gameObject;
            }
        }
    }
    public void ResetSlots()
    {
        for (int i = 0; i < slotList.Length; i++)
        {
            slotList[i].SetActive(true);
            slotList[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = " ";
        }
    }
    #endregion
    public void CloudSpawn()
    {
        if (spawning == true && Clouds.Count == 0) 
        {
            choosen = false;
            GameObject Cloud = Instantiate(CloudPrefab);
            GameObject Cloud2 = Instantiate(CloudPrefab);
            GameObject Cloud3 = Instantiate(CloudPrefab);
            Cloud.transform.SetParent(transform, false);
            Cloud2.transform.SetParent(transform, false);
            Cloud3.transform.SetParent(transform, false);
            Cloud.transform.localPosition = Cloudpos1.transform.localPosition;
            Cloud2.transform.localPosition = Cloudpos2.transform.localPosition;
            Cloud3.transform.localPosition = Cloudpos3.transform.localPosition;
            Clouds.Add(Cloud);
            Clouds.Add(Cloud2);
            Clouds.Add(Cloud3);
        }
    }
    public void CloudKill()
    {
        Destroy(Clouds[0]);
        Clouds.RemoveAt(0);
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
