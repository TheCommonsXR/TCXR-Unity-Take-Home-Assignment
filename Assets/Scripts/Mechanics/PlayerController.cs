using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Model;
using TMPro;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private GameObject collisionImmunityGraphic;
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private float projectileVelocity = 30;
        [SerializeField] private bool collisionImmunity;
        [SerializeField] private int playerDamage;
        [SerializeField] private float collisionImmunityTimer = 1f;

        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public AudioClip deathAudio;

        /// Max horizontal speed of the player.
        public float maxSpeed = 7;
        /// Initial jump velocity at the start of a jump.
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/
        public Collider2D collider2d;
        /*internal new*/
        public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();

            if (Input.GetMouseButtonDown(0))
            {
                ShootProjectile();
            }
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
            {
                spriteRenderer.flipX = false;
                spawnPosition.transform.position = new Vector2(transform.position.x + 0.3f, spawnPosition.transform.position.y);
            }
            else if (move.x < -0.01f)
            {

                spriteRenderer.flipX = true;
                spawnPosition.transform.position = new Vector2(transform.position.x - 0.3f, spawnPosition.transform.position.y);
            }

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public void ShootProjectile()
        {
            var projectileToShoot = Instantiate(projectile, spawnPosition.position, Quaternion.identity);

            projectileToShoot.GetComponent<Projectile>().projectileVelocity = projectileVelocity;
            projectileToShoot.GetComponent<Projectile>().flipX = spriteRenderer.flipX;
            projectileToShoot.GetComponent<Projectile>().damage = playerDamage;
        }

        public void UpdateDamageText(int damage)
        {
            damageText.text = "-" + damage.ToString();
        }

        public void ToggleCollisionImmunity()
        {
            collisionImmunity = true;
            collisionImmunityGraphic.SetActive(true);

            Invoke("CollisionImmunityOver", collisionImmunityTimer);
        }

        public void CollisionImmunityOver()
        {
            collisionImmunity = false;
            collisionImmunityGraphic.SetActive(false);
        }

        public void ToggleTextPopup()
        {
            if (!damageText.isActiveAndEnabled)
            {
                TurnOnTextPopup();
            }

            Invoke("TurnOffTextPopup", 0.5f);
        }

        public void TurnOnTextPopup()
        {
            damageText.gameObject.SetActive(true);
        }

        public void TurnOffTextPopup()
        {
            damageText.gameObject.SetActive(false);
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        public void SetPlayerDamage(int damage)
        {
            playerDamage = damage;
        }

        public void SetProjectileVelocity(float speed)
        {
            projectileVelocity = speed;
        }

        public void SetCollisionImmunityTimer(float time)
        {
            collisionImmunityTimer = time;
        }

        public int GetPlayerDamage => playerDamage;
        public bool GetCollisionImmunity => collisionImmunity;
    }
}