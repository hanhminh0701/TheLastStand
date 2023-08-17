using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController Ins;
    private void Awake()
    {
        Ins = this;
    }
    #endregion

    public CharacterController controller;
    public float speed;
    Vector2 moveInput;
    Animator animator;
    Vector3 direction;
    [HideInInspector] public bool isDead;

    void Start() => animator = GetComponent<Animator>();
    void Update()
    {
        if (isDead) return;
        MovePlayer();
    }
    public void OnMove(InputAction.CallbackContext context) => moveInput = context.ReadValue<Vector2>();
    void MovePlayer()
    {        
        direction = new Vector3(moveInput.x, 0, moveInput.y);
        controller.SimpleMove(direction * speed);
        if(direction!=Vector3.zero) 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), .15f);
        animator.SetFloat("Speed", moveInput.magnitude, .05f, Time.deltaTime);
    }       
    
}
