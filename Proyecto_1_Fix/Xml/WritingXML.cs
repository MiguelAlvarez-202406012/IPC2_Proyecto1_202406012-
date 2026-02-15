using System;
using System.Xml;

namespace Proyecto_1_Fix.Xml
{
    public static class Write //Clase Principal a instanciar
    {
        public static void Print(string path) //Metodo para imprimir los pacientes y mostrar sus estados
        {
            try
            {
                //Nuevo documento vacio -> XmlDocument _ = New XmlDocument //OBLIGATORIO PARA CREAR NUEVO DOCUMENTO
                XmlDocument docXML = new XmlDocument(); //para crear un nuevo arvchivo en memoria
                //solo es una instancia para el documento, no tiene contenido aun
                //servira para incertar la raiz del documento

                //Insrtar encabezado -> XmlDeclaration _ = XmlDocument.Variable.CreateXmlDeclaration //OBLIGATORIO
                XmlDeclaration declaracion = docXML.CreateXmlDeclaration("1.0","UTF-8",null);
                //Crea una declaracion para saber la version y el formato del archivo, new XmlDoc estan relacionadas
                // ??? . appendChild <- sirve para insertar lo instanciado en el documento, por ejemplo etiquetas o hijos

                //Insertar raices -> XMLElement representa una etiqueta en el documento
                //         v Nombre de variable   v Crear elemento de <> 
                XmlElement pacientes = docXML.CreateElement("pacientes");
                docXML.AppendChild(pacientes); //Raiz Hijo del elemento DocXML, estara encima de todos

                //INSERTAR ELEMENTO HIJO
                //se debe referenciar una etiqueta para ser padre

                //WIP //AGREGAR CLIENTES

                //

                docXML.Save(path);
            }
            catch (Exception e)
            {
                //en caso de que estalle
                Console.WriteLine(e);
            }
        }

    }
    
    
}