
using System.Xml;
using Proyecto_1_Fix.Clases;
using Proyecto1;

namespace Proyecto_1_Fix.Xml
{


    public class NodoCelda
    {
        public Cell Celda {get;set;}
        public NodoCelda? siguente {get;set;} // < esto salva vidas y tiempo
        //Constructo
         public NodoCelda(Cell celda)
        {
            this.siguente = null;
            this.Celda = celda;
        }
    }


    public class ListaCeldas
    {
        public NodoCelda head{get;set;}
        public int Cont{get;set;}

        public ListaCeldas()
        {
            head = null;
            Cont = 0;

        }
         public void Vaciar()
        {
            head = null;
            Cont = 0;
        }

        public void AddCell(Cell celda)
        {
            NodoCelda newNode = new NodoCelda(celda);
            if (head == null)
            {
                head = newNode;//si no hay nadie agrega como cabecera a una nueva celda
                //cabecera sera el nodo creado
            }
            else
            {
                NodoCelda current = head; //crea un ndo igual a la cabecera
                while (current.siguente != null)// si el el pivote no tiene al siguente como nulo
                {
                    current = current.siguente; //sera el siguente
                }
                current.siguente = newNode; //hasta que llegue a nulo
                

            }
            Cont++;

        }

        public bool ExistenCelda(int fila,int columna)


        {
            NodoCelda actual = head; //toma como referencia a la cabeza
            while (actual != null) //revisara si no sea nulo
            {
                if (actual.Celda.getFila() == fila && actual.Celda.getColumna() == columna)
                //accede a la clase y llama a la funcion
                    return true;
                actual = actual.siguente; //Puede ser vacio y tomara el valor para detener el bucle
            }
            return false; //sino retorna falos;

        }

        
        //comparar celdas
        public bool SameBS(ListaCeldas theList)
        {
            if (this.Cont != theList.Cont) //si el contador es distinto 
                return false;

            NodoCelda actual1 = this.head; //obitene cada cabeza para validar ambas 
            NodoCelda actual2 = theList.head;

            while (actual1 != null && actual2 != null)
            {
                if (actual1.Celda.getFila() != actual2.Celda.getFila() ||
                    actual1.Celda.getColumna() != actual2.Celda.getColumna())
                    return false;
                
                actual1 = actual1.siguente;
                actual2 = actual2.siguente;
            }
            return true;
        }
    }


//lista enlazada
 public class NodoPatron
    {
        public ListaCeldas Patron { get; set; }
        public int Periodo { get; set; }
        public NodoPatron Siguiente { get; set; }

        public NodoPatron(ListaCeldas patron, int periodo)
        {
            Patron = patron;
            Periodo = periodo;
            Siguiente = null;
        }
    }

public class ListaPatrones
    {
        public NodoPatron head { get; private set; }
        public int Contador { get; private set; }

        public ListaPatrones()
        {
            head = null;
            Contador = 0;
        }

        public void AgregarPatron(ListaCeldas patron, int periodo)
        {
            NodoPatron nuevoNodo = new NodoPatron(patron, periodo);
            
            if (head == null)
            {
                head = nuevoNodo;
            }
            else
            {
                NodoPatron actual = head;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoNodo;
            }
            Contador++;
        }

        // Buscar si un patrón ya existe en el historial
        public int BuscarPatron(ListaCeldas patron)
        {
            NodoPatron actual = head;
            while (actual != null)
            {
                if (actual.Patron.SameBS(patron))
                    return actual.Periodo;
                actual = actual.Siguiente;
            }
            return -1;
        }
    }
    




    

    
    public static class LecturaXML
    {
            public static int contadorPeriodos = 0;
            /// static para que solo la utilize la clase actual



       
        public static void lecturaArchivo(string path) //PRINCIPAL
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

                //RAIZ PADRE = pacientes


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
                //como el hijo son paciente leera cuantos nodos hijos contiene Pacientes

                //Foreach = el tipo de dato que recorre el cOntenedor hasta el final de este
                //

                // variable que tomara el valor en cada iteracion
                //esta variable tomara el valor del mismo tipo que el contenedor en este caso pacientes.Childnodes
                foreach (XmlNode nodoPaciente in pacientes.ChildNodes) //para un "Señalador de tipo Nodo en un contenedor de tipo NODO"
                //para cada nodo Hijo "paciente" buscar en el contenedor de NodosHIjos
                // nodoPaciente -> "Pivote" del mimso tipo que pacientes"
                //buscara en los nodos hijos de pacientes (Childones)

                /*
                    <Pacientes>
                        <paciente> <- nodoPaciente (UBICACION ACTUAL)

                                            Data

                        <paciente> <- NODOS HIJOS
                    </Paciente>
                
                */
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


         // NodoHijo
        public static void ReadPacientes(XmlNode nodoPaciente)
        { 
            try
            {
                    //Lectura de un nodo Hijo de Pacientes
                    XmlNode? datosPacientes = nodoPaciente.SelectSingleNode("datospersonales");//Permite seleccionar un unico nodo
                    //del tipo nodo DatosPaciente = nodoIngresado que es el padre seleccione en el rango de nodos llamados "datospersonales" 
                    //Extraer los datos de pacientes

                    //SELECCION DE DATOS DE UN NODO HIJO
                            // variable que puede ser nulo si es nulo no ejecutara seleccionara el nodo
                                        //v . seleccionaElNodo De tipo "Nombre"(Puede ser nulo)?.
                                        //                                              v EN CASO QUE SEA NULO SUSTITUIRLO CON "" u Otro string a declarar en el lado Derecho
                    string nombre = datosPacientes?.SelectSingleNode("nombre")?.InnerText ??"";
                    string edad = datosPacientes?.SelectSingleNode("edad")?.InnerText ?? "0";

                    //Fuera de datospersonales
                    string periodos = nodoPaciente?.SelectSingleNode("periodos")?.InnerText ?? "0";
                    string matriz = nodoPaciente?.SelectSingleNode("m")?.InnerText ?? "0";
                    int periods = -1; //numero de periodos
                    int m = int.Parse(matriz);


                        if (int.TryParse(matriz,out periods))
                        {
                            periods = int.Parse(periodos);
                        }
                

                    /*
                        ? = protege de datos Nulos (Colocarse despues de declarar las variables)
                        ?? = protege de nulos y sustituye con un valor string(citation needed) = ?? "valor a remplazaar"
                        // :D
                    */

                    //Crear un objeto de tipo paciente desde la clase de pacientes "patients"
                    //Instanciar Clase Pacientes

                    Paciente paciente = new Paciente(nombre,edad,int.Parse(periodos),int.Parse(matriz));
                    paciente.ImprimirDatosPaciente();

                    XmlNode? reja = nodoPaciente.SelectSingleNode("rejilla");
                    //selecciona el nodo para rejas , tomando la tag que tenga el nombre rejilla
                    //es un nodo hijo con posibles subhijos

                    if (reja != null && reja.HasChildNodes) //Si el nodo reja no es nulo y tiene nodos hijos
                    {
                        ListaCeldas initial = new ListaCeldas(); //estado inicial
                        //REINICIO 
   

                    
                       
                    }
                
            }
            catch (Exception E)
            {
                Console.WriteLine($"Ocurrio un error {E}");   
                throw;
            }
        }

        public static void procesarRejas(XmlNode rejas, int m, ListaCeldas listaCeldas)
        {
            int cont = 0; //Contador de celdas

            foreach (XmlNode nodoCelda in rejas.ChildNodes) //para cada NodoCelda en el contenedor de rejas
            {
                if (nodoCelda.Name == "celda" && nodoCelda.Attributes != null)
                {
                    string? fila = nodoCelda.Attributes["f"]?.Value; //obetener los valores
                    string? columna = nodoCelda.Attributes["c"]?.Value;//obetener los valores

                    if (fila != null && columna != null)
                    {
                        int filNum = int.Parse(fila); //parsear los valores si no son nulos
                        int colNum = int.Parse(columna);

                        if (filNum >= 1 && filNum <= m && colNum >= 1 && colNum <= m)
                        {
                            Cell celda = new Cell(filNum, colNum); //Crear nueva celda y agregar los parametros
                            listaCeldas.AddCell(celda); //agregar celda creada a la lista
                            cont++;
                        }
                    }
                }
            }
            Console.WriteLine($"\nPatrón inicial - Total de celdas contagiadas: {cont}");
            ImprimirRejillaDesdeLista(listaCeldas, m);
        }

         public static void ImprimirRejillaDesdeLista(ListaCeldas listaCeldas, int m)
        {
            Console.WriteLine("Rejilla de células (1 = contagiada, 0 = sana):");
            Console.WriteLine(new string('-', m * 3));

            for (int i = 1; i <= m; i++) //para i que debe ser menor o igual al numero de filas del paciente
            {
                for (int j = 1; j <= m; j++) //lo mismo pero para columnas
                {
                    if (listaCeldas.ExistenCelda(i, j)) //valida si existe 
                        Console.Write("1 ");
                    else
                        Console.Write("0 ");
                }
                Console.WriteLine(); //salto de linea
            }
            Console.WriteLine(new string('-', m * 3));
        }

         public static void AnalizarEnfermedad(ListaCeldas patronInicial, int m, int periodos, string nombrePaciente)
        {
            Console.WriteLine($"\nANALIZANDO EVOLUCIÓN PARA {periodos} PERÍODOS...\n");

            ListaPatrones historialPatrones = new ListaPatrones();
            ListaCeldas patronActual = new ListaCeldas();
            
            // Copiar patrón inicial
            CopiarLista(patronInicial, patronActual);
            
            // Guardar patrón inicial en historial
            ListaCeldas patronInicialCopia = new ListaCeldas();
            CopiarLista(patronInicial, patronInicialCopia);
            historialPatrones.AgregarPatron(patronInicialCopia, 0);

            Console.WriteLine("Período 0 (Inicial):");
            ImprimirRejillaDesdeLista(patronActual, m);

            bool patronRepetido = false;
            int periodoRepeticion = -1;
            int periodoRepeticionN1 = -1;

            for (int periodo = 1; periodo <= periodos; periodo++)
            {
                Console.WriteLine($"\n--- Período {periodo} ---");
                
                // Calcular siguiente generación
                ListaCeldas siguientePatron = CalcularSiguienteGeneracion(patronActual, m);
                
                // Mostrar resultado
                ImprimirRejillaDesdeLista(siguientePatron, m);
                Console.WriteLine($"Células contagiadas: {siguientePatron.Cont}");

                // Verificar si este patrón ya apareció antes
                int periodoAnterior = historialPatrones.BuscarPatron(siguientePatron);
                
                if (periodoAnterior >= 0)
                {
                    patronRepetido = true;
                    periodoRepeticion = periodo;
                    periodoRepeticionN1 = periodo - periodoAnterior;
                    
                    Console.WriteLine($"\n¡PATRÓN REPETIDO DETECTADO!");
                    Console.WriteLine($"Este patrón apareció antes en el período {periodoAnterior}");
                    Console.WriteLine($"Período de repetición (N1): {periodoRepeticionN1}");
                    
                    break;
                }

                // Guardar patrón actual en historial
                ListaCeldas patronCopia = new ListaCeldas();
                CopiarLista(siguientePatron, patronCopia);
                historialPatrones.AgregarPatron(patronCopia, periodo);
                
                // Actualizar para siguiente iteración
                patronActual.Vaciar();
                CopiarLista(siguientePatron, patronActual);
            }

            // Determinar gravedad de la enfermedad
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine($"RESULTADO PARA PACIENTE: {nombrePaciente}");
            Console.WriteLine(new string('=', 50));

            if (patronRepetido)
            {
                if (periodoRepeticionN1 == 1)
                {
                    if (periodoRepeticion == 1)
                    {
                        Console.WriteLine("⚠️ RESULTADO: MORTAL - El patrón inicial se repite en el período 1 (enfermedad incurable)");
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ RESULTADO: MORTAL - Patrón se repite cada 1 período desde el período {periodoRepeticion - periodoRepeticionN1} (enfermedad incurable)");
                    }
                }
                else
                {
                    if (periodoRepeticion == 0)
                    {
                        Console.WriteLine($"🏥 RESULTADO: GRAVE - El patrón inicial es estable (se repite siempre)");
                    }
                    else
                    {
                        Console.WriteLine($"🏥 RESULTADO: GRAVE - Patrón se repite cada {periodoRepeticionN1} períodos desde el período {periodoRepeticion - periodoRepeticionN1}");
                    }
                }
            }
            else
            {
                Console.WriteLine("✅ RESULTADO: BENIGNA - No se detectaron patrones repetidos en el período analizado");
                Console.WriteLine("La enfermedad no presenta patrones de repetición");
            }
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
                        // Permanece contagiada
                        siguientePatron.AddCell(new Cell(i, j));
                    }
                    else if (!estaContagiada && vecinosContagiados == 3)
                    {
                        // Se contagia
                        siguientePatron.AddCell(new Cell(i, j));
                    }
                    // En cualquier otro caso, la célula está sana (no se agrega a la lista)
                }
            }

            return siguientePatron;
        }

        public static int ContarVecinosContagiados(ListaCeldas patron, int fila, int columna, int m)
        {
            int contador = 0;
            
            // Recorrer las 8 posiciones vecinas
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // Saltar la propia célula
                    if (i == 0 && j == 0)
                        continue;

                    int filaVecina = fila + i; //revisa las filas vecinas
                    int columnaVecina = columna + j; //revisa la columna vecina

                    // Verificar que esté dentro de los límites de la rejilla
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
            NodoCelda actual = origen.head; //Copia la cabeza del origne
            while (actual != null) //mientras no sea nulo
            {
                destino.AddCell(new Cell(actual.Celda.getFila(), actual.Celda.getColumna())); //agregara la celda
                actual = actual.siguente;
            }
        }
        /*
        
        
        */




    }
    

}