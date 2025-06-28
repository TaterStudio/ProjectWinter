using System;
using System.Collections.Generic;
using System.IO;
using TaterGame.SnowIsland.Shared;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TaterGame.SnowIsland.Editor
{
    public class ItemCreator: EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        private const string m_ResRootPath = "Assets/SnowIsland/Resources/Item/";

        private Dictionary<string, string> m_ItemNameToPath = new()
        {
            { "食物", "Food" }, { "饮料", "Drink" }, { "放置物", "Placement" },
            { "远程", "Ranged" }, { "近战", "Melee" }, { "投掷物", "Throwable" },
            { "装备", "Equipment" }, { "材料", "Material" }
        };

        private string m_Path;

        [MenuItem("Tools/TaterGame/ProjectWinter/ItemCreator")]
        public static void ShowExample()
        {
            ItemCreator wnd = GetWindow<ItemCreator>();
            wnd.titleContent = new GUIContent("ItemCreator");
        }

        private DropdownField m_Category;
        private Label m_Debug;
        private TextField m_ItemName;
        private Button m_Invoke;

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // VisualElements objects can contain other VisualElement following a tree hierarchy.
            VisualElement label = new Label("Hello World! From C#");
            root.Add(label);

            // Instantiate UXML
            VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
            root.Add(labelFromUXML);
            m_Category = labelFromUXML.Q<DropdownField>("Category");
            m_Debug = labelFromUXML.Q<Label>("Debug");
            m_Category.RegisterValueChangedCallback((evt) =>
            {
                if (m_ItemNameToPath.TryGetValue(evt.newValue, out string path))
                {
                    m_Path = Path.Combine(m_ResRootPath, path);
                    m_Debug.text = $"当前类别{evt.newValue}路径{m_Path}";
                }
                else
                {
                    m_Debug.text = $"未知类别{evt.newValue}";
                }
            });
            m_ItemName = labelFromUXML.Q<TextField>("ItemName");
            m_Invoke = labelFromUXML.Q<Button>("Invoke");
            m_Invoke.clicked += InvokeSpawn;
        }

        private void InvokeSpawn()
        {
            var itemNameValue = m_ItemName.value;
            var assetPath = Path.Combine(m_Path, itemNameValue + ".asset");
            var assetDataPath = Path.Combine(m_Path, itemNameValue + "_Data.asset");
            if (AssetDatabase.LoadAssetAtPath<ItemSO>(assetPath))
            {
                m_Debug.text = $"错误{assetPath}已有资产";
                return;
            }

            var itemSo = CreateInstance<ItemSO>();
            var itemData = CreateItemDataSO(m_Category.value);
            itemSo.SetInitialData(itemNameValue, $"物品_名字_{itemNameValue}", $"物品_介绍_{itemNameValue}", itemData);
            AssetDatabase.CreateAsset(itemData, assetDataPath);
            AssetDatabase.CreateAsset(itemSo, assetPath);
        }

        public ItemDataSO CreateItemDataSO(string itemType)
        {
            switch (itemType)
            {
                case "饮料":
                case "食物":
                    var itemFoodDataSo = CreateInstance<ItemFoodDataSO>();
                    return itemFoodDataSo;
                case "远程":
                    var itemRangedDataSo = CreateInstance<ItemRangedDataSO>();
                    return itemRangedDataSo;
                case "近战":
                    var itemMeleeDataSo = CreateInstance<ItemMeleeDataSO>();
                    itemMeleeDataSo.SetInitialData(1, 0.5f, 80);
                    return itemMeleeDataSo;
                case "投掷物":
                    var itemThrowableDataSo = CreateInstance<ItemThrowableDataSO>();
                    return itemThrowableDataSo;
                case "放置物":
                    var itemPlaceableDataSo = CreateInstance<ItemPlaceableDataSO>();
                    return itemPlaceableDataSo;
                case "装备":
                    var itemEquipmentDataSo = CreateInstance<ItemEquipmentDataSO>();
                    return itemEquipmentDataSo;
                case "材料":
                    var itemMaterialDataSo = CreateInstance<ItemDataSO>();
                    return itemMaterialDataSo;
            }

            throw new Exception("未知类别");
        }
    }
}