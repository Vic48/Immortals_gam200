using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour
{
    public GameObject settingMenu;

    // Start is called before the first frame update
    void Start()
    {
        Button mainMenuBtn = gameObject.transform.Find("MainMenuBtn").gameObject.GetComponent<Button>();
        mainMenuBtn.onClick.AddListener(mainMenuOnclick);

        Button settingBtn = gameObject.transform.Find("SettingsBtn").gameObject.GetComponent<Button>();
        settingBtn.onClick.AddListener(settingBtnOnclick);

        Button resumeBtn = gameObject.transform.Find("ResumeBtn").gameObject.GetComponent<Button>();
        resumeBtn.onClick.AddListener(resumeBtnOnclick);

        Button quitBtn = gameObject.transform.Find("QuitBtn").gameObject.GetComponent<Button>();
        quitBtn.onClick.AddListener(quitBtnOnclick);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void mainMenuOnclick() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    void settingBtnOnclick()
    {
        // open setting menu
        gameObject.SetActive(false);
        settingMenu.SetActive(true);
    }

    void resumeBtnOnclick()
    {
        gameObject.SetActive(false);
        gameControl.Instance.setPauseToggle(false);
        Time.timeScale = 1;
    }

    void quitBtnOnclick()
    {
        Application.Quit();
    }

    public void show() 
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        gameControl.Instance.setPauseToggle(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        gameControl.Instance.setPauseToggle(false);
    }
}
