using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    public HealthBar healthBar;
    [SerializeField]
    public Image healthBarAvatar;
    // animation
    public Animator LvAnim;
    public Animator HeAnim;

    // Icon is the small avatar beside HP bar
    [SerializeField]
    public Sprite LvDonbinIcon;
    [SerializeField]
    public Sprite HeXianguIcon;

    public GameObject LvDonbinObject;
    public GameObject HeXianguObject;

    public Vector2 position;

    public int maxHealth = 100;
    public int currentHealth;

    [SerializeField]
    public float deadBodyDestroyTime;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        this.position = gameObject.transform.position;
    }

    private void Update()
    {}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        healthBar.SetHealth(currentHealth);
    }

    public void SetAvatar(gameControl.playerName name)
    {
        if (name == gameControl.playerName.LvDongbin)
        {
            healthBarAvatar.sprite = LvDonbinIcon;
        }
        else
        {
            healthBarAvatar.sprite = HeXianguIcon;
        }
    }

    public void Die(gameControl.playerName playerName)
    {
        //die animation
        if (playerName == gameControl.playerName.LvDongbin)
        {
            LvAnim.SetBool("LvDead", true);
            //Disable the collision
            LvDonbinObject.GetComponent<Collider2D>().enabled = false;
            LvDonbinObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            // any force cannot move the dead body
            LvDonbinObject.GetComponent<Rigidbody2D>().drag = 100000;
            LvDonbinObject.GetComponent<Rigidbody2D>().mass = 100000;

            // set Avatar gray when Lv Donbin die
            // healthBarAvatar.sprite = LvDonbinGrayIcon;
        }
        else
        {
            HeAnim.SetBool("HeDead", true);
            // Disable the collision
            HeXianguObject.GetComponent<Collider2D>().enabled = false;
            HeXianguObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            // any force cannot move the dead body
            HeXianguObject.GetComponent<Rigidbody2D>().drag = 100000;
            HeXianguObject.GetComponent<Rigidbody2D>().mass = 100000;

            // set Avatar gray when He Xiangu die
            // healthBarAvatar.sprite = HeXianguGrayIcon;
        }

        // Destry dead body
        StartCoroutine(playerDieDestory(playerName));
    }
    IEnumerator playerDieDestory(gameControl.playerName playerName)
    {
        yield return new WaitForSeconds(deadBodyDestroyTime);
        // Destroy enemy dead body after 3s
        if (playerName == gameControl.playerName.LvDongbin)
        {
            gameControl.Instance.swithPlayer(gameControl.playerName.HeXiangu);
            Destroy(LvDonbinObject, .5f);
        }
        else
        {
            gameControl.Instance.swithPlayer(gameControl.playerName.LvDongbin);
            Destroy(HeXianguObject, .5f);
        }
        StopCoroutine(playerDieDestory(playerName));
    }
}
