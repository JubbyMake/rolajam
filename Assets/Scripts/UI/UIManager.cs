using Rola.Levels;
using Rola.Nodes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rola.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Color _regularText;
        [SerializeField] private Color _backdrop;

        [SerializeField] private Color _redText;
        [SerializeField] private Color _greenText;
        [SerializeField] private Color _blueText;

        [SerializeField] private TMP_FontAsset _font;

        [Header("References")]
        [SerializeField] private LevelSelectorMenu _levelSelector;

        [SerializeField] private AndNodeMenu _andNodeMenu;
        [SerializeField] private AndNodeLockedMenu _andNodeLockedMenu;
        [SerializeField] private NotNodeMenu _notNodeMenu;
        [SerializeField] private NotNodeLockedMenu _notNodeLockedMenu;
        [SerializeField] private OrNodeMenu _orNodeMenu;
        [SerializeField] private OrNodeLockedMenu _orNodeLockedMenu;
        [SerializeField] private SwitchMenu _switchMenu;
        [SerializeField] private SwitchLockedMenu _switchLockedMenu;
        [SerializeField] private AbsorbMenu _absorbMenu;
        [SerializeField] private ValueInMenu _valueInMenu;
        [SerializeField] private ValueOutMenu _valueOutMenu;
        [SerializeField] private ValueOutLockedMenu _valueOutLockedMenu;

        private bool _isInspecting = false;

        public static UIManager Instance { get; private set; }
        public bool IsLevelSelect { get; private set; }

        public static Color RegularTextColour => Instance._regularText;
        public static Color RedTextColour => Instance._redText;
        public static Color GreenTextColour => Instance._greenText;
        public static Color BlueTextColour => Instance._blueText;
        public static Color BackdropColour => Instance._backdrop;

        private void Awake()
        {
            if(Instance != null && Instance != this)
                Destroy(Instance);

            Instance = this;

            foreach(var temp in GetComponentsInChildren<TMP_Text>(true))
                temp.font = _font;

            OpenLevelMenu();
        }

        public void OpenLevelMenu()
        {
            DisableAll();

            IsLevelSelect = true;
            _levelSelector.gameObject.SetActive(true);
        }

        #region Node menus
        public void EditAndNode(AndNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _andNodeMenu.gameObject.SetActive(true);
            _andNodeMenu.StartEdit(node);
        }
        
        public void EditAndNodeLocked(AndNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _andNodeLockedMenu.gameObject.SetActive(true);
            _andNodeLockedMenu.StartEdit(node);
        }

        public void EditNotNode(NotNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _notNodeMenu.gameObject.SetActive(true);
            _notNodeMenu.StartEdit(node);
        }
        
        public void EditNotNodeLocked(NotNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _notNodeLockedMenu.gameObject.SetActive(true);
            _notNodeLockedMenu.StartEdit(node);
        }

        public void EditOrNode(OrNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _orNodeMenu.gameObject.SetActive(true);
            _orNodeMenu.StartEdit(node);
        }
        
        public void EditOrNodeLocked(OrNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _orNodeLockedMenu.gameObject.SetActive(true);
            _orNodeLockedMenu.StartEdit(node);
        }

        public void EditSwitch(SwitchNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _switchMenu.gameObject.SetActive(true);
            _switchMenu.StartEdit(node);
        }

        public void EditSwitchLocked(SwitchNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _switchLockedMenu.gameObject.SetActive(true);
            _switchLockedMenu.StartEdit(node);
        }

        public void EditAbsorb(ValueAbsorbNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _absorbMenu.gameObject.SetActive(true);
            _absorbMenu.StartEdit(node);
        }

        public void EditValueIn(ValueInNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _valueInMenu.gameObject.SetActive(true);
            _valueInMenu.StartEdit(node);
        }

        public void EditValueOut(ValueOutNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _valueOutMenu.gameObject.SetActive(true);
            _valueOutMenu.StartEdit(node);
        }

        public void EditValueOutLocked(ValueOutNode node)
        {
            DisableAll();
            DoSelectedSound();

            _isInspecting = true;
            _valueOutLockedMenu.gameObject.SetActive(true);
            _valueOutLockedMenu.StartEdit(node);
        }
        #endregion

        public void DisableAll()
        {
            _isInspecting = IsLevelSelect = false;

            _levelSelector.gameObject.SetActive(false);

            _andNodeMenu.gameObject.SetActive(false);
            _andNodeLockedMenu.gameObject.SetActive(false);
            _notNodeMenu.gameObject.SetActive(false);
            _notNodeLockedMenu.gameObject.SetActive(false);
            _orNodeMenu.gameObject.SetActive(false);
            _orNodeLockedMenu.gameObject.SetActive(false);
            _switchMenu.gameObject.SetActive(false);
            _switchLockedMenu.gameObject.SetActive(false);
            _absorbMenu.gameObject.SetActive(false);
            _valueInMenu.gameObject.SetActive(false);
            _valueOutMenu.gameObject.SetActive(false);
            _valueOutLockedMenu.gameObject.SetActive(false);
        }

        public bool OnEscPressed()
        {
            if(_isInspecting)
            {
                DisableAll();
                return true;
            }

            return false;
        }

        public static void DoButtonSound()
        {
            var temp = AudioPool.GetSource();

            temp.Stop();

            temp.clip = GameManager.GetButtonPress;
            temp.pitch = 0.9f;
            temp.gameObject.SetActive(true);
        }

        private void DoSelectedSound()
        {
            var temp = AudioPool.GetSource();

            temp.Stop();

            temp.clip = GameManager.GetSelectGuy;
            temp.volume = 0.4f;
            temp.gameObject.SetActive(true);

        }
    }
}
