using System.Collections;
using System.Linq;
using UnityEngine;


public class Rock : MonoBehaviour
{
    private BoxCollider2D box2d;
    private Rigidbody2D body2d;
    public GameObject groundPoint;

    [SerializeField] private LayerMask layer;
    public bool isGrounded;
    float distanceToWall;
    float speed;
    private void Start()
    {
        speed = 3f;
        box2d = GetComponent<BoxCollider2D>();
        body2d = GetComponent <Rigidbody2D>();
    }
    private void Update()
    {
        Raycast();
        if (!isGrounded)
        {
            StartCoroutine(applyGravity());
        }
        else
        {
            StopCoroutine(applyGravity());
        }
    }
    #region DrawRayCast
    private void Raycast()
    {
        RaycastHit2D distanceHitdown = Physics2D.Raycast(groundPoint.transform.position, Vector2.down,layer);
        if (distanceHitdown.collider != null && !distanceHitdown.collider.CompareTag("Spike") && distanceHitdown.distance < 0.1f)
        {
            distanceToWall = distanceHitdown.distance;
            isGrounded = true;
        }
        else
        {

            distanceToWall = 1f;
            isGrounded = false;
        }
    }
    #endregion
    #region Gravity

    private IEnumerator applyGravity()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector2(transform.position.x, transform.position.y - distanceToWall * speed * Time.deltaTime);
    }
    #endregion
}
