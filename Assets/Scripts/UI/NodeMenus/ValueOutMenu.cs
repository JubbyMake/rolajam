using Rola.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rola.UI
{
    public class ValueOutMenu : BaseTextUI
    {
        [SerializeField] private TMP_Text _textR;
        [SerializeField] private TMP_Text _textG;
        [SerializeField] private TMP_Text _textB;

        [SerializeField] private Slider _sliderR;
        [SerializeField] private Slider _sliderG;
        [SerializeField] private Slider _sliderB;

        private ValueOutNode _selectedNode;

        protected override async void Awake()
        {
            base.Awake();

            await Task.Delay(10);

            _textR.color = UIManager.RedTextColour;
            _textG.color = UIManager.GreenTextColour;
            _textB.color = UIManager.BlueTextColour;
        }

        public void StartEdit(ValueOutNode selectedNode)
        {
            _selectedNode = selectedNode;

            UpdateValues();

            _sliderR.value = _selectedNode.GetValue.Red;
            _sliderG.value = _selectedNode.GetValue.Green;
            _sliderB.value = _selectedNode.GetValue.Blue;
        }

        public void ChangeRedSlider(float red)
        {
            _selectedNode.SetValue(new Vector3(
                (float)Math.Round(red, 1),
                _selectedNode.GetValue.Green,
                _selectedNode.GetValue.Blue));

            UpdateValues();
        }

        public void ChangeGreenSlider(float green)
        {
            _selectedNode.SetValue(new Vector3(
                _selectedNode.GetValue.Red,
                (float)Math.Round(green, 1),
                _selectedNode.GetValue.Blue));

            UpdateValues();
        }

        public void ChangeBlueSlider(float blue)
        {
            _selectedNode.SetValue(new Vector3(
                _selectedNode.GetValue.Red,
                _selectedNode.GetValue.Green,
                (float)Math.Round(blue, 1)));

            UpdateValues();
        }

        private void UpdateValues()
        {
            _textR.text = _selectedNode.GetValue.Red.ToString();
            _textG.text = _selectedNode.GetValue.Green.ToString();
            _textB.text = _selectedNode.GetValue.Blue.ToString();
        }
    }
}
