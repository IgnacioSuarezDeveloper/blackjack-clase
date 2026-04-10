using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class PokerCard
    {
        //palo.
        public Suit suit;

        //nombre.
        public Name name;

        //carta cara arriba
        private PictureBox pictureBoxFront;

        //carta cara abajo.
        private PictureBox pictureBoxBack;

        //cuadro de dibujo de la carta.
        public PictureBox Picture;

        //geter del Palo.
        public enum Suit
        {
           Treboles,
           Diamantes,
           Corazones,
           Picas,
        }

        //geter de Name.
        public enum Name
        {
            A,
            _2,
            _3,
            _4,
            _5,
            _6,
            _7,
            _8,
            _9,
            _10,
            J,
            Q,
            K,
        }

        //geter modo.
        public enum showmode
        {
            faceup,
            facedown,
        }
       
        //contructor de la carta.
        public PokerCard(Name name, Suit suit, Bitmap cardImageFront, Bitmap cardImageBack) 
        {
            this.name = name;
            this.suit = suit;

            //creamos la parte de adelante
            pictureBoxFront = new PictureBox();
            pictureBoxFront.Image = cardImageFront;

            //ajustar el picturebox al tamaño del sprite
            pictureBoxFront.SizeMode = PictureBoxSizeMode.AutoSize;

            //creamos la parte de atras
            pictureBoxBack = new PictureBox();
            pictureBoxBack.Image = cardImageBack;

            //ajustar el picturebox al tamaño del sprite
            pictureBoxBack.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        //seleccionar posicion.
        public void SetLocation(int x, int y)
        {
            pictureBoxBack.Location = new Point(x, y);
            pictureBoxFront.Location = new Point(x, y);
        }

        //devuelve la imagen de cara o boca abajo.
        public PictureBox GetPicture(showmode mode)
        {
            switch(mode)
            {
                case showmode.faceup:
                    return pictureBoxFront;
                case showmode.facedown:
                    return pictureBoxBack;
                default: return pictureBoxFront;
            }
        }

        //obtener valor de la carta.
        public static void GetValue(List<PokerCard>cards)
        {

        }
    }
}
