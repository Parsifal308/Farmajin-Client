using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;

namespace Farmanji.Pacman
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class PacmanGhost : MonoBehaviour
    {
        #region FIELDS
        private Animator animator;
        private AIDestinationSetter destinationSetter;
        private AIPath aIPath;
        private SpriteRenderer sprite;
        private bool isHostile = true;
        // private NavMeshAgent navMeshAgent;
        [Header("DATA:")]
        [SerializeField] private GhostData data;
        [Header("SETTINGS:")]
        [SerializeField] private float baseSpeed = 2f;
        [SerializeField] private float speedModifier = 1f;
        [SerializeField] private Transform homeTransform;
        [SerializeField] private UnityEvent OnEatPlayer;

        [Header("STATE:")]
        [SerializeField] private GhostState state = GhostState.Chasing;
        [SerializeField] private PacmanSection currentSection;

        #endregion

        #region PROPERTIES
        public PacmanSection CurrentSection { get { return currentSection; } set { currentSection = value; } }
        public AIDestinationSetter DestinationSetter { get { return destinationSetter; } }
        public AIPath AIPath { get { return aIPath; } }
        public Animator Animator { get { return animator; } }
        public bool IsHostile { get { return isHostile; } }
        #endregion

        #region UNITY METHODS
        void Awake()
        {
            TryGetComponent<SpriteRenderer>(out sprite);
            animator = GetComponentInChildren<Animator>();
            destinationSetter = GetComponent<AIDestinationSetter>();
            aIPath = GetComponent<AIPath>();
            // TryGetComponent<NavMeshAgent>(out navMeshAgent);
        }
        void Start()
        {
            Initialize();
            //navMeshAgent.SetDestination(PacmanGameManager.Instance.Player.transform.position);

        }
        void Update()
        {
            //navMeshAgent.SetDestination(PacmanGameManager.Instance.Player.transform.position);
        }

        #endregion

        #region PRIVATE METHODS
        private void Initialize()
        {
            SetBehavioir();
            state = GhostState.Chasing;
            sprite.sprite = data.Sprite;
            sprite.color = data.Color;
            // navMeshAgent.updateRotation = false;
            // navMeshAgent.updateUpAxis = false;
        }
        private void SetBehavioir()
        {
            switch (data.Type)
            {
                case GhostData.GhostType.Chaser:
                    destinationSetter.target = PacmanGameManager.Instance.Player.transform;
                    break;
                case GhostData.GhostType.LeftAmbusher:
                    AmbushLeft();
                    break;
                case GhostData.GhostType.RightAmbusher:
                    AmbushRight();
                    break;
                case GhostData.GhostType.BackAmbusher:
                    AmbushRandom();
                    break;
            }
        }

        public void Eated()
        {
            GetComponent<AIPath>().maxSpeed = 4f;
            animator.SetTrigger("Eated");
            sprite.color = Color.white;
            ReturnToHome();
        }
        internal void Weaken(float duration, float speed)
        {
            GetComponent<AIPath>().maxSpeed = speed;

            SetHostile(false);
            StartCoroutine(Restore(duration));
        }
        internal void Restore()
        {
            aIPath.maxSpeed = baseSpeed;
            SetBehavioir();
            SetHostile(true);
        }
        internal void SetHostile(bool value)
        {
            if (value)
            {
                isHostile = true;
                UnsubscribeEscapeBehaviors();
                animator.SetTrigger("Restore");
                sprite.color = data.Color;
            }
            else
            {
                isHostile = false;
                SubscribeEscapeBehaviors();
                animator.SetTrigger("Weaken");
                sprite.color = Color.blue;
            }
        }
        private void SetFartestPosToPlayer(object sender, EventArgs e)
        {
            switch (PacmanGameManager.Instance.Player.GetComponent<PacmanPlayer>().CurrentSection.gameObject.name)
            {
                case "Section 1.1":
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[8].transform;
                    break;
                case "Section 1.2":
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[7].transform;
                    break;
                case "Section 1.3":
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[6].transform;
                    break;
                case "Section 2.1":
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[5].transform;
                    break;
                case "Section 2.2":
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[UnityEngine.Random.Range(0, 9)].transform;
                    break;
                case "Section 2.3":
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[3].transform;
                    break;
                case "Section 3.1":
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[2].transform;
                    break;
                case "Section 3.2":
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[1].transform;
                    break;
                case "Section 3.3":
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[0].transform;
                    break;
            }
        }
        private void AmbushLeft()
        {
            switch (PacmanGameManager.Instance.Player.GetComponent<PacmanPlayer>().CurrentSection.gameObject.name)
            {
                case "Section 1.1": //[0]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[3].transform;
                    StartCoroutine(Attack(3));
                    break;
                case "Section 1.2"://[1]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[0].transform;
                    StartCoroutine(Attack(3));
                    break;
                case "Section 1.3"://[2]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[1].transform;
                    StartCoroutine(Attack(3));
                    break;
                case "Section 2.1"://[3]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[6].transform;
                    StartCoroutine(Attack(3));
                    break;
                case "Section 2.2"://[4]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[3].transform;
                    StartCoroutine(Attack(3));
                    break;
                case "Section 2.3"://[5]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[4].transform;
                    StartCoroutine(Attack(3));
                    break;
                case "Section 3.1"://[6]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[3].transform;
                    StartCoroutine(Attack(3));
                    break;
                case "Section 3.2"://[7]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[6].transform;
                    StartCoroutine(Attack(3));
                    break;
                case "Section 3.3"://[8]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[7].transform;
                    StartCoroutine(Attack(3));
                    break;
            }
        }
        private void AmbushRight()
        {
            switch (PacmanGameManager.Instance.Player.GetComponent<PacmanPlayer>().CurrentSection.gameObject.name)
            {
                case "Section 1.1": //[0]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[1].transform;
                    StartCoroutine(Attack(4));
                    break;
                case "Section 1.2"://[1]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[2].transform;
                    StartCoroutine(Attack(4));
                    break;
                case "Section 1.3"://[2]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[5].transform;
                    StartCoroutine(Attack(4));
                    break;
                case "Section 2.1"://[3]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[4].transform;
                    StartCoroutine(Attack(4));
                    break;
                case "Section 2.2"://[4]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[5].transform;
                    StartCoroutine(Attack(4));
                    break;
                case "Section 2.3"://[5]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[8].transform;
                    StartCoroutine(Attack(4));
                    break;
                case "Section 3.1"://[6]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[7].transform;
                    StartCoroutine(Attack(4));
                    break;
                case "Section 3.2"://[7]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[8].transform;
                    StartCoroutine(Attack(4));
                    break;
                case "Section 3.3"://[8]
                    destinationSetter.target = PacmanGameManager.Instance.LevelSections[5].transform;
                    StartCoroutine(Attack(4));
                    break;
            }
        }
        private void AmbushRandom()
        {
            destinationSetter.target = PacmanGameManager.Instance.LevelSections[UnityEngine.Random.Range(0, 9)].transform;
            StartCoroutine(Attack(2));
        }
        private void ReturnToHome()
        {
            destinationSetter.target = homeTransform;
        }
        internal void SubscribeEscapeBehaviors()
        {
            foreach (var section in PacmanGameManager.Instance.LevelSections)
            {
                section.OnPacmanEnteredEvent += SetFartestPosToPlayer;
            }
        }
        internal void UnsubscribeEscapeBehaviors()
        {
            foreach (var section in PacmanGameManager.Instance.LevelSections)
            {
                section.OnPacmanEnteredEvent -= SetFartestPosToPlayer;
            }
        }

        #endregion

        #region ENUMERATORS
        enum GhostState
        {
            Chasing,
            Edible,
            Eated
        }
        #endregion

        #region COROUTINES
        IEnumerator Restore(float time)
        {
            yield return new WaitForSeconds(time);
            Restore();
        }
        IEnumerator Attack(float delay)
        {
            yield return new WaitForSeconds(delay);
            destinationSetter.target = PacmanGameManager.Instance.Player.transform;
            yield return new WaitForSeconds(delay*2);
        }
        #endregion
    }
}