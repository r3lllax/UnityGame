using UnityEngine;

public class TriggerToStartFallingWalls : MonoBehaviour
{
    [SerializeField] private GameObject statesTint;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if(collision.name == "player"){
            bookTrigger.ShowUI = false;
            FallingWalls.toMove = true;
            statesTint.SetActive(true);
        }
    }
}
