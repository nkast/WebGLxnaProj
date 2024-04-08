using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WebGLxna
{
    public class QuadTxComponent : DrawableGameComponent
    {
        ContentManager _content;
        Texture2D _tx;
        BasicEffect _fx;
        VertexBuffer _vb;
        IndexBuffer _ib;
        VideoPlayer _videoPlayer;
        Video _video1;

        public QuadTxComponent(Game game) : base(game)
        {
            _content = new ContentManager(game.Services);
            _content.RootDirectory = "Content";

        }


        protected override void LoadContent()
        {
            var vertices = new VertexPositionColorTexture[4];
            vertices[0].Position = new Vector3(-0.5f, -0.5f, 0.0f);
            vertices[1].Position = new Vector3(-0.5f,  0.5f, 0.0f);
            vertices[2].Position = new Vector3( 0.5f,  0.5f, 0.0f);
            vertices[3].Position = new Vector3( 0.5f, -0.5f, 0.0f);
            vertices[0].Color = new Color(new Vector3(1.0f, 0.0f, 0.0f));
            vertices[1].Color = new Color(new Vector3(0.0f, 0.0f, 1.0f));
            vertices[2].Color = new Color(new Vector3(0.0f, 1.0f, 0.0f));
            vertices[3].Color = new Color(new Vector3(1.0f, 1.0f, 1.0f));
            vertices[0].TextureCoordinate = new Vector2(0.0f, 1.0f);
            vertices[1].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertices[2].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertices[3].TextureCoordinate = new Vector2(1.0f, 1.0f);

            var indices = new short[]
            {
                    0,1,2,
                    2,3,0
            };

            _tx = _content.Load<Texture2D>("BlazorIcon");
            for(int i = 0; i < vertices.Length; i++)
                vertices[i].Position.X *= ((float)_tx.Width / (float)_tx.Height);

            // 
            _video1 = _content.Load<Video>("Media\\Video1");
            _videoPlayer = new VideoPlayer();


            _vb = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.VertexDeclaration, 4, BufferUsage.None);
            _vb.SetData(vertices);

            _ib = new IndexBuffer(GraphicsDevice, IndexElementSize.SixteenBits, 6, BufferUsage.None);
            _ib.SetData(indices);

            _fx = new BasicEffect(GraphicsDevice);
            //_fx.LightingEnabled = false;
            _fx.TextureEnabled = true;
            _fx.VertexColorEnabled = false;
            
            _fx.FogEnabled = false;
        }

        KeyboardState prevkb;

        public override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Q) && prevkb.IsKeyUp(Keys.Q))
            {
                _videoPlayer.IsLooped = true;
                _videoPlayer.Play(_video1);
            }
            if (kb.IsKeyDown(Keys.W) && prevkb.IsKeyUp(Keys.W))
                _videoPlayer.Pause();
            if (kb.IsKeyDown(Keys.E) && prevkb.IsKeyUp(Keys.E))
                _videoPlayer.Resume();
            if (kb.IsKeyDown(Keys.R) && prevkb.IsKeyUp(Keys.R))
                _videoPlayer.Stop();

            this.Game.Window.Title = _videoPlayer.State.ToString();

            prevkb = kb;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            //GraphicsDevice.DepthStencilState = DepthStencilState.None;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
            GraphicsDevice.BlendState = BlendState.Opaque;

            if (_videoPlayer.Video != null)
            {
                Texture2D _videoTx = _videoPlayer.GetTexture();
                _tx = _videoTx;
            }

            float rps = 1 / 4f;
            Matrix world = Matrix.CreateRotationX(-MathHelper.Tau * rps  * (float)gameTime.TotalGameTime.TotalSeconds);

            _fx.World = world;
            _fx.View = Matrix.CreateLookAt(Vector3.Backward * 2f, Vector3.Zero, Vector3.Up);
            float aspectratio = GraphicsDevice.Viewport.AspectRatio;
            float hw = (2f * aspectratio) / 2f;
            float hh = (2f) / 2f;
            _fx.Projection = Matrix.CreatePerspectiveOffCenter(-hw, hw, -hh, hh, 1f, 3f);
            
            _fx.Texture = _tx;
            _fx.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.SetVertexBuffer(_vb);
            GraphicsDevice.Indices = _ib;

            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 2);
        }

        protected override void Dispose(bool disposing) 
        {
            if (disposing)
            {
                _vb.Dispose();
                _ib.Dispose();
                _fx.Dispose();
            }

            _content = null;
            _tx = null;
            _vb = null;
            _ib = null;
            _fx = null;

        }
    }
}
