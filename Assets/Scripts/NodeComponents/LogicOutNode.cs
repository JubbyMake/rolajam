using Rola.Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    public sealed class LogicOutNode : Node
    {
        [SerializeField] private Wire _outWire;
        [SerializeField] private MeshRenderer _nodeMesh;

        private bool _isHovered;

        public static ParticleSystem ClickedEffect;
        public static bool WaitingInput = false;
        public static LogicOutNode Current = null;

        private void Awake()
        {
            if(_outWire != null)
                _outWire.Init(transform.position);
        }

        public override void UpdateValue(NodeValue newValue)
        {
            base.UpdateValue(newValue);

            if(_outWire != null)
                _outWire.UpdateValue(newValue);
        }

        private void OnMouseDown()
        {
            if(_outWire != null || WaitingInput)
                return;

            WaitingInput = true;
            Current = this;

            var temp = AudioPool.GetSource();

            temp.Stop();

            temp.clip = GameManager.GetButtonPress;
            temp.volume = 1f;
            temp.gameObject.SetActive(true);

            if(LogicInNode.WaitingInput)
            {
                CreateWire(LogicInNode.Current);

                WaitingInput = false;
                Current = null;

                LogicInNode.WaitingInput = false;
                LogicInNode.Current = null;

                if(ClickedEffect != null)
                {
                    ClickedEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);

                    ClickedEffect = null;
                }

                if(LogicInNode.ClickedEffect != null)
                {
                    LogicInNode.ClickedEffect.Stop(true,
                        ParticleSystemStopBehavior.StopEmitting);

                    LogicInNode.ClickedEffect = null;
                }

                return;
            }

            OnClickedEffect();
        }

        private void OnMouseEnter()
        {
            _isHovered = true;

            OnMouseOverEffect();
        }

        private void OnMouseOver()
        {
            if(_isHovered)
                return;

            _isHovered = true;

            OnMouseOverEffect();
        }

        private void OnMouseExit()
        {
            _isHovered = false;

            if(_nodeMesh == null)
                return;

            _nodeMesh.material.SetColor("_EmissionColor",
                GameManager.GetMetalDefault);
        }

        private void OnMouseOverEffect()
        {
            if(_nodeMesh == null)
                return;

            _nodeMesh.material.SetColor("_EmissionColor",
                GameManager.GetMetalHovered);
        }

        private void OnClickedEffect()
        {
            if(ClickedEffect != null)
            {
                ClickedEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);

                ClickedEffect = null;
            }

            ClickedEffect = Instantiate(GameManager.GetWireSelectEffect,
                transform).GetComponent<ParticleSystem>();
            ClickedEffect.transform.position += Vector3.up;
        }

        public void CreateWire(LogicInNode next)
        {
            var temp = Instantiate(GameManager.GetWirePrefab,
                transform)
                .GetComponent<Wire>();

            temp.Init(next, transform.position, next.transform.position);

            _outWire = temp;
            _outWire.UpdateValue(_value);
        }
    }
}
