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

        protected void Awake() => StartCoroutine(OnAwake());

        protected virtual IEnumerator OnAwake()
        {
            yield return null;

            _title.color = UIManager.RegularTextColour;
            _backdrop.color = UIManager.BackdropColour;
        }
    }
}
