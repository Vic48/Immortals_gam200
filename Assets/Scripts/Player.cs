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

    public Vector2 position;

    public int maxHealth = 100;
    public int currentHealth;

    // dead related
    public bool isDead = false;
    Vector3 deadPosition;
    [SerializeField]
    public float deadBodyDestroyTime;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        this.position = gameObject.transform.position;
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
    //void Die()
    //{
    //    this.isDead = true;

    //    //die animation
    //    anim.SetBool("IsDead", true);

    //    //Disable the enemy collision
    //    GetComponent<Collider2D>().enabled = false;

    //    // record dead position
    //    this.deadPosition = this.gameObject.transform.position;


    //    // Destry dead body
    //    StartCoroutine(playerDieDestory());
    //}
    //IEnumerator playerDieDestory()
    //{
    //    yield return new WaitForSeconds(deadBodyDestroyTime);
    //    // Destroy enemy dead body after 3s
    //    this.gameObject.SetActive(false);
    //    this.enabled = false;
    //    Destroy(this);
    //}
}
