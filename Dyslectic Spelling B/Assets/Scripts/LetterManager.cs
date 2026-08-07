using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LetterManager : MonoBehaviour
{
    public static char letterchoosen;
    public GameObject[] slotList;
    private static GameObject selectedSlot;
    private GameObject[] letteredSlots;
    public GameObject Camera;
    private int Spelled = 0;
    Words word;
    private char[] wordletters;
    public static char letter;
    public GameObject CloudUI;
    public GameObject SpeachUI;
    public GameObject FailedUI;
    public GameObject ContinueUI;
    public GameObject WinUI;
    public GameObject SlotTtext;
    public GameObject CloudTtext;
    private string combine;
    public TextMeshProUGUI TimerUI;
    public GameObject Cloudpos1;
    public GameObject Cloudpos2;
    public GameObject Cloudpos3;
    private bool TimerS;
    private float Timer = 120;
    private bool spawning;
    public List<LevelWords> levelWords = new List<LevelWords>();
    private List<UnityEngine.Object> usedWords = new List<UnityEngine.Object>();
    public AudioSource audioSource;
    public static List<GameObject> Clouds = new List<GameObject>();
    public GameObject CloudPrefab;
    public static bool choosen;
    private bool UI;
    public static bool sab = false;
    private bool tutoral = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SlotTtext.SetActive(false);
        CloudTtext.SetActive(false);
        FailedUI.SetActive(false);
        CloudUI.SetActive(false);
        SpeachUI.SetActive(true);
        WinUI.SetActive(false);
        ResetSlots();
        TimerUI.text = Timer.ToString();
        ChooseWord();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawning == true)
        {
            StartCoroutine(WaitAnime());
        }
        if (choosen == true) // deletes clouds when player chooses them
        {
            CloudTtext.SetActive(false);
            CloudKill();
            CloudKill();
            CloudKill();
        }
        SlotChooseSab();
        if (TimerS == true) 
        {
            TempStop();
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
                TimerUI.text = Mathf.CeilToInt(Timer).ToString();
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
        CombineSlots();
        TimerS = false;
        spawning = false;
        CloudUI.SetActive(false);
        CloudKill();
        CloudKill();
        CloudKill();
        UI = true;
        ResetSlots();
        Camera.GetComponent<Animator>().Play("CameraToTeacher");
        StartCoroutine(WaitBack());
        if (combine == word.GetWord())
        {
            Spelled++;
            if (Spelled == 30)
            {
                WinUI.SetActive(true);
                SpeachUI.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "You Win with the final word " + word.GetWord();
            }
            else
            {
                ChooseWord();
                SetTimer();
                combine = "";
            }
        }
        else
        {
            FailedUI.SetActive(true);
            SpeachUI.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "You misspelled " + word.GetWord();
            Spelled = 0;
            usedWords.Clear();
            combine = "";
        }
    }
    //Go to Spelling
    public void NowSpell()
    {
        spawning = true;
        UI = false;
        SpeachUI.SetActive(false);
        Camera.GetComponent<Animator>().Play("RCameraToTeacher");
        StartCoroutine(WaitBack());
        StartCoroutine(WaitAnime());
    }
    public void ListenAgain()
    {
        audioSource.clip = word.GetAudio();
        audioSource.Play();
    }
    //try again
    public void Retry()
    {
        FailedUI.SetActive(false);
        ContinueUI.SetActive(true);
        ResetSlots();
        ChooseWord();
    }
    //back to menu
    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
    }
    //Quit game
    public void Quiting()
    {
        Application.Quit();
    }
    //Waits for animtion to play before turning on
    IEnumerator WaitBack()
    {
        yield return new WaitForSeconds(1);
        if (UI == true)
        {
            SpeachUI.SetActive(true);
        }
        else
        {
            CloudUI.SetActive(true);
        }
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
        choosen = true; // deletes clouds
        sab = true; // gives chance to sabatoge
    }
    //Has a chance to pick a slot to sabatoge
    public void SlotChooseSab()
    {
        if (sab == true)
        {
            int chance;
            Random random = new Random();
            chance = random.Next(0, wordletters.Length);
            if (slotList[chance].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text != " ")
            {
                Sabatoge(slotList[chance]);
            }
            sab = false;
        }
    }
    // Uses different sabatogues based on how many words are Spelled
    public void Sabatoge(GameObject slot)
    {
        if (Spelled <= 10)
        {
            switch (slot.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text)
            {
                case "W":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "M":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "P":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "B":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "U":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "V":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "T":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "I":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "O":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "Q":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "F":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "E":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "Z":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
                case "S":
                    slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().flipLetters();
                    slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
                    if (tutoral == true)
                    {
                        SlotTtext.SetActive(true);
                        SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the flipped letter out of the cloud!";
                    }
                    break;
            }
        }
        else if (Spelled > 10 && Spelled <= 20) 
        {
            slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().floatLetters();
            slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
            if (tutoral == true)
            {
                SlotTtext.SetActive(true);
                SlotTtext.GetComponent<TextMeshProUGUI>().text = "Quick! Drag the floating letter back down!";
            }
        }
        else
        {
            slot.transform.GetChild(1).gameObject.GetComponent<SlotCode>().dispearLetters();
            slot.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("BadBlink");
            if (tutoral == true)
            {
                SlotTtext.SetActive(true);
                SlotTtext.GetComponent<TextMeshProUGUI>().text = "Be on the look out for diseparing letters! Nothing can stop them!";
                tutoral = false;
            }
        }
    }
    //choose the frist blank slot
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
    //clears the slots and sets them all to active for the next round
    public void ResetSlots()
    {
        for (int i = 0; i < slotList.Length; i++)
        {
            slotList[i].SetActive(true);
            slotList[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = " ";
        }
    }
    //Used for combining all Letters of slots into one word
    private void CombineSlots()
    {
        for (int i = 0; i < wordletters.Length; i++)
        {
            combine += slotList[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;
        }
    }
    #endregion
    #region Clouds
    public void CloudSpawn()
    {
        if (spawning == true && Clouds.Count == 0) 
        {
            choosen = false;
            CurrentSlot();//called to inform what letter clouds need to be spawning and what slot it's connected to
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
    private void TempStop()
    {
        int slotnum = 0;
        for (int i = 0; i < wordletters.Length; i++)
        {
            if (slotList[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text != " " && slotList[i].activeInHierarchy == true)
            {
                slotnum++;
            }
        }
        if (slotnum == wordletters.Length)
        {
            choosen = true;
            spawning = false;
        }
        else
        {
            choosen = false;
            spawning = true;
        }
    }
    public void CloudKill()
    {
        if(Clouds.Count != 0)
        {
            Destroy(Clouds[0]);
            Clouds.RemoveAt(0);
        }
    }
    IEnumerator WaitAnime()
    {
        yield return new WaitForSeconds(1);
        CloudSpawn();
        TimerS = true;
    }
    #endregion
    private void SetTimer()
    {
        if (Spelled <= 10)
        {
            Timer = 120;
        }
        else if (Spelled > 10 && Spelled <= 20)
        {
            Timer = 90;
            tutoral = true;
        }
        else if (Spelled > 20)
        {
            Timer = 60;
            tutoral = true;
        }
    }
}
