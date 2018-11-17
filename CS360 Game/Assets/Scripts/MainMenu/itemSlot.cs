using UnityEngine;
using UnityEngine.UI;

public class itemSlot : Button {

    [SerializeField] Image image;
    private equipment _item;

    public equipment Item {
        get { return _item; }
        set {
            _item = value;

            if (!_item.owned)
            {
                image.sprite = _item.Icon;
                image.enabled = false;
            } else {
                image.sprite = _item.Icon;
                image.enabled = true;
            }
        }
    }

    void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
