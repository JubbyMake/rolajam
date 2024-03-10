using Rola.Levels;
using Rola.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Rola.UI
{
    public sealed class LevelSelectorMenu : MonoBehaviour
    {
        public void OnLevelSelected(int level)
        {
            GameManager.Instance.LoadLevel(level);

            UIManager.Instance.DisableAll();
        }
    }
}
