using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject cameraMove;
    public GameObject titlegp;
    public GameObject settinggp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titlegp.SetActive(true);
        settinggp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SettingsMenu()
    {
        cameraMove.GetComponent<Animator>().Play("SettingsCameraMovement");
        titlegp.SetActive(false);
        settinggp.SetActive(true);
    }
    public void BackSettingsMenu()
    {
        cameraMove.GetComponent<Animator>().Play("RSettingsCameraMovement");
        titlegp.SetActive(true);
        settinggp.SetActive(false);
    }
}
