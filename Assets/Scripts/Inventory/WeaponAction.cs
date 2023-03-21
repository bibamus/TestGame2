﻿using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using World;

namespace Inventory
{
    public class WeaponAction : MonoBehaviour
    {
        private const float SwingDuration = 0.4f;
        private bool _facingRight;

        public void StartAction(PlayerManager playerManager, WorldManager worldManager, Item item)
        {
            _facingRight = playerManager.PlayerController.FacingRight;
            StartCoroutine(SwingAndDeactivate(item));
        }

        private IEnumerator SwingAndDeactivate(Item item)
        {
            const float swingAngle = 120f;
            const float swingOffset = 30f;
            const float radius = 1f;
            yield return StartCoroutine(SwingableItem.SwingCoroutine(_facingRight, SwingDuration, transform,
                transform.parent,
                swingAngle, swingOffset, radius));
            item.UseEnd();
        }
    }
}