using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public PatrolPath path;
        public AudioClip ouch;

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;

        public Bounds Bounds => _collider.bounds;

        public int damage;

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        //using OnCollisionStay2D instead of OnCollisionEnter2D for player


        void OnCollisionStay2D(Collision2D collision)
        {

            var bullet = collision.gameObject.GetComponent<Bullet>();

            if (bullet != null)
            {
                this.GetComponent<Health>().Decrement(model.player.bulletDamage);

                if (!(this.GetComponent<Health>().IsAlive))
                {
                    Schedule<EnemyDeath>().enemy = this.GetComponent<EnemyController>();
                }
                Destroy(bullet.gameObject);
            }

            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                if(player.invincible == false && player.health.IsAlive)
                {
                    player.StartCoroutine("Invincibility");
                    var ev = Schedule<PlayerEnemyCollision>();
                    ev.player = player;
                    ev.enemy = this;
                    Vector2 difference = (player.transform.position - transform.position).normalized;
                    Vector2 force = difference * 50;
                    player.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                }

            }


        }


        void Update()
        {
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
        }

    }
}