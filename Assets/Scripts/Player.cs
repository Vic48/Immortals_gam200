using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    public HealthBar healthBar;
    [SerializeField]
    public Image healthBarAvatar;
    // Icon is the small avatar beside HP bar
    [SerializeField]
    public Sprite LvDonbinIcon;
    [SerializeField]
    public Sprite HeXianguIcon;

    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        //everytime press space bar
        if (Input.GetKeyDown("0"))
        {
            TakeDamage(20);
        }
    }

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
}
