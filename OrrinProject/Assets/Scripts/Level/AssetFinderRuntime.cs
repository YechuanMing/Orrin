using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetFinderRuntime : MonoBehaviour
{
    public static T FindAssetByName<T>(string assetName, string path) where T : Object
    {
        // 拼接完整路径
        string fullPath = $"{path}/{assetName}";
        T asset = Resources.Load<T>(fullPath);

        if (asset == null)
        {
            Debug.LogWarning($"Asset with name '{assetName}' not found in Resources/{path}");
        }

        return asset;
    }
}
