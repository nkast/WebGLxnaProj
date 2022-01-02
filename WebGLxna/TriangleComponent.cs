using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WebGLxna
{
    public class TriangleComponent : DrawableGameComponent
    {
        BasicEffect _fx;
        VertexBuffer _vb;

        public TriangleComponent(Game game) : base(game)
        {

        }


        protected override void LoadContent() 
        {
            _fx = new BasicEffect(GraphicsDevice);
            _fx.TextureEnabled = false;
            _fx.VertexColorEnabled = true;
            _fx.LightingEnabled = false;
            _fx.FogEnabled = false;

            var vertices = new VertexPositionColor[3];
            vertices[0].Position = new Vector3(-0.5f, -0.5f, 0.0f);
            vertices[1].Position = new Vector3( 0.0f,  0.5f, 0.0f);
            vertices[2].Position = new Vector3( 0.5f, -0.5f, 0.0f);
            vertices[0].Color = new Color(new Vector3(1.0f, 0.0f, 0.0f));
            vertices[1].Color = new Color(new Vector3(0.0f, 0.0f, 1.0f));
            vertices[2].Color = new Color(new Vector3(0.0f, 1.0f, 0.0f));
            _vb = new VertexBuffer(GraphicsDevice, VertexPositionColor.VertexDeclaration, 3, BufferUsage.None);
            _vb.SetData(vertices);
        }

        public override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            //GraphicsDevice.DepthStencilState = DepthStencilState.None;
            //GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
            //GraphicsDevice.BlendState = BlendState.Opaque;

            GraphicsDevice.SetVertexBuffer(_vb);

            _fx.Projection = Matrix.Identity;
            _fx.View = Matrix.Identity;
            _fx.World = Matrix.Identity;
            _fx.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
        }

        protected override void Dispose(bool disposing) 
        {
            if (disposing)
            {
                _vb.Dispose();
                _fx.Dispose();
            }
        }
    }
}
