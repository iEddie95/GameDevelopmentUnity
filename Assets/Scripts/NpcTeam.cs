using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcTeam : MonoBehaviour, INpc
{
    public GameObject leader;
    public LayerMask aggroLayerMask;

    public GameObject enemy;
    private Combat combatEnemy;
    private NavMeshAgent navAgent;
    private Animator animator;
    private bool alive = true;
    public int hp = 100;
    private Collider[] withinAggroColliders;
    public GameObject weapon;
    public bool isRanged; // archer
    private AudioSource soundSword;
    public float radius;
    public Text npcDieText;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        soundSword = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.enabled)
        {
            if (!GetComponent<Combat>().IsAlive())
                Die();
            else
            {
                withinAggroColliders = Physics.OverlapSphere(transform.position, radius, aggroLayerMask);
                if (withinAggroColliders.Length > 0)
                {
                    ChasePlayer(withinAggroColliders[0].GetComponent<Combat>());
                }
                else
                {
                    if (IsInvoking("PerformAttack"))
                        CancelInvoke("PerformAttack");
                    FollowLeader();
                }
            }
        }
    }

    public virtual void PerformAttack()
    {
        if (!isRanged)
        {
            combatEnemy.TakeDamage(Random.Range(5, 20));
        }

        else
            NpcArrowAttack();

    }

    public void NpcArrowAttack()
    {
        soundSword.Play();
        GameObject newArrow = Instantiate(weapon, weapon.transform.position, weapon.transform.rotation, transform);
        newArrow.GetComponent<NpcArrow>().enemytag = this.gameObject.CompareTag("Enemy") ? "Player" : "Enemy";
        newArrow.SetActive(true);
        newArrow.GetComponent<BoxCollider>().enabled = true;
        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(50f * transform.forward, ForceMode.VelocityChange);
    }

    public void ChasePlayer(Combat enemy)
    {
        this.combatEnemy = enemy;

        if (combatEnemy != null && combatEnemy.IsAlive())
        {

            navAgent.SetDestination(combatEnemy.transform.position);
            animator.SetBool("isWalk", true);

            //Debug.Log("Team Distanse is " + navAgent.remainingDistance);
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {

                animator.SetBool("isWalk", false);
                FaceTarget(combatEnemy);
                StartCoroutine(Attack());
                if (!IsInvoking("PerformAttack"))
                {
                    InvokeRepeating("PerformAttack", .5f, 2f);
                }

            }
        }
        else
        {
            CancelInvoke("PerformAttack");
            combatEnemy = null;
        }
    }

    void FollowLeader()
    {
        navAgent.SetDestination(leader.transform.position);
        animator.SetBool("isWalk", true);

        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            animator.SetBool("isWalk", false);
        }
    }

    private void FaceTarget(NpcTeam enemy)
    {
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void FaceTarget(Combat enemy)
    {
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void Die()
    {
        animator.SetBool("isWalk", false);
        animator.SetInteger("Status", 5);
        alive = false;
        navAgent.enabled = false;
        StartCoroutine(ShowText());
        gameObject.layer = LayerMask.NameToLayer("Dead");
        GetComponent<BoxCollider>().enabled = false;
        if (IsInvoking("PerformAttack"))
            CancelInvoke("PerformAttack");
    }

    public bool IsAlive()
    {
        return alive;
    }

    IEnumerator Attack()
    {
        if (!isRanged)
        {
            soundSword.Play();
        }
        animator.SetTrigger("Base_Attack");
        yield return new WaitForSeconds(0.01f);

    }

    IEnumerator ShowText()
    {
        npcDieText.gameObject.SetActive(true);
        string team = CompareTag("Enemy") ? "Enemy's Team" : "Your's Team";
        npcDieText.text = team + " soldier is dead";
        yield return new WaitForSeconds(1f);
        npcDieText.gameObject.SetActive(false);
    }
}
