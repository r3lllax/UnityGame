using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    private Animator animator;
    private System.Random random;
    private static string[] Monsters = { 
            "Enemy1",
            "Enemy2",
            "Enemy3",
        };

    void Awake()
    {
        animator = GetComponent<Animator>();
        random = new System.Random();
    }

    void Start()
    {
        RandomEnemy();
    }


    private void RandomEnemy(){
        int currentEnemy = random.Next(0,Monsters.Length);
        animator.SetBool(Monsters[currentEnemy],true);
    }

}
