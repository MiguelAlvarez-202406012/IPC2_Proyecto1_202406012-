using System;
using System.Xml;
using Proyecto_1_Fix.Clases;
using Proyecto1;

namespace Proyecto_1_Fix.Xml
{


  
    public static class Write //Clase Principal a instanciar ACA SE PROCEDER LOS PACIENTES Y PERIODOS
    {

        
        public static void Print(string inFile, string outFile ) //Metodo para imprimir los pacientes y mostrar sus estados
        {
            try
            {
                //Nuevo documento vacio -> XmlDocument _ = New XmlDocument //OBLIGATORIO PARA CREAR NUEVO DOCUMENTO
                XmlDocument docXML = new XmlDocument(); //Documento de entrada
                docXML.Load(inFile); //carga el documento.
                XmlDocument output = new XmlDocument(); //documento de salida
                
                //solo es una instancia para el documento, no tiene contenido aun
                //servira para incertar la raiz del documento

                //Insrtar encabezado -> XmlDeclaration _ = XmlDocument.Variable.CreateXmlDeclaration //OBLIGATORIO <CABECERA>
                XmlDeclaration declaracion = output.CreateXmlDeclaration("1.0","UTF-8",null); //ENCABEZADO DE XML
                //Crea una declaracion para saber la version y el formato del archivo, new XmlDoc estan relacionadas
                // ??? . appendChild <- sirve para insertar lo instanciado en el documento, por ejemplo etiquetas o hijos

                //Insertar raices -> XMLElement representa una etiqueta en el documento
                //         v Nombre de variable   v Crear elemento de <> 
                XmlElement pacientes = output.CreateElement("pacientes"); // <nodo padre de todos jesucristo>
                output.AppendChild(pacientes); //Raiz Hijo del elemento DocXML, estara encima de todos para doc de salida
                Console.WriteLine($"Se creo el documento con {pacientes.ChildNodes.Count} pacientes"); //cuenta cuandos nodos hijos tiene el documento 


                XmlNode? nodoRaiz = docXML.DocumentElement; //nodo principal del archivo de lectura, lo declara como Elemento
                if(nodoRaiz == null)
                {
                    Console.WriteLine("El archivo no existe, esta vacio o dañado, No se pudo concluir la impresion del reporte");
                    return; //explota la funcion

                }

                foreach (XmlNode nodoPaciente in nodoRaiz.ChildNodes) //el pivote del mismo tipo recorre el contenedor que tiene los nodos hijos
                {
                        if(nodoPaciente.Name == "paciente") //si tiene el nombre pacinte, procesar
                    {
                        XmlElement pacienteElm = ProcesarPacientes(output,nodoPaciente); //Procesa con el documento de entrada y cada nodo pivote del contenedor
                        if(pacienteElm != null) { pacientes.AppendChild(pacienteElm); /*<< Agrega al documento*/}

                    }
                }
                output.Save(outFile); // < IMPRESION
                Console.WriteLine($"Impresion Concluida... {outFile}");
            }
            catch (Exception e)
            {
                //en caso de que estalle
                Console.WriteLine(e);
            }
        }


        public static XmlElement ProcesarPacientes(XmlDocument doc,XmlNode nodoPaciente) // < nodo padre
        {
            try
            {
                //Del nodo paciente y el document de entrada se procesan los pacientes
             XmlNode? matrizNozo = nodoPaciente.SelectSingleNode("m");
             XmlNode? PeriodosNOzo = nodoPaciente.SelectSingleNode("periodos");

             XmlNode? datos = nodoPaciente.SelectSingleNode("datospersonales"); // los datos los busca en el unico nodo hijo de paciente llamado datos personales, no lleva periodos
                string nombre = datos.SelectSingleNode("nombre")?.InnerText ?? "???";
                string edad = datos.SelectSingleNode("edad")?.InnerText ?? "DESCONOCIDO"; //puede llegar vacio )?. remplaza el texto si llega vacio con "DESCONOCIDO"
                string m = matrizNozo?.InnerText ?? "0";
                string periodos = PeriodosNOzo?.InnerText ?? "ninguno";

                int matrix = int.Parse(m);
                int periods = int.Parse(periodos);

                XmlNode? rejillaNode = nodoPaciente.SelectSingleNode("rejilla"); //obtiene la rejilla 
                ListaCeldas patronInicial = new ListaCeldas(); //crea una nueva lista para analisis
                
                if (rejillaNode != null && rejillaNode.HasChildNodes)
                {
                    foreach (XmlNode nodoCelda in rejillaNode.ChildNodes)
                    {
                        if (nodoCelda.Name == "celda" && nodoCelda.Attributes != null)
                        {
                            string? fila = nodoCelda.Attributes["f"]?.Value; //encuentran celdas de las rejillas
                            string? columna = nodoCelda.Attributes["c"]?.Value; //Encuentran celdas de las rejillas
                            
                            if (fila != null && columna != null) //sean distintos de nulo
                            {
                                int filNum = int.Parse(fila); //parsea el valor
                                int colNum = int.Parse(columna); //parsea el valor
                                
                                if (filNum >= 1 && filNum <= matrix && colNum >= 1 && colNum <= matrix)
                                {
                                    //Agrega celda al patron
                                    patronInicial.AddCell(new Cell(filNum, colNum));
                                }
                            }
                        }
                    }
                }

                string status= AnalizarEstadoEnfermedad(patronInicial,matrix,periods);
                XmlElement ElmPaciente = doc.CreateElement("paciente");//nodo paciente
                XmlElement ElmDatos = doc.CreateElement("datospersonales");//nodo datos personales
                XmlElement nombreNode = doc.CreateElement("nombre");

                nombreNode.InnerText = nombre; // < asigna el valor dentro de las etiquetas, en este caso el de Nombre
                ElmDatos.AppendChild(nombreNode); //en el nodo de datos inserta el nodo hijo ya con los datos
                XmlElement edadNode = doc.CreateElement("edad");
                edadNode.InnerText = edad; 
                ElmDatos.AppendChild(edadNode); //va adentro del nodo datospersonales

                //Agregar peridos y matriz
                XmlElement periodosElm = doc.CreateElement("periodos");
                periodosElm.InnerText = periodos;
                ElmPaciente.AppendChild(periodosElm); //va afuera de datospersonales entonces estara como hijo de paciente

                XmlElement mElm = doc.CreateElement("m");
                mElm.InnerText = m;
                ElmPaciente.AppendChild(mElm); //misma situacion

                XmlElement statusElm = doc.CreateElement("estado");
                statusElm.InnerText = status;
                ElmPaciente.AppendChild(statusElm);
                //Regresa el nodoprincipal
                return ElmPaciente;


            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al procesar paciente: {e.Message}");
                throw;
            }

                


           

        }

        public static string AnalizarEstadoEnfermedad(ListaCeldas patronInicial, int m, int periodos)
        {
            ListaPatrones historialPatrones = new ListaPatrones();
            ListaCeldas patronActual = new ListaCeldas();
            
            // Copiar patrón inicial para  uso posterior
            CopiarLista(patronInicial, patronActual);
            ListaCeldas patronInicialCopia = new ListaCeldas(); //crea lsita para historial
            CopiarLista(patronInicial, patronInicialCopia);
            historialPatrones.AgregarPatron(patronInicialCopia, 0);
            
            // si se repite el periodo
            if (periodos == 1)
            {
                // Calcular siguiente generación para ver si el patrón inicial se mantiene
                ListaCeldas siguientePatron = CalcularSiguienteGeneracion(patronActual, m);
                if (siguientePatron.SameBS(patronInicial))
                {
                    return "MORTAL"; // Patrón se repite en período 1
                }
            }
            
            // Analizar evolución
            for (int periodo = 1; periodo <= periodos; periodo++)
            {
                ListaCeldas siguientePatron = CalcularSiguienteGeneracion(patronActual, m);
                
                // Verificar si este patrón ya apareció antes
                int periodoAnterior = historialPatrones.BuscarPatron(siguientePatron);
                
                if (periodoAnterior >= 0)
                {
                    int periodoRepeticionN1 = periodo - periodoAnterior;
                    
                    // Si N1 == 1, es MORTAL
                    if (periodoRepeticionN1 == 1)
                    {
                        return "MORTAL";
                    }
                    else
                    {
                        return "GRAVE"; // Pero el usuario pide solo MORTAL/CURABLE, así que CURABLE
                    }
                }
                
                // Guardar patrón actual en historial
                ListaCeldas patronCopia = new ListaCeldas();
                CopiarLista(siguientePatron, patronCopia);
                historialPatrones.AgregarPatron(patronCopia, periodo);
                
                // Actualizar para siguiente iteración
                patronActual.Vaciar();
                CopiarLista(siguientePatron, patronActual);
            }
            
            // Si no se detectaron patrones repetidos, es CURABLE
            return "CURABLE";
        }

        public static ListaCeldas CalcularSiguienteGeneracion(ListaCeldas patronActual, int m)
        {
            ListaCeldas siguientePatron = new ListaCeldas();

            // Recorrer todas las celdas posibles (1..m)
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int vecinosContagiados = ContarVecinosContagiados(patronActual, i, j, m);
                    bool estaContagiada = patronActual.ExistenCelda(i, j);

                    // Aplicar reglas del juego de la vida
                    if (estaContagiada && (vecinosContagiados == 2 || vecinosContagiados == 3))
                    {
                        siguientePatron.AddCell(new Cell(i, j));
                    }
                    else if (!estaContagiada && vecinosContagiados == 3)
                    {
                        siguientePatron.AddCell(new Cell(i, j));
                    }
                }
            }

            return siguientePatron;
        }

        public static int ContarVecinosContagiados(ListaCeldas patron, int fila, int columna, int m)
        {
            int contador = 0;
            
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int filaVecina = fila + i;
                    int columnaVecina = columna + j;

                    if (filaVecina >= 1 && filaVecina <= m && columnaVecina >= 1 && columnaVecina <= m)
                    {
                        if (patron.ExistenCelda(filaVecina, columnaVecina))
                        {
                            contador++;
                        }
                    }
                }
            }
            
            return contador;
        }

        public static void CopiarLista(ListaCeldas origen, ListaCeldas destino)
        {
            NodoCelda actual = origen.head;
            while (actual != null)
            {
                destino.AddCell(new Cell(actual.Celda.getFila(), actual.Celda.getColumna()));
                actual = actual.siguente;
            }
        }

    }
    
    
}