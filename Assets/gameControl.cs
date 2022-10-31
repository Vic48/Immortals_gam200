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
    public enum playerName
    {
        LvDongbin,
        HeXiangu
    };
    public playerName defaultPlayer = playerName.LvDongbin;
    public playerName currentPlayer;

    // hp for two player
    private Dictionary<playerName, int> playerHP;

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
    }

    private void swithPlayer(playerName name) 
    {
        if (name == playerName.LvDongbin)
        {
            this.playerScript.SetHealth(playerHP[playerName.LvDongbin]);
            this.playerScript.SetAvatar(playerName.LvDongbin);
            // update Lv Dongbin position
            this.LvDongbin.transform.localPosition = this.HeXiangu.transform.localPosition;
            this.LvDongbin.SetActive(true);
            this.HeXiangu.SetActive(false);

            this.currentPlayer = playerName.LvDongbin;
        }
        else 
        {
            this.playerScript.SetHealth(playerHP[playerName.HeXiangu]);
            this.playerScript.SetAvatar(playerName.HeXiangu);
            // update He Xiangu position
            this.HeXiangu.transform.localPosition = this.LvDongbin.transform.localPosition;
            this.LvDongbin.SetActive(false);
            this.HeXiangu.SetActive(true);

            this.currentPlayer = playerName.HeXiangu;
        }
    }
}
