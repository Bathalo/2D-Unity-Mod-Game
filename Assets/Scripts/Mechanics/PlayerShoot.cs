using UnityEngine;
using Platformer.Mechanics;

// Shooting Control

public class PlayerShoot: MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] fireballs;

    public PlayerController playerController;
    private Animator animator;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerController.CanShoot())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        cooldownTimer = 0;

        float playerDirection = (playerController.spriteRenderer.flipX) ? -1f : 1f;
        fireballs[FindFireball()].transform.position = FirePoint.position;
        fireballs[FindFireball()].GetComponent<BulletController>().SetDirection(playerDirection);
    }


    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}