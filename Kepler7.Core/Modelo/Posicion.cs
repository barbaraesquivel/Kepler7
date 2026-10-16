namespace Kepler7.Core.Modelo
{
    /// <summary>
    /// Posicion (X, Y) de una unidad en el mapa.
    /// </summary>
    public class Posicion
    {
        public int X { get; }
        public int Y { get; }

        public Posicion(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Distancia en casillas sin contar diagonales
        public int DistanciaA(Posicion otra)
        {
            return Math.Abs(X - otra.X) + Math.Abs(Y - otra.Y);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}
