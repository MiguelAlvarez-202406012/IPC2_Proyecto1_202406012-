namespace Proyecto1.Clases
{
    class Patient
    {
        //Atributos
        private string name;
        private string edad;
        private int periodos;
        private int matriz;

        //constructor
        public Patient(string n,string ed, int periods, int matrix)
        {
            n = this.name;
            ed = this.edad;
            periods = this.periodos;
            matrix = this.matriz;
        }


        //Metodos

        public String getName()
        {
            return name;
        }

        public void getPatient()
        {
            Console.WriteLine("////////// INFORMACION DE PACIENTE//////////");
            Console.WriteLine("NOMBRE: " + this.name);
            Console.WriteLine("EDAD:" + this.edad );
            Console.WriteLine("PERIODOS:" + this.periodos);
            Console.WriteLine("////////// INFORMACION DE PACIENTE//////////");
        }


    }


}