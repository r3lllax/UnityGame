using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    private Animator animator;
    public static bool isTrigger = false;
    private int player, enemy, playerWins, enemyWins;
    private int Count = 0;

    [SerializeField] private GameObject text;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI enemyText;
    [SerializeField] private GameObject[] ObgToOn;
    [SerializeField] private TextMeshProUGUI playerCurrentScore;
    [SerializeField] private TextMeshProUGUI EnemyCurrentScore;


    private System.Random random;

    private int curPlayer, curEnemy;
    private bool isSpining = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        random = new System.Random();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!DialogueSystem.Dialogue)
        {
            text.SetActive(true);
            isTrigger = true;
        }
    }


    private IEnumerator SpinScore()
    {
        isSpining = true;
        float OneFrameDuration = 0.1f;
        while (OneFrameDuration < 1)
        {
            curPlayer = random.Next(0, 100);
            playerCurrentScore.text = $"Выпало: {curPlayer}";
            curEnemy = random.Next(0, 100);
            EnemyCurrentScore.text = $"Выпало: {curEnemy}";
            yield return new WaitForSeconds(OneFrameDuration);
            OneFrameDuration += 0.1f;
        }
        FinnalyGenerateScore(curPlayer, curEnemy);
        isSpining = false;

    }


    private IEnumerator WaitSec()
    {
        animator.SetBool("TurnOn", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("TurnOn", false);

    }

    private void FinnalyGenerateScore(int Player, int Enemy)
    {

        Count++;
        player = Player;
        enemy = Enemy;
        Debug.Log($"Player - {player}, enemy - {enemy}");
        playerCurrentScore.text = $"Выпало: {player}";
        EnemyCurrentScore.text = $"Выпало: {enemy}";

        if (player > enemy)
        {
            Debug.Log($"В раунде номер {Count} победил игрок");
            playerCurrentScore.color = new Color32(53, 189, 1, 255);
            EnemyCurrentScore.color = new Color32(189, 53, 1, 255);
            playerWins++;
        }
        else if (player < enemy)
        {
            Debug.Log($"В раунде номер {Count} победил противник");
            EnemyCurrentScore.color = new Color32(53, 189, 1, 255);
            playerCurrentScore.color = new Color32(189, 53, 1, 255);
            enemyWins++;
        }
        else
        {
            Debug.Log($"В раунде номер {Count} ничья");
        }
        if (playerWins == 3)
        {
            Debug.Log("Игрок выиграл, ура!");
            animator.SetBool("TurnOn", false);
            text.SetActive(false);
            BackToLobbyButClosed.DoorIsOpen = true;
            Destroy(GetComponent<ButtonTrigger>());
            foreach (var obj in ObgToOn)
            {
                obj.SetActive(false);
            }

        }
        else if (enemyWins == 3)
        {
            Debug.Log("Игрок проиграл, смерть");
            animator.SetBool("TurnOn", false);
            text.SetActive(false);
            Destroy(GetComponent<ButtonTrigger>());
            foreach (var obj in ObgToOn)
            {
                obj.SetActive(false);
            }

        }
    }

    private void Update()
    {

        playerText.text = playerWins.ToString();
        enemyText.text = enemyWins.ToString();
        Debug.Log(isTrigger);
        if (!isSpining && isTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (playerWins < 3 && enemyWins < 3)
            {
                if (DialogueSystem.Dialogue == false)
                {
                    foreach (var obj in ObgToOn)
                    {
                        obj.SetActive(true);
                    }
                    if (playerCurrentScore != null)
                    {
                        playerCurrentScore.color = new Color32(255, 255, 255, 255);
                    }
                    if (EnemyCurrentScore != null)
                    {
                        EnemyCurrentScore.color = new Color32(255, 255, 255, 255);
                    }
                    StartCoroutine(SpinScore());
                    StartCoroutine(WaitSec());
                }



            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        text.SetActive(false);
        isTrigger = false;
    }
}
