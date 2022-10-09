using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WebGLxna
{
    public class SpriteComponent : DrawableGameComponent
    {
        ResourceContentManager _content;
        SpriteBatch _sb;
        Texture2D _tx;

        public SpriteComponent(Game game) : base(game)
        {
            _content = new ResourceContentManager(game.Services);
            _content.RootDirectory = "Content";

        }


        protected override void LoadContent()
        {           
            _sb = new SpriteBatch(GraphicsDevice);
            _tx = _content.Load<Texture2D>("BlazorIcon");

        }

        public override void Draw(GameTime gameTime)
        {
            _sb.Begin();
            _sb.Draw(_tx, Vector2.Zero, Color.White);
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
