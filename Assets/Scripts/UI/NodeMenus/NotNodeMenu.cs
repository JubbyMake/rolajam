using Rola.Nodes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rola.UI
{
    public class NotNodeMenu : BaseTextUI
    {
        [SerializeField] private TMP_Dropdown _selector;

        private NotNode _selectedNode;

        public void StartEdit(NotNode selectedNode)
        {
            _selectedNode = selectedNode;

            _selector.value = (int)_selectedNode.Type;
        }

        public void SetNotType(int type) => _selectedNode.SetNotType(type);
    }
}
