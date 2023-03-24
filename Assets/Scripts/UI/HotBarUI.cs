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
        [FormerlySerializedAs("playerManager")] [SerializeField] private PlayerEntity playerEntity;
        [SerializeField] private HotBarSlotUI slotPrefab;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        private HotBarSlotUI[] _slots;


        private void Start()
        {
            InitializeHotBar();
        }

        private void Update()
        {
            for (int i = 0; i < playerEntity.HotBar.Actions.Length; i++)
            {
                _slots[i].SetHotBarAction(playerEntity.HotBar.Actions[i], i == playerEntity.HotBar.SelectedActionIndex);
            }
        }

        private void InitializeHotBar()
        {
            _slots = new HotBarSlotUI[playerEntity.HotBar.Actions.Length];
            for (int i = 0; i < playerEntity.HotBar.Actions.Length; i++)
            {
                var slot = Instantiate(slotPrefab, gridLayoutGroup.transform);
                _slots[i] = slot;
            }
        }
    }
}