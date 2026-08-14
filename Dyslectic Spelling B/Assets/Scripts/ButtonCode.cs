using System;
using System.Collections;
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
            StartCoroutine(Animtion());
        }
    }
    public void Blur()
    {
        transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(0f,0f,0f, 0.25f);
    }
    public void Deathref()
    {
        StartCoroutine(Animtion());
    }
    IEnumerator Animtion()
    {
        transform.GetChild(0).gameObject.GetComponent<Animator>().Play("CloudPop");
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Death();
    }
}
