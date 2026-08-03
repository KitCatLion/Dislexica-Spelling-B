using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SlotCode : MonoBehaviour
{
    TextMeshProUGUI Letter;
    private Vector3 TextStartPos;
    private float speed = 30f;
    public bool floating = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextStartPos = transform.position;
        Letter = transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (floating == true)
        {
            transform.Translate(0, speed * Time.deltaTime, 0, Space.World);
        }
    }
    public void floatLetters()
    {
        floating = true;
    }
    public void flipLetters()
    {
        switch (Letter.text)
        {
            case "W":
                Letter.text = "M";
                break;
            case "M":
                Letter.text = "W";
                break;
            case "P":
                Letter.text = "B";
                break;
            case "B":
                Letter.text = "P";
                break;
            case "U":
                Letter.text = "V";
                break;
            case "V":
                Letter.text = "U";
                break;
            case "T":
                Letter.text = "I";
                break;
            case "I":
                Letter.text = "T";
                break;
            case "O":
                Letter.text = "Q";
                break;
            case "Q":
                Letter.text = "O";
                break;
            case "F":
                Letter.text = "E";
                break;
            case "E":
                Letter.text = "F";
                break;
            case "Z":
                Letter.text = "S";
                break;
            case "S":
                Letter.text = "Z";
                break;
        }
    }
    public void dispearLetters()
    {
        Letter.text = " ";
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cloud"))
        {
            Letter.text = " ";
            transform.position = TextStartPos;
            floating = false;
        }
    }
}
