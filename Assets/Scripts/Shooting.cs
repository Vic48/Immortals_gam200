using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] private HeXiangu hXG;
    public Animator anim;


    public GameObject arrow;
    public float launchForce;
    public Transform shotPoint;

    public bool isFlip;

    // shoot arrow rate
    private float nextAttackTime;
    public float shootArrowAnimationTime;

    private void Start()
    {
    }
    private void Update()
    {
        //bow pos as pt A
        Vector2 bowPosition = transform.position;

        //mouse pos as pt B
        //returns the pos of mouse cursor in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //direction bet A and B
        Vector2 Direction = mousePosition - bowPosition;

        isFlip = hXG.GetComponent<SpriteRenderer>().flipX;

        //Make right pos as the direction
        if ((Direction.x >= 0 && isFlip == false) || (Direction.x <= 0 && isFlip == true))
        {
            transform.right = Direction;
        }
        else {
            transform.right = -Direction;
        }

        if (Input.GetMouseButtonDown(0) && (gameControl.Instance.isHeDead == false) && !gameControl.Instance.getPauseToggle())
        {
            if (Time.time > nextAttackTime) {
                shoot();
                nextAttackTime = Time.time + shootArrowAnimationTime;
            }
        }
    }
    void shoot()
    {
        anim.SetTrigger("NormalAttack");
        StartCoroutine(shootArrow());
    }

    IEnumerator shootArrow()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.4f);

        //store new created arrow in an object
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);

        //fetch rb components 
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;

        StopCoroutine(shootArrow());
    }
}
