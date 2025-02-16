using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    public Animator animator;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {


        //ПРосто тест, объявлять дефолтные данные не тут
            // List<int> Doors = new List<int>();
            // Doors.Add(2);
            // Doors.Add(4);
            // Doors.Add(6);
            // Doors.Add(8);
            // PlayerDataManager.Instance.playerData.health = 25;
            // PlayerDataManager.Instance.playerData.AvalibleDoors = Doors;
        // SaveLoadManager.Instance.inventory.RemoveItem("Stick");
        // SaveLoadManager.Instance.inventory.RemoveItem("Sword");

        //  // Добавить предмет в инвентарь
        // SaveLoadManager.Instance.inventory.AddItem(new Item("Sword",12,"Damage","Описание","Дебаф","Путь к картинке"));
        // SaveLoadManager.Instance.inventory.AddItem(new Item("Stick",5,"Heal","Описание","Дебаф","Путь к картинке"));
        
        // Вывести инвентарь в консоль
        Debug.Log("Инвентарь на текущий момент:");
        SaveLoadManager.Instance.inventory.PrintInventory();

        // Сохранить инвентарь
            // Debug.Log(PlayerDataManager.Instance.playerData.health);
            // for(int i = 0;i<PlayerDataManager.Instance.playerData.AvalibleDoors.Count;i++){
            //     Debug.Log(PlayerDataManager.Instance.playerData.AvalibleDoors[i]);
            // }
        // SaveLoadManager.Instance.SaveInventory();
    }

    // Update is called once per frame
    void Update()
    {

        // direction.x = Input.GetAxisRaw("Horizontal");
        // direction.y = Input.GetAxisRaw("Vertical");

        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontalMove",horizontalMove*-1);
        animator.SetFloat("verticalMove",verticalMove*1);

        
    }

    private void FixedUpdate() {
        Vector2 direction = new Vector2(0,0);
        if(Input.GetKey(KeyCode.W)){
            direction.y = 1f;
        }

        if(Input.GetKey(KeyCode.S)){
            direction.y = -1f;
        }

        if(Input.GetKey(KeyCode.A)){
            direction.x = -1f;
        }

        if(Input.GetKey(KeyCode.D)){
            direction.x = 1f;
        }


        direction.Normalize();
        rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
    }
}
