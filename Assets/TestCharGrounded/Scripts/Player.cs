///* 
//    ------------------- Code Monkey -------------------

//    Thank you for downloading this package
//    I hope you find it useful in your projects
//    If you have any questions let me know
//    Cheers!

//               unitycodemonkey.com
//    --------------------------------------------------
// */

//using System;
//using UnityEngine;
//using V_AnimationSystem;
//using CodeMonkey.Utils;

//public class Player : MonoBehaviour {

//    [SerializeField] private LayerMask platformLayerMask;

//    private Player_Base playerBase;
//    private Rigidbody2D rigidbody2d;
//    private BoxCollider2D boxCollider2d;

//    private void Awake() {
//        playerBase = gameObject.GetComponent<Player_Base>();
//        rigidbody2d = transform.GetComponent<Rigidbody2D>();
//        boxCollider2d = transform.GetComponent<BoxCollider2D>();
//    }

//    private void Update() {
//        // Handle Jump
//        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
//            float jumpVelocity = 100f;
//            rigidbody2d.velocity = Vector2.up * jumpVelocity;
//        }

//        PlayAnimations(IsGrounded());
//    }

//    private void FixedUpdate() {
//        // Handle Movement
//        float moveSpeed = 40f;
//        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
//        if (Input.GetKey(KeyCode.LeftArrow)) {
//            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
//        } else {
//            if (Input.GetKey(KeyCode.RightArrow)) {
//                rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
//            } else {
//                // No keys pressed
//                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
//                rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
//            }
//        }
//    }

//    private bool IsGrounded() {
//        //return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;

//        float extraHeightText = 1f;
//        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);

//        Color rayColor;
//        if (raycastHit.collider != null) {
//            rayColor = Color.green;
//        } else {
//            rayColor = Color.red;
//        }
//        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
//        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
//        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2d.bounds.extents.x * 2f), rayColor);

//        Debug.Log(raycastHit.collider);
//        return raycastHit.collider != null;
//    }

//    private void PlayAnimations(bool isGrounded) {
//        if (isGrounded) {
//            if (rigidbody2d.velocity.x == 0) {
//                playerBase.PlayIdleAnim();
//            } else {
//                playerBase.PlayMoveAnim(new Vector2(rigidbody2d.velocity.x, 0f));
//            }
//        } else {
//            playerBase.PlayJumpAnim(rigidbody2d.velocity);
//        }

//    }

//}
