using UnityEngine;
using UnityEngine.AI;

public class MonsterPatrol : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 12f;
    public float attackDistance = 1.5f;

    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseDistance)
        {
            agent.SetDestination(player.position);

            if (animator != null)
                animator.SetBool("isMoving", true);
        }
        else
        {
            agent.ResetPath();

            if (animator != null)
                animator.SetBool("isMoving", false);
        }

        if (distance <= attackDistance)
        {
            Debug.Log("PLAYER CAUGHT");
            // nanti bisa game over di sini
        }
    }
}