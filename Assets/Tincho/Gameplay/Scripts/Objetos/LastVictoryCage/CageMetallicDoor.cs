using UnityEngine;

public class CageMetallicDoor : MonoBehaviour
{
    private Animator _metallicDoorAnimation;
    private bool _puzzleWasSolved;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private AudioSource _openedDoorSFX;
    [SerializeField] private EnemySpawner _enemySpawner1;
    [SerializeField] private EnemySpawner _enemySpawner2;
    [SerializeField] private int _enemyQuantity;
    [SerializeField] private Animator _mainGate;

    private void Start()
    {
        _metallicDoorAnimation = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;
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
    }

    private void OpenMetallicDoor()
    {
        _openedDoorSFX.Play();
        _metallicDoorAnimation.SetTrigger("doorOpened");
        _boxCollider.enabled = true;
        _puzzleWasSolved = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entro");
            _enemySpawner1.StartSpawning(_enemyQuantity);
            _enemySpawner2.StartSpawning(_enemyQuantity);
            _boxCollider.enabled = false;
            _openedDoorSFX.Play();
            _metallicDoorAnimation.SetTrigger("doorClosed");
            _mainGate.SetTrigger("closeMainGate");
        }
    }

    private void FinalVictory()
    {

    }
}