using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;               // Assign this in Inspector
    public float followDistance = 20f;     // Detection range
    public float stopAngle = 60f;          // Player's view cone angle
    public float updateRate = 0.3f;        // Path update interval

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateRate)
        {
            timer = 0f;

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= followDistance && !PlayerIsLookingAtEnemy())
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath(); // Stop moving
            }
        }
    }

    bool PlayerIsLookingAtEnemy()
    {
        Vector3 dirToEnemy = (transform.position - player.position).normalized;
        float angle = Vector3.Angle(player.forward, dirToEnemy);
        return angle < stopAngle;
    }
}
