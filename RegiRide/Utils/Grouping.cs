namespace RegiRide.Utils
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        private readonly IGrouping<TKey, TElement> group;

        public Grouping(IGrouping<TKey, TElement> group)
        {
            this.group = group; 
        }

        public TKey Key
        {
            get
            {
                return this.group.Key;
            }
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return group.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if (obj is Grouping<TKey, TElement>)
            {
                var grouping = obj as Grouping<TKey, TElement>;
                return grouping.Equals(Key);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}