using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TiroAviao
{

    public struct DadosDoJogador
    {
        public Vector2 PosicaoDoAviao;
        public Color CorDoAviao;
        public float AnguloDoAviao;
        public bool TiroEmCurso;
        public Vector2 PosicaoDoTiro;
        public Vector2 DirecaoDoTiro;
        public float AnguloDoTiro;
    }

    public class GameXNAServidor : Microsoft.Xna.Framework.Game
    {
        IntPtr interfaceJogo;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GraphicsDevice device;
        private Texture2D TexturaDeFundo;
        private Texture2D TexturaDoAviao;
        private Texture2D TexturaDoTiro;
        private int LarguraDaTela;
        private int AlturaDaTela;
        private DadosDoJogador[] Jogador;
        private int NumeroDoJogador = -1;
        private float EscalaDoAviao;
        private float VelocidadeAviao = 2;
        private float VelocidadeTiro = 3;



        private Vector2 OrigemDaRotacaoDoAviao = new Vector2(11, 50);

        public GameXNAServidor(IntPtr InterfaceJogo)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.interfaceJogo = InterfaceJogo;
            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            Control.FromHandle((this.Window.Handle)).VisibleChanged += new EventHandler(Game1_VisibleChanged);
            graphics.ApplyChanges();
        }

        #region Tratamentos do Form

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = interfaceJogo;
        }

        private void Game1_VisibleChanged(object sender, EventArgs e)
        {
            if (Control.FromHandle((this.Window.Handle)).Visible == true)
                Control.FromHandle((this.Window.Handle)).Visible = false;
        }

        #endregion

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Combat Biplanes";
            Jogador = new DadosDoJogador[4];
            base.Initialize();
        }

        /// <summary>
        /// Este metodo é chamado quando o cliente se conecta no servidor. O servidor inicia um novo jogador, define
        /// sua posicao no tabuleiro e retorna para o cliente os dados do jogador. (cor, vida, numero)
        /// </summary>
        private void CriarJogador()
        {
            if (NumeroDoJogador == 3)
                return; // servidor esgotado

            NumeroDoJogador++;
            Jogador[NumeroDoJogador].CorDoAviao = ObterCorDoAviao(NumeroDoJogador);
            Jogador[NumeroDoJogador].PosicaoDoAviao = ObterPosicaoInicialDoAviao(NumeroDoJogador);
            Jogador[NumeroDoJogador].AnguloDoAviao = MathHelper.ToRadians(0);
            Jogador[NumeroDoJogador].TiroEmCurso = false;
        }

        private Color ObterCorDoAviao(int numeroDoJogador)
        {
            switch (numeroDoJogador)
            {
                case 0: return Color.Red;
                case 1: return Color.Green;
                case 2: return Color.Blue;
                case 3: return Color.Yellow;
            }
            return Color.White;
        }

        private Vector2 ObterPosicaoInicialDoAviao(int numeroDoJogador)
        {
            switch (numeroDoJogador)
            {
                case 0: return new Vector2(100, 193);
                case 1: return new Vector2(243, 123);
                case 2: return new Vector2(215, 174);
                case 3: return new Vector2(342, 193);
            }
            return new Vector2(0, 0);
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            device = graphics.GraphicsDevice;
            TexturaDeFundo = Content.Load<Texture2D>("background");
            TexturaDoAviao = Content.Load<Texture2D>("aviao");
            TexturaDoTiro = Content.Load<Texture2D>("rocket");
            LarguraDaTela = device.PresentationParameters.BackBufferWidth;
            AlturaDaTela = device.PresentationParameters.BackBufferHeight;

            CriarJogador();

            EscalaDoAviao = 40.0f / (float)TexturaDoAviao.Width;
        }


        protected override void UnloadContent()
        { }

        protected override void Update(GameTime gameTime)
        {
            LerTeclado();
            AtualizarPosicaoTiro();
            base.Update(gameTime);
        }

        private void AtualizarPosicaoTiro()
        {
            for (int i = 0; i <= NumeroDoJogador; i++)
                if (Jogador[i].TiroEmCurso)
                {
                    Jogador[i].PosicaoDoTiro += Jogador[i].DirecaoDoTiro;
                    Jogador[i].AnguloDoTiro = (float)Math.Atan2(Jogador[i].DirecaoDoTiro.X, -Jogador[i].DirecaoDoTiro.Y);

                    if (Jogador[i].PosicaoDoTiro.Y > AlturaDaTela || Jogador[i].PosicaoDoTiro.X < 0 || Jogador[i].PosicaoDoTiro.X > LarguraDaTela)
                        Jogador[i].TiroEmCurso = false;

                    EnviarPosicaoDoTiroParaClientes(i);
                }
        }

        private bool VerificarTiroEmMovimento(int numeroDoJogador)
        {
            return Jogador[numeroDoJogador].TiroEmCurso;
        }


        // este metodo é chamado pelo cliente, que envia a requisicao para movimentar em determinada direcao.
        // O servidor incrementa a posicao do aviao do cliente e reenvia esta mesma posicao para todos os clientes.
        // os clientes entao redesenham a posicao do aviao na sua tela
        private enum Direcao { Esquerda, Direita, Cima, Baixo };
        private void AtualizarPosicaoDoAviao(int numeroDoJogador, Direcao direcao)
        {
            switch (direcao)
            {
                case Direcao.Esquerda:
                    Jogador[numeroDoJogador].PosicaoDoAviao.X -= VelocidadeAviao;
                    break;
                case Direcao.Direita:
                    Jogador[numeroDoJogador].PosicaoDoAviao.X += VelocidadeAviao;
                    break;
                case Direcao.Cima:
                    Jogador[numeroDoJogador].PosicaoDoAviao.Y -= VelocidadeAviao;
                    break;
                case Direcao.Baixo:
                    Jogador[numeroDoJogador].PosicaoDoAviao.Y += VelocidadeAviao;
                    break;
                default:
                    break;
            }
            enviarPosicaoDoAviaoParaClientes(numeroDoJogador);
        }

        // este metodo é chamado pelo cliente quando o mesmo dispara um tiro. O servidor calcula a posicao
        // inicial do tiro e seta a propriedade tiro em movimento deste jogador para true. A partir deste
        // momento o servidor fica claculando a nova posicao do tiro de cada cliente, e envia essas posicoes para
        // todos clientes.
        private void DispararTiro(int numeroDoJogador)
        {
            Jogador[numeroDoJogador].TiroEmCurso = true;

            Jogador[numeroDoJogador].PosicaoDoTiro = Jogador[numeroDoJogador].PosicaoDoAviao;
            Jogador[numeroDoJogador].PosicaoDoTiro.X += 20;
            Jogador[numeroDoJogador].PosicaoDoTiro.Y -= 10;
            Jogador[numeroDoJogador].AnguloDoTiro = Jogador[numeroDoJogador].AnguloDoAviao;

            Vector2 up = new Vector2(1, 0);
            Matrix rotMatrix = Matrix.CreateRotationZ(Jogador[numeroDoJogador].AnguloDoTiro);
            Jogador[numeroDoJogador].DirecaoDoTiro = Vector2.Transform(up, rotMatrix);
            Jogador[numeroDoJogador].DirecaoDoTiro *= VelocidadeTiro;
        }

        // este metodo é chamado sempre que ha alguma alteracao na posicao de algum aviao. Ele
        // informa a todos os clientes a nova posicao do aviao.
        private void enviarPosicaoDoAviaoParaClientes(int numeroDoJogador)
        {

        }

        // este metodo é chamado sempre que ha alguma alteracao na posicao de algum tiro.
        // ele envia a nova posicao do tiro para todos os clientes.
        private void EnviarPosicaoDoTiroParaClientes(int numeroDoJogador)
        {

        }



        // metodo que simula o teclado de um cliente
        private void LerTeclado()
        {
            KeyboardState keybState = Keyboard.GetState();
            if (keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                AtualizarPosicaoDoAviao(0, Direcao.Esquerda);
            if (keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                AtualizarPosicaoDoAviao(0, Direcao.Direita);
            if (keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                AtualizarPosicaoDoAviao(0, Direcao.Cima);
            if (keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                AtualizarPosicaoDoAviao(0, Direcao.Baixo);

            if (keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && !VerificarTiroEmMovimento(0))
                DispararTiro(0);
        }


        // metodo simula o desenho de um cliente na tela
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            DesenharCenario();
            DesenharJogadores();
            DesenharTiro();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DesenharCenario()
        {
            Rectangle screenRectangle = new Rectangle(0, 0, LarguraDaTela, AlturaDaTela);
            spriteBatch.Draw(TexturaDeFundo, screenRectangle, Color.White);
        }

        private void DesenharJogadores()
        {
            foreach (DadosDoJogador player in Jogador)
            {
                spriteBatch.Draw(TexturaDoAviao, player.PosicaoDoAviao, null, player.CorDoAviao, player.AnguloDoAviao,
                    OrigemDaRotacaoDoAviao, EscalaDoAviao, SpriteEffects.None, 1);
            }
        }

        private void DesenharTiro()
        {
            if (Jogador[0].TiroEmCurso)
                spriteBatch.Draw(TexturaDoTiro, Jogador[0].PosicaoDoTiro, null, Jogador[0].CorDoAviao,
                    Jogador[0].AnguloDoTiro, new Vector2(42, 240), 0.1f, SpriteEffects.None, 1);
        }

    }
}
