using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Rigidbody2D rb;
    public Animator animator;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontalMove",horizontalMove*-1);
        animator.SetFloat("verticalMove",verticalMove*1);
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
