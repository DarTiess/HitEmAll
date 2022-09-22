using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private AnimatorController animator;
    private bool move = false;
    private Transform target;
    [SerializeField] private float walkSpeed;

   [SerializeField] private StatesPoints states;
    [SerializeField] private float timerToMove;
    private float time;
    [SerializeField] private float distanceToChangeGoal;
   
    private int indexState;
    private Points _statesPoint;
    [HideInInspector] public bool last;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<AnimatorController>();
        navMesh.speed = walkSpeed;
        time = timerToMove;
        GameManager.Instance.IsGaming += PlayGame;
        GameManager.Instance.OnMove += CanMove;
        GameManager.Instance.OnHit += RotateToEnemy;
    }

    // Update is called once per frame
    void Update()
    {

        if (!move) { return; }
        GetNextTarget();

        if (!target) return;
        if (navMesh == null)
        {
            navMesh = GetComponent<NavMeshAgent>();
            navMesh.speed = walkSpeed;
        }
        navMesh.SetDestination(target.position);

        return;
    }

    private void LateUpdate()
    {
        animator.MoveAnimation(navMesh.velocity.magnitude / navMesh.speed);
    }

    private void PlayGame()
    {
        move = true;
    }

    protected void GetNextTarget()
    {
        if (!last)
        {
            time += Time.deltaTime;
            if (time < timerToMove) return;
            time = 0;
            navMesh.isStopped = false;

            _statesPoint = states.GetState(indexState);

            target = _statesPoint.Transform;
            last = _statesPoint.isLast;
            if (Vector3.Distance(transform.position, target.position) < distanceToChangeGoal)
            {
                indexState += 1;
                move = false;
                navMesh.isStopped = true;
                RotateToEnemy();
               
                //поворот к врагу/врагам
                //прицел на врага
                GameManager.Instance.Fire();
                // по тапу fire
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.position) < distanceToChangeGoal)
            {
                move = false;
                GameManager.Instance.RestartDisplay();
            }

        }
    }

    void RotateToEnemy()
    {

        Transform enemyPos = EnemySpawner.Instance.EnemyForward();
        var look = Quaternion.LookRotation(new Vector3(enemyPos.transform.position.x, transform.position.y, enemyPos.transform.position.z) - transform.position);

        transform.DOLocalRotateQuaternion(look, 0.5f);
    }

    void CanMove()
    {
        StartCoroutine(ContinueMove());
    }
    IEnumerator ContinueMove()
    {
        yield return new WaitForSeconds(2f);
        move = true;
        navMesh.isStopped = false;
    }
}
