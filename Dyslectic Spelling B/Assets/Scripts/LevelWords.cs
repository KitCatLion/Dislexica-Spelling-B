using UnityEngine;
[CreateAssetMenu(fileName = "levelWords", menuName = "Custom/levelWords")]
public class LevelWords : ScriptableObject
{
    [SerializeField]
    public Words[] word;
    public Words[] GetWord()
    {
        { return word; }
    }
}
