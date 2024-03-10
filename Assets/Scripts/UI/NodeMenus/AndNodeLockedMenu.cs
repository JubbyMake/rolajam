using Rola.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rola.UI
{
    public class AndNodeLockedMenu : BaseTextUI
    {
        [SerializeField] private TMP_Text _label;

        private AndNode _selectedNode;

        protected override IEnumerator OnAwake()
        {
            yield return base.OnAwake();

            _label.color = UIManager.RegularTextColour;
        }

        public void StartEdit(AndNode selectedNode)
        {
            _selectedNode = selectedNode;

            _label.text = "Is Logical: " + _selectedNode.IsLogical.ToString();
        }
    }
}
