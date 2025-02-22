using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class DeleteFloor : MonoBehaviour
{
    private bool isStart = false;
    public float speed = 1f;
    public static bool DeleteTriggerActive = false;
    public GameObject[] platforms;
    [SerializeField] private GameObject GameZone;
    [SerializeField] private GameObject[] lavaBlock;
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isStart){
            SearchPlatforms();
            StartCoroutine(DeletingFloor());
        }
        isStart = true;
    }

    
    private void SearchPlatforms(){
        platforms = GameObject.FindGameObjectsWithTag("Platform");
    }
    public IEnumerator DeletingFloor(){
        yield return new WaitForSeconds(1f);
        Debug.Log($"Длинна - {platforms.Length}");
        int i = platforms.Length;
        while(platforms.Length >0){
            Debug.Log($"Цикл while, i:{i}");
            for(int j = 0;j<platforms.Length;j++){
                
                if(platforms[j]!= null && platforms[j].name.EndsWith(i.ToString().Length==1?$"0{i}":$"{i}")){
                    if(platforms[j] != null){
                        platforms[j].transform.GetChild(0).GetComponent<PlatformScript>().LavaBlockBelowPlatform.transform.GetChild(0).GetComponent<LavaTrigger>().enabled = true;
                        platforms[j].transform.GetChild(0).GetComponent<PlatformScript>().LavaBlockBelowPlatform.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
                        Destroy(platforms[j]);
                    }
                    yield return new WaitForSeconds(speed);
                    speed *=0.95f;
                }
            }
            i--;
            if(i == 0){
                Debug.Log("ВЫход из цикла");
                break;
            }
        }
    }

}
