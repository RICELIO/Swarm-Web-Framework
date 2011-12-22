using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SimpleVelocity.Adapters
{
    public class DictionaryAdapter : Adapter
    {
        internal DictionaryAdapter() { }

        #region Methods

        public override object GetItem(object provider, int indexValue, object valueDEFAULT)
        {
            return this.GetItem(provider, indexValue, valueDEFAULT);
        }
        public override object GetItem(object provider, string nameValue, object valueDEFAULT)
        {
            return this.GetItem(provider, nameValue, valueDEFAULT);
        }

        public int GetLastIndexItem(object provider)
        {
            DictionaryBase obj = provider as DictionaryBase;
            return obj.Count - 1; // Because zero pos.
        }

        private object GetItem(object provider, object value, object valueDEFAULT)
        {
            IDictionary obj = provider as IDictionary;
            IDictionaryEnumerator enumerator = obj.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.Equals(value))
                    return enumerator.Value == null || string.IsNullOrEmpty(enumerator.ToString()) ?
                           valueDEFAULT : enumerator.Value;
            }
            return valueDEFAULT;
        }

        #endregion
    }
}
