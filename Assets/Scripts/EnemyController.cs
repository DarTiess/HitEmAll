using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
     private Animator animator;
   
    [SerializeField] private int health;
    Rigidbody[] rigidbodies;
   [HideInInspector]public bool onRagdoll;
    GameObject player;

    [SerializeField] private HeathBar heathBar;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        SwitchRagdoll(true);
        player = GameObject.Find("CharMain");
        heathBar.SetMaxValus(health);
    }

    void SwitchRagdoll(bool onAnimator)
    {
        onRagdoll = !onAnimator;
        foreach(Rigidbody rbBodies in rigidbodies)
        {
            rbBodies.isKinematic = onAnimator;
        }
        animator.enabled = onAnimator;

        if (onAnimator)
        {
            RandomAnimation();
        }
    }
    void SwitchRagdoll(bool onAnimator, Rigidbody partBody)
    {
        onRagdoll = !onAnimator;
        partBody.isKinematic = onAnimator;
        Vector3 force = partBody.transform.up * 350f;
        partBody.velocity = force;
        animator.enabled = onAnimator;

        if (onAnimator)
        {
            RandomAnimation();
        }
    }

    IEnumerator GetUp()
    {
        yield return new WaitForSeconds(1.5f);
        SwitchRagdoll(true);
    }

    void RandomAnimation()
    {
        int rndAnim = Random.Range(0, 2);

    }
   public void TakeDamage(Rigidbody partBody)
    {
        heathBar.SetOnSlider();
        if (health <= 0)
        {
            return;
        }
        health=health-1;
        heathBar.SetValues(1f, 0.5f);
        RotateToPlayer();
        
        if (health <= 0)
        {
            SwitchRagdoll(false);
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
