// © 2016 Mario Lelas
using UnityEngine;
using DG.Tweening;


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
       
     
    }
}
