using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static List<LevelData> LevelsData { get; private set; }

    [SerializeField] private List<LevelData> _levelsData;

    private void Awake()
    {
        LevelsData = _levelsData;

#if UNITY_EDITOR
        foreach (var data in LevelsData)
        {
            data.IsCompleted = false;
        }
#endif
    }

    public static void LoadLevel(LevelData data)
    {
        SceneManager.LoadSceneAsync(data.BuildIndex, LoadSceneMode.Single);
    }

    public static void SetLevelCompleted(string name)
    {
        foreach (var data in LevelsData)
        {
            if (data.Name == name)
            {
                data.IsCompleted = true;
            }
        }
    }

    public static LevelData GetLevelData(int buildIndex)
    {
        foreach (var data in LevelsData)
        {
            if(data.BuildIndex == buildIndex)
            {
                var dataInstance = ScriptableObject.CreateInstance<LevelData>();

                dataInstance = data;

                return dataInstance;
            }
        }

        return null;
    }

    public static LevelData GetLevelData(string name)
    {
        foreach (var data in LevelsData)
        {
            if (data.Name == name)
            {
                var dataInstance = ScriptableObject.CreateInstance<LevelData>();

                dataInstance = data;

                return dataInstance;
            }
        }

        return null;
    }
}