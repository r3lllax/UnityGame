using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.VFX;
using System.Collections;

public class pressButtons : MonoBehaviour
{
    public static int PlayerScore;
    public static int EnemeyScore;
    public static bool MGEnded;
    [SerializeField] private string Type;
    [SerializeField] private TextMeshProUGUI ScorePlayer;
    [SerializeField] private TextMeshProUGUI ScoreEnemy;
    public List<string> ButtonsNames = new List<string>();
    public List<GameObject> Buttons = new List<GameObject>();
    private int ButtonsCount = 4;
    [SerializeField] private GameObject PrefabBtn;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject DamageText;
    [SerializeField] private GameObject Player;
    private Vector3 defaultscale;
    private int currentIndexOfButton;
    [SerializeField] private GameObject enemySlashObj;
    private System.Random random = new System.Random();
    private float startPos;
    [SerializeField] private GameObject StartTrigger;
    public static bool DeleteSelf;
    void Awake()
    {
        DeleteSelf = false;
        MGEnded = false;
        currentIndexOfButton = 0;
        PlayerScore = 0;
        EnemeyScore = 0;
    }
    void Start()
    {
        GenerateButtons();
    }
    private void GenerateButtons()
    {
        startPos = -411f;
        for (int i = 0; i < ButtonsCount; i++)
        {
            startPos += 160f;
            var btn = Instantiate(PrefabBtn).transform;
            btn.SetParent(transform, false);
            btn.localPosition = new Vector2(startPos, 0);
            Buttons.Add(btn.gameObject);
        }
    }
    public IEnumerator WinnerText()
    {
        defaultscale = Enemy.GetComponent<Transform>().localScale;
        var scale = defaultscale;
        scale.x = 2.6f;
        scale.y = 4.984451f;
        scale.z = 1.458372f;
        for (int i = 0; i < 3; i++)
        {
            ScoreEnemy.enabled = false;
            ScorePlayer.enabled = false;
            yield return new WaitForSeconds(1f);
            ScoreEnemy.enabled = true;
            ScorePlayer.enabled = true;
            yield return new WaitForSeconds(1f);
        }
        if (PlayerScore > EnemeyScore)
        {
            ScorePlayer.color = new Color32(0, 204, 0, 255);
            ScorePlayer.fontSize = 50f;
            ScoreEnemy.color = new Color32(255, 0, 0, 255);
            Enemy.GetComponent<Animator>().SetBool("IsDead", true);
            Enemy.GetComponent<Transform>().localScale = scale;
            yield return new WaitForSeconds(1f);
            Enemy.GetComponent<Transform>().localScale = defaultscale;
            Destroy(Enemy);
        }
        else if (PlayerScore < EnemeyScore)
        {
            
            ScoreEnemy.color = new Color32(0, 204, 0, 255);
            ScoreEnemy.fontSize = 50f;
            ScorePlayer.color = new Color32(255, 0, 0, 255);
            int Dmg = random.Next(0, 100);
            //Выключаю коллизию противника, чтобы он не сдвигал игрока, когда появляется возле него
            Enemy.GetComponent<BoxCollider2D>().enabled = false;

            //Параметр IsDead отвечает за анимацию испарения, поэтому чтобы он исчез и потом появился ставлю isdead тру, а потом фолс, чтобы при появлении возле игрока анимация перешла в состояние анимации монстра
            Enemy.GetComponent<Animator>().SetBool("IsDead", true);

            //Увеличивыю анимацию испарения
            Enemy.GetComponent<Transform>().localScale = scale;
            //жду пока она проиграется
            yield return new WaitForSeconds(1);
            //возвращаю нормальный размер противника
            Enemy.GetComponent<Transform>().localScale = defaultscale;
            //Перемещаю противника справа от игрока
            var enemyPos = Enemy.GetComponent<Transform>().position;
            enemyPos.x = Player.GetComponent<Transform>().position.x + 1.4f;
            enemyPos.y = Player.GetComponent<Transform>().position.y;
            enemyPos.z = Player.GetComponent<Transform>().position.z;
            Enemy.GetComponent<Transform>().position = enemyPos;

            //Возвращаю анимацию монстра(как и говорилось ранее(до этого была анимация испарения))
            Enemy.GetComponent<Animator>().SetBool("IsDead", false);
            //Небольшая задержка, чтобы игрок успел понять что его атакуют
            yield return new WaitForSeconds(0.5f);

            //Так как анимация сплеша в другом объекте(изначально его не видно, поэтому спрайт рендерер стоит фолс, но перед анимацией я включаю его) обращаюсь к включению ее отображения
            enemySlashObj.GetComponent<SpriteRenderer>().enabled = true;

            //Активирую анимацию атаки и пишу сколько урона нанесено
            enemySlashObj.GetComponent<Animator>().SetBool("EnemyAttacking", true);
            DamageText.SetActive(true);
            DamageText.GetComponent<TextMeshProUGUI>().text = $"Вас атаковали на {Dmg} урона";
            //Красным показывается что игрока атаковали, позже можно сделать нормальную анимацию(мигание красным как в майнкрафте и тряску игрока к примеру)
            Player.GetComponent<SpriteRenderer>().color = new Color32(222, 169, 169, 255);
            //Жду пока проиграется анимация сплеша
            yield return new WaitForSeconds(1);
            //Выключаю видимость сплеша
            enemySlashObj.GetComponent<SpriteRenderer>().enabled = false;
            //Возвращаю нормальный цвет игрока
            Player.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            //Выключаю анимацию сплеша
            enemySlashObj.GetComponent<Animator>().SetBool("EnemyAttacking", false);
            //Ставлю увеличенный размер спрайта противника и проигрываю анимацию испарения(для этого и ставил увеличенный размер)
            Enemy.GetComponent<Transform>().localScale = scale;
            Enemy.GetComponent<Animator>().SetBool("IsDead", true);
            //Выключаю все текстовые элементы и триггеры
            if (PlayerDataManager.Instance.playerData.health > 0) { BackToLobbyButClosed.DoorIsOpen = true; }

            //Жду пока проиграется анимация испарения и после чего удаляю скрипт из объекта
            yield return new WaitForSeconds(1.5f);
            Enemy.SetActive(false);
            DamageText.SetActive(true);
            //Возвращаю контроль над игроком
            PlayerDataManager.Instance.playerData.health -= Dmg;
            PlayerDataManager.Instance.SavePlayerData();

        }
        else
        {
            ScoreEnemy.color = new Color32(255, 255, 102, 255);
            ScoreEnemy.fontSize = 50f;
            ScorePlayer.fontSize = 50f;
            ScorePlayer.color = new Color32(255, 255, 102, 255);
            Enemy.GetComponent<Animator>().SetBool("IsDead", true);
            Enemy.GetComponent<Transform>().localScale = scale;
            yield return new WaitForSeconds(1f);
            Enemy.GetComponent<Transform>().localScale = defaultscale;
            Destroy(Enemy);
        }
        yield return new WaitForSeconds(1f);
        BackToLobbyMG3.DoorIsOpen = true;
        DeleteSelf = true;
        playerController.isFreezed = false;

    }

    void Update()
    {
        if (DeleteSelf)
        {
            if (Type == "Player")
            {
                StartTrigger.GetComponent<PanelController>().TogglePanel();
                ScoreEnemy.enabled = false;
                ScorePlayer.enabled = false;
            }
            Destroy(gameObject);
        }
        if (MGEnded) { return; }
        var input = Input.inputString;
        if (Type != "Player")
        {
            ScorePlayer.text = PlayerScore.ToString();
            ScoreEnemy.text = EnemeyScore.ToString();
            if (random.Next(0, 301) < 300)
            {
                return;
            }
            input = random.Next(0, 6) > 2 ? ButtonsNames[currentIndexOfButton].ToLower() : "";

        }

        if (input.Length >= 1)
        {
            Debug.Log($"Инпут - {input}");
            Debug.Log($"Нужно - {ButtonsNames[currentIndexOfButton].ToLower()}");
            Debug.Log(input.Equals(ButtonsNames[currentIndexOfButton].ToLower()));
            if (input.Equals(ButtonsNames[currentIndexOfButton].ToLower()))
            {
                currentIndexOfButton++;
                if (currentIndexOfButton == 4)
                {
                    Debug.Log("Все кнопки нажаты");
                    if (Type == "Player")
                    {
                        PlayerScore += 1;
                    }
                    else
                    {
                        EnemeyScore += 1;
                    }
                    for (int i = 0; i < Buttons.Count; i++)
                    {
                        Destroy(Buttons[i]);
                    }
                    Buttons.Clear();
                    ButtonsNames.Clear();
                    GenerateButtons();
                    currentIndexOfButton = 0;
                }
            }
            else
            {
                currentIndexOfButton = 0;
            }
            Debug.Log("Осталось нажать:");
            for (int i = 0; i < Buttons.Count; i++)
            {
                // Buttons[i].SetActive(false);
                Buttons[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 150);
            }
            for (int i = currentIndexOfButton; i < Buttons.Count; i++)
            {
                Buttons[i].SetActive(true);
                Buttons[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

                // Debug.Log(ButtonsNames[i]);
            }


        }
    }

}
