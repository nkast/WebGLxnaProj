using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace WebGLxna
{
    public class SpriteFontComponent : DrawableGameComponent
    {
        ContentManager _content;
        SpriteBatch _sb;
        SpriteFont _font;

        public SpriteFontComponent(Game game) : base(game)
        {
            _content = new ContentManager(game.Services);
            _content.RootDirectory = "Content";

        }


        protected override void LoadContent()
        {           
            _sb = new SpriteBatch(GraphicsDevice);
            _font = _content.Load<SpriteFont>("Font");

        }

        public override void Draw(GameTime gameTime)
        {
            _sb.Begin();
            _sb.DrawString(_font, "Hello Blazor!", Vector2.Zero, Color.White);
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
            _font = null;
        }
    }
}
