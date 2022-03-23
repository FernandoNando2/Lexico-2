using System;

namespace Lexico_2{
    public class Program{

        static void Main(string[] args){

            Lexico_2 a = new Lexico_2();
            
            while(!a.FinArchivo()){
                a.NextToken();
            }
            a.Cerrar();
        }
    }
}