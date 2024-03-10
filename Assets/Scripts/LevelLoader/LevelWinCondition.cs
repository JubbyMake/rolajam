using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rola.Nodes;

namespace Rola.Levels
{
    public sealed class LevelWinCondition : MonoBehaviour
    {
        private ValueInNode _matchNode;
        private ValueAbsorbNode _absorbNode;

        private void Awake()
        {
            _matchNode = GetComponent<ValueInNode>();
            _absorbNode = GetComponent<ValueAbsorbNode>();

            if(_matchNode != null)
                _matchNode.SetLock(true);

            if(_absorbNode != null)
                _absorbNode.SetLock(true);
        }

        public bool Evaluate()
        {
            if(_matchNode != null)
                return _matchNode.IsCorrect;

            if(_absorbNode != null)
                return _absorbNode.IsCorrect;

            return false;
        }
    }
}
