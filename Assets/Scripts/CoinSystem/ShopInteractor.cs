using UnityEngine;
using UnityEngine.UI;

public class ShopInteractor : MonoBehaviour
{
    [Header("UI References")]
    public GameObject shopUI;                 // The panel or canvas for the shop
    public Text interactionPrompt;            // Text that shows "Press E to interact"

    [Header("Interaction Settings")]
    public float interactionRange = 3f;       // How close the player needs to be
    public KeyCode interactionKey = KeyCode.E;

    [Header("Sword Settings")]
    public GameObject swordPrefab;
    public Transform swordSpawnPoint;
    public int swordCost = 5;
    public Button swordButton; // Assign your sword button in Inspector

    [Header("Bow Settings")]
    public GameObject bowPrefab;
    public Transform bowSpawnPoint;
    public int bowCost = 5;
    public Button bowButton; // Assign your bow button in Inspector

    [Header("Health Settings")]
    public GameObject healthPrefab;  // Your health pickup/prefab
    public Transform healthSpawnPoint;
    public int healthCost = 3;
    public Button healthButton;
    public float healthSpawnCooldown = 1f; // Prevent rapid spawning

    private bool healthOnCooldown = false;

    public CoinManager coinManager;
    
    private Transform player;
    private bool playerInRange = false;

    void Start()
    {
        // Find player by tag
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure your Player GameObject is tagged 'Player'.");
        }

        // Ensure UI starts hidden
        if (shopUI != null) shopUI.SetActive(false);
        if (interactionPrompt != null) interactionPrompt.gameObject.SetActive(false);
        
        swordButton.onClick.AddListener(BuySword);
        bowButton.onClick.AddListener(BuyBow);
        healthButton.onClick.AddListener(BuyHealth);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distance to player: " + distance);

        if (distance <= interactionRange)
        {
            Debug.Log("Player is in range of shop");

            if (!playerInRange)
            {
                playerInRange = true;

                if (interactionPrompt != null)
                {
                    interactionPrompt.text = $"Press '{interactionKey}' to interact";
                    interactionPrompt.gameObject.SetActive(true);
                }
            }

            if (Input.GetKeyDown(interactionKey))
            {
                ToggleShop();
            }
        }
        else if (playerInRange)
        {
            playerInRange = false;
            if (interactionPrompt != null)
                interactionPrompt.gameObject.SetActive(false);

            CloseShop();
        }
    }


    void ToggleShop()
    {
        if (shopUI == null) return;

        bool isActive = !shopUI.activeSelf;
        shopUI.SetActive(isActive);
        Time.timeScale = isActive ? 0f : 1f;

        GameManager.isShopOpen = isActive;
    }


    void CloseShop()
    {
        if (shopUI == null) return;

        shopUI.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.isShopOpen = false;
    }

    public void BuySword()
    {
        if (coinManager.SpendCoins(swordCost))
        {
            Instantiate(swordPrefab, swordSpawnPoint.position, Quaternion.identity);
            Debug.Log("Sword purchased!");
        }
        else
        {
            Debug.Log("Not enough coins for sword!");
        }
    }

    public void BuyBow()
    {
        if (coinManager.SpendCoins(bowCost))
        {
            Instantiate(bowPrefab, bowSpawnPoint.position, Quaternion.identity);
            Debug.Log("Bow purchased!");
        }
        else
        {
            Debug.Log("Not enough coins for bow!");
        }
    }

    public void BuyHealth()
    {
        if (healthOnCooldown) return;

        if (coinManager.SpendCoins(healthCost))
        {
            Instantiate(healthPrefab, healthSpawnPoint.position, Quaternion.identity);
            Debug.Log("Health purchased!");
        }
        else
        {
            Debug.Log("Not enough coins for health!");
        }
    }
}
