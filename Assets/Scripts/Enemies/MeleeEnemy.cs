using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Stats")]
    [SerializeField] private int damage;
    [SerializeField] private float attackRate;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Attack Box")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float rangeX;
    [SerializeField] private float rangeY;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float colliderHeight;

    [Header("Attacking Layer")]
    [SerializeField] private LayerMask playerLayer;

    // References
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyAI enemyAI;
    private Player player;

    [HideInInspector] public bool canAttack = true;

    private bool isAttacking;


    // Update is called once per frame
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        animator.SetBool("PlayerInRange", PlayerInSight());

        if (PlayerInSight() && !player.isDead && canAttack)
        {
            if (cooldownTimer >= attackRate)
            {
                // Attack && Animate
                ResetAttackCooldown();
                animator.SetTrigger("Attack");
                StartAttack();
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + (transform.right * rangeX * transform.localScale.x * colliderDistance) + (transform.up * rangeY * transform.localScale.y * colliderHeight),
            new Vector3(boxCollider.bounds.size.x * rangeX, boxCollider.bounds.size.y * rangeY, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            player = hit.transform.GetComponent<Player>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + (transform.right * rangeX * transform.localScale.x * colliderDistance) + (transform.up * rangeY * transform.localScale.y * colliderHeight),
            new Vector3(boxCollider.bounds.size.x * rangeX, boxCollider.bounds.size.y * rangeY, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        // Damage player if in sight
        if (PlayerInSight())
        {
            player.TakeDamage(damage);
        }
    }

    public void ResetAttackCooldown()
    {
        this.cooldownTimer = 0;
    }

    public void StartFollow()
    {
        enemyAI.followEnabled = true;
    }
    public void StopFollow()
    {
        enemyAI.followEnabled = false;
    }

    public void StartAttack()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", isAttacking);
    }
    public void FinishAttack()
    {
        isAttacking = false;
        animator.SetBool("IsAttacking", isAttacking);
    }
}
