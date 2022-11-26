using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button restartBtn = gameObject.transform.Find("Restart").gameObject.GetComponent<Button>();
        restartBtn.onClick.AddListener(restartLevel);

        Button backMenuBtn = gameObject.transform.Find("Menu").gameObject.GetComponent<Button>();
        backMenuBtn.onClick.AddListener(backToMainMenu);
    }

    // Update is called once per frame
    void Update()
    {}

    void restartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    void backToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
