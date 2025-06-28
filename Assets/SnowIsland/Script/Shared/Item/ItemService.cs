using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace TaterGame.SnowIsland.Shared
{
    public class ItemService: IInitializable
    {
        private Dictionary<string, ItemSO> m_ItemSos;
        private Dictionary<int, ItemSO> m_HashItemSos;
        public IEnumerable<string> ItemKeys { get=>m_ItemSos.Keys;}

        public void Initialize()
        {
            var loadAll = Resources.LoadAll<ItemSO>("Item/");
            m_ItemSos = loadAll.ToDictionary(x=>x.Id);
            m_HashItemSos = loadAll.ToDictionary(x => x.Id.GetHashCode());
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
        
        public ItemSO GetItem(int key)
        {
            if (key == 0)
            {
                return GetFist();
            }   
            return m_HashItemSos.GetValueOrDefault(key);
        }

        public ItemSO GetFist()
        {
            return m_ItemSos[Constraints.Item.Fist];
        }
    }
}