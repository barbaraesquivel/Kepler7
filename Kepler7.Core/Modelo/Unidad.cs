namespace Kepler7.Core.Modelo
{
    /// <summary>
    /// Unidad del mapa (soldado, medico, enemigo, etc).
    /// Es el modelo base que usan Command, State, Strategy y Template Method.
    /// </summary>
    public class Unidad
    {
        public string Nombre { get; set; }
        public int Vida { get; set; }
        public int VidaMax { get; set; }
        public Posicion Posicion { get; set; }
        public int Energia { get; set; }

        public Unidad(string nombre, int vidaMax, int x, int y, int energia)
        {
            Nombre = nombre;
            VidaMax = vidaMax;
            Vida = vidaMax;
            Posicion = new Posicion(x, y);
            Energia = energia;
        }

        public bool EstaViva()
        {
            return Vida > 0;
        }

        public void MoverA(int x, int y)
        {
            Posicion = new Posicion(x, y);
        }

        public void RecibirDanio(int cantidad)
        {
            Vida -= cantidad;
            if (Vida < 0)
                Vida = 0;
        }

        public void Curar(int cantidad)
        {
            Vida += cantidad;
            if (Vida > VidaMax)
                Vida = VidaMax;
        }

        public override string ToString()
        {
            return Nombre + " - Vida: " + Vida + "/" + VidaMax + " - Energia: " + Energia + " - Pos: " + Posicion;
        }
    }
}
