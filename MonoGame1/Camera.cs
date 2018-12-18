using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame1
{
    public class Camera
    {
        private GraphicsDevice _graphicsDevice;

        private const float RotationSpeed = (float) Math.PI / 1000;    
        
        private double _theta;
        private double _phi;
        private Vector2 _mousePosition = new Vector2(-1F);

        private Vector3 _position = new Vector3(0F, 0F, 2F);

        public Camera(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public Matrix ViewMatrix
        {
            get
            {
                var cameraTarget = new Vector3(
                    (float) (Math.Cos(_theta) * Math.Cos(_phi)),
                    (float) (Math.Sin(_theta) * Math.Cos(_phi)),
                    (float) (Math.Sin(_phi))
                );
                cameraTarget += _position;

                var upVector = Vector3.UnitZ;

                return Matrix.CreateLookAt(_position, cameraTarget, upVector);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                var fieldOfView = MathHelper.PiOver4;
                var aspectRatio = _graphicsDevice.Viewport.Width / (float) _graphicsDevice.Viewport.Height;
                var nearClip = 1F;
                var farClip = 200F;

                return Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClip, farClip);
            }
        }

        public void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();
            if (mouse.RightButton == ButtonState.Pressed)
            {
                if (_mousePosition != new Vector2(-1F))
                {
                    var dx = mouse.X - _mousePosition.X;
                    var dy = mouse.Y - _mousePosition.Y;

                    _theta += dx * RotationSpeed;
                    _phi += dy * RotationSpeed;
                    if (_phi > Math.PI / 2)
                        _phi = Math.PI / 2;
                    else if (_phi < -Math.PI / 2)
                        _phi = -Math.PI / 2;
                }

                _mousePosition = new Vector2(mouse.X, mouse.Y);
            }
            else
            {
                _mousePosition = new Vector2(-1F);
            }
        }
    }
}