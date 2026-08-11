using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject cameraMove;
    public GameObject titlegp;
    public GameObject settinggp;
    public GameObject creditgp;
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
    //go to settings
    public void SettingsMenu()
    {
        cameraMove.GetComponent<Animator>().Play("SettingsCameraMovement");
        titlegp.SetActive(false);
        settinggp.SetActive(true);
    }
    //go to menu
    public void BackSettingsMenu()
    {
        cameraMove.GetComponent<Animator>().Play("RSettingsCameraMovement");
        titlegp.SetActive(true);
        settinggp.SetActive(false);
    }
    public void CreditMenu()
    {
        cameraMove.GetComponent<Animator>().Play("CreditsCameraMovement");
        titlegp.SetActive(false);
        creditgp.SetActive(true);
    }
    public void BackCreditMenu()
    {
        cameraMove.GetComponent<Animator>().Play("RCreditsCameraMovement");
        titlegp.SetActive(true);
        creditgp.SetActive(false);
    }
    //start game
    public void BeginMenu()
    {
        SceneManager.LoadScene(1);
    }
    //quit game
    public void QuitMenu()
    {
        Application.Quit();
    }
}
