using System.Collections.Generic;
using UnityEngine;

namespace Sources.BaseLogic
{
    public class ListView<TItem> : MonoBehaviour
        where TItem : Component
    {
        [SerializeField] private TItem _prefab;
        [SerializeField] private Transform _container;

        private readonly List<TItem> _items = new();
        private readonly Queue<TItem> _freeList = new();

        public TItem SpawnItem()
        {
            if (_freeList.TryDequeue(out TItem item))
                item.gameObject.SetActive(true);
            else
                item = Instantiate(_prefab, _container);
            
            _items.Add(item);

            return item;
        }

        public void UnspawnItem(TItem item)
        {
            if (item != null && _items.Remove(item))
            {
                item.gameObject.SetActive((false));
                _freeList.Enqueue(item);
            }
        }

        public void Clear()
        {
            for (int i = 0, count = _items.Count; i < count; i++)
            {
                TItem item = _items[i];
                item.gameObject.SetActive(false);
                _freeList.Enqueue(item);
            }
            
            _items.Clear();
        }
    }
}