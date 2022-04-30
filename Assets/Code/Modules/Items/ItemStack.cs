using System;

/*
 * ItemStack is an instance of an Item and the specific quantity of that item.
 * There will only be one instance of each Item and multiple instances of ItemStacks that reference that Item from ItemManager.
 */
public class ItemStack
{

    public Item Item { get; private set; }
    public int Quantity { get; set; }

    public ItemStack(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"{{ItemStack - {Item.ID}, {Quantity}}}";
    }

    #region Operators
    public static ItemStack operator +(ItemStack a, ItemStack b)
    {
        if (a.Item != b.Item)
            throw new InvalidOperationException();

        return new ItemStack(a.Item, a.Quantity + b.Quantity);
    }

    public static ItemStack operator -(ItemStack a, ItemStack b)
    {
        if (a.Item != b.Item)
            throw new InvalidOperationException();

        return new ItemStack(a.Item, a.Quantity - b.Quantity);
    }

    public static ItemStack operator +(ItemStack a, int i) => new ItemStack(a.Item, a.Quantity + i);
    public static ItemStack operator -(ItemStack a, int i) => new ItemStack(a.Item, a.Quantity - i);
    public static ItemStack operator *(ItemStack a, int i) => new ItemStack(a.Item, a.Quantity * i);
    public static ItemStack operator /(ItemStack a, int i) => new ItemStack(a.Item, a.Quantity / i);
    public static ItemStack operator %(ItemStack a, int i) => new ItemStack(a.Item, a.Quantity % i);
    public static bool operator >(ItemStack a, ItemStack b) => a.Quantity > b.Quantity;
    public static bool operator <(ItemStack a, ItemStack b) => a.Quantity < b.Quantity;
    public static bool operator ==(ItemStack a, ItemStack b) => ReferenceEquals(a, null) && ReferenceEquals(b, null) || a.Equals(b);
    public static bool operator !=(ItemStack a, ItemStack b) => !(a == b);
    #endregion

    public override bool Equals(object o)
    {
        if (ReferenceEquals(o, null))
            return false;

        if (o.GetType() != GetType())
            return false;

        var itemStack = (ItemStack) o;

        return Quantity == itemStack.Quantity && Item == itemStack.Item;
    }

    public override int GetHashCode()
    {
        return Item.GetHashCode() + Quantity.GetHashCode();
    }
}
