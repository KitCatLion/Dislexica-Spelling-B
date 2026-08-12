using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    public GameObject cameraMove;
    public GameObject titlegp;
    public GameObject settinggp;
    public GameObject creditgp;
    private AudioMixer mainAudio;
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
    public void SetFullscreen(bool full)
    {
        Screen.fullScreen = full;
    }
    public void SetVolume(float vol)
    {
        mainAudio.SetFloat("volume", vol);
    }
    //go to menu
    public void BackSettingsMenu()
    {
        cameraMove.GetComponent<Animator>().Play("RSettingsCameraMovement");
        titlegp.SetActive(true);
        settinggp.SetActive(false);
    }
    //go to credits
    public void CreditMenu()
    {
        cameraMove.GetComponent<Animator>().Play("CreditsCameraMovement");
        titlegp.SetActive(false);
        creditgp.SetActive(true);
    }
    //back to menu from credits
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
