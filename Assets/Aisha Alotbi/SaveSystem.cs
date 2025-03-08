using UnityEngine;
using Gamekit2D;
using System.IO;

namespace AishaAlotbi
{
    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance { get; private set; }
        PlayerData playerData = new PlayerData();
        public string playerJsonFileName = "Player.json";
        public string folderPath = Application.streamingAssetsPath;
        private string fullFilePath = string.Empty;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }



        private void Start()
        {
            fullFilePath = Path.Combine(folderPath, playerJsonFileName);
        }

        [System.Obsolete]
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                SaveGame();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadGame();
            }
        }

        [System.Obsolete]
        public void SaveGame()
        {
            PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

            Damageable damageable = FindObjectOfType<Damageable>();
            InventoryController.InventoryChecker inventory = new InventoryController.InventoryChecker();

            playerData.currentHealth = damageable.m_CurrentHealth;
            playerData.playerPosition = player.transform.position;
            playerData.playerInventory = inventory.inventoryItems;



            string jsonData = JsonUtility.ToJson(playerData);

            File.WriteAllText(fullFilePath, jsonData);

            Debug.Log("Game Saved, Player Health = " + playerData.currentHealth + " Player Position = " + playerData.playerPosition + " Player Inventory = " + playerData.playerInventory);

        }

        [System.Obsolete]
        public void LoadGame()
        {
            if (File.Exists(fullFilePath))
            {
                string jsonData = File.ReadAllText(fullFilePath);

                playerData = JsonUtility.FromJson<PlayerData>(jsonData);

                if (playerData != null)
                {
                    PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
                    Damageable damageable = FindObjectOfType<Damageable>();
                    InventoryController.InventoryChecker inventory = new InventoryController.InventoryChecker();


                    player.transform.position = playerData.playerPosition;
                    damageable.m_CurrentHealth = playerData.currentHealth;
                    inventory.inventoryItems = playerData.playerInventory;

                }
                else
                {
                    Debug.LogError("No Data within JSON");
                }
            }
            else
            {
                Debug.LogError("No Player Data Found");
            }
        }
    }
}
