using Rola.Nodes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rola.UI
{
    public class NotNodeLockedMenu : BaseTextUI
    {
        [SerializeField] private TMP_Text _selector;

        private NotNode _selectedNode;

        public void StartEdit(NotNode selectedNode)
        {
            _selectedNode = selectedNode;

            _selector.text = _selectedNode.Type.ToString();
        }
    }
}
