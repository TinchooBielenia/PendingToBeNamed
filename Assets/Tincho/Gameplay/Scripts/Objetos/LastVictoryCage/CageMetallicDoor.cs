using TMPro;
using UnityEngine;

public class CageMetallicDoor : MonoBehaviour
{
    private Animator _metallicDoorAnimation;
    private bool _puzzleWasSolved;
    [SerializeField] private AudioSource _openedDoorSFX;
    //[SerializeField] private EnemySpawner _enemySpawner1;
    //[SerializeField] private EnemySpawner _enemySpawner2;
    //[SerializeField] private int _enemyQuantity;
    [SerializeField] private Animator _mainGate;
    [SerializeField] private GameObject _blockerDoor;
    [SerializeField] private float _openMainDoorTimer;
    //[SerializeField] private bool _playerCrossedTrigger;
    //[SerializeField] private bool _timerFinished;
    //[SerializeField] private TextMeshProUGUI _timerOnScreenText;
    //[SerializeField] private GameObject _timerOnScreenCanvas;
    [SerializeField] private GameObject _boss;
    [SerializeField] private Boss _bossBool;

    private void Start()
    {
        _metallicDoorAnimation = GetComponent<Animator>();
        _blockerDoor.SetActive(false);
        //_playerCrossedTrigger = false;
        //_timerOnScreenCanvas.SetActive(false);
        //_timerFinished = false;
        //_boss.SetActive(false);
    }

    public bool puzzleWasSolved
    {
        set { _puzzleWasSolved = value; }
    }

    private void Update()
    {
        if (_puzzleWasSolved)
        {
            OpenMetallicDoor();
        }

        //if (_playerCrossedTrigger)
        //{
        //    ManageBattleTimer();
        //}

        //if (_timerFinished)
        //{
        //    TimerFinished();
        //}
    }

    private void OpenMetallicDoor()
    {
        _openedDoorSFX.Play();
        _metallicDoorAnimation.SetTrigger("doorOpened");
        _puzzleWasSolved = false;
    }

    public void FinalZoneBattle()
    {
        Debug.Log("Player entro");
        _blockerDoor.SetActive(true);
        _boss.SetActive(true);
        //_enemySpawner1.StartSpawning(_enemyQuantity);
        //_enemySpawner2.StartSpawning(_enemyQuantity);
        _openedDoorSFX.Play();
        _metallicDoorAnimation.SetTrigger("doorClosed");
        _mainGate.SetBool("closeDoor", true);
        //_playerCrossedTrigger = true;
    }

    public void OpenMainGate()
    {
        Debug.Log("Player logro escapar");
        _mainGate.SetBool("closeDoor", false); // <- Esto resetea el bool
        _mainGate.SetTrigger("mainGateOpened");
    }

    //private void ManageBattleTimer()
    //{
    //    _timerOnScreenCanvas.SetActive(true);
    //    _openMainDoorTimer -= Time.deltaTime;
    //    _timerOnScreenText.SetText(((int)_openMainDoorTimer).ToString());

    //    if (_openMainDoorTimer <= 0)
    //    {
    //        _timerFinished = true;
    //    }
    //}

    //public void TimerFinished()
    //{
    //    //_timerOnScreenCanvas.SetActive(false);
    //    Debug.Log("Player logro escapar");
    //    _mainGate.SetTrigger("mainGateOpened");
    //}
}