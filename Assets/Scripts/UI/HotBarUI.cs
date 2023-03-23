using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class HotBarUI : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private HotBarSlotUI slotPrefab;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        private HotBarSlotUI[] _slots;


        private void Start()
        {
            InitializeHotBar();
        }

        private void Update()
        {
            for (int i = 0; i < playerManager.HotBar.Actions.Length; i++)
            {
                _slots[i].SetHotBarAction(playerManager.HotBar.Actions[i], i == playerManager.HotBar.SelectedActionIndex);
            }
        }

        private void InitializeHotBar()
        {
            _slots = new HotBarSlotUI[playerManager.HotBar.Actions.Length];
            for (int i = 0; i < playerManager.HotBar.Actions.Length; i++)
            {
                var slot = Instantiate(slotPrefab, gridLayoutGroup.transform);
                _slots[i] = slot;
            }
        }
    }
}