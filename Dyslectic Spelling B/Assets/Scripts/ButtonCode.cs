using System;
using TMPro;
using UnityEngine;

public class ButtonCode : MonoBehaviour
{
    private float speed = 40f;
    public void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0, Space.World);
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
