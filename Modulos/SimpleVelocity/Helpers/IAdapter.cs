using System;
using System.Text;

namespace SimpleVelocity.Adapters
{
    internal interface IAdapter
    {
        object GetItem(object provider, int indexValue);
        object GetItem(object provider, string nameValue);
    }
}
