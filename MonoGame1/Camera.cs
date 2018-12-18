using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame1
{
    public class Camera
    {
        private GraphicsDevice _graphicsDevice;
        
        private float _theta = 0F;
        private MouseState _mouse;
    
        private Vector3 _position = new Vector3(0F, 0F, 2F);

        public Camera(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public Matrix ViewMatrix
        {
            get
            {
                var rotationZ = Matrix.CreateRotationZ(_theta);
                var cameraTarget = Vector3.Transform(Vector3.UnitX, rotationZ) + _position;
                
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
            
        }
    }
}