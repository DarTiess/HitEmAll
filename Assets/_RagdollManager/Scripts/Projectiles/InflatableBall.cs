
using UnityEngine;
using System.Collections;

namespace MLSpace
{
    public class InflatableBall : BallProjectile
    {
        /// <summary>
        /// start scale of projectile ball
        /// </summary>
        [Tooltip("Starting scale of projectile ball.")]
        public static float MinScale = 0.015f;
        [HideInInspector] public float currentScale;

       [HideInInspector] public float maxBall = 0;
       [HideInInspector] public float inflateVal = 0.01f;
      
        /// <summary>
        /// max scale of projectile ball
        /// </summary>
        [Tooltip("Maximum scale of projectile ball.")]
        public static float MaxScale =1.0f;

        private float m_CurrentBallScale = 0.1f;      // current ball scale
        public float CurrentBallScale { get { return m_CurrentBallScale; } set { m_CurrentBallScale = value; } }



        public void inflate(float inflateValue = 0.1f)
        {
            m_CurrentBallScale += inflateValue;
            if (maxBall > MaxScale)
            {
                MaxScale = maxBall;
            }
            m_CurrentBallScale = Mathf.Min(m_CurrentBallScale, InflatableBall.MaxScale);
            transform.localScale =new Vector3(1,1,1) * m_CurrentBallScale;
            CurrentHitStrength = hitStrength * m_CurrentBallScale;

            currentScale = m_CurrentBallScale;
        }
        
        public static void Setup(InflatableBall ball)
        {
            ball.OnHit = () =>
            {
                if (ball.HitInfo.hitObject)
                {
                    RagdollManager ragMan = null;
                    IRagdollUser ragdollUser = null;
                    ragdollUser = ball.HitInfo.hitObject.GetComponent<IRagdollUser>();
                    if (ragdollUser == null)
                    {
#if DEBUG_INFO
                        Debug.LogError("Ball::OnHit cannot find ragdoll user object on " +
                            ball.HitInfo.hitObject.name + ".");
#endif
                        return;
                    }
                    ragMan = ragdollUser.RagdollManager;

                    if (!ragMan)
                    {
#if DEBUG_INFO
                        Debug.LogError("Ball::OnHit cannot find RagdollManager component on " +
                            ball.HitInfo.hitObject.name + ".");
#endif
                        return;
                    }

                    if (!ragMan.AcceptHit)
                    {
                        return;
                    }

                if (ragdollUser.IgnoreHit)
                   {
                        BallProjectile.DestroyBall(ball);
                      return;
                   }

                    Vector3 force = ball.HitInfo.hitDirection * ball.CurrentHitStrength;
                    ragMan.StartHitReaction(ball.HitInfo.bodyPartIndices, force*2f);
                    if (ragMan.gameObject.CompareTag("NPC"))
                    {
                        Debug.Log("Had Touch Bot");
                       // ragMan.GetComponent<EnemyController>().TakeDamage(ball.m_CurrentBallScale);
                      ball.gameObject.SetActive(false);
                      
                    }
                  
                }
               
            };
        }

      

       
    } 
}
