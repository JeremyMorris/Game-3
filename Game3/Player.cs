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
    public class Player
    {
        private Game1 game;
        private float _maxSpeed = 0.4f;
        private float _runAcceleration = 0.08f;
        private float _jumpSpeed = 0.5f;
        private float _friction;
        private float _gravity;
        private int _framesSinceJump = 0;
        private Rectangle _animRectangle;
        private Rectangle _collisionBox;
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Dictionary<string, SoundEffect> _soundEffects;

        private Texture2D tempSprite;

        private KeyboardState _oldKeyboardState;
        private KeyboardState _currentKeyboardState;

        public float X { get; set; }

        public float Y { get; set; }

        public float HorizontalSpeed { get; set; }

        public float VerticalSpeed { get; set; }

        public float MaxSpeed { get { return _maxSpeed; } }

        public bool FacingRight { get; set; }

        public bool Airborne { get; set; }

        public bool FatalCollision { get; set; }

        public int Partition { get; set; }

        public Rectangle CollisionBox { get { return _collisionBox; } }

        public int Height { get { return _animRectangle.Height; } }

        public Player(Vector2 playerPosition, Game1 game, float f, float g)
        {
            this.game = game;

            X = playerPosition.X;
            Y = playerPosition.Y;
            HorizontalSpeed = 0;
            VerticalSpeed = 0;
            _friction = f;
            _gravity = g;

            _collisionBox = new Rectangle((int)X + 8, (int)Y + 8, 32, 56);
            _animRectangle = new Rectangle((int)X, (int)Y, 48, 64);

            FacingRight = true;

            LoadContent(game.Content);

            //_animationManager = new AnimationManager(_animations.First().Value);
            _oldKeyboardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            _currentKeyboardState = Keyboard.GetState();

            // Jump
            if (_currentKeyboardState.IsKeyDown(Keys.J) && !_oldKeyboardState.IsKeyDown(Keys.J) && !Airborne && !FatalCollision)
            {
                VerticalSpeed -= _jumpSpeed;
                Airborne = true;
            }

            // Count jumping frames
            if (Airborne) _framesSinceJump++;

            // Gravity
            if (Airborne)
            {
                if (_currentKeyboardState.IsKeyDown(Keys.J) && _framesSinceJump < 30 && !FatalCollision)
                {
                    VerticalSpeed += (_gravity / 4);
                }
                else
                {
                    VerticalSpeed += _gravity;
                }
            }

            // Run
            if (FatalCollision == false) // if the player has not died
            {
                if (_currentKeyboardState.IsKeyDown(Keys.D)) // move right
                {
                    HorizontalSpeed += _runAcceleration; // increase player's speed based on acceleration

                    if (HorizontalSpeed > _maxSpeed) HorizontalSpeed = _maxSpeed; // cap speed to player's max

                    FacingRight = true; // make the player face right
                }

                if (_currentKeyboardState.IsKeyDown(Keys.A)) // move left
                {
                    HorizontalSpeed -= _runAcceleration; // decrease player's speed based on acceleration

                    if (HorizontalSpeed < 0 - _maxSpeed) HorizontalSpeed = 0 - _maxSpeed; // cap speed to player's max

                    FacingRight = false; // make the player face left
                }
            }

            // reduce speed based on friction if a movement key is not held
            if (!_currentKeyboardState.IsKeyDown(Keys.D) && !_currentKeyboardState.IsKeyDown(Keys.A) && HorizontalSpeed != 0 || FatalCollision && HorizontalSpeed != 0)
            {
                if (HorizontalSpeed > 0)
                {
                    HorizontalSpeed -= _friction;
                    if (HorizontalSpeed < 0) HorizontalSpeed = 0;
                }
                else if (HorizontalSpeed < 0)
                {
                    HorizontalSpeed += _friction;
                    if (HorizontalSpeed > 0) HorizontalSpeed = 0;
                }
            }

            // Keep player on screen
            if (Y < 0)
            {
                SetY(0);
            }
            if (Y > game.GraphicsDevice.Viewport.Height - _animRectangle.Height)
            {
                SetY(game.GraphicsDevice.Viewport.Height - _animRectangle.Height);
                VerticalSpeed = 0;
                Airborne = false;
                _framesSinceJump = 0;
            }
            if (_collisionBox.X < 0) // left side
            {
                SetX(-8);
                HorizontalSpeed = 0;
            }
            if (_collisionBox.X > game.GraphicsDevice.Viewport.Width - _collisionBox.Width) // right side
            {
                SetX(game.GraphicsDevice.Viewport.Width - _collisionBox.Width - 8);
                HorizontalSpeed = 0;
            }

            SetY((int)(Y + VerticalSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds));
            SetX((int)(X + HorizontalSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds));

            // Update old keyboard state
            _oldKeyboardState = _currentKeyboardState;
        }

        public void LoadContent(ContentManager content)
        {
            // Load player animations
            var animations = new Dictionary<string, Animation>()
            {

            };

            _animations = animations;

            // Load SFX
            _soundEffects = new Dictionary<string, SoundEffect>()
            {

            };

            tempSprite = content.Load<Texture2D>("Player/Pixel");

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
            //_animationManager.Draw(spriteBatch, _animRectangle);
            spriteBatch.Draw(tempSprite, _animRectangle, Color.Red);
        }

        // Change the currently playing animation based on the player's state
        public void UpdateAnimation(GameTime gameTime)
        {
            _animationManager.Update(gameTime);

            if (FacingRight)
            {
                
            }
            else
            {
               
            }
        }
    }
}