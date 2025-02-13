using UnityEngine;

public class WinTracker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerDataManager.Instance.playerData.AvalibleDoors.Count==0){
            //Загружем сцену финала игры
            Debug.Log("Победа!");
        }
    }
}
