using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace sauceDemo.Base;

public class ItemComparer : IEqualityComparer<ItemPrice>
{
    public bool Equals(ItemPrice x, ItemPrice y)
    {
        if (ReferenceEquals(x, y)) return true;

        if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;

        return x.Name == y.Name && x.Price == y.Price;
    }

    public int GetHashCode([DisallowNull] ItemPrice obj)
    {
        if (ReferenceEquals(obj, null)) return 0;

        int hashCodeName = obj.Name == null ? 0 : obj.Name.GetHashCode();
        int hasCodePrice = obj.Price.GetHashCode();
        
        return hashCodeName ^ hasCodePrice;
    }

}
