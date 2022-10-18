using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DisableBullet());
    }

    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
