using System;
using TMPro;
using UnityEngine;

public class ButtonCode : MonoBehaviour
{
    private float speed = 40f;
    public void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0, Space.World);
        Death();
    }
    public void Clicking()
    {
        LetterManager.AddtoSlot(transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text);
    }
    private void Death()
    {
        if (transform.position.y > 2000)
        {
            Destroy(LetterManager.Clouds[0]);
            LetterManager.Clouds.RemoveAt(0);
        }
    }
}
