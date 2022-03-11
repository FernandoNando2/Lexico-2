using System;

namespace Lexico_2
{

    public class Lexico : Token
    {
        StreamReader archivo;
        StreamWriter log;
        const int f = -1;
        const int e = -2;
        public Lexico()
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

        public void NextToken()
        {
            string buffer = "";
            char c;
            int estado = 0;

            while (estado >= 0)
            {
                c = (char)archivo.Peek(); //Función de transición.
                estado = Automata(estado,c);
                if(estado>=0){
                    archivo.Read();
                    if(estado>0){
                        buffer += c;  
                    }
                }
            }
            setContenido(buffer);
            log.WriteLine(getContenido() +" | " + getClasificacion());
        }

        private int Automata(int estado, char t)
        {
            switch (estado)
            {
                case 0:
                    if(char.IsLetter(t)){
                        estado = 1;
                    }
                    break;
                case 1:
                    if(char.IsLetterOrDigit(t)){
                        estado = 1;
                    }
                    else{
                        estado = f;
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    break;
                case 18:
                    break;
                case 19:
                    break;
                case 20:
                    break;
                case 21:
                    break;
                case 22:
                    break;
                case 23:
                    break;
                case 24:
                    break;
                case 25:
                    break;
                case 26:
                    break;
                case 27:
                    break;
                case 28:
                    break;
                case 29:
                    break;
                case 30:
                    break;
                case 31:
                    break;
                case 32:
                    break;
                case 33:
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