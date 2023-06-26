using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _fastSpeed;

    private float _movementSpeed;
    [SerializeField] private float _movementTime;

    private Vector3 _newPosition;
    private Quaternion _newRotation;
    private Vector3 _newZoom;
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _rotationAmount;
    [SerializeField] private Vector3 _zoomAmount;

    // Start is called before the first frame update
    private void Start()
    {
        _newPosition = transform.position;
        _newRotation = transform.rotation;
        _newZoom = _cameraTransform.localPosition;

        _movementSpeed = _normalSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            _movementSpeed = _fastSpeed;
        else
            _movementSpeed = _normalSpeed;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            _newPosition += transform.forward * _movementSpeed;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            _newPosition += transform.forward * -_movementSpeed;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            _newPosition += transform.right * _movementSpeed;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            _newPosition += transform.right * -_movementSpeed;
        if (Input.GetKey(KeyCode.Q)) _newRotation *= Quaternion.Euler(Vector3.up * _rotationAmount);
        if (Input.GetKey(KeyCode.E)) _newRotation *= Quaternion.Euler(Vector3.up * -_rotationAmount);
        if (Input.GetKey(KeyCode.X) && _newZoom.y >= 1 && _newZoom.z <= -1) _newZoom += _zoomAmount;
        if (Input.GetKey(KeyCode.Z) && _newZoom.y <= 60 && _newZoom.z >= -60) _newZoom -= _zoomAmount;

        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * _movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * _movementTime);
        _cameraTransform.localPosition =
            Vector3.Lerp(_cameraTransform.localPosition, _newZoom, Time.deltaTime * _movementTime);
    }

    public void ResetCamera()
    {
        _newPosition = new Vector3(50, 40, 0);
        transform.position = _newPosition;
    }
}