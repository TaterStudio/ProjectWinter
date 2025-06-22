using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace TaterGame.SnowIsland.Shared
{
    public class ItemService: IInitializable
    {
        private Dictionary<string, ItemSO> m_ItemSos;
        public void Initialize()
        {
            m_ItemSos = Resources.LoadAll<ItemSO>("Item/Melee").ToDictionary(x=>x.Id);
        }

        public bool TryGetItem(string key, out ItemSO so)
        {
            return m_ItemSos.TryGetValue(key, out so);
        }

        public ItemSO GetItem(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return GetFist();
            }   
            return m_ItemSos.GetValueOrDefault(key);
        }

        public ItemSO GetFist()
        {
            return m_ItemSos[Constraints.Item.Fist];
        }
    }
}