using System.Security.Cryptography;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    private bool isDragged = false;
    private Vector3 TextStartPos;

    private bool touchingMouse;
    public GameObject SlotTtext;
    private void Update()
    {
        if ((Input.mousePosition - transform.position).magnitude < 20)
        {
            touchingMouse = true;
        }
        else
        {
            touchingMouse = false;
        }

        if (touchingMouse && Input.GetMouseButtonDown(0))
        {
            isDragged = true;
            TextStartPos = transform.position;

        }

        if (isDragged)
        {
            transform.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && isDragged)
        {
            isDragged = false;
            transform.GetComponent<SlotCode>().floating = false;
            transform.position = transform.GetComponent<SlotCode>().TextPos;
            SlotTtext.SetActive(false);
        }
    }

}
