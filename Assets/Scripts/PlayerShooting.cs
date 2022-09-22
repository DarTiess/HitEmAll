using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerShooting : MonoBehaviour
{
    public static PlayerShooting Instance;
    [SerializeField] private Transform firePlace;
    [SerializeField] private List<GameObject> bulletsList;
    bool canShoot;
    [SerializeField] private float hitStrength;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnFire += StartFire;
        GameManager.Instance.OnMove += StopFire;
       
    }

    void StartFire()
    {
        canShoot = true;
    }

    void StopFire()
    {
        canShoot = false;
    }

    public void CreateBall()
    {
        if (canShoot)
        {
            PushBall();
        }

    }

    public void FireBall()
    {
        if (canShoot)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100000f))
            {
                fireBall(hit.point);
            }
            else
            {
                fireBall();
            }

            Debug.DrawLine(ray.origin, hit.point, Color.red);

        }
    }
    int indexBullet = 0;
    GameObject currentBall;
    void PushBall()
    {
        if (indexBullet >= bulletsList.Count)
        {
            indexBullet = 0;
        }
          currentBall =bulletsList[indexBullet];
        currentBall.tag = "Fire";

        currentBall.transform.position = firePlace.position;
       currentBall.gameObject.SetActive(true);
       indexBullet++;
    }

    void fireBall()
    {
        if (!currentBall) { return; }

        Vector3 force = firePlace.forward * hitStrength;
        currentBall.GetComponent<Rigidbody>().velocity = force;

        currentBall.transform.position = firePlace.position;
    
    }
    void fireBall(Vector3 diference)
    {
        if (!currentBall) { return; }

        float direction = Vector3.Distance(currentBall.transform.position, diference);
        
        currentBall.GetComponent<Rigidbody>().DOMove(diference, direction / 20f);
       
    }
}
