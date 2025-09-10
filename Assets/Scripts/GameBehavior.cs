using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{

    public Stack<string> lootStack = new Stack<string>();

    public bool showWinScreen = false;

    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;

    private int _itemsCollected = 0;
    private string _state;

    public bool showLossScreen = false;

    public string labelText = "Collect all 4 items and win yourfreedom!";
    public int maxItems = 1; //количество предметов, которое нужно собрать дл€ победы

    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new InventoryList<string>();

        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }

    public void Initialize()
    {
        _state = "Manager initialized..";
        //_state.FancyDebug();
        //Debug.Log(_state);
        debug(_state);
        LogWithDelegate(debug);

        GameObject player = GameObject.Find("Player");
        PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
        playerBehavior.playerJump += HandlePlayerJump;

        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");
        lootStack.Push("Last");
    }

    public void End()
    {
        Time.timeScale = 0f;
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        if (lootStack.Count == 0)
        {
            return;
        }
        var nextItem = lootStack.Peek();

        Debug.LogFormat("You got a {0}! YouТve got a good chance of finding a {1} next!", currentItem, nextItem);

        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
    }

    public int Items
    {
        get 
        { 
            return _itemsCollected;
        }

        set
        {
            _itemsCollected = value;
            Debug.LogFormat("Items: {0}", _itemsCollected);

            //проверка, собрали ли мы все предметы
            if (_itemsCollected >= maxItems)
            {
                labelText = "YouТve found all the items!";
                showWinScreen = true; //все предметы собраны, победа
                //Time.timeScale = 0f;
                End();
            }
            else
            {
                labelText = "Item found, only " +
                (maxItems - _itemsCollected) + " more to go!";
            }

        }
    }

    private int _playerHP = 3;

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;

            if (_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                //Time.timeScale = 0;
                End();
            }
            else
            {
                labelText = "Ouch... thatТs got hurt.";
            }
            //Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

    void OnGUI()
    {
        // 4
        GUI.Box(new Rect(20, 20, 150, 25),
        "Player Health:" + _playerHP);
        // 5
        GUI.Box(new Rect(20, 50, 150, 25),
        "Items Collected: " + _itemsCollected);
        // 6
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50,
        300, 50), labelText);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
            Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                // 1
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully...");
                }
                // 2
                catch (System.ArgumentException e)
                {
                    // 3
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene 0: " + e.ToString());
                }
                // 4
                finally
                {
                    debug("Restart handled...");
                }
            }
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
            Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                Utilities.RestartLevel(0);
            }
        }

    }
}
