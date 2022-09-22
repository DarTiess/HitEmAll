using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLSpace;
public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
         //  collision.gameObject.GetComponent<RagdollManager>().StartHitReaction(collision.gameObject.GetComponent<RagdollManager>().RagdollBones, 30f * 2f);
        }
    }
}
