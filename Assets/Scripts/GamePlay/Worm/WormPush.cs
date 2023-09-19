using DoozyUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormPush : MonoBehaviour
{
    public static WormPush Instance;
    [Header("Push Setting")]

    [Range(-10f, 100f)]
    public float PushPower = 10f;
    [Range(-10f, 10f)]
    public float PushDistance = 1f;
    public GameObject[] Wall;
    public GameObject[] ObjToPush;
    //public Animator animator;
    //Vector2 Direction;
    //private bool ObjectMoving = false;

    //private bool CanPush = false;   

    private void Awake()
    {
        Instance = this;
    }
    //public bool Move(Vector2 direction)
    //{
    //    if (ObjBlocked(transform.position, direction))
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        transform.Translate(direction);
    //        return true;
    //    }
    //}
    //public bool ObjBlocked(Vector3 pos, Vector2 direction)
    //{
    //    Vector2 newpos = new Vector2(pos.x, pos.y) + direction;
    //    foreach (var db in Wall)
    //    {
    //        if (db.transform.position.x == newpos.x && db.transform.position.y == newpos.y)
    //        {
    //            return true;
    //        }
    //    }
    //    foreach (var rock in ObjToPush)
    //    {
    //        if (rock.transform.position.x == newpos.x && rock.transform.position.y == newpos.y)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    //public void DoPush()
    //{
    //    Collider2D[] colliders = Physics2D.Overlap(transform.position, PushDistance);

    //    foreach (Collider2D pushedObject in colliders)
    //    {
    //        if (pushedObject.CompareTag("Rock"))
    //        {
    //            Rigidbody2D pushedBody = pushedObject.GetComponent<Rigidbody2D>();

    //            // Get direction from your postion toward the object you wish to push
    //            var direction = pushedBody.transform.position - transform.position;

    //            // Normalization is important, to have constant unit!
    //            pushedBody.AddForce(direction.normalized * 500, ForceMode2D.Force);

    //        }
    //    }

    //}
    //This is Character pushAction
    //void PushAction()
    //{
    //    //// if object moving and player is pressing F key, animation will start.
    //    //if (Input.GetKey(KeyCode.F))
    //    //{
    //    //    CanPush = true;
    //    //}
    //    //else if (Input.GetKeyUp(KeyCode.F))
    //    //{
    //    //    CanPush = false;
    //    //    ObjectMoving = false;
    //    //    animator.SetBool("pushing", false); // deactivating the push animation
    //    //}

    //    //if (ObjectMoving)
    //    //{
    //    //    animator.SetBool("pushing", true); // activating the push animation
    //    //}
    //}
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body != null)
        {
            Vector2 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            body.AddForceAtPosition(forceDirection * PushPower, transform.position, ForceMode.Impulse);
        }

        ////No Rigidbody
        //if (body == null || body.tag != "Rock") // Check tag
        //{
        //    return;
        //}

        //// Prevent pushing objects to the ground
        //if (hit.moveDirection.y < -0.2)
        //{
        //    return;
        //}

        //// Prevent obj rotate
        //body.GetComponent<Rigidbody>().freezeRotation = true;

        ////Calculating distance between player and object
        //float Dist = Vector2.Distance(body.gameObject.transform.position, this.transform.position);
        //Debug.Log(Dist);

        //Calculate push direction from move direction,
        //we only push object to the sides not from up and down
        //Vector2 PushDirection = new Vector2(hit.moveDirection.x, 0);

        ////Apply the Push
        //if (CanPush && Dist < PushDistance)
        //{
        //    body.isKinematic = false;
        //    AppleForceToReachVelocity(body, PushDirection.normalized * PushPower, 0.1f);
        //    ObjectMoving = true;
        //}
        //else
        //{
        //    ObjectMoving = false;
        //    body.isKinematic = true;
        //    body.GetComponent<Rigidbody>().freezeRotation = false;
        //}

        ////if object is moving, it is calling to the animation.
        //if (body.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.01f)
        //{
        //    ObjectMoving = true;
        //}
        //else
        //{
        //    ObjectMoving = false;
        //}
    }

    ////Physic Library Source: https://gist.github.com/ditzel/1f207c838f0023fcbd34c5c67955fd25
    //private static void AppleForceToReachVelocity(Rigidbody rigidbody,Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    //{
    //    if(force == 0 || velocity.magnitude == 0)
    //    {
    //        return;
    //    }
    //    velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

    //    //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
    //    force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

    //    if (rigidbody.velocity.magnitude == 0)
    //    {
    //        rigidbody.AddForce(velocity * force, mode);
    //    }
    //    else
    //    {
    //        var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
    //        rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
    //    }
    //}
}

