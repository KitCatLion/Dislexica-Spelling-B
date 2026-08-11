using System;
using TMPro;
using UnityEngine;

public class ButtonCode : MonoBehaviour
{
    public static float speed = 55f;
    public void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0, Space.World);
        if (LetterManager.UI == true)// fail safe incase there are still clouds during speaking
        {
            if (LetterManager.Clouds.Count != 0)
            {
                LetterManager.spawning = false;
                Death();
            }
            Destroy(gameObject);
        }
    }
    public void Clicking()
    {
        LetterManager.AddtoSlot(transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text);
    }
    private void Death()
    {
        LetterManager.Clouds.RemoveAt(0);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cloud"))
        {
            Death();
        }
    }
    public void Blur()
    {
        transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(0f,0f,0f, 0.25f);
    }
}
