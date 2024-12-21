using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Level Data", menuName = "ScriptableObjects/LevelData")]
public class LevelData_SO : ScriptableObject
{
    [Header("General Settings")]
    public Sprite thumbnailNotFound;
    public List<LevelInfo> levels = new List<LevelInfo>();

    private const string UNLOCKED_LEVEL = "UNLOCKED_LEVEL";
    private const string LOCKED_SIGN = "1";
    private const string UNLOCKED_SIGN = "0";

    private GameObject[] prefabs;
    private Sprite[] thumbnails;
    private bool[] isLocked;

    private bool noLevelDataFound = true;

    private void Awake()
    {
        LoadUnlockedLevels();
        AutoFillLevelData();
    }

    private void LoadUnlockedLevels()
    {
        string unlockedLevels = PlayerPrefs.GetString(UNLOCKED_LEVEL);
        noLevelDataFound = string.IsNullOrEmpty(unlockedLevels);

        if (!noLevelDataFound)
        {
            var unlockedArray = unlockedLevels.Split(' ');
            isLocked = new bool[unlockedArray.Length];
            for (int i = 0; i < unlockedArray.Length; i++)
            {
                isLocked[i] = unlockedArray[i] == LOCKED_SIGN;
            }
        }
    }

    public void AutoFillLevelData()
    {
        levels.Clear();

        // Load prefabs and thumbnails
        prefabs = Resources.LoadAll<GameObject>("Level/L_Prefab");
        Array.Sort(prefabs, new ObjectNameComparer());

        thumbnails = Resources.LoadAll<Sprite>("Level/L_Thumbnail");
        Array.Sort(thumbnails, new ObjectNameComparer());

        // Initialize levels
        StringBuilder rawString = new StringBuilder();

        for (int i = 0; i < prefabs.Length; i++)
        {
            LevelInfo level = new LevelInfo
            {
                prefab = prefabs[i],
                id = i + 1,
                thumbnail = (thumbnails != null && i < thumbnails.Length) ? thumbnails[i] : thumbnailNotFound
            };

            if (noLevelDataFound)
            {
                // Default locking behavior
                level.isLocked = i != 0; // First level unlocked by default
                rawString.Append(i == 0 ? UNLOCKED_SIGN : LOCKED_SIGN).Append(" ");
            }
            else
            {
                level.isLocked = isLocked[i];
            }

            levels.Add(level);
        }

        if (noLevelDataFound)
        {
            PlayerPrefs.SetString(UNLOCKED_LEVEL, rawString.ToString().Trim());
        }
    }

    public void UnlockedLevel(int index)
    {
        string[] data = PlayerPrefs.GetString(UNLOCKED_LEVEL).Split(" ");
        if (index < 0 || index >= data.Length) return; // Geçersiz index kontrolü
        if (data[index].Equals(UNLOCKED_SIGN)) return;

        data[index] = UNLOCKED_SIGN;
        PlayerPrefs.SetString(UNLOCKED_LEVEL, string.Join(" ", data));
        levels[index].isLocked = false;
    }


    public void ResetData()
    {
        Awake();
    }
}

[Serializable]
public class LevelInfo
{
    public int id;
    public Sprite thumbnail;
    public GameObject prefab;
    public bool isLocked = true;
}

class ObjectNameComparer : IComparer
{
    public int Compare(object x, object y)
    {
        if (x == null || y == null)
        {
            Debug.LogError("Null values found in comparison!");
            return 0;
        }

        try
        {
            string xName = ((UnityEngine.Object)x).name;
            string yName = ((UnityEngine.Object)y).name;

            int xId = int.Parse(xName.Split('_')[1]);
            int yId = int.Parse(yName.Split('_')[1]);

            return xId.CompareTo(yId);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in ObjectNameComparer: {ex.Message}");
            return 0;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LevelData_SO))]
public class LevelDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelData_SO script = (LevelData_SO)target;

        if (GUILayout.Button("Update Data"))
        {
            script.ResetData();
            script.AutoFillLevelData();
        }
    }
}
#endif
