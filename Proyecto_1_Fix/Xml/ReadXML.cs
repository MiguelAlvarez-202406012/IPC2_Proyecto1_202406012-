
using System.Xml;
using Proyecto_1_Fix.Clases;

namespace Proyecto_1_Fix.Xml
{

    public static class LecturaXML
    {
        // NodoHijo
        public static void ReadPacientes(XmlNode nodoPaciente)
        { 
            try
            {
                    //Lectura de un nodo Hijo de Pacientes
                    XmlNode? datosPacientes = nodoPaciente.SelectSingleNode("datospersonales");//Permite seleccionar un unico nodo
                    //Extraer los datos de pacientes

                    //SELECCION DE DATOS DE UN NODO HIJO
                            // variable que puede ser nulo si es nulo no ejecutara seleccionara el nodo
                                        //v . seleccionaElNodo De tipo "Nombre"(Puede ser nulo)?.
                                        //                                              v EN CASO QUE SEA NULO SUSTITUIRLO CON "" u Otro string a declarar en el lado Derecho
                    string nombre = datosPacientes?.SelectSingleNode("nombre")?.InnerText ??"";
                    string edad = datosPacientes?.SelectSingleNode("edad")?.InnerText ?? "0";
                    string periodos = datosPacientes?.SelectSingleNode("periodos")?.InnerText ?? "0";
                    string matriz = datosPacientes?.SelectSingleNode("m")?.InnerText ?? "0";

                    /*
                        ? = protege de datos Nulos (Colocarse despues de declarar las variables)
                        ?? = protege de nulos y sustituye con un valor string(citation needed) = ?? "valor a remplazaar"
                        // :D
                    */

                    //Crear un objeto de tipo paciente desde la clase de pacientes "patients"
                    //Instanciar Clase Pacientes

                    Paciente paciente = new Paciente(nombre,edad,int.Parse(periodos),int.Parse(matriz));
                    paciente.ImprimirDatosPaciente();
                
            }
            catch (Exception E)
            {
                Console.WriteLine($"Ocurrio un error {E}");   
                throw;
            }
            
            
        

        




        }
        public static void lecturaArchivo(string path)
        {
            if (!File.Exists(path))
            {
                //SI EL NO ARCHIVO EXISTE ENTONCES NO LEERA NADA Y EXPLOTA LA FUNCION
                Console.WriteLine($"Archivo no EXISTE, Porfavor ingrese una Ruta valida");
                return;
            }

            try
            {
                //crear documento en memoria
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path); //Leer desde una fuente externa, en este caso la ruta a especificar o el archivo
                //Obtener el nodo raiz
                //Representar un unico nodo de un documento XML
                // el ? al inicio de la declaracion indica nullified o que un dato puede ser nulo
                //en caso de que una raiz sea nula:
                XmlNode? pacientes = xmlDoc.DocumentElement; //Devuelve los elementos del documento, RAIZ = Pacientes
                //Contiene el elemento raiz de un documento.
                //Permite se nullified
                    if(pacientes == null) // en el caso raro que sea nulo
                {
                    Console.WriteLine($"no hay contenido en el Archivo o tiene un formato diferente");
                    return; // termina la funcion prinicpal
                }

                //ChildNodes = devuelve todos los nodos Hijos actuales los cuales son paciente
                Console.WriteLine($"Existen {pacientes.ChildNodes.Count} Pacientes en el Docuemnto");

                //Foreach = el tipo de dato que recorre el cOntenedor hasta el final de este
                //
                foreach (XmlNode nodoPaciente in pacientes.ChildNodes) //para un "Señalador de tipo Nodo en un contenedor de tipo NODO"
                //para cada nodo Hijo "paciente" buscar en el contenedor de NodosHIjos
                {
                    if(nodoPaciente.Name == "paciente")
                    {
                        //Si el nodoPaciente.Name se ubica en un nodo hijo que sea igual o tenga el nombre "paciente" procesara la funcion read paciente
                        ReadPacientes(nodoPaciente);
                    }
                    
                } 
            }
            catch (Exception exe)
            {
                
                Console.WriteLine($"ERROR OCURRIDO EN lecturaArchivo: {exe}");

            }


        }

    }
    

}