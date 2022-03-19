using UnityEngine;

namespace CardBattle.Core.Collections
{
    [System.Serializable]
    public class Dictionary<T0, T1>
    {
        public T1 this[T0 term] => Find(term);
        public T0 this[T1 term] => Find(term);
        
        public System.Collections.Generic.List<DictionaryPair<T0, T1>> pairs = new System.Collections.Generic.List<DictionaryPair<T0, T1>>();

        public T1 Find(T0 term)
        {
            T1 res = default;
            foreach (DictionaryPair<T0, T1> pair in pairs)
            {
                if (pair.item0.Equals(term))
                {
                    res = pair.item1;
                }
            }
            return res;
        }

        public T0 Find(T1 term)
        {
            T0 res = default;
            foreach (DictionaryPair<T0, T1> pair in pairs)
            {
                if (pair.item1.Equals(term))
                {
                    res = pair.item0;
                }
            }
            return res;
        }

        public DictionaryPair<T0,T1> GetDictionaryPair(int index) => pairs[index];

        public void Add(T0 t0, T1 t1) => pairs.Add(new DictionaryPair<T0, T1> { item0 = t0, item1 = t1 });
    }
}
