using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Linq;

namespace MonoGame1
{
    public class MyGame : Game
    {
        private GraphicsDeviceManager _graphics;

        private VertexBuffer _vertexBuffer;
        private BasicEffect _effect;
        private Camera _camera;

        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            var vertices = new VertexPositionColor[3];
            vertices[0] = new VertexPositionColor(new Vector3(1F, 0F, 0F), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(-.5F, -.9F, 0F), Color.Blue);
            vertices[2] = new VertexPositionColor(new Vector3(-.5F, .9F, 0F), Color.Green);
            
            _vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            _vertexBuffer.SetData(vertices);
            
            _effect = new BasicEffect(GraphicsDevice);

            _camera = new Camera(GraphicsDevice);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _camera.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            _effect.World = _camera.WorldMatrix;
            _effect.View = _camera.ViewMatrix;
            _effect.Projection = _camera.ProjectionMatrix;
            _effect.VertexColorEnabled = true;
            
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetVertexBuffer(_vertexBuffer);
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
            }

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
        }
    }
}