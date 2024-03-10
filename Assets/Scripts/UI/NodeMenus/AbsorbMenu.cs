using Rola.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Rola.UI
{
    public class AbsorbMenu : BaseTextUI
    {
        [SerializeField] private TMP_Text _textR;
        [SerializeField] private TMP_Text _textG;
        [SerializeField] private TMP_Text _textB;

        private ValueAbsorbNode _selectedNode;

        protected override IEnumerator OnAwake()
        {
            yield return base.OnAwake();

            _textR.color = UIManager.RedTextColour;
            _textG.color = UIManager.GreenTextColour;
            _textB.color = UIManager.BlueTextColour;
        }

        public void StartEdit(ValueAbsorbNode selectedNode)
        {
            _selectedNode = selectedNode;

            _textR.text = _selectedNode.ToAbsorb.Red.ToString();
            _textG.text = _selectedNode.ToAbsorb.Green.ToString();
            _textB.text = _selectedNode.ToAbsorb.Blue.ToString();
        }
    }
}
