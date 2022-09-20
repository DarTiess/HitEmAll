// © 2015 Mario Lelas


using UnityEngine;

namespace MLSpace
{
    /// <summary>
    /// derived cball projectile class 
    /// </summary>
    public class SoapBallProjectile : BallProjectile
    {
        public float up_force = 1.0f;
     
        public static void Setup(SoapBallProjectile ball)
        {
            /*
            On hit start lifting character up by adding extra force on all bodyparts and set ragdoll event time delegate to fire 
            in 5 sec. then start another ragdoll removing extra force to make character fall down again.
            also set another timed event to fire getting up animation
            */

            ball.OnHit = () =>
            {
                if (ball.HitInfo.hitObject)
                {
                    RagdollManager rman = null;
                    IRagdollUser ragdollUser = null;


                    ragdollUser = ball.HitInfo.hitObject.GetComponent<IRagdollUser>();
                    if (ragdollUser == null )
                    {
#if DEBUG_INFO
                        Debug.LogError("Ball::OnHit cannot find ragdoll user object on " +
                            ball.HitInfo.hitObject.name + ".");
#endif
                        return;
                    }
                    rman = ragdollUser.RagdollManager;

                    if (!rman)
                    {
#if DEBUG_INFO
                        Debug.LogError("Ball::OnHit cannot find RagdollManager component on " +
                            ball.HitInfo.hitObject.name + ".");
#endif
                        return;
                    }
                    if (!rman.AcceptHit)
                    {
                        BallProjectile.DestroyBall(ball);
                        return;
                    }
                    if (ragdollUser.IgnoreHit)
                    {
                        BallProjectile.DestroyBall(ball);
                       return;
                    }


                    Vector3 boundsSize = ragdollUser.Bound.size ;
                    float max = boundsSize.x+0.7f;
                    if (boundsSize.y > max) max = boundsSize.y+0.7f;
                    if (boundsSize.z > max) max = boundsSize.z+0.7f;

                    ball.transform.localScale = new Vector3(max, max, max);
                    ball.RigidBody.isKinematic = true;
                    ball.SphereCollider.isTrigger = true;
                    ball.RigidBody.detectCollisions = false;
                    ball.transform.position = ragdollUser.Bound.center; 
                    ball.transform.SetParent(rman.RootTransform, true);
                    ball.lifetime = 120f;
                 
                    rman.StartRagdoll(null, null/*Vector3.zero*/, Vector3.zero, true);

                    Vector3 v = new Vector3(0.0f, ball.up_force, 0.0f);
                    for (int i = 0; i < (int)BodyParts.BODY_PART_COUNT; i++)
                    {
                        RagdollManager.BodyPartInfo b = rman.getBodyPartInfo(i);
                        b.extraForce = v;
                    }

                    ragdollUser.IgnoreHit = true;
                  //  rman.gameObject.tag = "DeadNPC";
                    rman.RagdollEventTime = 1.0f;
                    
                    rman.OnTimeEnd = () =>
                    { Vector3 up;
                       for (int i = 0; i < (int)BodyParts.BODY_PART_COUNT; i++)
                        {
                            RagdollManager.BodyPartInfo b = rman.getBodyPartInfo(i);
                            b.extraForce = Vector3.zero;
                           
                            up = new Vector3(b.transform.position.x, 2f, b.transform.position.z);
                            b.transform.position = Vector3.Lerp(b.transform.position,up , Time.deltaTime* 3f);
                            b.transform.position = new Vector3(b.transform.position.x, 2f, b.transform.position.z);
                            b.rigidBody.useGravity = false;
                        }

                     rman.StartRagdoll(null, Vector3.zero, Vector3.zero, true);
                        
                        up = new Vector3(rman.transform.position.x, 2f, rman.transform.position.z);
                          rman.transform.position = Vector3.Lerp(rman.transform.position,up , Time.deltaTime * 3f);
                        rman.transform.position = new Vector3(rman.transform.position.x, 2f, rman.transform.position.z);
                        rman.GetComponent<Rigidbody>().useGravity = false;
                       rman.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                      //  PlayerController.Instance.DeadEnemy();
                      //  BallProjectile.DestroyBall(ball);

                        rman.RagdollEventTime = 55.0f;
                        rman.OnTimeEnd = () =>
                        {
                           // rman.BlendToMecanim();
                        };
                        ragdollUser.IgnoreHit = false;
                    };
                }
            };
        }

       
    } 
}
