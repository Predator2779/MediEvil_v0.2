using Character.ComponentContainer;
using Global;
using UnityEngine;

namespace Economy.Items.Souls
{
    public class SoulsHandler
    {
        private PersonContainer _personContainer;
        private Item _droppedSoulsUnit;
        private string _soulName;
        
        public SoulsHandler(PersonContainer personContainer, SoulItem dropSoulsUnitUnit)
        {
            _personContainer = personContainer;
            _droppedSoulsUnit = dropSoulsUnitUnit;
            _soulName = dropSoulsUnitUnit.ItemData.Name;
        }

        public void Subscribe()
        {
            if (_personContainer.SoulWallet != null)
                _personContainer.ItemHandler.OnSoulPickedUp += PickUpSouls;

            EventBus.OnPlayerDied.AddListener(DropSouls);
        }

        public void Unsubscribe()
        {
            if (_personContainer.SoulWallet != null)
                _personContainer.ItemHandler.OnSoulPickedUp -= PickUpSouls;
            
            EventBus.OnPlayerDied.RemoveListener(DropSouls);
        }

        private void PickUpSouls(Item item)
        {
            item.ItemData.Name = _soulName;
            _personContainer.Inventory.AddItems(item);
            _personContainer.SoulWallet.Increase(item.Count);
            _droppedSoulsUnit.gameObject.SetActive(false);
        }
        
        private void DropSouls(PersonContainer container)
        {
            _droppedSoulsUnit.transform.position = _personContainer.transform.position;
            _droppedSoulsUnit.gameObject.SetActive(true);
            _droppedSoulsUnit.Count = _personContainer.Inventory.TryGetItems(_soulName, out ItemSet set) ? set.Count : 0;
            _personContainer.SoulWallet.Decrease(_droppedSoulsUnit.Count);
            Test();
        }

        private void Test()
        {
            if (_personContainer.Inventory.TryGetItems(_soulName, out ItemSet set))
                Debug.Log(set.Count);
        }
    }
}