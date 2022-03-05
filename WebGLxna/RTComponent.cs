using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WebGLxna
{
    public class RTComponent : DrawableGameComponent
    {
        SpriteBatch _sb;
        Texture2D _tx;
        RenderTarget2D _rt;

        public RTComponent(Game game) : base(game)
        {

        }


        protected override void LoadContent()
        {
            _sb = new SpriteBatch(GraphicsDevice);
            _rt = new RenderTarget2D(GraphicsDevice, 128, 128, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PlatformContents);
            var txstream = TitleContainer.OpenStream("icon-192.png");
            _tx = Texture2D.FromStream(GraphicsDevice, txstream);
        }


        public override void Update(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_rt);
            GraphicsDevice.Clear(Color.DarkOrange);

            _sb.Begin();
            _sb.Draw(_tx, new Vector2(0,0), null, Color.White, 0, Vector2.Zero, 0.2f, SpriteEffects.None, 0);
            _sb.Draw(_tx, new Vector2(50,50), null, Color.White, 2, Vector2.Zero, 0.2f, SpriteEffects.None, 0);
            _sb.End();

            GraphicsDevice.SetRenderTarget(null);
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

            _sb = null;
            _tx = null;
        }
    }
}
