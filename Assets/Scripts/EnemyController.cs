using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
     private Animator animator;
    NavMeshAgent navMesh;
    [SerializeField] private int health;
    Rigidbody[] rigidbodies;
   [HideInInspector]public bool onRagdoll;
    GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private HeathBar heathBar;
  
    bool move;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        SwitchRagdoll(true);
        player = GameObject.Find("CharMain");
        heathBar.SetMaxValus(health);
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = speed;
       
    }


    private void Update()
    {
        if (move)
        {
            navMesh.SetDestination(player.transform.position);
        }
    }

    void SwitchRagdoll(bool onAnimator)
    {
        onRagdoll = !onAnimator;
        foreach(Rigidbody rbBodies in rigidbodies)
        {
            rbBodies.isKinematic = onAnimator;
        }
        animator.enabled = onAnimator;

        if (move)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            if (onAnimator)
            {
                RandomAnimation();
            }
        }
      
    }
    void SwitchRagdoll(bool onAnimator, Rigidbody partBody)
    {
        onRagdoll = !onAnimator;
        partBody.isKinematic = onAnimator;
        Vector3 force = partBody.transform.up * 350f;
        partBody.velocity = force;
        animator.enabled = onAnimator;

        animator.SetBool("Move", true);

    }

    IEnumerator GetUp()
    {
        yield return new WaitForSeconds(0.5f);
        SwitchRagdoll(true);
    }

    void RandomAnimation()
    {
        int rndAnim = Random.Range(0, 2);
        animator.SetInteger("numAnim", rndAnim);

    }
   public void TakeDamage(Rigidbody partBody)
    {
        heathBar.SetOnSlider();
        if (health <= 0)
        {
            return;
        }
        health=health-1;
        move = true;
        heathBar.SetValues(1f, 0.5f);
        RotateToPlayer();
        
        if (health <= 0)
        {
            SwitchRagdoll(false);
            navMesh.isStopped = true;
            heathBar.SetOffSlider();
            EnemySpawner.Instance.HitEnemy();
        }
        else
        {
            SwitchRagdoll(false, partBody);
            StartCoroutine(GetUp());
        }
    }

    void RotateToPlayer()
    {
        var look = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);

        transform.DOLocalRotateQuaternion(look, 0.5f);
    }
}
