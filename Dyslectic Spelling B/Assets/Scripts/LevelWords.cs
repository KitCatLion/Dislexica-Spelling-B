using UnityEngine;
[CreateAssetMenu(fileName = "levelWords", menuName = "Custom/levelWords")]
public class LevelWords : ScriptableObject
{
    [SerializeField]
    private Object[] word;
    public Object[] GetWord()
    {
        { return word; }
    }
}
