using UnityEngine;
using UnityEngine.InputSystem;

public class FPSControls : MonoBehaviour
{
    [Header("Actions")]
    public InputAction LookAction;
    public InputAction FireAction;
    [SerializeField] private float _camSensitivity;
    private Camera _cam;
    private Vector2 _rotation = new Vector2();
    private bool _firing = false;
    [SerializeField] private float _fireCooldown;
    private float _fireCountdown;
    [SerializeField] private Animator _gunAnim;
    void Start()
    {
        LookAction.Enable();
        FireAction.Enable();
        
        _cam = Camera.main;
        FireAction.performed += context => _firing = true;
        FireAction.canceled += context => _firing = false;

        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        //Camera shite
        Vector2 lookDir = LookAction.ReadValue<Vector2>();
        _rotation.x += lookDir.x * _camSensitivity;
        _rotation.y = Mathf.Clamp(_rotation.y + lookDir.y * _camSensitivity, -90, 90);

        transform.rotation = Quaternion.Euler(_rotation.y, _rotation.x, 0);
        
        if(_firing && _fireCountdown <= 0)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward, Color.red, 0.5f);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("TargetHitBox")))
                {
                    Debug.Log("hit something!!");
                    Destroy(hit.collider.gameObject);
                }
            _fireCountdown = _fireCooldown;
        }
        else if(_firing)
        {
            if(_gunAnim.GetFloat("SpinSpeed") < 2)
                _gunAnim.SetFloat("SpinSpeed", _gunAnim.GetFloat("SpinSpeed") + Time.deltaTime * 10);
        }
        else if(!_firing && _gunAnim.GetFloat("SpinSpeed") > 0)
            _gunAnim.SetFloat("SpinSpeed", _gunAnim.GetFloat("SpinSpeed") - Time.deltaTime * 2);
        if(_fireCountdown > 0){
            _fireCountdown -= Time.deltaTime;
        }
    }
}
