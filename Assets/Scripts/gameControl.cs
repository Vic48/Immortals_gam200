using System.Collections.Generic;
using UnityEngine;

// This is game manager, control the whole game!
public class gameControl : MonoBehaviour
{
    public static gameControl Instance { get; private set; }
    public GameObject player;
    public GameObject LvDongbin;
    public GameObject HeXiangu;
    public Player playerScript;

    // dead related
    public bool isHeDead = false;
    public bool isLvDead = false;

    public enum playerName
    {
        LvDongbin,
        HeXiangu
    };
    public playerName defaultPlayer = playerName.LvDongbin;
    public playerName currentPlayer;

    // hp for two player
    private Dictionary<playerName, int> playerHP;

    // pause menu related
    private bool pauseToggle = false;
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    private CameraZoom camZoom;
    public bool gameOver = false;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // start with Lv Donbin
        this.currentPlayer = playerName.LvDongbin;
        // initial both player HP
        this.playerHP = new Dictionary<playerName, int>();
        int maxHP = this.playerScript.maxHealth;
        this.playerHP.Add(playerName.LvDongbin, maxHP);
        this.playerHP.Add(playerName.HeXiangu, maxHP);

        // enable default player
        if (this.defaultPlayer == playerName.LvDongbin)
        {
            this.LvDongbin.SetActive(true);
            this.HeXiangu.SetActive(false);
        }
        else 
        {
            this.LvDongbin.SetActive(false);
            this.HeXiangu.SetActive(true);
        }

        // find the camera zoom script
        camZoom = GameObject.Find("Main Camera/Zoom").GetComponent<CameraZoom>();
    }

    // Update is called once per frame
    private void Update()
    {
        // on press e, switch player
        if (Input.GetKeyDown("e"))
        {
            if (this.currentPlayer == playerName.LvDongbin)
            {
                // store Lv Donbin hp
                this.playerHP[playerName.LvDongbin] = playerScript.currentHealth;
                // switch to He Xiangu
                this.swithPlayer(playerName.HeXiangu);
            }
            else 
            {
                // store He Xiangu hp
                this.playerHP[playerName.HeXiangu] = playerScript.currentHealth;
                // switch to Lv Donbin
                this.swithPlayer(playerName.LvDongbin);
            }
        }
        // on press esc, open pause menu
        if (!camZoom.isZoomActive && Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseToggle)
            {
                pauseMenu.GetComponent<pauseMenu>().hide();
            }
            else
            {
                pauseMenu.GetComponent<pauseMenu>().show();
            }
        }

        // check if HP <= 0
        if (this.playerScript.currentHealth <= 0) 
        {
            if (this.currentPlayer == playerName.LvDongbin)
            {
                this.isLvDead = true;
            }
            else
            {
                this.isHeDead = true;
            }
            this.playerScript.Die(this.currentPlayer);
        }

        // both dead
        if (isLvDead && isHeDead) 
        {
            // game over
            this.gameOver = true;
            gameOverScreen.SetActive(true);
        }
    }

    public void swithPlayer(playerName name) 
    {
        if (name == playerName.LvDongbin && !isLvDead)
        {
            this.playerScript.SetHealth(playerHP[playerName.LvDongbin]);
            this.playerScript.SetAvatar(playerName.LvDongbin);
            // update Lv Dongbin position
            if (this.HeXiangu != null)
            {
                this.LvDongbin.transform.localPosition = this.HeXiangu.transform.localPosition;
                this.HeXiangu.SetActive(false);
            }
            this.LvDongbin.SetActive(true);

            this.currentPlayer = playerName.LvDongbin;
        }
        else if (name == playerName.HeXiangu && !isHeDead)
        {
            this.playerScript.SetHealth(playerHP[playerName.HeXiangu]);
            this.playerScript.SetAvatar(playerName.HeXiangu);
            // update He Xiangu position
            if (this.LvDongbin != null)
            {
                this.HeXiangu.transform.localPosition = this.LvDongbin.transform.localPosition;
                this.LvDongbin.SetActive(false);
            }
            this.HeXiangu.SetActive(true);

            this.currentPlayer = playerName.HeXiangu;
        }
    }

    public bool getIsPlayerDead()
    {
        if (this.currentPlayer == playerName.LvDongbin)
        {
            return this.isLvDead;
        }
        else
        {
            return this.isHeDead;
        }
    }

    public void setPauseToggle(bool isPauseToggle) 
    {
        this.pauseToggle = isPauseToggle;
    }
}
