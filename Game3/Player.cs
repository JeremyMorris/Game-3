﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Game3
{
    public enum State
    {
        Standing,
        Running,
        Jumping
    };

    public class Player
    {
        private Game1 game;
        private World world;
        public float _maxSpeed = 0.4f;
        public float _runAcceleration = 0.08f;
        public float _jumpSpeed = 0.5f;
        public float _friction;
        public float _gravity;
        public int _framesSinceJump = 0;
        private int _footstepCount = 200;
        private Rectangle _animRectangle;
        private Rectangle _collisionBox;
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Dictionary<string, SoundEffect> _soundEffects;
        public PlayerState States;

        private KeyboardState _oldKeyboardState;
        private KeyboardState _currentKeyboardState;

        public IPlayerState State { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float HorizontalSpeed { get; set; }

        public float VerticalSpeed { get; set; }

        public float MaxSpeed { get { return _maxSpeed; } }

        public bool FacingRight { get; set; }

        public bool FatalCollision { get; set; }

        public int Partition { get; set; }

        public Rectangle CollisionBox { get { return _collisionBox; } }

        public int Height { get { return _animRectangle.Height; } }

        public Player(Vector2 playerPosition, Game1 game, World world, float f, float g)
        {
            this.game = game;
            this.world = world;

            X = playerPosition.X;
            Y = playerPosition.Y;
            HorizontalSpeed = 0;
            VerticalSpeed = 0;
            _friction = f;
            _gravity = g;

            _collisionBox = new Rectangle((int)X + 8, (int)Y + 8, 32, 56);
            _animRectangle = new Rectangle((int)X, (int)Y, 48, 64);

            FacingRight = true;
            States = new PlayerState();

            LoadContent(game.Content);

            _animationManager = new AnimationManager(_animations.First().Value);
            _oldKeyboardState = Keyboard.GetState();
            State = States.falling;
        }

        public void Update(GameTime gameTime)
        {
            _currentKeyboardState = Keyboard.GetState();

            HandleSFX(gameTime);

            if (world.GameComplete)
            {
                State = States.gameComplete;
            }

            // Invoke player's state object
            State.Update(this, _oldKeyboardState, _currentKeyboardState, game);

            // Keep player in bounds
            KeepInBounds();

            SetY((int)(Y + VerticalSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds));
            SetX((int)(X + HorizontalSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds));

            // Update old keyboard state
            _oldKeyboardState = _currentKeyboardState;
        }

        public void HandleSFX(GameTime gameTime)
        {
            // Play running SFX
            if (State == States.running)
            {
                if (_animationManager.CurrentFrame == 0 && _footstepCount > 200)
                {
                    _soundEffects["Footstep1"].Play(1f, 0, 0);
                    _footstepCount = 0;
                }
                else if (_animationManager.CurrentFrame == 5 && _footstepCount > 200)
                {
                    _soundEffects["Footstep2"].Play(1f, 0, 0);
                    _footstepCount = 0;
                }
            }
            _footstepCount += gameTime.ElapsedGameTime.Milliseconds;

            // Jump
            if (_currentKeyboardState.IsKeyDown(Keys.J) && !_oldKeyboardState.IsKeyDown(Keys.J) && State != States.jumping && State != States.falling)
            {
                _soundEffects["Jump"].Play(0.7f, 0.3f, 0);
            }
        }

        public void KeepInBounds()
        {
            if (Y < 0)
            {
                SetY(0);
            }
            if (Y > world.Bounds.Height - _animRectangle.Height)
            {
                SetY(world.Bounds.Height - _animRectangle.Height);
                VerticalSpeed = 0;
                State = States.standing;
                _framesSinceJump = 0;
            }
            if (_collisionBox.X < 0) // left side
            {
                SetX(-8);
                HorizontalSpeed = 0;
            }
            if (_collisionBox.X > world.Bounds.Width - _collisionBox.Width) // right side
            {
                SetX(world.Bounds.Width - _collisionBox.Width - 8);
                HorizontalSpeed = 0;
            }
        }

        public void LoadContent(ContentManager content)
        {
            // Load player animations
            var animations = new Dictionary<string, Animation>()
            {
                { "IdleRight", new Animation(content.Load<Texture2D>("Player/Guy-IdleRight"), 4) },
                { "IdleLeft", new Animation(content.Load<Texture2D>("Player/Guy-IdleLeft"), 4) },
                { "RunRight", new Animation(content.Load<Texture2D>("Player/Guy-RunRight"), 10, true, true) },
                { "RunLeft", new Animation(content.Load<Texture2D>("Player/Guy-RunLeft"), 10, true, true) },
                { "JumpRight", new Animation(content.Load<Texture2D>("Player/Guy-JumpRight"), 3, false, true) },
                { "JumpLeft", new Animation(content.Load<Texture2D>("Player/Guy-JumpLeft"), 3, false, true) },
                { "DeathRight", new Animation(content.Load<Texture2D>("Player/Guy-DeathRight"), 6, false, false) },
                { "DeathLeft", new Animation(content.Load<Texture2D>("Player/Guy-DeathLeft"), 6, false, false) },
            };

            // Correct frame speeds
            animations["RunRight"].FrameSpeed = 75f;
            animations["RunLeft"].FrameSpeed = 75f;
            animations["JumpRight"].FrameSpeed = 150f;
            animations["JumpLeft"].FrameSpeed = 150f;
            animations["DeathRight"].FrameSpeed = 100f;
            animations["DeathLeft"].FrameSpeed = 100f;

            _animations = animations;

            // Load SFX
            _soundEffects = new Dictionary<string, SoundEffect>()
            {
                { "Footstep1", content.Load<SoundEffect>("SoundEffects/Footstep1") },
                { "Footstep2", content.Load<SoundEffect>("SoundEffects/Footstep2") },
                { "Jump", content.Load<SoundEffect>("SoundEffects/Jump") },
            };

        }

        public void SetX(int x)
        {
            _collisionBox.X = x + 8;
            _animRectangle.X = x;
            this.X = x;

            Partition = (int)_collisionBox.X / 100;
        }

        public void SetY(int y)
        {
            _collisionBox.Y = y + 8;
            _animRectangle.Y = y;
            this.Y = y;
        }

        // Draw the player
        public void Draw(SpriteBatch spriteBatch)
        {
            _animationManager.Draw(spriteBatch, _animRectangle);
        }

        // Change the currently playing animation based on the player's state
        public void UpdateAnimation(GameTime gameTime)
        {
            _animationManager.Update(gameTime);

            if (FacingRight)
            {
                if (State == States.jumping || State == States.falling) { _animationManager.Play(_animations["JumpRight"]); }
                else if (State == States.standing || State == States.gameComplete) { _animationManager.Play(_animations["IdleRight"]); }
                else { _animationManager.Play(_animations["RunRight"]); }
            }
            else
            {
                if (State == States.jumping || State == States.falling) { _animationManager.Play(_animations["JumpLeft"]); }
                else if (State == States.standing || State == States.gameComplete) { _animationManager.Play(_animations["IdleLeft"]); }
                else { _animationManager.Play(_animations["RunLeft"]); }
            }
        }
    }
}