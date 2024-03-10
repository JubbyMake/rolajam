using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    public sealed class NodeShaderHandler : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _litMat;

        private Node _node;

        private void Awake()
        {
            _node = GetComponent<Node>();
        }

        //tragic
        private void Update()
        {
            if(_litMat == null)
                return;

            var temp = new Color(
                _node.GetValue.Red,
                _node.GetValue.Green,
                _node.GetValue.Blue, 1f);

            _litMat.material.SetColor("_nodevalue", temp);
        }
    }
}
