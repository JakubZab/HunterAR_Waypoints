using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    public GameObject baseUI;
    public GameObject battleUI;

    public GameObject youDiedPopup;
    public GameObject youWonPopup;
    public Text battleMessage;
    public Slider playerHealthSlider;
    public Slider monsterHealthSlider;

    public Text playerHealthText; 
    public Text monsterHealthText; 

    public Button attackButton;
    public Button potionButton;
    public Button specialSkillButton;
    public Button mainMenuButton;

    public Character player;
    public Monster monster;
    

    private bool isPlayerTurn = true;
    private bool isGameOver = false;
    private bool waitingForClick = false;
    //public WaypointMovement movement;

    void Start()
    {
       
        battleUI.SetActive(false);
       

        attackButton.onClick.AddListener(PlayerAttack);
        potionButton.onClick.AddListener(PlayerUsePotion);
        specialSkillButton.onClick.AddListener(PlayerUseSpecialSkill);
        
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        player.OnHealthChanged += UpdatePlayerHealthUI;
        monster.OnHealthChanged += UpdateMonsterHealthUI;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartBattle();
        }
    }

    void StartBattle()
    {
        baseUI.SetActive(false);
        battleUI.SetActive(true);

        playerHealthSlider.maxValue = player.maxHealth;
        playerHealthSlider.value = player.health;
        playerHealthText.text = $"HP: {player.health}/{player.maxHealth}";

        monsterHealthSlider.maxValue = monster.maxHealth;
        monsterHealthSlider.value = monster.health;
        monsterHealthText.text = $"HP: {monster.health}/{monster.maxHealth}"; 

        player.OnHealthChanged += UpdatePlayerHealthUI;
        monster.OnHealthChanged += UpdateMonsterHealthUI;

        
    }

    void UpdatePlayerHealthUI(int currentHealth)
    {
        playerHealthSlider.value = currentHealth;
        playerHealthText.text = $"HP: {currentHealth}/{player.maxHealth}"; 
    }
    void UpdateMonsterHealthUI(int currentHealth)
    {
        monsterHealthSlider.value = currentHealth;
        monsterHealthText.text = $"HP: {currentHealth}/{monster.maxHealth}"; 
    }

    void Update()
    {
        if (waitingForClick && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            waitingForClick = false;
            MonsterAttack();
        }
    }

    void PlayerAttack()
    {
        if (isPlayerTurn && !isGameOver)
        {
            int diceRoll = Random.Range(1, 7);
            int damage = player.attack * diceRoll;
            
            battleMessage.text = $"You rolled a {diceRoll} and dealt {damage} damage!";
            monster.health -= damage;
            monsterHealthSlider.maxValue = monster.health;
            monsterHealthSlider.value = monster.health;
            monsterHealthText.text = $"HP: {monster.health}/{monster.maxHealth}";

            player.AttackAnimation();
            monster.ReceiveDamageAnimation();

            if (monster.health <= 0)
            {
                monster.DeathAnimation();
                ShowResultPopup(true);
            }
            else
            {
                isPlayerTurn = false;
                waitingForClick = true;
            }
        }
    }

    void PlayerUsePotion()
    {
        if (isPlayerTurn && !isGameOver)
        {
            int diceRoll = Random.Range(1, 7);
            int healing = player.alchemy * diceRoll;

            battleMessage.text = $"You rolled a {diceRoll} and healed {healing} health!";
            player.health += healing;

            if (player.health > player.maxHealth)
            {
                player.health = player.maxHealth;
            }
            playerHealthSlider.value = player.health;
            playerHealthText.text = $"HP: {player.health}/{player.maxHealth}";

            isPlayerTurn = false;
            waitingForClick = true;
        }
    }

    void PlayerUseSpecialSkill()
    {
        if (isPlayerTurn && !isGameOver)
        {
            int diceRoll = Random.Range(1, 11);
            int damage = player.specialSkill * diceRoll;

            battleMessage.text = $"You rolled a {diceRoll} and dealt {damage} special damage!";
            monster.health -= damage;
            monsterHealthSlider.value = monster.health;
            monsterHealthText.text = $"HP: {monster.health}/{monster.maxHealth}";
            
            player.SpecialSkillAnimation();
            monster.ReceiveDamageAnimation();

            if (monster.health <= 0)
            {
                monster.DeathAnimation();
                ShowResultPopup(true);
            }
            else
            {
                isPlayerTurn = false;
                waitingForClick = true;
            }
        }
    }

    void MonsterAttack()
    {
        if (!isPlayerTurn && !isGameOver)
        {
            int damage = Random.Range(3, 9);

            battleMessage.text = $"Monster attacked and dealt {damage} damage!";
            player.health -= damage;
            playerHealthSlider.value = player.health;
            playerHealthText.text = $"HP: {player.health}/{player.maxHealth}";

            monster.AttackAnimation();
            player.ReceiveDamageAnimation();

            if (player.health <= 0)
            {
                player.DeathAnimation();
                ShowResultPopup(false);
            }
            else
            {
                isPlayerTurn = true;
            }
        }
    }

    void ShowResultPopup(bool playerWon)
    {
        isGameOver = true;
        
       

        if (playerWon)
        {
            youWonPopup.SetActive(true);
        }
        else
        {
            youDiedPopup.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
