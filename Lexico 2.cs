using System;

namespace Lexico_2
{

    public class Lexico_2 : Token
    {
        StreamReader archivo;
        StreamWriter log;
        const int f = -1;
        const int e = -2;
        public Lexico_2()
        {
            archivo = new StreamReader("C:\\Users\\Fernando Hernández\\Desktop\\ITQ\\4to Semestre\\Lenguajes y Autómatas 1\\Lexico 2\\Prueba.cpp");
            log = new StreamWriter("C:\\Users\\Fernando Hernández\\Desktop\\ITQ\\4to Semestre\\Lenguajes y Autómatas 1\\Lexico 2\\Prueba.log");
            log.AutoFlush = true;
        }

        public void Cerrar()
        {
            archivo.Close();
            log.Close();
        }

        private void clasifica(int estado)
        {
            switch (estado)
            {
                case 1:
                    setClasificacion(tipos.identificador);
                    break;
                case 2:
                    setClasificacion(tipos.numero);
                    break;
                case 8:
                    setClasificacion(tipos.asignacion);
                    break;
                case 9:
                case 17:
                case 18:
                case 19:
                    setClasificacion(tipos.operador_relacional);
                    break;
                case 10:
                case 13:
                case 14:
                    setClasificacion(tipos.caracter);
                    break;
                case 11:
                    setClasificacion(tipos.inicializacion);
                    break;
                case 12:
                    setClasificacion(tipos.fin_sentencia);
                    break;
                case 15:
                case 16:
                    setClasificacion(tipos.operador_logico);
                    break;
                case 21:
                case 22:
                    setClasificacion(tipos.operador_termino);
                    break;
                case 23:
                    setClasificacion(tipos.incremento_termino);
                    break;
                case 24:
                case 29:
                    setClasificacion(tipos.operador_factor);
                    break;
                case 25:
                    setClasificacion(tipos.incremento_factor);
                    break;
                case 26:
                    setClasificacion(tipos.cadena);
                    break;
                case 28:
                    setClasificacion(tipos.operador_ternario);
                    break;
                case 33:
                    setClasificacion(tipos.caracter);
                    break;
            }
        }

        public void NextToken()
        {
            string buffer = "";
            char c;
            int estado = 0;

            while (estado >= 0)
            {
                c = (char)archivo.Peek(); //Función de transición.
                estado = Automata(estado, c);
                clasifica(estado);
                if (estado >= 0)
                {
                    archivo.Read();
                    if (estado > 0)
                        buffer += c;
                    else
                        buffer = "";
                }
            }
            setContenido(buffer);
            switch (buffer)
            {
                case "char":
                case "int":
                case "float":
                case "double":
                case "long":
                    setClasificacion(tipos.tipo_datos);
                    break;
                case "private":
                case "protected":
                case "public":
                    setClasificacion(tipos.zona);
                    break;
                case "if":
                case "else":
                case "switch":
                    setClasificacion(tipos.condicion);
                    break;
                case "while":
                case "for":
                case "do":
                    setClasificacion(tipos.ciclo);
                    break;
            }
            log.WriteLine(getContenido() + " | " + getClasificacion());
        }

        private int Automata(int estado, char t)
        {
            switch (estado)
            {
                case 0:
                    if (char.IsLetter(t))
                        estado = 1;
                    else if (char.IsDigit(t))
                        estado = 2;
                    else if (t == '=')
                        estado = 8;
                    else if (t == ':')
                        estado = 10;
                    else if (t == ';')
                        estado = 12;
                    else if (t == '&')
                        estado = 13;
                    else if (t == '|')
                        estado = 14;
                    else if (t == '!')
                        estado = 15;
                    else if (t == '>')
                        estado = 18;
                    else if (t == '<')
                        estado = 19;
                    else if (t == '+')
                        estado = 21;
                    else if (t == '-')
                        estado = 22;
                    else if (t == '%' || t == '*')
                        estado = 24;
                    else if (t == '"')
                        estado = 26;
                    else if (t == '?')
                        estado = 28;
                    else if (t == '/')
                        estado = 29;
                    else if (!char.IsWhiteSpace(t))
                        estado = 33;
                    break;
                case 1:
                    if (!char.IsLetterOrDigit(t))
                        estado = f;
                    break;
                case 2:
                    if (t == '.')
                        estado = 3;
                    else if (t == 'E' || t == 'e')
                        estado = 5;
                    else if (!char.IsDigit(t))
                        estado = f;
                    break;
                case 3:
                    if (char.IsDigit(t))
                        estado = 4;
                    else
                        estado = e;
                    break;
                case 4:
                    if (t == 'E' || t == 'e')
                        estado = 5;
                    else if (!char.IsDigit(t))
                        estado = f;
                    break;
                case 5:
                    if (t == '+' || t == '-')
                        estado = 6;
                    else if (char.IsDigit(t))
                        estado = 7;
                    else 
                        estado = e;
                    break;
                case 6:
                    if (char.IsDigit(t))
                        estado = 7;
                    else
                        estado = e;
                    break;
                case 7:
                    if (!char.IsDigit(t))
                        estado = f;
                    break;
                case 8:
                    if (t == '=')
                        estado = 9;
                    else
                        estado = f;
                    break;
                case 9:
                case 11:
                case 12:
                case 16:
                case 17:
                case 20:
                case 23:
                case 25:
                case 27:
                case 28:
                case 33:
                    estado = f;
                    break;
                case 10:
                    if (t == '=')
                        estado = 11;
                    else
                        estado = f;
                    break;
                case 13:
                    if (t == '&')
                        estado = 16;
                    else
                        estado = f;
                    break;
                case 14:
                    if (t == '|')
                        estado = 16;
                    else
                        estado = f;
                    break;
                case 15:
                    if (t == '=')
                        estado = 17;
                    else
                        estado = f;
                    break;
                case 18:
                    if (t == '=')
                        estado = 20;
                    else
                        estado = f;
                    break;
                case 19:
                    if (t == '=' || t == '>')
                        estado = 20;
                    else
                        estado = f;
                    break;
                case 21:
                    if (t == '+' || t == '=')
                        estado = 23;
                    else
                        estado = f;
                    break;
                case 22:
                    if (t == '-' || t == '=')
                        estado = 23;
                    else
                        estado = f;
                    break;
                case 24:
                    if (t == '=')
                        estado = 23;
                    else
                        estado = f;
                    break;
                case 26:
                    if (t == (10) || FinArchivo() == true)
                        estado = e;
                    else if (t == '"')
                        estado = 27;
                    else
                        estado = 26;
                    break;
                case 29:
                    if (t == '=')
                        estado = 25;
                    else if (t == '*')
                        estado = 31;
                    else if (t == '/')
                        estado = 30;
                    else
                        estado = f;
                    break;
                case 30:
                    if (t == (10) || FinArchivo() == true)
                        estado = 0;
                    else
                        estado = 30;
                    break;
                case 31:
                    if (t == '*')
                        estado = 32;
                    else if (FinArchivo() == true)
                        estado = e;
                    else
                        estado = 31;
                    break;
                case 32:
                    if (t == '/')
                        estado = 0;
                    else if (t == '*')
                        estado = 32;
                    else if (FinArchivo() == true)
                        estado = e;
                    break;
            }
            return estado;
        }
        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}