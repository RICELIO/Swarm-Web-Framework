using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SimpleVelocity.Adapters
{
    public class ListAdapter : Adapter
    {
        internal ListAdapter() { }

        #region Methods

        public override object GetItem(object provider, int indexValue, object valueDEFAULT)
        {
            ArrayList list = ArrayList.Adapter((IList)provider);
            return base.GetValue(list[indexValue], valueDEFAULT);
        }
        public override object GetItem(object provider, string nameValue, object valueDEFAULT)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
