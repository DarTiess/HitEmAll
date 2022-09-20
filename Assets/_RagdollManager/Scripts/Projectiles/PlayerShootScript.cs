// © 2016 Mario Lelas
using UnityEngine;



namespace MLSpace
{

    public class PlayerShootScript : ShootScript
    {
        public static PlayerShootScript Instance { get; private set; }
        [HideInInspector] public bool canShoot;

        private void Awake()
        {
            Instance = this;
        }
        // Update is called once per frame
        void LateUpdate()
        {
#if DEBUG_INFO

            if (!ProjectilePrefab)
            {
                Debug.LogError("ProjectilePrefab cannot be null.");
                return;
            }
#endif
            Shooting();
            
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
                      //  SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerAttack);
                        createBall();
                    }
                    if (Input.GetButton("Fire1"))
                    {
                        scaleBall();
                    }
                    if (Input.GetButtonUp("Fire1"))
                    {
                      //  SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerAttack);
                        fireBall();
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                      //  SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerAttackBubble);
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
