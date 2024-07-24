using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Farmanji.Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Farmanji.Pacman
{
    [RequireComponent(typeof(PlayerInput))]
    public class PacmanPlayer : MonoBehaviour
    {
        #region FIELDS

        //CAMBIAR A PRIVATE DEPSUES DE TESTEAR
        private Animator animator;
        private bool canPlay = false;
        private AudioSource audioSource;
        [SerializeField] private PlayerInput inputAsset;
        private Vector2 inputDelta;
        private Vector2 inputPosition;
        public Vector2 inputToCanvasPosition;
        public Vector3 moveDirection;
        private Rigidbody2D ridigbody;
        private CharacterController characterController;
        [SerializeField] private MovingDirection direction = MovingDirection.Right;
        private SpriteRenderer pacmanSprite;
        private bool isMoving;

        [Header("DEVELOPMENT:"), Space(15)]
        [SerializeField] private bool debug;

        [Header("INPUTS:"), Space(15)]
        [Space(10), SerializeField] private string primaryTouchActionName = "PrimaryTouch";
        [SerializeField] private string primaryTouchDeltaActionName = "PrimaryDelta";
        [SerializeField] private string primaryTouchPositionActionName = "PrimaryPosition";

        [Header("TOUCH TRAIL:"), Space(15)]
        [SerializeField] private RectTransform trailCanvas;
        [SerializeField] private TrailRenderer touchscreenTrail;

        [Header("MOVEMENT:"), Space(15)]
        [SerializeField] private float stepValue = 0.25f;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float speedChange = 1.25f;

        [Header("CHARACTER:"), Space(15)]
        [SerializeField] private Color color = Color.yellow;

        [Header("SFX:"), Space(15)]
        [SerializeField] private AudioClip movingSFX;
        [SerializeField] private AudioClip eatDotSFX;
        [SerializeField] private AudioClip eatPowerUpSFX;
        [SerializeField] private AudioClip eatGemSFX;
        [SerializeField] private AudioClip eatQuizSFX;
        [SerializeField] private AudioClip poweredSFX;
        [SerializeField] private AudioClip diesSFX;
        [SerializeField] private AudioClip eatGhostSFX;
        [Header("STATUS:")]
        [SerializeField] private PacmanSection currentSection;
        [Header("EVENTS")]
        [SerializeField] private UnityEvent OnDie;
        [SerializeField] private UnityEvent OnEatDot;
        [SerializeField] private UnityEvent OnEatGhost;
        [SerializeField] private UnityEvent OnEatPower;
        [SerializeField] private UnityEvent OnPowerUp;
        [SerializeField] private UnityEvent OnEatGem;
        [SerializeField] private UnityEvent OnEatQuiz;
        #endregion

        #region PROPERTIES
        public Animator Animator { get { return animator; } }
        public PacmanSection CurrentSection { get { return currentSection; } set { currentSection = value; } }
        public float SpeedChange { get { return speedChange; } set { speedChange = value; } }
        public bool CanPlay { get { return canPlay; } set { canPlay = value; } }
        public AudioSource AudioSource { get { return audioSource; } }
        public UnityEvent OnDieEvent { get { return OnDie; } set { OnDie = value; } }
        public UnityEvent OnEatDotEvent { get { return OnEatDot; } set { OnEatDot = value; } }
        public UnityEvent OnEatGhostEvent { get { return OnEatGhost; } set { OnEatGhost = value; } }
        public UnityEvent OnPowerUpEvent { get { return OnPowerUp; } set { OnPowerUp = value; } }
        public UnityEvent OnEatGemEvent { get { return OnEatGem; } set { OnEatGem = value; } }
        public UnityEvent OnEatQuizEvent { get { return OnEatQuiz; } set { OnEatQuiz = value; } }

        #endregion

        #region UNITY METHODS
        void Awake()
        {
            inputAsset = GetComponent<PlayerInput>();
            animator = GetComponentInChildren<Animator>();
            audioSource = GetComponent<AudioSource>();
            characterController = GetComponent<CharacterController>();
            pacmanSprite = GetComponentInChildren<SpriteRenderer>();
            //SubscribeInputs();
            //InitializeSFX();
        }
        void Update()
        {
            if (canPlay)
            {
                Move(moveDirection, speedChange);
                SetMovingDirection();
                Rotate();
            }
            // moveDirection = GetClampedVector(inputToCanvasPosition);
            // Move(moveDirection, speedChange);
        }
        // void OnDestroy()
        // {
        //     UnsubscribeInputs();
        // }
        // private void OnDisable()
        // {
        //     UnsubscribeInputs();
        // }
        #endregion

        #region 
        internal void Weaken(float duration, float speed)
        {
            animator.SetTrigger("Weaken");
            speedChange = speed;
            StartCoroutine(Restore(duration));
        }
        internal void Restore()
        {
            animator.SetTrigger("Restore");
            speedChange = speed;

        }
        private void InitializeSFX()
        {
            UnityAction OnDieAction = () => { audioSource.clip = diesSFX; audioSource.loop = false; audioSource.Play(); };
            OnDie.AddListener(OnDieAction);

            UnityAction OnEatAction = () => { audioSource.clip = eatDotSFX; audioSource.loop = false; audioSource.Play(); };
            OnEatDot.AddListener(OnEatAction);

            UnityAction OnEatGemAction = () => { audioSource.clip = eatGemSFX; audioSource.loop = false; audioSource.Play(); };
            OnEatGem.AddListener(OnEatGemAction);

            UnityAction OnEatQuizAction = () => { audioSource.clip = eatQuizSFX; audioSource.loop = false; audioSource.Play(); };
            OnEatQuiz.AddListener(OnEatQuizAction);

            UnityAction OnEatPowerAction = () => { audioSource.clip = eatPowerUpSFX; audioSource.loop = false; audioSource.Play(); };
            OnEatPower.AddListener(OnEatPowerAction);
        }
        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "PacmanDot":
                    animator.SetTrigger("Eat");
                    other.GetComponent<PacmanDot>().Eat();
                    other.gameObject.SetActive(false);
                    break;
                case "Enemy":
                    PacmanGhost ghost = other.GetComponent<PacmanGhost>();
                    if (ghost.IsHostile)
                    {
                        Die();
                        OnDie?.Invoke();
                        SoundManager.Instance.SetSFX(diesSFX);
                        SoundManager.Instance.PlaySFX();
                    }
                    else
                    {
                        animator.SetTrigger("Eat");
                        ghost.Eated();
                        OnEatGhost?.Invoke();
                        SoundManager.Instance.SetSFX(eatGhostSFX);
                        SoundManager.Instance.PlaySFX();
                    }
                    break;
            }
        }
        #endregion

        #region PRIVATE METHODS
        private void Die()
        {
            animator.SetTrigger("Death");
        }
        private void PowerUp()
        {
            OnPowerUp?.Invoke();
            animator.SetTrigger("Power");
        }
        private void SetMovingDirection()
        {
            if (moveDirection == new Vector3(0f, 0f, 1f))
            {
                direction = MovingDirection.Up;
                return;
            }
            if (moveDirection == new Vector3(0f, 0f, -1f))
            {
                direction = MovingDirection.Down;
                return;
            }
            if (moveDirection == new Vector3(1f, 0f, 0f))
            {
                direction = MovingDirection.Right;
                return;
            }
            if (moveDirection == new Vector3(-1f, 0f, 0f))
            {
                direction = MovingDirection.Left;
                return;
            }
        }
        private void Rotate()
        {
            switch (direction)
            {
                case MovingDirection.Up:
                    pacmanSprite.transform.rotation = Quaternion.Euler(90f, 90f, transform.rotation.eulerAngles.z);
                    break;
                case MovingDirection.Down:
                    pacmanSprite.transform.rotation = Quaternion.Euler(90f, -90f, transform.rotation.eulerAngles.z);
                    break;
                case MovingDirection.Left:
                    pacmanSprite.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    pacmanSprite.flipY = false;
                    break;
                case MovingDirection.Right:
                    pacmanSprite.transform.rotation = Quaternion.Euler(90f, 0f, 180f);
                    pacmanSprite.flipY = true;
                    break;
            }
        }
        private void Move(Vector3 direction, float speed)
        {
            //ridigbody.MovePosition(transform.position + direction * Time.deltaTime * speed * this.speed * 1000);
            characterController.Move(direction * speed * Time.deltaTime);
        }
        private Vector3 GetClampedVector(Vector2 vector)
        {
            float x = vector.x;
            float z = vector.y;
            return Mathf.Abs(vector.x) <= Mathf.Abs(vector.y) ? new Vector3(0f, 0f, Mathf.Sign(vector.y)) : new Vector3(Mathf.Sign(vector.x), 0f, 0f);
        }
        public void SubscribeInputs()
        {
            Debug.Log("SUSCRIBIR INPUTS");
            inputAsset.enabled = true;
            if (inputAsset.currentActionMap.FindAction(primaryTouchPositionActionName) != null)
            {
                inputAsset.currentActionMap.FindAction(primaryTouchPositionActionName).performed += PositionPerformed;
                inputAsset.currentActionMap.FindAction(primaryTouchPositionActionName).canceled += PositionCancelled;
            }
            if (inputAsset.currentActionMap.FindAction(primaryTouchActionName) != null)
            {
                inputAsset.currentActionMap.FindAction(primaryTouchActionName).performed += PrimaryTouchPerformed;
                inputAsset.currentActionMap.FindAction(primaryTouchActionName).canceled += PrimaryTouchCancelled;
            }
            if (inputAsset.currentActionMap.FindAction(primaryTouchDeltaActionName) != null)
            {
                inputAsset.currentActionMap.FindAction(primaryTouchDeltaActionName).performed += PrimaryTouchDeltaPerformed;
                inputAsset.currentActionMap.FindAction(primaryTouchDeltaActionName).canceled += PrimaryTouchDeltaCancelled;
            }
        }
        public void UnsubscribeInputs()
        {
            Debug.Log("DESUSCRIBIR INPUTS");
            inputAsset.enabled = true;
            if (inputAsset.currentActionMap.FindAction(primaryTouchPositionActionName) != null)
            {
                inputAsset.currentActionMap.FindAction(primaryTouchPositionActionName).performed -= PositionPerformed;
                inputAsset.currentActionMap.FindAction(primaryTouchPositionActionName).canceled -= PositionPerformed;
            }
            if (inputAsset.currentActionMap.FindAction(primaryTouchActionName) != null)
            {
                inputAsset.currentActionMap.FindAction(primaryTouchActionName).performed -= PrimaryTouchPerformed;
                inputAsset.currentActionMap.FindAction(primaryTouchActionName).canceled -= PrimaryTouchPerformed;
            }
            if (inputAsset.currentActionMap.FindAction(primaryTouchDeltaActionName) != null)
            {
                inputAsset.currentActionMap.FindAction(primaryTouchDeltaActionName).performed -= PrimaryTouchDeltaPerformed;
                inputAsset.currentActionMap.FindAction(primaryTouchDeltaActionName).canceled -= PrimaryTouchDeltaPerformed;
            }
        }
        #endregion

        #region PUBLIC METHODS
        public void ResetRotation()
        {
            pacmanSprite.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            pacmanSprite.flipY = false;
            pacmanSprite.flipX = false;
        }
        #endregion

        #region EVENTS CALLBACKS
        private void PrimaryTouchDeltaPerformed(InputAction.CallbackContext context)
        {
            //Debug.Log("Primary Touch Delta X Started: " + context.ReadValue<Vector2>());
            inputDelta = context.ReadValue<Vector2>();
            moveDirection = GetClampedVector(inputToCanvasPosition);
        }
        private void PrimaryTouchPerformed(InputAction.CallbackContext context)
        {
            //Debug.Log("Primary Touch Performed");
        }
        private void PositionPerformed(InputAction.CallbackContext context)
        {
            //Debug.Log("Position X Started: " + context.ReadValue<Vector2>());
            inputPosition = context.ReadValue<Vector2>();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(trailCanvas, inputPosition, Camera.main, out inputToCanvasPosition);

            if (touchscreenTrail.enabled == false)
            {
                touchscreenTrail.transform.localPosition = inputToCanvasPosition;
                touchscreenTrail.enabled = true;
                return;
            }
            touchscreenTrail.transform.localPosition = inputToCanvasPosition;
        }
        private void PrimaryTouchDeltaCancelled(InputAction.CallbackContext context)
        {
            //Debug.Log("Primary Touch Delta X Cancelled");
            //moveDirection = GetClampedVector(inputToCanvasPosition);
            // touchscreenTrail.enabled = false;
        }
        private void PrimaryTouchCancelled(InputAction.CallbackContext context)
        {
            //Debug.Log("Primary Touch Cancelled");
        }
        private void PositionCancelled(InputAction.CallbackContext context)
        {
            //Debug.Log("Position X Cancelled: " + context.ReadValue<Vector2>());
        }


        #endregion
        private enum MovingDirection { Up, Down, Left, Right }
        #region COROUTINES
        IEnumerator Restore(float time)
        {
            yield return new WaitForSeconds(time);
            Restore();
        }
        #endregion
    }
}