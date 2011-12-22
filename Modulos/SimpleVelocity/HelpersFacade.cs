using System;
using System.Text;
using SimpleVelocity.Adapters;

namespace SimpleVelocity
{
    public class Helpers
    {
        public ListAdapter List { get; private set; }
        public DictionaryAdapter Dictionary { get; private set; }

        public Helpers()
        {
            this.List = new ListAdapter();
            this.Dictionary = new DictionaryAdapter();
        }
    }
}
