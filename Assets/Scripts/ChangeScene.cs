using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int NumberOfScene;
    public void Change(int number){
        SceneManager.LoadScene(number);
    }

    public void EXIT(){
        Application.Quit();
    }
}
