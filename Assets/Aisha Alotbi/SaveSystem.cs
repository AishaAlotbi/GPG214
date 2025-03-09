using UnityEngine;
using Gamekit2D;
using System.IO;
using System.Collections.Generic;

namespace AishaAlotbi
{
    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance { get; private set; }
        PlayerData playerData = new PlayerData();
        public string playerJsonFileName = "Player.json";
        public string folderPath;
        private string fullFilePath = string.Empty;

        private void Awake()
        {
            if (Instance != null && Instance != this)
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
            folderPath = Application.persistentDataPath;
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
            InventoryController inventoryController = FindObjectOfType<InventoryController>();

            Data inventoryData = inventoryController.SaveData();
            HashSet<string> inventorySet = ((Data<HashSet<string>>)inventoryData).value;

            string[] inventoryItemsArray = new string[inventorySet.Count];
            inventorySet.CopyTo(inventoryItemsArray);

            playerData.currentHealth = damageable.startingHealth;
            playerData.playerPosition = player.transform.position;
            playerData.playerInventory = inventoryItemsArray;


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
                    InventoryController inventoryController = FindObjectOfType<InventoryController>();


                    player.transform.position = playerData.playerPosition;
                    damageable.m_CurrentHealth = playerData.currentHealth;

                    inventoryController.Clear();

                    foreach (string item in playerData.playerInventory)
                    {
                        inventoryController.AddItem(item);
                    }

                    Debug.Log("Game Loaded, Player Health = " + playerData.currentHealth + " Player Position = " + playerData.playerPosition + " Player Inventory = " + playerData.playerInventory);


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
