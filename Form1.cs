using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace BlackJack
{
    public partial class Form1 : Form
    {
        // Baraja completa
        private List<PokerCard> Deck = new List<PokerCard>();

        // Manos del crupier y jugador
        private List<PokerCard> croupier;
        private List<PokerCard> player;

        // Tamaño de las cartas
        private const int cardWidth = 95;
        private const int cardHeight = 126;

        // Posiciones Y de jugador y crupier
        private const int yPlayer = 250;
        private const int yCroupier = 50;

        // Offset inicial (para centrar)
        private int xInitOffset;
        private int yInitOffset;

        // Puntuaciones
        private int playerPoints;
        private int croupierPoints;
        private int count = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Desactivar botones al inicio
            StayButton.Enabled = false;
            AskForCardButton.Enabled = false;

            // Calcular posición inicial (centro pantalla)
            xInitOffset = this.Width / 2 - cardWidth;
            yInitOffset = this.Height / 2;

            // Cargar sprite completo de cartas
            Bitmap deckSprite = new Bitmap("deck.png");

            // Obtener imagen del reverso de la carta
            Bitmap cardBackImage = GetcartaBitmapFromImage(deckSprite, 13 * cardWidth, 1 * cardHeight);

            // Crear todas las cartas (52)
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 13; col++)
                {
                    Bitmap cardImage = GetcartaBitmapFromImage(deckSprite, col * cardWidth, row * cardHeight);

                    PokerCard card = new PokerCard(
                        (PokerCard.Name)col,
                        (PokerCard.Suit)row,
                        cardImage,
                        cardBackImage
                    );

                    Deck.Add(card);
                }
            }
        }

        // Recorta una carta del sprite
        private Bitmap GetcartaBitmapFromImage(Bitmap image, int x, int y)
        {
            Bitmap cardBitmap = new Bitmap(cardWidth, cardHeight);

            using (Graphics g = Graphics.FromImage(cardBitmap))
            {
                g.DrawImage(image,
                    new Rectangle(0, 0, cardWidth, cardHeight), // destino
                    new Rectangle(x, y, cardWidth, cardHeight), // origen
                    GraphicsUnit.Pixel);
            }
            return cardBitmap;
        }

        // INICIAR PARTIDA
        private async void StartGame(object sender, EventArgs e)
        {
            // Inicializar manos
            player = new List<PokerCard>();
            croupier = new List<PokerCard>();

            // Desactivar botones
            buttonStartGame.Enabled = false;
            AskForCardButton.Enabled = false;
            StayButton.Enabled = false;

            // Repartir 4 cartas (2 y 2)
            for (int i = 0; i < 4; i++)
            {
                // Carta aleatoria
                int randomIndex = RandomNumberGenerator.GetInt32(Deck.Count);
                PokerCard card = Deck.ElementAt(randomIndex);
                Deck.RemoveAt(randomIndex);

                // Añadir control visual
                Controls.Add(card.Picture);

                if (i < 2) // CRUPIER
                {
                    card.SetLocation(xInitOffset + (cardWidth * croupier.Count), yCroupier);
                    croupier.Add(card);
                    croupierPoints = CardsCount(croupier);

                    if (i == 0)
                        label1.Text = croupierPoints.ToString();
                }
                else // JUGADOR
                {
                    card.SetLocation(xInitOffset + (cardWidth * player.Count), yPlayer);
                    player.Add(card);
                    playerPoints = CardsCount(player);
                    label2.Text = playerPoints.ToString();
                }

                // Segunda carta del crupier boca abajo
                if (i == 1)
                    Controls.Add(card.GetPicture(PokerCard.showmode.facedown));
                else
                    Controls.Add(card.GetPicture(PokerCard.showmode.faceup));

                await Task.Delay(500);
            }

            // Activar botones
            AskForCardButton.Enabled = true;
            StayButton.Enabled = true;
        }

        // PEDIR CARTA
        private async void AskForCard(object sender, EventArgs e)
        {
            count++;
            AskForCardButton.Enabled = false;

            // Carta aleatoria
            int randomIndex = RandomNumberGenerator.GetInt32(Deck.Count);
            PokerCard card = Deck.ElementAt(randomIndex);
            Deck.RemoveAt(randomIndex);

            // Posición
            card.SetLocation(xInitOffset + (cardWidth * player.Count), yPlayer);

            player.Add(card);

            // Mostrar carta
            Controls.Add(card.GetPicture(PokerCard.showmode.faceup));

            AskForCardButton.Enabled = true;

            // Actualizar puntos
            playerPoints = CardsCount(player);
            croupierPoints = CardsCount(croupier);
            label2.Text = playerPoints.ToString();

            await Task.Delay(500);

            // Si se pasa
            if (playerPoints > 21)
            {
                AskForCardButton.Enabled = false;
                StayButton.Enabled = false;

                MessageBox.Show("Te Pasaste");

                // Mostrar carta oculta del crupier
                PokerCard hiddenCard = croupier[1];
                Controls.Remove(hiddenCard.GetPicture(PokerCard.showmode.facedown));
                Controls.Add(hiddenCard.GetPicture(PokerCard.showmode.faceup));

                label1.Text = croupierPoints.ToString();

                ResetGame();
            }
            else
            {
                StayButton.Enabled = true;
            }
        }

        // CALCULAR PUNTOS
        public int CardsCount(List<PokerCard> cards)
        {
            int sum = 0;

            foreach (PokerCard c in cards)
            {
                // Figuras valen 10
                if (c.name == PokerCard.Name.J || c.name == PokerCard.Name.Q || c.name == PokerCard.Name.K)
                {
                    sum += 10;
                }
                // As vale 11 o 1
                else if (c.name == PokerCard.Name.A)
                {
                    if (sum + 11 <= 21)
                        sum += 11;
                    else
                        sum += 1;
                }
                else
                {
                    sum += (int)c.name + 1;
                }
            }

            return sum;
        }

        // PLANTARSE
        private async void Stay(object sender, EventArgs e)
        {
            StayButton.Enabled = false;
            AskForCardButton.Enabled = false;

            // Mostrar carta oculta
            PokerCard hiddenCard = croupier[1];
            Controls.Remove(hiddenCard.GetPicture(PokerCard.showmode.facedown));
            Controls.Add(hiddenCard.GetPicture(PokerCard.showmode.faceup));

            await Task.Delay(500);

            // El crupier roba hasta 15
            while (croupierPoints < 15)
            {
                int randomIndex = RandomNumberGenerator.GetInt32(Deck.Count);
                PokerCard card = Deck.ElementAt(randomIndex);
                Deck.RemoveAt(randomIndex);

                card.SetLocation(xInitOffset + (cardWidth * croupier.Count), yCroupier);

                croupier.Add(card);
                croupierPoints = CardsCount(croupier);

                label1.Text = croupierPoints.ToString();

                Controls.Add(card.GetPicture(PokerCard.showmode.faceup));

                await Task.Delay(500);
            }

            ShowWiner();
        }

        // DECIDIR GANADOR
        public void ShowWiner()
        {
            label1.Text = croupierPoints.ToString();

            if (croupierPoints <= 21)
            {
                if (croupierPoints > playerPoints)
                    MessageBox.Show("Croupier Gana");
                else if (playerPoints > croupierPoints)
                    MessageBox.Show("Jugador Gana");
                else
                    MessageBox.Show("Empate");
            }

            ResetGame();
        }

        // REINICIAR PARTIDA
        private async void ResetGame()
        {
            playerPoints = 0;
            croupierPoints = 0;

            label1.Text = "0";
            label2.Text = "0";

            // Devolver cartas del crupier
            foreach (PokerCard card in croupier)
            {
                if (card != null)
                {
                    Deck.Add(card);
                    Controls.Remove(card.GetPicture(PokerCard.showmode.faceup));
                    await Task.Delay(100);
                }
            }

            croupier.Clear();

            // Devolver cartas del jugador
            foreach (PokerCard card in player)
            {
                if (card != null)
                {
                    Deck.Add(card);
                    Controls.Remove(card.GetPicture(PokerCard.showmode.faceup));
                }
                await Task.Delay(100);
            }

            player.Clear();

            // Reiniciar partida automáticamente
            buttonStartGame.Enabled = true;
            buttonStartGame.PerformClick();
        }
    }
}