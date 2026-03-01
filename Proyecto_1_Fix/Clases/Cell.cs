namespace Proyecto1
{
    public class Cell
    {
        private int fila;
        private int columna;

        public Cell(int f, int c)
        {
            this.fila = f;
            this.columna = c;
        }

        public int getFila()
        {
            return this.fila;
        }
        

        public int getColumna()
        {
            
            return this.columna;
        }

        public void setFila(int f)
        {
            this.fila = f;
        }

        public void setColumna(int c)
        {
            this.columna = c;
        }

        public void ImprimirDatosCelda()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Fila: {fila}");
            Console.WriteLine($"Columna: {columna}");
        }


    }

}