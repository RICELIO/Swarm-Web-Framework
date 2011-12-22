using System;
using System.Text;

namespace SimpleVelocity.Adapters
{
    public abstract class Adapter : IAdapter
    {
        public Adapter() { }

        #region Methods

        public abstract object GetItem(object provider, int indexValue, object itemDEFAULT);
        public abstract object GetItem(object provider, string nameValue, object itemDEFAULT);

        public object GetItem(object provider, int indexValue)
        {
            return GetItem(provider, indexValue, null);
        }
        public object GetItem(object provider, string nameValue)
        {
            return GetItem(provider, nameValue, null);
        }

        protected object GetValue(object item, object itemDEFAULT)
        {
            if (itemDEFAULT == null) return item;
            return item == null || string.IsNullOrEmpty(item.ToString()) ? itemDEFAULT : item;
        }

        #endregion
    }
}
