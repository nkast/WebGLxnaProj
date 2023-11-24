using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

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
            MouseState ms = Mouse.GetState();
            KeyboardState ks = Keyboard.GetState();
            GamePadState ps = default;
            try { ps = GamePad.GetState(PlayerIndex.One); }
            catch (NotImplementedException) { /* ignore gamePadState */ }

            if (keyboardState.IsKeyDown(Keys.Escape) ||
                keyboardState.IsKeyDown(Keys.Back) ||
                gamePadState.Buttons.Back == ButtonState.Pressed)
            {
                try { Exit(); }
                catch (PlatformNotSupportedException) { /* ignore */ }
            }

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
