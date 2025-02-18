using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed;
    public static bool isFreezed = false;

    private Rigidbody2D rb;
    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Debug.Log("Инвентарь на текущий момент:");
        SaveLoadManager.Instance.inventory.PrintInventory();
    }

    // Update is called once per frame
    void Update()
    {

        // direction.x = Input.GetAxisRaw("Horizontal");
        // direction.y = Input.GetAxisRaw("Vertical");

        if (!isFreezed)
        {
            float horizontalMove = Input.GetAxisRaw("Horizontal");
            float verticalMove = Input.GetAxisRaw("Vertical");

            animator.SetFloat("horizontalMove", horizontalMove * -1);
            animator.SetFloat("verticalMove", verticalMove * 1);
        }


    }

    private void FixedUpdate()
    {
        if(!isFreezed){
            Vector2 direction = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1f;
        }


        direction.Normalize();
        rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
        }
    }
}
