using InventorySpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : UIItem
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TextMeshProUGUI _textAmount;

    public IInventoryItem Item { get; private set; }
    public void Refresh(IInventorySlot slot)
    {
        if (slot.IsEmpty)
        {
            Cleanup();
            return;
        }
        Item = slot.Item;
        _imageIcon.sprite = Item.Info.Icon;
        _imageIcon.gameObject.SetActive(true);

        bool textAmountEnabled = slot.Amount > 1;
        _textAmount.gameObject.SetActive(textAmountEnabled);
        
        if (textAmountEnabled)
        {
            _textAmount.text = slot.Amount.ToString();  
        }
    }
    private void Cleanup()
    {
        _imageIcon.gameObject.SetActive(false);
        _textAmount.gameObject.SetActive(false);
    }
}
