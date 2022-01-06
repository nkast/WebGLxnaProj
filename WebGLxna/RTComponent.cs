using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WebGLxna
{
    public class RTComponent : DrawableGameComponent
    {
        ResourceContentManager _content;
        SpriteBatch _sb;
        Texture2D _tx;
        RenderTarget2D _rt;

        public RTComponent(Game game) : base(game)
        {
            _content = new ResourceContentManager(game.Services);
            _content.RootDirectory = "Content";

        }


        protected override void LoadContent()
        {           
            _sb = new SpriteBatch(GraphicsDevice);
            _rt = new RenderTarget2D(GraphicsDevice, 128, 128, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PlatformContents);
            _tx = _content.Load<Texture2D>("Icon192");
        }


        public override void Update(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_rt);
            GraphicsDevice.Clear(Color.DarkOrange);

        }

        public override void Draw(GameTime gameTime)
        {
            _sb.Begin();
            _sb.Draw(_rt, Vector2.Zero, Color.White);
            _sb.End();

        }

        protected override void Dispose(bool disposing) 
        {
            if (disposing)
            {
                _sb.Dispose();
            }

            _content = null;
            _sb = null;
            _tx = null;
        }
    }
}
