using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WebGLxna
{
    public class BatchTilesComponent : DrawableGameComponent
    {
        ResourceContentManager _content;
        SpriteBatch _sb;
        Texture2D _tx1;
        Texture2D _tx2;

        const int SizeX = 50;
        const int SizeY = 30;
        Texture2D[,] _tiles;

        public BatchTilesComponent(Game game) : base(game)
        {
            _content = new ResourceContentManager(game.Services);
            _content.RootDirectory = "Content";

        }


        protected override void LoadContent()
        {           
            _sb = new SpriteBatch(GraphicsDevice);

            _tx1 = GenerateTexture(Color.DarkGreen);
            _tx2 = GenerateTexture(Color.DarkCyan);

            _tiles = new Texture2D[SizeX, SizeY];
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    _tiles[x, y] = ((x+y) % 2) == 0 ? _tx1 : _tx2;
                }
            }
        }

        private Texture2D GenerateTexture(Color color)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, 16, 16);
            var cdata = new Color[16 * 16];
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    cdata[i * 16 + j] = color;
                }
            }
            texture.SetData(cdata);
            return texture;
        }

        public override void Draw(GameTime gameTime)
        {
            _sb.Begin();
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    _sb.Draw(_tiles[x, y], new Vector2(x * 16, y * 16), Color.White);
                }
            }
            _sb.End();

            Game.Window.Title = ""+ this.GraphicsDevice.PresentationParameters.BackBufferWidth;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sb.Dispose();
            }

            _content = null;
            _sb = null;
            _tx1 = null;
            _tx2 = null;
        }
    }
}
