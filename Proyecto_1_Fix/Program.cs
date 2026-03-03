using System.Data;
using System.Diagnostics;
using Proyecto_1_Fix.Clases;
using Proyecto_1_Fix.Xml;

namespace Proyecto_1_Fix
{
    class program //PROGRAMA PRINCIPAL
    {
        
        static void Main(String[] args)
        {
            Boolean listStatus = false;


            //ELEGIR UN PACIENTE A ANALIZAR + EJECUCION DE SUS PERIODOS Y VERIFICAR SI ES GRAVE + GRAFICA
            //IMPRIMIR UNA SALIDA Y GENERAR SEGUN LOS PACIENTES INGRESADOS Y SUS CONDICIONES ACTUALES
            // LIMPIAR MEMORIA?



            
            Console.WriteLine("////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////");
            Console.WriteLine("Bienvenido al Laboratorio de investigación epidemiológica de Guatemala \n porfavor eliga una opcion para continuar");
            Console.WriteLine("////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////     ////");

            while (listStatus == false)
            {
                int opcion = -1;
                String opcionChecker = "";

                //Inicio de programa
                Console.WriteLine("Eliga una de las Siguentes opciones Disponibles:");
                
                    Console.WriteLine("\n 1) elegir Pacientes a Analizar \n 2) Generar Reporte de estados \n 3) Limpiar Memoria de Paciente (⚠️) \n 4) DEVELOP, crear Paciente \n 0) Salir");
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
                    //SALIR
                    case 0:
                       Console.WriteLine("Saliendo del Programa...");
                       listStatus = true;

                    break;
                    case 1: //ELEGIR PACIENTE
                    //Prueba para paciente DESDE XML 

                    Console.WriteLine("TEMPORAL FIX");
                    Console.WriteLine("ingrese LA RUTA del Archivo XML a Leer (Ingresar con extension .xml) \n ejemplo: D:\\USAC\\U_2026\\IPC2\\IPC2_Proyecto1_202406012\\IPC2_Proyecto1_202406012-\\Proyecto_1_Fix\\Entrada2.xml");
                    
                    string? filePath = Console.ReadLine()?.Trim(); //Puede ser vacio

                        while (string.IsNullOrEmpty(filePath)) 
                        //                                           es nulo o vacio de: filePath
                        {
                            Console.WriteLine("Se ingreso una ruta vacia vuelva a intentarlo");
                            filePath = Console.ReadLine();
                            
                        }

                    //llamada a clase de lectura
                    string realPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    string debugPath = "D:\\USAC\\U_2026\\IPC2\\IPC2_Proyecto1_202406012\\IPC2_Proyecto1_202406012-\\Proyecto_1_Fix\\Entrada2.xml"; //ruta original en la PC actual
                    
                  
                    LecturaXML.lecturaArchivo(realPath);
                    break;
                    case 2://REPORTE

                    //REPORTE CON EL MISMO ARCHIVO PERO DETERMINANDO SI LAS CELULAS SE ENCUENTRAN INFECTADAS O NO
                    //E IMPRIMIR EL ARCHIVO CON LOS PACIENTES ANALIZADOS
                    Console.WriteLine("TEMPORAL FIX");
                    Console.WriteLine("ingrese LA RUTA del Archivo XML a Procesar (Ingresar con extension .xml) \n ejemplo: D:\\USAC\\U_2026\\IPC2\\IPC2_Proyecto1_202406012\\IPC2_Proyecto1_202406012-\\Proyecto_1_Fix\\Entrada2.xml");
                    string? readFile = Console.ReadLine()?.Trim(); //Puede ser vacio
                    string output = "D:\\USAC\\U_2026\\IPC2\\IPC2_Proyecto1_202406012\\IPC2_Proyecto1_202406012-\\Proyecto_1_Fix\\Salida.xml";

                    Write.Print(readFile,output);
                    break;

                    case 3: //ELIMINAR DATA DE XML
                    break;

                 

                    default:

                    Console.WriteLine("OPCION NO VALIDA!");
                    break;
                }
            }

        }


    }
}