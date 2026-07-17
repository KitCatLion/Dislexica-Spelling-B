using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWord", menuName = "Custom/newWord")]
public class Words : ScriptableObject
{
    [SerializeField]
    private string word;
    [SerializeField]
    private AudioClip wordSound;
    [SerializeField]
    private char[] brokenWord;
    [SerializeField]
    private string pointOfOrgin;
    public string GetWord()
    {
        { return word; }
    }
    public AudioClip GetAudio()
    {
        { return wordSound; }
    }
    public char[] BreakingWord()
    {
        brokenWord = word.ToCharArray();
        return brokenWord;
    }
    public string Origin()
    {
        { return pointOfOrgin; }
    }
    private void OnValidate()
    {
        BreakingWord();
    }
}
