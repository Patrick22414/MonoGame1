using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace MonoGame1
{
    public class MyGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _sprite;

        private VertexPositionTexture[] _vertexFloor;
        private Texture2D _textureCheckerboard;

        private BasicEffect _effect;
        private Camera _camera;

        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _vertexFloor = new VertexPositionTexture[6];
            _vertexFloor[0].Position = new Vector3(-20, -20, 0);
            _vertexFloor[1].Position = new Vector3(-20, +20, 0);
            _vertexFloor[2].Position = new Vector3(+20, +20, 0);

            _vertexFloor[3].Position = new Vector3(+20, +20, 0);
            _vertexFloor[4].Position = new Vector3(+20, -20, 0);
            _vertexFloor[5].Position = new Vector3(-20, -20, 0);

            const int repetitions = 20;

            _vertexFloor[0].TextureCoordinate = new Vector2(0, 0);
            _vertexFloor[1].TextureCoordinate = new Vector2(0, repetitions);
            _vertexFloor[2].TextureCoordinate = new Vector2(repetitions, 0);

            _vertexFloor[3].TextureCoordinate = _vertexFloor[1].TextureCoordinate;
            _vertexFloor[4].TextureCoordinate = new Vector2(repetitions, repetitions);
            _vertexFloor[5].TextureCoordinate = _vertexFloor[2].TextureCoordinate;

            _effect = new BasicEffect(_graphics.GraphicsDevice);

            _camera = new Camera(_graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _camera.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _effect.View = _camera.ViewMatrix;
            _effect.Projection = _camera.ProjectionMatrix;

            _effect.TextureEnabled = true;
            _effect.Texture = _textureCheckerboard;

            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphics.GraphicsDevice.DrawUserPrimitives(
                    primitiveType: PrimitiveType.TriangleList,
                    vertexData: _vertexFloor,
                    vertexOffset: 0,
                    primitiveCount: 2
                );
            }

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            _sprite = new SpriteBatch(GraphicsDevice);
            using (var stream = TitleContainer.OpenStream("Content/checkerboard.png"))
            {
                _textureCheckerboard = Texture2D.FromStream(this.GraphicsDevice, stream);
            }
        }

        protected override void UnloadContent()
        {
        }
    }
}