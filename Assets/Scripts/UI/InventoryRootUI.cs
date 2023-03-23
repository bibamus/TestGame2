using System;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class InventoryRootUI : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
