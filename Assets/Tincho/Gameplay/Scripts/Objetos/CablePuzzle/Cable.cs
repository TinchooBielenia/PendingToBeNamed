using UnityEngine;

public class Cable : MonoBehaviour
{
    public SpriteRenderer cableBody;

    private Vector2 _ogPosition;
    private Vector2 _ogSize;
    private WirePuzzleController _puzzleController;
    private Quaternion _ogRotation;
    public int cableIndex;
    private bool _wasCorrect;

    void Start()
    {
        _ogPosition = transform.position;
        _ogSize = cableBody.size;
        _puzzleController = GetComponentInParent<WirePuzzleController>();
        _ogRotation = transform.rotation;

    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CheckConnection();
            if(!_wasCorrect)
            {
                Reset();
            }
        }
    }

    private void OnMouseDrag()
    {
        UpdatePosition();
        UpdateRotation();
        UpdateSize();
    }

    private void UpdatePosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    private void UpdateRotation()
    {
        Vector2 currentPosition = transform.position;
        Vector2 ogPoint = transform.parent.position;

        Vector2 dir = currentPosition - ogPoint;

        float angulo = Vector2.SignedAngle(Vector2.right * transform.lossyScale, dir);

        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    private void UpdateSize()
    {
        Vector2 currentPosition = transform.position;
        Vector2 ogPoint = transform.parent.position;

        float distance = Vector2.Distance(currentPosition, ogPoint);

        cableBody.size = new Vector2(distance, cableBody.size.y);
    }

    private void Reset()
    {
        transform.position = _ogPosition;
        transform.rotation = _ogRotation;
        cableBody.size = _ogSize;
    }

    private void CheckConnection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                transform.position = col.transform.position;

                int expectedOrder = _puzzleController.correctOrder[cableIndex];
                GameObject correctHole = _puzzleController.Holes[expectedOrder];

                if (col.gameObject == correctHole)
                {
                    Connect();
                    Debug.Log("Conexión correcta");
                    _puzzleController.connectionSFX.Play();
                    _puzzleController.currentConnections++;
                    _puzzleController.VerifyVictory();
                    _wasCorrect = true;
                }
                else
                {
                    Debug.Log("Conexión incorrecta");
                    _puzzleController.wrongSFX.Play();
                    _puzzleController.wrongTries++;
                    _puzzleController.VerifyLose();
                    _wasCorrect = false;
                }
            }
        }
    }

    public void Connect()
    {
        Destroy(this);
    }
}
