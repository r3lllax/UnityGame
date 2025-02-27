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
    [SerializeField] private GameObject enemyObj;
    [SerializeField] private GameObject enemySlashObj;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI enemyText;
    [SerializeField] private GameObject[] ObgToOn;
    [SerializeField] private TextMeshProUGUI playerCurrentScore;
    [SerializeField] private TextMeshProUGUI EnemyCurrentScore;
    [SerializeField] private GameObject DamageText;


    private System.Random random;

    private int curPlayer, curEnemy;
    private bool isSpining = false;
    private int Dmg;
    private void Awake()
    {
        DialogueSystem.Dialogue = true;
        DialogueSystem.NowIsADialogue = false;
        
        animator = GetComponent<Animator>();
        random = new System.Random();
        enemySlashObj.GetComponent<SpriteRenderer>().enabled = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(!DialogueSystem.Dialogue);
        if (DialogueSystem.Dialogue == false)
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
            StartCoroutine(EndMiniGameByWin());
        }
        else if (enemyWins == 3)
        {
            Debug.Log($"ХП игрока: {PlayerDataManager.Instance.playerData.health}");
            
            Dmg = random.Next(0,100);
            if (Dmg >= PlayerDataManager.Instance.playerData.health){
                Debug.Log("Урон больше здоровья");
                PlayerDataManager.Instance.playerData.health = 0;
                PlayerDataManager.Instance.SavePlayerData();
                StartCoroutine(EndMiniGameByLoose(Dmg));
                //Включаем анимацию смерти игрока
                
                //Переходим на экран поражения

            }
            else{
                Debug.Log($"Урон: {Dmg}");
                
                StartCoroutine(EndMiniGameByLoose(Dmg));
            }
        
            
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        isTrigger = true;
    }

    public void LooseBtn(){
        StartCoroutine(EndMiniGameByLoose(Dmg));
    }
    private IEnumerator EndMiniGameByLoose(int Dmg){
        //Отключаю возможность игрока ходить
        playerController.isFreezed = true;

        //Получаю дефолтный размер противника и увеличенный(чтобы эффект испарения работал правильно)
        var defaultscale = enemyObj.GetComponent<Transform>().localScale;
        var scale = defaultscale;
        scale.x = 2.6f;
        scale.y = 4.984451f;
        scale.z = 1.458372f; 
        //Выключаю коллизию противника, чтобы он не сдвигал игрока, когда появляется возле него
        enemyObj.GetComponent<BoxCollider2D>().enabled = false;

        //Параметр IsDead отвечает за анимацию испарения, поэтому чтобы он исчез и потом появился ставлю isdead тру, а потом фолс, чтобы при появлении возле игрока анимация перешла в состояние анимации монстра
        enemyObj.GetComponent<Animator>().SetBool("IsDead",true);

        //Увеличивыю анимацию испарения
        enemyObj.GetComponent<Transform>().localScale = scale;
        //жду пока она проиграется
        yield return new WaitForSeconds(1);
        //возвращаю нормальный размер противника
        enemyObj.GetComponent<Transform>().localScale = defaultscale;
        //Перемещаю противника справа от игрока
        var enemyPos = enemyObj.GetComponent<Transform>().position;
        enemyPos.x = playerObj.GetComponent<Transform>().position.x + 1.4f;
        enemyPos.y = playerObj.GetComponent<Transform>().position.y;
        enemyPos.z = playerObj.GetComponent<Transform>().position.z;
        enemyObj.GetComponent<Transform>().position = enemyPos;

        //Возвращаю анимацию монстра(как и говорилось ранее(до этого была анимация испарения))
        enemyObj.GetComponent<Animator>().SetBool("IsDead",false);
        //Небольшая задержка, чтобы игрок успел понять что его атакуют
        yield return new WaitForSeconds(0.5f);

        //Так как анимация сплеша в другом объекте(изначально его не видно, поэтому спрайт рендерер стоит фолс, но перед анимацией я включаю его) обращаюсь к включению ее отображения
        enemySlashObj.GetComponent<SpriteRenderer>().enabled = true;

        //Активирую анимацию атаки и пишу сколько урона нанесено
        enemySlashObj.GetComponent<Animator>().SetBool("EnemyAttacking",true);
        DamageText.GetComponent<TextMeshProUGUI>().text = $"Вас атаковали и нанесли такое количество урона: {Dmg}";
        //Красным показывается что игрока атаковали, позже можно сделать нормальную анимацию(мигание красным как в майнкрафте и тряску игрока к примеру)
        playerObj.GetComponent<SpriteRenderer>().color = new Color32(222,169,169,255);
        //Жду пока проиграется анимация сплеша
        yield return new WaitForSeconds(1);
        //Выключаю видимость сплеша
        enemySlashObj.GetComponent<SpriteRenderer>().enabled = false;
        //Возвращаю нормальный цвет игрока
        playerObj.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
        //Выключаю анимацию сплеша
        enemySlashObj.GetComponent<Animator>().SetBool("EnemyAttacking",false);
        //Ставлю увеличенный размер спрайта противника и проигрываю анимацию испарения(для этого и ставил увеличенный размер)
        enemyObj.GetComponent<Transform>().localScale = scale;
        enemyObj.GetComponent<Animator>().SetBool("IsDead",true);
        //Выключаю все текстовые элементы и триггеры
        text.SetActive(false);
        if(PlayerDataManager.Instance.playerData.health >0){BackToLobbyButClosed.DoorIsOpen = true;}
        foreach (var obj in ObgToOn)
        {
            obj.SetActive(false);
        }
        //Жду пока проиграется анимация испарения и после чего удаляю скрипт из объекта
        yield return new WaitForSeconds(1.5f);
        enemyObj.SetActive(false);
        Destroy(GetComponent<ButtonTrigger>());
        //Возвращаю контроль над игроком
        PlayerDataManager.Instance.playerData.health -= Dmg;
        PlayerDataManager.Instance.SavePlayerData();
        playerController.isFreezed = false;
    }

    private IEnumerator EndMiniGameByWin()
    {
        enemyObj.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        animator.SetBool("TurnOn", false);
        enemyObj.GetComponent<Animator>().SetBool("IsDead",true);
        var scale = enemyObj.GetComponent<Transform>().localScale;
        scale.x = 2.6f;
        scale.y = 4.984451f;
        scale.z = 1.458372f; 
        enemyObj.GetComponent<Transform>().localScale = scale;
        text.SetActive(false);
        BackToLobbyButClosed.DoorIsOpen = true;
        foreach (var obj in ObgToOn)
        {
            obj.SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);
        enemyObj.SetActive(false);
        Destroy(GetComponent<ButtonTrigger>());
        
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
