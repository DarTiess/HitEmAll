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
                //������� � �����/������
                //������ �� �����
                GameManager.Instance.Fire();
                // �� ���� fire
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.position) < distanceToChangeGoal)
            {
                move = false;
              
            }

        }
    }

    IEnumerator ContinueMove()
    {
        yield return new WaitForSeconds(2f);
        move = true;
        navMesh.isStopped = false;
    }
}
