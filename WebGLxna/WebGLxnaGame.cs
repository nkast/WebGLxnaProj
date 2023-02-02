using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WebGLxna
{
    public class WebGLxnaGame : Game
    {
        private GraphicsDeviceManager graphics;

        TriangleComponent _tri;
        QuadTxComponent _triTx;
        SpriteComponent _sprite;
        SpriteFontComponent _spriteFont;
        RTComponent _rt;
        BatchTilesComponent _batchTilesComponent;

        public WebGLxnaGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _tri = new TriangleComponent(this);
            _triTx = new QuadTxComponent(this);
            _sprite = new SpriteComponent(this);
            _spriteFont = new SpriteFontComponent(this);
            _rt = new RTComponent(this);
            _batchTilesComponent = new BatchTilesComponent(this);
            //Components.Add(_tri);
            Components.Add(_triTx);
            //Components.Add(_sprite);
            //Components.Add(_spriteFont);
            //Components.Add(_rt);
            //Components.Add(_batchTilesComponent);

            //tests
            var wnd = this.Window;
            var wndbounds = wnd.ClientBounds;

            return;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void Update(GameTime gameTime)
        {
            var ms = Mouse.GetState();
            var ks = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
