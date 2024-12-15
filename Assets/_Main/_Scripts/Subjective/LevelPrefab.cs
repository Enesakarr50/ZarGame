using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelPrefab : MonoBehaviour
{
    #region  Variable
    //------------------------------------//

    [HideInInspector] public Transform playerStart;
    [Header("Config")]
    [SerializeField] public int moves;
    [SerializeField] public int CurrentLevelIndex;
    public int totalEndTiles; // Elle girilecek EndTile sayýsý
    // [Range(1,6)]
    // [SerializeField] public int diceUpNumber = 1;

    Tile[] tiles;

    public bool Completed{
        get
        {
            bool allDone = true;

            foreach (var tile in tiles)
            {
                allDone = allDone && tile.isActive;
                if(!allDone) break;
            }
            return allDone;
        }
    }

    [HideInInspector]
    public UnityEvent onObjectiveComplete;

    //------------------------------------//
    #endregion

    private void Start()
    {
        // Tüm "EndTile" taglý objeleri say
        totalEndTiles = GameObject.FindGameObjectsWithTag("EndTile").Length;
        Debug.Log("Total EndTile: " + totalEndTiles);
    }

    public void EndTileCompleted()
    {
        LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
        if (levelLoader != null)
        {
            levelLoader.EndTileCompleted();
        }
    }


    #region  Unity Method
    //------------------------------------//

    private void Awake() {
        tiles = GetComponentsInChildren<Tile>();

        foreach (var tile in tiles)
        {
            tile.onFill.AddListener(OnAnyTileFill);
        }

        playerStart = transform.GetChild(0);
    }

    //------------------------------------//
    #endregion



    #region  Private
    //------------------------------------//
    
    private void OnAnyTileFill(){
        if(Completed){
            onObjectiveComplete.Invoke();
        }
    }

    //------------------------------------//
    #endregion
    
}
