using GameConstants;
using UnityEngine;

public class Enermy_Movement : MonoBehaviour
{
    protected Animator anim;
    protected float attackTimer = 0f;
    protected bool canAttackFromMountain = false;
    protected float coolDown;
    public Transform player;
    protected float rangeAttack;
    protected float rangeChasing;
    protected Rigidbody2D rb;
    protected float speed;
    protected CHARACTER_STATE state;
    private float timerAppear = 0f;
    private float timerDead = 0f;

    public virtual void Attack() { }

    public void AttackEvent() // Animation event
    {
        Attack();
    }

    public virtual void BeforeUpdate() { }

    public void ChangeState(CHARACTER_STATE newState)
    {
        anim.SetBool(getNameState(state), false);
        state = newState;
        anim.SetBool(getNameState(state), true);
    }

    public void ChangeStateIdleEvent() // Animation event
    {
        ChangeState(CHARACTER_STATE.IDLE);
    }

    public void ChangeStateToDie()
    {
        ChangeState(CHARACTER_STATE.DIE);
        if (!rb)
            return;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;
        gameObject.layer = (int)Layer_Collision.Default;
    }

    public virtual void ChangeVelocity(Vector2 dir)
    {
        rb.linearVelocity = dir;
    }

    public void Chasing()
    {
        if (!IsSameLayer())
            return;

        LookAtPlayer();

        Vector2 dir = Vector2.zero;
        if (Vector2.Distance(player.position, transform.position) > rangeAttack)
        {
            dir = (player.position - transform.position).normalized * speed;
        }
        ChangeVelocity(dir);
    }

    public void CheckForAttack()
    {
        if (!IsSameLayer())
            return;
        if (Vector2.Distance(player.position, transform.position) <= rangeAttack)
        {
            if (attackTimer <= 0)
            {
                attackTimer = coolDown;
                ChangeState(CHARACTER_STATE.ATTACK);
            }
            ChangeVelocity(Vector2.zero);

            return;
        }
        ChangeState(CHARACTER_STATE.CHASE);
    }

    public virtual void CheckUpdate()
    {
        if (CHARACTER_STATE.KNOCK == state)
            return;
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        CheckForChasing();
        if (CHARACTER_STATE.CHASE == state)
        {
            Chasing();
        }
    }

    public void LookAtPlayer()
    {
        if ((player.position.x - transform.position.x) * transform.localScale.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public virtual string getNameState(CHARACTER_STATE state)
    {
        return "isIdle";
    }

    public virtual void Init() { }

    public bool isHiding()
    {
        return state == CHARACTER_STATE.HIDE || state == CHARACTER_STATE.APPEAR;
    }

    public bool IsSameLayer()
    {
        if (!player)
            return false;
        int playerLayer = player.gameObject.layer;
        int enermyLayer = gameObject.layer;
        if (
            playerLayer == (int)Layer_Collision.Player_Ground
            && enermyLayer == (int)Layer_Collision.Enermy_Ground
        )
            return true;
        if (
            playerLayer == (int)Layer_Collision.Player_Mountain
            && enermyLayer == (int)Layer_Collision.Enermy_Mountain
        )
            return true;
        if (canAttackFromMountain)
        {
            if (
                playerLayer == (int)Layer_Collision.Player_Ground
                && enermyLayer == (int)Layer_Collision.Enermy_Mountain
            )
                return true;
        }
        return false;
    }

    private void CheckAppear()
    {
        LayerMask? playerLayer = GetPlayerLayer();
        if (playerLayer == null)
            return;
        CheckAssignPlayer(playerLayer);
        if (!player)
            return;
        if (!IsSameLayer())
            return;

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        ChangeState(CHARACTER_STATE.APPEAR);
        GetComponent<Material_Controller>()?.Change(MATERIAL_TYPE.APPEAR);
    }

    private void CheckAssignPlayer(LayerMask? playerLayer)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            rangeChasing,
            playerLayer.Value
        );
        player = hits.Length > 0 ? hits[0].transform : null;
    }

    private void CheckChangeStateIdle()
    {
        timerAppear += Time.deltaTime;
        if (timerAppear >= EnermiesStat.Ins.timeAppear)
        {
            GetComponent<Enermy_Controller>()?.Active();
            ChangeState(CHARACTER_STATE.IDLE);
        }
    }

    private void CheckForChasing()
    {
        LayerMask? playerLayer = GetPlayerLayer();
        if (playerLayer == null)
            return;
        CheckAssignPlayer(playerLayer);
        if (player != null)
        {
            CheckForAttack();
        }
        else
        {
            ChangeState(CHARACTER_STATE.IDLE);
            ChangeVelocity(Vector2.zero);
        }
    }

    private void Die()
    {
        timerDead += Time.deltaTime;
        if (timerDead >= EnermiesStat.Ins.timeDeath)
        {
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Destroy(gameObject);
            GlobalEventManager.TriggerOnEnermyDead(gameObject);
            return;
        }
    }

    private LayerMask? GetPlayerLayer()
    {
        if (canAttackFromMountain)
        {
            LayerMask mask1 = 1 << (int)Layer_Collision.Player_Mountain;
            LayerMask mask2 = 1 << (int)Layer_Collision.Player_Ground;
            return mask1 | mask2;
        }
        else
        {
            LayerMask? layer = LayerUtils.GetPlayerLayerByEnermy(gameObject);
            if (layer == null)
                return null;
            return 1 << layer.Value;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = CHARACTER_STATE.HIDE;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Init();
    }

    void Update()
    {
        BeforeUpdate();
        if (state == CHARACTER_STATE.HIDE)
        {
            CheckAppear();
            return;
        }
        if (state == CHARACTER_STATE.APPEAR)
        {
            CheckChangeStateIdle();
            return;
        }

        if (state == CHARACTER_STATE.DIE)
        {
            Die();
            return;
        }
        CheckUpdate();
    }
}
