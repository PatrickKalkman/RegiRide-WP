namespace RegiRide.Utils
{
    using System.Collections.ObjectModel;

    public class LongListItem<T, TKey> : ObservableCollection<T>
    {
        public LongListItem()
        {
        }

        public LongListItem(TKey key)
        {
            this.Key = key;
        }

        public TKey Key
        {
            get;
            set;
        }

        public bool HasItems
        {
            get
            {
                return this.Count > 0;
            }
        }
    }
}