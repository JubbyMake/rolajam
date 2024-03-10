using Rola.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Rola.UI
{
    public class SwitchMenu : BaseTextUI
    {
        [SerializeField] private TMP_Text _textR;
        [SerializeField] private TMP_Text _textG;
        [SerializeField] private TMP_Text _textB;

        [SerializeField] private TMP_Dropdown _selector;

        private SwitchNode _selectedNode;

        protected override async void Awake()
        {
            base.Awake();

            await Task.Delay(10);

            _textR.color = UIManager.RedTextColour;
            _textG.color = UIManager.GreenTextColour;
            _textB.color = UIManager.BlueTextColour;
        }

        public void StartEdit(SwitchNode selectedNode)
        {
            _selectedNode = selectedNode;

            _textR.text = _selectedNode.GetValue.Red.ToString();
            _textG.text = _selectedNode.GetValue.Green.ToString();
            _textB.text = _selectedNode.GetValue.Blue.ToString();

            _selector.value = (int)_selectedNode.SwitchType;
        }

        public void SetSwitchType(int type) => _selectedNode.SetSwitchType(type);
    }
}
