using System;
using System.Diagnostics;
using Proyecto1.xml;
using Proyecto1.Clases;


namespace Proyecto1
{
    class program //PROGRAMA PRINCIPAL
    {
        
        static void Main(String[] args)
        {
            Boolean listStatus = false;



            
            Console.WriteLine("////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////");
            Console.WriteLine("Bienvenido al Laboratorio de investigación epidemiológica de Guatemala \n porfavor eliga una opcion para continuar");
            Console.WriteLine("////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////");

            while (listStatus == false)
            {
                int opcion = -1;
                String opcionChecker = "";

                //Inicio de programa
                Console.WriteLine("Eliga una de las Siguentes opciones Disponibles:");
                
                    Console.WriteLine("\n 1) elegir Paciente a Analizar \n 2) ejecucion de periodos \n 3) Generar Reporte \n 4) Limpiar Memoria de Paciente (⚠️) \n 5) Salir");
                //Si  en caso que entre un string, no permita que crashee el programa
                opcionChecker = Console.ReadLine();
                //Validacion de Numeros
                // ! (no se pudo parsear) int.TryParse(Parsea opcion string a opcion que es un int activara el while haciendo que pida ingresar un numero de nuevo)
                while (!int.TryParse(opcionChecker,out opcion))
                {
                    Console.WriteLine("Ingrese un digito valido, se intento ingresar: " + opcionChecker);
                    opcionChecker = Console.ReadLine();
                }
                Console.WriteLine("Se eleigio la Opcion: " + opcion);

                switch(opcion){
                    case 1: //ELEGIR PACIENTE
                    //Prueba para paciente
                    



                    break;

                    case 2: //PERIODOS

                    
                    break;

                    case 3://REPORTE


                    break;

                    case 4: //ELIMINAR DATA DE XML
                    break;

                    case 5: //SALIR DE PROGRAMA
                    Console.WriteLine("Saliendo del Programa...");
                    listStatus = true;
                    break;

                    default:

                    Console.WriteLine("OPCION NO VALIDA!");
                    break;
                }
            }

        }


    }
}