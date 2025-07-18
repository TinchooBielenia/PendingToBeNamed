using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float minDistance = 3f;      
    public float desiredDistance = 5f; 
    public float maxDistance = 10f;     
    public NavMeshAgent agent;
    public Animator animator;
    public bool allowAIControl = true;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.isStopped = false;
        animator.SetBool("IsWalking", true);
    }

    void Update()
    {
        if (!allowAIControl) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > maxDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("IsWalking", true);
        }
        else if (distance < minDistance)
        {
            Vector3 dirAway = (transform.position - player.position).normalized;
            Vector3 newPos = transform.position + dirAway * (minDistance - distance + 1f);
            agent.isStopped = false;
            agent.SetDestination(newPos);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("IsWalking", false);
        }
    }
    //void lateupdate()
    //{
    //    vector3 euler = transform.eulerangles;
    //    euler.x = 0;
    //    euler.z = 0;
    //    transform.eulerangles = euler;
    //}
}
