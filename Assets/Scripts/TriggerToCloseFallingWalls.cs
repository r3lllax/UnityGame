using UnityEngine;

public class TriggerToCloseFallingWalls : MonoBehaviour
{
    [SerializeField] private GameObject statesTint;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player"){
            statesTint.SetActive(false);
            collision.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
        }
    }
}
