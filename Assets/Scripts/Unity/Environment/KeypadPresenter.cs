using System.Collections;
using System.Collections.Generic;
using Core.Environment.Abstractions;
using TMPro;
using UnityEngine;

namespace Unity.Environment
{
    public class KeypadPresenter : MonoBehaviour, IKeypadPresenter
    {
        [SerializeField] private TextMeshProUGUI _text;
        public void ClearDisplay()
        {
            _text.text = "";
        }

        public void UpdateDisplay(List<int> entry)
        {
            string displayText = "";
            for (int i = 0; i < entry.Count; i++)
            {
                displayText += entry[i].ToString();
            }
            _text.text = displayText;
        }
    }
}