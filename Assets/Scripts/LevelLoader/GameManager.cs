using Rola.Nodes;
using Rola.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

namespace Rola.Levels
{
    public sealed class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LevelHandler[] _levels;
        [SerializeField] private Volume _winVolume;
        [SerializeField] private Camera _cam;
        [SerializeField] private AudioMixer _masterGroup;
        //to save
        //no saving for jam(it doesnt do anything)
        [SerializeField] private int _levelsCompleted;

        [Header("Globals")]
        [SerializeField] private GameObject _wirePrefab;
        [SerializeField] private GameObject _wireSelectedEffect;

        [SerializeField] private Color _metalDefault;
        [SerializeField] private Color _metalHovered;

        [SerializeField] private GameObject _lockPrefab;

        [SerializeField] private AudioClip _solved1;
        [SerializeField] private AudioClip _solved2;
        [SerializeField] private AudioClip _buttonPress;
        [SerializeField] private AudioClip _plugin;
        [SerializeField] private AudioClip _removeWire;
        [SerializeField] private AudioClip _selectGuy;

        [Header("I AM TESTINTESTING")]
        [SerializeField] private int STARTLEVELTESTTEST;

        public static GameManager Instance { get; private set; }
        public static GameObject GetWirePrefab => Instance._wirePrefab;
        public static GameObject GetWireSelectEffect => Instance._wireSelectedEffect;
        public static GameObject GetLockPrefab => Instance._lockPrefab;
        public static Color GetMetalDefault => Instance._metalDefault;
        public static Color GetMetalHovered => Instance._metalHovered;

        public static AudioClip GetButtonPress => Instance._buttonPress;
        public static AudioClip GetPlugin => Instance._plugin;
        public static AudioClip GetSelectGuy => Instance._selectGuy;
        public static AudioClip GetRemoveWire => Instance._removeWire;

        private LevelHandler _loadedLevel;
        private int _currentLevel = 1;
        private List<ValueOutNode> _registeredNodes = new();
        private bool _canUnload, _isMuted;

        private void Awake()
        {
            if(Instance != null && Instance != this)
                Destroy(Instance);

            Instance = this;
            _winVolume.weight = 0f;
            _canUnload = true;
        }

        //unity is trash
        /*
        private void OnEnable()
        {
            if(Instance == null)
                Instance = this;
        }

        private void Start()
        {
            if(Instance == null)
                Instance = this;
        }
        */

        [ContextMenu("LOAD GUY")]
        public void DOTHINGTEST() => LoadLevel(STARTLEVELTESTTEST);

        //the strongest input system
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
                LoadLevel(_currentLevel);

            if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                && _loadedLevel != null)
                EvaluateCurrentLevel();

            if(Input.GetKeyDown(KeyCode.M))
            {
                if(_isMuted)
                {
                    _masterGroup.SetFloat("_volumeMaster", -9f);
                    _isMuted = false;
                }
                else
                {
                    _masterGroup.SetFloat("_volumeMaster", 0f);
                    _isMuted = true;
                }
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(UIManager.Instance.OnEscPressed())
                    return;

                if(_canUnload)
                    EndGame();
            }

#if UNITY_EDITOR
            return;
#endif

            if(Input.GetKeyDown(KeyCode.Backspace))
            {
                EndGame();
                Application.Quit();
            }
        }

        public void LoadLevel(int level)
        {
            if(!_canUnload)
                return;

            _canUnload = false;

            if(level == 6)
            {
                BGMHandler.Instance.StopNow();

                var temp = AudioPool.GetSource();

                temp.Stop();

                temp.clip = _solved2;
                temp.pitch = 0.75f;
                temp.gameObject.SetActive(true);
            }

            if(_loadedLevel != null)
            {
                StartCoroutine(_loadedLevel.DecomissionLevel(() =>
                {
                    try
                    {
                        _loadedLevel = Instantiate(_levels[level - 1]);
                        StartCoroutine(_loadedLevel.BeginLevel(() => _canUnload = true));
                        _currentLevel = level;
                    }
                    catch
                    {
                        EndGame();
                        return;
                    }
                }));
                return;
            }

            try
            {
                _loadedLevel = Instantiate(_levels[level - 1]);
                StartCoroutine(_loadedLevel.BeginLevel(() => _canUnload = true));
                _currentLevel = level;
            }
            catch
            {
                EndGame();
                return;
            }
        }

        public void EvaluateCurrentLevel()
        {
            if(_loadedLevel == null || !_canUnload)
                return;

            foreach(var temp in _registeredNodes)
                temp.StartUpdateChain();

            if(_loadedLevel.EvaluateLevel())
                StartCoroutine(CurrentLevelComplete());
        }

        public void EndGame()
        {
            if(!_canUnload)
                return;

            if(!UIManager.Instance.IsLevelSelect)
                UIManager.Instance.DisableAll();

            if(_loadedLevel != null)
                StartCoroutine(_loadedLevel.DecomissionLevel(() =>
                {
                    _canUnload = true;
                    _loadedLevel = null;
                    UIManager.Instance.OpenLevelMenu();
                }));

            BGMHandler.Instance.StartBGM();
        }

        private IEnumerator CurrentLevelComplete()
        {
            Debug.Log("LEVEL WIN");
            UIManager.Instance.DisableAll();
            _canUnload = false;

            var time = 0f;

            while(time < 1)
            {
                time += Time.deltaTime;

                _winVolume.weight = Mathf.Lerp(0f, 1f, time / 1f);

                _cam.fieldOfView = Mathf.SmoothStep(21f, 45f, time / 2f);

                yield return null;
            }

            var temp = AudioPool.GetSource();

            temp.Stop();

            temp.clip = _solved1;
            temp.volume = 0.6f;
            temp.gameObject.SetActive(true);

            time = 0f;

            while(time < 1f)
            {
                time += Time.deltaTime;

                _winVolume.weight = Mathf.Lerp(1f, 0f, time / 1f);

                _cam.fieldOfView = Mathf.SmoothStep(45f, 2000f, time / 1f);

                yield return null;
            }

            StartCoroutine(_loadedLevel.DecomissionLevel(() =>
            {
                _canUnload = true;
                _loadedLevel = null;

                _cam.fieldOfView = 21f;

                LoadLevel(++_currentLevel);
            }));
            /*
            if(_currentLevel >= _levelsCompleted)
            _levelsCompleted++;
            */
        }

        public void RegisterNode(ValueOutNode node)
        {
            if(!_registeredNodes.Contains(node))
                _registeredNodes.Add(node);
        }

        public void UnegisterNode(ValueOutNode node)
        {
            if(_registeredNodes.Contains(node))
                _registeredNodes.Remove(node);
        }
    }
}
