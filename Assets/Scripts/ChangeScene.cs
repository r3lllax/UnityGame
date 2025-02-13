using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int NumberOfScene;
    public void Change(int number){
        SceneManager.LoadScene(number);
    }

    public void SetDefaultParamsForPlayer(){
        Debug.Log("Сработали дефолтные настройки");
        List<int> Doors = new List<int>();
        for(int i = 0;i<8;i++){
            Doors.Add(i+1);
        }
        PlayerDataManager.Instance.playerData.health = 25;
        PlayerDataManager.Instance.playerData.attackPower = 10;
        PlayerDataManager.Instance.playerData.inventory.items.Clear();
        PlayerDataManager.Instance.playerData.AvalibleDoors = Doors;
        SaveLoadManager.Instance.inventory.items.Clear();
        PlayerDataManager.Instance.SavePlayerData();
    }

    public void EXIT(){
        Application.Quit();
    }
}
