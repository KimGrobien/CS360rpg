using UnityEngine;
using UnityEngine.UI;

// A slot used to hold equipment
public class egoSlot : MonoBehaviour
{   
    // The Image for the sprite
    [SerializeField] Image image;
    private equipment _item;

    // Ceate an equipment item and fill it with the given equipment using setter
    public equipment Item
    {
        get { return _item; }
        set
        {
            _item = value;
            image.sprite = _item.Icon;

            // You have bought the item, show it
            if (_item == null)
            {
                image.enabled = false;
            }

            // If you have not bought it, it will not show
            else
            {
                image.enabled = true;
            }
        }
    }

    private void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
