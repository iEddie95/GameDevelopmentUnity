using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Catapult : MonoBehaviour, INpc
{
    private Animator animator;
    private NavMeshAgent navAgent;

    public GameObject ball;
    private bool alive = true;
    public int hp = 100;
    private Collider[] withinAggroColliders;
    public GameObject leader;
    public LayerMask aggroLayerMask;
    private Combat combatEnemy;
    private Rigidbody ballRG;
    private GameObject ballFire;
    private Rigidbody ballFireRG;
    public GameObject aCamera;
    public GameObject destroyedObject;
    private AudioSource sound;

    public Text npcDieText;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ballRG = ball.GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();

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
                withinAggroColliders = Physics.OverlapSphere(transform.position, 60f, aggroLayerMask);
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

    public void ChasePlayer(Combat enemy)
    {
        this.combatEnemy = enemy;

        if (combatEnemy != null && combatEnemy.IsAlive())
        {

            navAgent.SetDestination(combatEnemy.transform.position);
            animator.SetBool("isWalk", true);

            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {

                animator.SetBool("isWalk", false);
                FaceTarget(combatEnemy);
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

    public void Die()
    {
        //animator.SetInteger("Status", 5);
        alive = false;
        navAgent.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Dead");
        StartCoroutine(ShowText());
        GetComponent<BoxCollider>().enabled = false;
        if (IsInvoking("PerformAttack"))
            CancelInvoke("PerformAttack");

        GameObject destoryed = Instantiate(destroyedObject, transform.position, destroyedObject.transform.rotation);
        gameObject.SetActive(false);

    }

    //public void TakeDamage(int amount)
    //{
    //    hp -= amount;
    //    if (hp <= 0)
    //        Die();
    //}

    public void PerformAttack()
    {
        sound.Play();
        animator.SetTrigger("Base_Attack");
        ballFire = Instantiate(ball, ball.transform.position, ball.transform.rotation, this.transform);
        ballFire.SetActive(true);
        ballFire.GetComponent<CatapultStone>().enemytag = this.gameObject.CompareTag("Enemy") ? "Player" : "Enemy";

        Vector3 direction = aCamera.transform.forward;
        //direction.y = 1;
        ballFireRG = ballFire.GetComponent<Rigidbody>();
        ballFireRG.AddForce(40f * direction, ForceMode.VelocityChange); // 10* is the power of throwing
        //ballFireRG.velocity = direction * 30f;
        ballFireRG.useGravity = true;

    }

    private void FaceTarget(Combat enemy)
    {
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public bool IsAlive()
    {
        return alive;
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

