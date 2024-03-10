using Rola.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Rola.UI
{
    public class ValueInMenu : BaseTextUI
    {
        [SerializeField] private TMP_Text _textR;
        [SerializeField] private TMP_Text _textG;
        [SerializeField] private TMP_Text _textB;

        [SerializeField] private TMP_Text _textType;

        private ValueInNode _selectedNode;

        protected override async void Awake()
        {
            base.Awake();

            await Task.Delay(10);

            _textR.color = UIManager.RedTextColour;
            _textG.color = UIManager.GreenTextColour;
            _textB.color = UIManager.BlueTextColour;
            _textType.color = UIManager.RegularTextColour;
        }

        public void StartEdit(ValueInNode selectedNode)
        {
            _selectedNode = selectedNode;

            _textR.text = _selectedNode.RequiredValue.Red.ToString();
            _textG.text = _selectedNode.RequiredValue.Green.ToString();
            _textB.text = _selectedNode.RequiredValue.Blue.ToString();

            _textType.text = _selectedNode.Type.ToString();
        }
    }
}
