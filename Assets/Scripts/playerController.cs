using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerController : MonoBehaviour
{
    public static string State ="Fire";
    public float speed;
    public Vector3 defaultscale;
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
        defaultscale = GetComponent<Transform>().localScale;
        isFreezed = false;
    }



    private IEnumerator loose(){
        playerController.isFreezed = true;
        GetComponent<Animator>().SetFloat("horizontalMove",0);
        GetComponent<Animator>().SetFloat("verticalMove",0);
        GetComponent<CircleCollider2D>().enabled = false;
        for(int i =0;i<3;i++){
            GetComponent<SpriteRenderer>().color = new Color32(255,255,255,150);
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
            yield return new WaitForSeconds(0.2f);
        }
        GetComponent<SpriteRenderer>().color = new Color32(255,255,255,0);
        SceneManager.LoadScene(8);
        playerController.isFreezed = false;
    }
    
    void Update()
    {
        
        if(PlayerDataManager.Instance.playerData.health <=0){
            StartCoroutine(loose());
        }

        if (!isFreezed)
        {
            float horizontalMove = Input.GetAxisRaw("Horizontal");
            float verticalMove = Input.GetAxisRaw("Vertical");

            animator.SetFloat("horizontalMove", horizontalMove * -1);
            animator.SetFloat("verticalMove", verticalMove * 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                State = InteractiveWall.States[0];
                Debug.Log($"нажата 1, стойка:{State}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                State = InteractiveWall.States[1];
                Debug.Log($"нажата 1, стойка:{State}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                State = InteractiveWall.States[2];
                Debug.Log($"нажата 1, стойка:{State}");
            }



    }

    private void FixedUpdate()
    {
        if (!isFreezed)
        {
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
    public void ReserveAttempt(){
        // StopCoroutine(SpendAttempt());
        StartCoroutine(SpendAttempt());
    }
    private IEnumerator SpendAttempt(){
        playerController.isFreezed = true;
        GetComponent<Animator>().SetFloat("horizontalMove",0);
        GetComponent<Animator>().SetFloat("verticalMove",0);
        for(int i =0;i<1;i++){
            GetComponent<SpriteRenderer>().color = new Color32(235,71,71,255);
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
            yield return new WaitForSeconds(0.2f);
        }
    

        //Получаю дефолтный размер противника и увеличенный(чтобы эффект испарения работал правильно)
        
        var scale = new Vector3();
        scale.x = 2.6f;
        scale.y = 4.984451f;
        scale.z = 1.458372f; 
        GetComponent<Animator>().SetBool("isDead",true);
        GetComponent<Transform>().localScale = scale;
        yield return new WaitForSeconds(1);
        GetComponent<Transform>().localScale = defaultscale;
        GetComponent<Animator>().SetBool("isDead",false);
        GetComponent<Transform>().position = new Vector2(-6.41f,1f);
        
        for(int i =0;i<3;i++){
            GetComponent<SpriteRenderer>().color = new Color32(255,255,255,150);
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
            yield return new WaitForSeconds(0.2f);
        }
        playerController.isFreezed = false;
    }
}
