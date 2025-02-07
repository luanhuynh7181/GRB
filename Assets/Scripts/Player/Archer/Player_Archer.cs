using Unity.VisualScripting;
using UnityEngine;

public class Player_Archer : MonoBehaviour
{
    private Animator anim;
    private Vector2 dir = new Vector2(1, 0);

    [SerializeField]
    private Transform launchPoint;

    private float timer;

    public void OnArrowCollision(Collision2D collision)
    {
        collision
            .gameObject.GetComponent<Enermy_Controller>()
            ?.PlayerAttack(PlayerStat.Ins.archerDame, transform);
    }

    public void OnShootFinish()
    {
        anim.SetBool("isShooting", false);
    }

    public void OnEnable()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.4f, 0.66f);
        anim.SetBool("isAttacking", false);
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            dir = new Vector2(horizontal, vertical).normalized;
            anim.SetFloat("aimX", dir.x);
            anim.SetFloat("aimY", dir.y);
        }
    }

    private void Shoot(string debug = "")
    {
        if (timer > 0)
            return;
        timer = PlayerStat.Ins.archerCooldown;

        GameObject obj = ObjectPoolManager.Instance.GetFromPool(GameConstants.POOL_TYPE.ARROW);
        obj.transform.position = launchPoint.position;
        Arrow arrow = obj.GetComponent<Arrow>();
        Layer_Collision layerCollision =
            gameObject.layer == (int)Layer_Collision.Player_Ground
                ? Layer_Collision.Player_Weapon_Ground
                : Layer_Collision.Player_Weapon_Mountain;
        float ratioSpeed = GetComponent<Player_Movement>().GetRatioSpeed();
        arrow.Init(
            dir,
            OnArrowCollision,
            (int)layerCollision,
            GetComponent<SpriteRenderer>(),
            ratioSpeed
        );
    }

    void Update()
    {
        timer -= Time.deltaTime;

        HandleAiming();
        if (InputHandler.GetInstance().IsAttack() && timer < 0)
        {
            anim.SetBool("isShooting", true);
        }
    }
}
