using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] public EnemyController enemyParent;
    Rigidbody rbBody;
    // Start is called before the first frame update
    void Start()
    {
        rbBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire") && !enemyParent.onRagdoll)
        {
            collision.gameObject.tag = "Untagged";
            enemyParent.TakeDamage(rbBody);
        }
    }
}
