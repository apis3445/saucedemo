using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using sauceDemo.Components;

namespace sauceDemo.Base
{
    public class ItemComparer : IEqualityComparer<InventoryItem>
    {
        public bool Equals(InventoryItem x, InventoryItem y)
        {
            if (object.ReferenceEquals(x, y)) return true;

            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

            return x.Name == y.Name && x.Price == y.Price;
        }

        public int GetHashCode([DisallowNull] InventoryItem obj)
        {
            if (object.ReferenceEquals(obj, null)) return 0;

            int hashCodeName = obj.Name == null ? 0 : obj.Name.GetHashCode();
            int hasCodePrice = obj.Price.GetHashCode();
            
            return hashCodeName ^ hasCodePrice;
        }
    }
}
