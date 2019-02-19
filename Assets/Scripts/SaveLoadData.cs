using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadData : MonoBehaviour {
    [Header("Save data combiner")]
    [SerializeField]
    private const string DATA_SEPERATOR = "#";
    [Space]
    [Header("Player Data To Save")]
    public FloatReference playerX;
    public FloatReference playerY;
    public FloatReference leftMovement;
    public FloatReference rightMovement;
    public FloatReference downMovement;
    public FloatReference upMovement;
    public FloatReference playerMoney;
    public FloatReference playerHealth;
    public CharacterItemsList playerItems;

    private Vector2 playerPosition;
    private SavedPlayerData playerData;


    private int saveFileNumber = 0;

    // Use this for initialization
    void Start() {
        saveFileNumber = SaveFileNumberGet();
        LoadPlayerData();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.L))
        {
            saveFileNumber++;
            playerPosition = new Vector2(playerX.Variable.Value, playerY.Variable.Value);
            playerData = new SavedPlayerData(playerPosition, playerHealth.Variable.Value, playerMoney.Variable.Value, playerItems.itemAndStackNumber);
            string savedString = playerData.SavedString(DATA_SEPERATOR);
            SavePlayerData(savedString);
            SaveFileNumberSet(saveFileNumber);
            Debug.Log("Saved" + saveFileNumber);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadPlayerData();
        }
    }

    private void SavePlayerData(string saveString)
    {
        File.WriteAllText(Application.dataPath + "/Saves/save" + saveFileNumber + ".txt", saveString);
    }

    private void LoadPlayerData()
    {
        string textOnSave = File.ReadAllText(Application.dataPath + "/Saves/save" + saveFileNumber + ".txt");
        string[] contentOfSave = textOnSave.Split(new[] { DATA_SEPERATOR }, System.StringSplitOptions.None);
        //for (int i = 0; i < contentOfSave.Length; i++)
        //{
        //    Debug.Log(contentOfSave[i]);
        //}
        SetPlayerDataFromLoad(contentOfSave);
        //Debug.Log(textOnSave);
    }

    private void SetPlayerDataFromLoad(string[] loadedStrings)
    {
        #region Set(health, money, position)
        // Set playerhealth and player gold
        playerHealth.Variable.Value = float.Parse(loadedStrings[0]);
        playerMoney.Variable.Value = float.Parse(loadedStrings[1]);
        //Debug.Log("completed health and money moving to player Position");

        //Set player position
        string[] playerPositionString = loadedStrings[2].Split(',');
        transform.position = new Vector3(float.Parse(playerPositionString[0].Split('(')[1]), float.Parse(playerPositionString[1].Split(')')[0]));
        //Debug.Log("Completed Player Position" + playerX.Value + "" + playerY.Value + "moving to player Items");
        #endregion

        //Set player inventory
        string[] playerInventoryList = loadedStrings[3].Split(':');
        for (int i = 0; i < playerInventoryList.Length; i++)
        {
            string[] tempItemAndStackNumberSplit = playerInventoryList[i].Split(',');
            
            if(1 < tempItemAndStackNumberSplit.Length)
            {
                playerItems.loadedItemNumbers[i].x = float.Parse(tempItemAndStackNumberSplit[0].Split('(')[1]);
                playerItems.loadedItemNumbers[i].y = float.Parse(tempItemAndStackNumberSplit[1].Split(')')[0]);
                //Debug.Log(tempItemAndStackNumberSplit[0].Split('(')[1] + ','  + tempItemAndStackNumberSplit[1].Split(')')[0]);
            }
        }
        playerItems.LoadingItems();
    }

    //Sets the save file number
    private void SaveFileNumberSet(int saveFileNumber)
    {
        File.WriteAllText(Application.dataPath + "/Saves/save.txt", saveFileNumber.ToString());
    }

    //Gets the number of the save file
    private int SaveFileNumberGet()
    {
        int returnFileNumber = 0;
        string textOnSave = File.ReadAllText(Application.dataPath + "/Saves/save.txt");
        if (textOnSave != null)
        {
            if(int.TryParse(textOnSave, out returnFileNumber))
            {
                return returnFileNumber;
            }
            else
            {
                return returnFileNumber;
            }
        }
        else
        {
            return returnFileNumber;
        }
    }
}

[System.Serializable]
public class SavedPlayerData
{
    private Vector3 savedPlayePosition;
    private float playerHealth;
    private float playerMoney;
    private Vector2[] inventoryList;
    private string inventoryString = "";


    public SavedPlayerData(Vector3 playerPosition, float playerHealth, float playerMoney, Vector2[] inventoryList)
    {
        this.savedPlayePosition = playerPosition;
        this.playerHealth = playerHealth;
        this.playerMoney = playerMoney;
        SaveInventory(inventoryList);
    }

    //converts and saves Item data from inventory as string so that it can be saved
    #region Saving Inventory(Vector2[] playerInventoryList)
    private void SaveInventory(Vector2[] playerInventoryList)
    {
        int numOfItems = playerInventoryList.Length;
        this.inventoryList = new Vector2[numOfItems];
        for (int i = 0; i < numOfItems; i++)
        {
            this.inventoryList[i] = playerInventoryList[i];
        }
        inventoryString = ItemListToString(this.inventoryList);
    }

    
    private string ItemListToString(Vector2[] playerInventoryList)
    {
        string itemDataString = "";
        for (int i = 0; i < playerInventoryList.Length; i++)
        {
            itemDataString += "" + playerInventoryList[i] + ":" + "\n";
        }
        return itemDataString;
    }
    #endregion

    //Returns the data as a string by seperating them by a data seperator
    public string SavedString(string dataSeperator)
    {
        string[] saveContent = {
            "" + this.playerHealth,
            "" + this.playerMoney,
            "" + this.savedPlayePosition,
            "" + this.inventoryString
        };

        return string.Join(dataSeperator + "\n", saveContent);
    }
}

