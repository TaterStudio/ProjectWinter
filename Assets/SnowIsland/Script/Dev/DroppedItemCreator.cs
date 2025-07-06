using System;
using Sirenix.OdinInspector;
using TaterGame.SnowIsland.Shared;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TaterGame.SnowIsland.Dev
{
    public class DroppedItemCreator: MonoBehaviour
    {

        public DroppedItem PrefabRoot;
        public CreateInfo[] CreateInfos;
        public string Root = "Assets/SnowIsland/GamePrefab/ItemDropped/Generated";

        [Button("创建")]
        public void Create()
        {
            foreach (var createInfo in CreateInfos)
            {
                var prefabNew = (DroppedItem)PrefabUtility.InstantiatePrefab(PrefabRoot);
                var transform1 = Object.Instantiate(createInfo.ArtPrefab).transform;
                transform1.SetParent(prefabNew.transform);
                transform1.localPosition = Vector3.zero;
                prefabNew.SetBindItemEditor(createInfo.Item);
                prefabNew.transform.localPosition = Vector3.zero;
                var newestprefab = PrefabUtility.SaveAsPrefabAsset(prefabNew.gameObject, Root + $"/{createInfo.Item.name}.prefab");
                createInfo.Item.SetDropPrefabEditor(newestprefab);
            }
        }
        
        [Serializable]
        public struct CreateInfo
        {
            public GameObject ArtPrefab;
            public ItemSO Item;
        }
    }
}