using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelList : SingletonBehaviour<LevelList>
{
    [SerializeField] GameObject pr_Card;
    [SerializeField] Button startButton;

    SmartToggleGroup toggleGroup;
    List<Card> cards = new List<Card>();

    int count = 1;

    // public LevelSO[] levels_so;
    public LevelData_SO levelData;

    public int Length
    {
        get {
            return levelData.levels.Count;
        }
    }


    private void Awake() {
        toggleGroup = GetComponent<SmartToggleGroup>();

        levelData = Resources.Load<LevelData_SO>("Level/Level Data");
        if (levelData == null) {
            Debug.LogError("LevelList: Resources.Load failed for 'Level/Level Data'.");
            levelData = ScriptableObject.CreateInstance<LevelData_SO>();
            levelData.ResetData();
        }

        if (startButton != null && startButton.GetComponent<StartButtonRaycastGuard>() == null)
            startButton.gameObject.AddComponent<StartButtonRaycastGuard>();

        startButton.onClick.AddListener(StartSelectedLevel);
    }

    /// <summary>Ana menü Start/Play: seçili seviye veya 1. seviye. Inspector veya kod tarafından çağrılabilir.</summary>
    public void StartSelectedLevel() {
        if (toggleGroup == null || GameManager.Instance == null || levelData == null) return;

        if (toggleGroup.Selected != null) {
            var card = toggleGroup.Selected.GetComponent<Card>();
            if (card != null) {
                GameManager.Instance.LoadLevel(card.Id - 1, $"Level {card.Id}");
                return;
            }
        }

        if (levelData.levels.Count > 0)
            GameManager.Instance.LoadLevel(0, "Level 1");
    }

    private void Start() {
        Toggle lastUnlockedLevel = null;

        foreach(var level in levelData.levels){
            Toggle t = Instantiate(pr_Card, transform).GetComponent<Toggle>();
            toggleGroup.Add(t);

            var card = t.gameObject.GetComponent<Card>();
            card.Initialize(level);
            cards.Add(card);

            count++;

            if(!level.isLocked) lastUnlockedLevel = t;
        }

        if(lastUnlockedLevel != null)
        lastUnlockedLevel.isOn = true;
    }

    public void UnlockIfLocked(int index){
        // Update unlock in db
        levelData.UnlockedLevel(index);

        // reflect changes on UI
        cards[index].UnlockCardIfNot();
    }
}