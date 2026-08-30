using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MonsterAI : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource chaseSound;
    public AudioSource jumpscareSound;

    [Header("Target")]
    public Transform player;
    public Transform head;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    public float waitAtPoint = 2f;

    [Header("Detection")]
    public float chaseDistance = 5f;
    public float attackDistance = 1.5f;
    public float maxHeightDifference = 2f;
    public float searchDuration = 3f;

    [Header("Movement")]
    public float patrolSpeed = 2.2f;
    public float chaseSpeed = 3.5f;

    NavMeshAgent agent;
    int patrolIndex;
    float waitTimer;
    float searchTimer;

    bool hasKilledPlayer;
    bool isChasing;
    bool playerHidden;

    Vector3 noiseTarget;
    bool investigatingNoise;

    Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.speed = patrolSpeed;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;

        hasKilledPlayer = false;
        agent.isStopped = false;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (hasKilledPlayer || player == null || !agent.isOnNavMesh) return;

        float distance = Vector3.Distance(transform.position, player.position);
        float heightDiff = Mathf.Abs(transform.position.y - player.position.y);

        if (playerHidden)
        {
            StopChase();
            investigatingNoise = false;
            Patrol();
            UpdateAnimation();
            return;
        }

        // Prevent the monster from detecting or chasing the player across different floors.
        if (heightDiff > maxHeightDifference)
        {
            ResetState();
            Patrol();
            UpdateAnimation();
            return;
        }

        // Chase only when the player is within range and visible.
        if (distance <= chaseDistance && CanSeePlayer())
        {
            StartChase();
        }

        if (isChasing)
        {
            agent.speed = chaseSpeed;
            searchTimer -= Time.deltaTime;

            agent.SetDestination(player.position);

            if (distance <= attackDistance && CanSeePlayer())
            {
                KillPlayer();
                return;
            }

            if (searchTimer <= 0f)
            {
                StopChase();
                GoToNextPatrolPoint();
            }

            UpdateAnimation();
            return;
        }

        if (investigatingNoise)
        {
            agent.SetDestination(noiseTarget);

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                investigatingNoise = false;
            }

            UpdateAnimation();
            return;
        }

        Patrol();
        UpdateAnimation();
    }

    // ================= PATROL =================
    void Patrol()
    {
        agent.speed = patrolSpeed;

        if (patrolPoints.Length == 0)
            return;

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.1f)
                {
                    waitTimer += Time.deltaTime;

                    if (waitTimer >= waitAtPoint)
                    {
                        waitTimer = 0f;
                        GoToNextPatrolPoint();
                    }
                }
            }
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.destination = patrolPoints[patrolIndex].position;

        patrolIndex++;
        if (patrolIndex >= patrolPoints.Length)
            patrolIndex = 0;
    }

    // ================= CHASE =================
    void StartChase()
    {
        if (hasKilledPlayer) return;

        isChasing = true;
        agent.isStopped = false;
        searchTimer = searchDuration;

        if (chaseSound && !chaseSound.isPlaying)
            chaseSound.Play();
    }

    void StopChase()
    {
        isChasing = false;

        if (chaseSound && chaseSound.isPlaying)
            chaseSound.Stop();
    }

    void ResetState()
    {
        StopChase();
        investigatingNoise = false;
    }

    // ================= NOISE =================
    public void HearNoise(Vector3 noisePos)
    {
        if (playerHidden || isChasing) return;

        investigatingNoise = true;
        noiseTarget = noisePos;
        agent.SetDestination(noiseTarget);
    }

    // ================= VISION =================
    bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up;
        Vector3 dir = (player.position - origin).normalized;

        if (Physics.Raycast(origin, dir, out hit, chaseDistance))
        {
            return hit.transform.CompareTag("Player");
        }
        return false;
    }

    // ================= KILL =================
    void KillPlayer()
    {
        if (hasKilledPlayer) return;

        hasKilledPlayer = true;

        agent.isStopped = true;
        agent.ResetPath();

        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0f;

        if (lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        StopChase();

        StartCoroutine(KillRoutine());
    }

    IEnumerator KillRoutine()
    {
        if (chaseSound && chaseSound.isPlaying)
            chaseSound.Stop();

        if (jumpscareSound)
            jumpscareSound.Play();

        Camera cam = Camera.main;

        if (cam == null)
        {
            Debug.LogError("Main Camera not found!");
            PlayerDeath.instance?.Die();
            yield break;
        }

        MouseLook mouseLook = cam.GetComponent<MouseLook>();
        if (mouseLook != null)
            mouseLook.enabled = false;

        Transform camTransform = cam.transform;

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = false;

        float duration = 1f;
        float timer = 0f;

        Vector3 startPos = camTransform.position;

        Vector3 lookTarget = head != null
            ? head.position
            : transform.position + Vector3.up * 1.6f;

        Vector3 dirFromPlayer =
            (transform.position - player.position).normalized;

        Vector3 targetPos =
            lookTarget - dirFromPlayer * 0.8f;

        float startFOV = cam.fieldOfView;
        float targetFOV = 40f;

        while (timer < duration)
        {
            float t = timer / duration;

            camTransform.position =
                Vector3.Lerp(startPos, targetPos, t);

            camTransform.LookAt(lookTarget);

            cam.fieldOfView =
                Mathf.Lerp(startFOV, targetFOV, t);

            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        cam.fieldOfView = startFOV;

        PlayerDeath.instance?.Die();
    }

    // ================= HIDE =================
    public void SetPlayerHidden(bool hidden)
    {
        playerHidden = hidden;

        if (hidden)
        {
            StopChase();
            investigatingNoise = false;
        }
    }

// ================= ANIMATION =================
    void UpdateAnimation()
    {
        if (anim == null || agent == null) return;

        float speed = agent.velocity.magnitude;

        anim.SetFloat("Speed", speed);
        anim.SetBool("IsChasing", isChasing);
    }
}
