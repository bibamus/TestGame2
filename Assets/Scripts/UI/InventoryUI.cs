using System;
using UnityEngine;
using UnityEngine.UI;
using Inventory;
using Player;
using UI;
using Unity.VisualScripting;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private GameObject inventorySlotPrefab;

    private PlayerManager _playerManager;

    private void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        gameObject.SetActive(false);
    }


    public void UpdateUI()
    {
        // Clear the current UI
        foreach (Transform child in gridLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        // Get the inventory slots
        InventorySlot[] slots = _playerManager.Inventory.GetSlots();

        // Create UI elements for each slot
        for (int i = 0; i < slots.Length; i++)
        {
            GameObject slotObj = Instantiate(inventorySlotPrefab, gridLayoutGroup.transform);
            InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();
            slotUI.SetSlot(slots[i]);
        }
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        if (inventoryPanel.activeSelf)
        {
            UpdateUI();
        }
    }
}