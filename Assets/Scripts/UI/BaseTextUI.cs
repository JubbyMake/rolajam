using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rola.UI
{
    public abstract class BaseTextUI : MonoBehaviour
    {
        [SerializeField] private RawImage _backdrop;
        [SerializeField] private TMP_Text _title;

        protected virtual async void Awake()
        {
            await Task.Delay(10);

            _title.color = UIManager.RegularTextColour;
            _backdrop.color = UIManager.BackdropColour;
        }
    }
}
