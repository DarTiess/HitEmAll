// © 2016 Mario Lelas
using UnityEngine;



namespace MLSpace
{

    public class PlayerShootScript : ShootScript
    {
        public static PlayerShootScript Instance { get; private set; }
        [HideInInspector] public bool canShoot;
        public float maxBallScale;
        public float speedScale;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            GameManager.Instance.OnFire += StartFire;
            GameManager.Instance.OnMove += StopFire;
        }
        // Update is called once per frame
        void LateUpdate()
        {
#if DEBUG_INFO

            if (ProjectilePrefab.Count<=0)
            {
                Debug.LogError("ProjectilePrefab cannot be null.");
                return;
            }
#endif
           // Shooting();
            
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
            if (canShoot) {
                if (m_DisableShooting) return;
                createBall();
            }
               
        }

        public void FireBall()
        {
            if (canShoot)
            {
                fireBall();
               
            }
        }
        public void Shooting()
        {
            if (canShoot)
            {
                if (m_DisableShooting) return;

                if (ProjectilePrefab is InflatableBall)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                          createBall();

                        //getFrom pull
                      
                    }
                    if (Input.GetButton("Fire1"))
                    {
                      // ProjectilePrefab.GetComponent<InflatableBall>().maxBall = maxBallScale;
                      // ProjectilePrefab.GetComponent<InflatableBall>().inflateVal = speedScale;
                       // scaleBall();
                    }
                    if (Input.GetButtonUp("Fire1"))
                    {
                      
                        fireBall();
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        createBall();
                        fireBall();
                    }
                }
                if (m_CurrentBall)
                {
                    if (m_CurrentBall.State == BallProjectile.ProjectileStates.Ready)
                        m_CurrentBall.transform.position = FireTransform.position;
                }
            }
           
        }
     
     
    }
}
