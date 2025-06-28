using UnityEngine;
using UnityEngine.Serialization;

namespace TaterGame.SnowIsland.Shared
{
    [CreateAssetMenu(menuName = "TaterGame/SnowIsland/Item")]
    public class ItemSO: ScriptableObject
    {
        [SerializeField]
        private GameObject m_ModelPrefab;

        [SerializeField]
        private string m_DisplayName;

        [SerializeField]
        private string m_Description;

        [Header("耐久度，-1则为无耐久")]
        [SerializeField]
        private int m_Durability = -1;

        [Header("可堆叠,1~3")]
        [Range(1,3)]
        [SerializeField]
        private int m_StackSize = 1;

        [SerializeField]
        private ItemDataSO m_ItemDataSo;

        public GameObject ModelPrefab
        {
            get { return m_ModelPrefab; }
        }

        public string DisplayName
        {
            get { return m_DisplayName; }
        }

        public string Description
        {
            get { return m_Description; }
        }

        public int Durability
        {
            get { return m_Durability; }
        }

        public int StackSize
        {
            get { return m_StackSize; }
        }
        
        public ItemDataSO ItemDataSo
        {
            get { return m_ItemDataSo; }
        }
        
        public string Id => name;

        public void SetInitialData(string name, string displayName, string description,ItemDataSO itemDataSo)
        {
            this.name = name;
            m_DisplayName = displayName;
            m_Description = description;
            m_ItemDataSo = itemDataSo;
        }
    }
}