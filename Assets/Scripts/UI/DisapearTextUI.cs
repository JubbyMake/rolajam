using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rola.UI
{
    public sealed class DisapearTextUI : MonoBehaviour
    {
        [SerializeField] private int _displayTimeMS;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private RawImage _backdrop;

        private async void OnEnable()
        {
            await Task.Delay(_displayTimeMS);

            var time = 0;

            while(time < 1000 && gameObject != null)
            {
                time += 10;

                _text.color -= new Color(0f, 0f, 0f, 0.01f);
                _backdrop.color -= new Color(0f, 0f, 0f, 0.01f);

                await Task.Delay(10);
            }

            gameObject.SetActive(false);
        }
    }
}
