using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace WebGLxna
{
    public class QuadTxComponent : DrawableGameComponent
    {
        ContentManager _content;
        Texture2D _tx;
        BasicEffect _fx;
        VertexBuffer _vb;
        IndexBuffer _ib;

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

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            //GraphicsDevice.DepthStencilState = DepthStencilState.None;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
            GraphicsDevice.BlendState = BlendState.Opaque;

            var rps = 1 / 4f;
            var world = Matrix.CreateRotationX(-MathHelper.Tau * rps  * (float)gameTime.TotalGameTime.TotalSeconds);

            _fx.World = world;
            _fx.View = Matrix.CreateLookAt(Vector3.Backward * 2f, Vector3.Zero, Vector3.Up);
            var aspectratio = GraphicsDevice.Viewport.AspectRatio;
            var hw = (2f * aspectratio) / 2f;
            var hh = (2f) / 2f;
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
