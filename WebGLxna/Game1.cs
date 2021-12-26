using Microsoft.Xna.Framework;

namespace WebGLxna
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;

        TriangleComponent _tri;
        QuadTxComponent _triTx;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _tri = new TriangleComponent(this);
            _triTx = new QuadTxComponent(this);
            //Components.Add(_tri);
            Components.Add(_triTx);

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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // clear
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

    }

}
