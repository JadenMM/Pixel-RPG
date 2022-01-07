using UnityEngine;
using UnityEngine.UI;

public class InventoryItemComponent : MonoBehaviour
{
    public ItemStack ItemStack { get; set; } = null;
    public Sprite Texture { get; set; }
    public Image Image;
    public Text QuantityText;

    public void SetTexture(Sprite texture)
    {
        Texture = texture;
        Image.sprite = texture;
    }

    public void UpdateQuantity()
    {
        var quantity = ItemStack.Quantity;

        Debug.Log("Updating item quantity text with: " + quantity);

        if (quantity <= 1)
            QuantityText.text = "";
        else
            QuantityText.text = ItemStack.Quantity.ToString();
    }


}


