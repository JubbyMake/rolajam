using Rola.Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rola.Nodes
{
    //vfx(particle) is so tragic
    public sealed class LogicInNode : Node
    {
        [SerializeField] private MeshRenderer _nodeMesh;

        private bool _isHovered;

        public event Action OnInputUpdated;

        public static ParticleSystem ClickedEffect;
        public static bool WaitingInput = false;
        public static LogicInNode Current = null;

        public bool IsConnected { get; private set; }

        public override void UpdateValue(NodeValue newValue)
        {
            base.UpdateValue(newValue);

            OnInputUpdated?.Invoke();
        }

        private void OnMouseDown()
        {
            if(IsConnected || WaitingInput)
                return;

            WaitingInput = true;
            Current = this;

            var temp = AudioPool.GetSource();

            temp.Stop();

            temp.clip = GameManager.GetButtonPress;
            temp.volume = 1f;
            temp.gameObject.SetActive(true);

            if(LogicOutNode.WaitingInput)
            {
                LogicOutNode.Current.CreateWire(this);

                WaitingInput = false;
                Current = null;

                LogicOutNode.WaitingInput = false;
                LogicOutNode.Current = null;

                if(ClickedEffect != null)
                {
                    ClickedEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);

                    ClickedEffect = null;
                }

                if(LogicOutNode.ClickedEffect != null)
                {
                    LogicOutNode.ClickedEffect.Stop(true,
                        ParticleSystemStopBehavior.StopEmitting);

                    LogicOutNode.ClickedEffect = null;
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

        public void SetConnected(bool connected) => IsConnected = connected;
    }
}
