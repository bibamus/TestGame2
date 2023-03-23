using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class HotBarSlotUI : MonoBehaviour
    {
        private IHotBarAction _hotBarAction;

        [SerializeField] private Image hotBarActionImage;
        [SerializeField] private Image activeImage;


        public void SetHotBarAction(IHotBarAction hotBarAction, bool active)
        {
            _hotBarAction = hotBarAction;
            hotBarActionImage.enabled = hotBarAction != null;
            activeImage.enabled = active;
            if (hotBarAction != null)
            {
                hotBarActionImage.sprite = hotBarAction.Sprite();
            }
        }
    }
}