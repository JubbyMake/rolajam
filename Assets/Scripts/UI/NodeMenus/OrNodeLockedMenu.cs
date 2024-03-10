using Rola.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rola.UI
{
    public class OrNodeLockedMenu : BaseTextUI
    {
        [SerializeField] private Toggle _toggleLogical;
        [SerializeField] private TMP_Text _labelLogical;
        [SerializeField] private Toggle _toggleExclusive;
        [SerializeField] private TMP_Text _labelExclusive;

        private OrNode _selectedNode;

        protected override async void Awake()
        {
            base.Awake();

            await Task.Delay(10);

            _labelLogical.color = UIManager.RegularTextColour;
            _labelExclusive.color = UIManager.RegularTextColour;
        }

        public void StartEdit(OrNode selectedNode)
        {
            _selectedNode = selectedNode;

            _toggleLogical.isOn = _selectedNode.IsLogical;
            _toggleExclusive.isOn = _selectedNode.IsExclusive;
        }
    }
}
