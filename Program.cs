using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmos_geneticos
{
    class Program
    {
        static void Main(string[] args)
        {
            //aqui esta la parte del codigo donde pedimos el numero de datos para generar la poblacion
            //primero pedimos el numero de entidades
            Console.WriteLine("--------Ingrese los datos para generar la población.:------");
            Console.Write("Dame el número de entidades para la población: ");
            int numC = Convert.ToInt32(Console.ReadLine());
            //aqui el numero de genes
            Console.Write("Dame el número de genes: ");
            int numG = Convert.ToInt32(Console.ReadLine());
            //aqui damos los valores con el que vamos a trabajar.
            Console.WriteLine("El número de cromosomas es: " + (Math.Pow(2, numC)));
            Console.WriteLine("El número de valores alcanzables por los genes es: " + (Math.Pow(2, numG)));
            //aqui inicializamos la matriz para la poblacion.
            Console.WriteLine("-------------------poblacion inicial-------------------");
            int[,] po = new int[numG + 1, Convert.ToInt32(Math.Pow(2, numC))];
            Console.WriteLine("La población inicial es de: ");
            //aqui empezamos a ingresar los valores para la poblacion de manera aleatoria donde
            //utilizamos el contador del ambiente para poder realizar un random simplificando la manera de
            //realizar la poblacion de esta manera
            var duracion = Environment.TickCount;
            var ramdom = new Random(duracion);
            //ingresamos las valores aleatorios a la matriz haciendo la poblacion
            //cabe destacar que pusimos una columna de mas para poder ingresar los valores
            //de cuando ya se evalua la ecuacion.
            for (int x = 0; x < (numG); x++)
            {
                for (int y = 0; y < Convert.ToInt32(Math.Pow(2, numC)); y++)
                {
                    po[x, y] = ramdom.Next(0, 2);
                }

            }
            Console.WriteLine();
            //en esta linea imprimimos la matriz de manera que las entidades queden en vertical y los genes en horizontal.
            for (int y = 0; y < Convert.ToInt32(Math.Pow(2, numC)); y++)
            {
                for (int x = 0; x < numG; x++)
                {
                    Console.Write(po[x, y] + ",");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("-------------evaluación---------------");
            int universal = 1;
            //entramos a un ciclo para poder convertir los valores binarios de las entidades respecto a esa fila
            //entramos al ciclo en el que solo revisaremos la primera poblacion y la hija.
            while (universal < 2)
            {
                //aqui tenemos cuanto es el total de los cromosomas por posicion ya en decimal.
                double total = 0;
                //inicializamos esta matriz para poner los resultados en decimal.
                double[] TotalC = new double[Convert.ToInt32(Math.Pow(2, numC))];
                Console.WriteLine("Totales por cromosoma y posicion");
                for (int x = 0; x < Convert.ToInt32(Math.Pow(2, numC)); x++)
                {
                    int guardar = 1;              
                    total = 0;
                    for (int y = 0; y < numG; y++)
                    {
                        total = total + po[y, x] * Math.Pow(2, y);
                        if (po[0, x] == 1)
                        {
                            guardar = -1;
                        }
                    }
                    //aqui imprimimos, el total y guardamos el valor en la matriz para posteriormente utilizarlo.
                    Console.WriteLine("Cromosoma " + (x+1) + ": " + ((total) * guardar));
                    TotalC[x] = total * guardar;
                }
                int hola = Convert.ToInt32(Math.Pow(2, numC));
                int[] matrizeva = new int[hola];

                
                Console.WriteLine();
                //aqui inicializamos la variable TE para evaluar la funcion aptitud.
                double[] TE = TotalC;
                //aqui evaluando función de comorbilidad ingrensando los valores en la matriz TE correspondiente a 
                //la posicion de cada resultado.
                Console.WriteLine("Evaluando los cromosomas en la funcion aptitud -0.1 * x^(2) - 0.01 * x + 5");
                for (int x = 0; x < Convert.ToInt32(Math.Pow(2, numC)); x++)
                {
                    //aqui evaluamos la ecuacion de comorbilidad descrita en la linea 89
                    TE[x] = (-0.1) * Math.Pow(TotalC[x], 2) - (0.01) * TotalC[x] + 5;
                    Console.WriteLine("Cromosoma" + (x+1) + ": " + TE[x]);
                    po[numG, x] = Convert.ToInt32(TE[x]);
                    hola = Convert.ToInt32(TE[x]);
                    matrizeva[x] = hola;
                }
                Console.WriteLine();
                Console.WriteLine("-------------------ordenamiento-------------------");
               //ordenamos los valores para compararlos
                Array.Sort(matrizeva);
                Array.Reverse(matrizeva);
               //imprimimos para verificar
                for (int i = 0; i < matrizeva.Length; i++)
                {
                    Console.WriteLine(matrizeva[i]);
                }
                Console.WriteLine();
                Console.WriteLine("el mas apto es, " + matrizeva[0]);
                Console.WriteLine();

                int[,] poOrd = new int[numG + 1, Convert.ToInt32(Math.Pow(2, numC))];

                for (int y = 0; y < Convert.ToInt32(Math.Pow(2, numC)); y++)
                {
                    for (int x = 0; x < numG; x++)
                    {
                        if (matrizeva[y] == po[numG, y])
                        {
                            poOrd[x, y] = po[x, y];
                        }
                        else { }0
                        
                    }
                }
                
                for (int y = 0; y < Convert.ToInt32(Math.Pow(2, numC)); y++)
                {
                    for (int x = 0; x < numG; x++)
                    {
                        Console.Write(poOrd[x, y] + " ");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
                //aqui imprimimos la matriz con los valores ya ordenados, de manera que visualizamos la ultima columna
                //donde nos muestra los valores antes presentados en la parte de la evaluacion. 
                for (int y = 0; y < Convert.ToInt32(Math.Pow(2, numC)); y++)
                {
                    for (int x = 0; x < numG+1; x++)
                    {
                        Console.Write(po[x, y] + ",");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
                //aqui abrimos una condicional que es donde nos permitira hacer el cruce del emparejamiento previo respecto
                //a las parejas hechas previamente respecto a las entidades mas aptas.
                Console.WriteLine("----------------------emparejamiento-----------------------");
                if (universal < 2)
                {
                    // para el cruze realizamos otro random con el contador del ambiente.
                    var duracion2 = Environment.TickCount;
                    var ramdom2 = new Random(duracion2);
                    int PC = ramdom2.Next(0, (numG-1));
                    Console.WriteLine("Punto de cruce: " + (PC));
                    //inicializamos las variables para el punto de cruce
                    int auxiliarPCl1 = 0;
                    int auxiliarPCl2 = 0;
                    //aqui recorremos el array de 2 en 2 para poder realizar el emparejamiento respecto al Punto de cruce "PC"
                    for (int y = 0; y < Convert.ToInt32(Math.Pow(2, numC)); y = y + 2)
                    {
                        for (int x = 0; x < numG ; x++)
                        {
                            //aqui es para que cuando x sea igual al punto de cruze se detenga para realizarlo, donde
                            //los auxiliares nos ayudan para tener el valor de la pocision de cada uno
                            //podemos observar que tenemos "po[PC,y+1]" y esto es para que tengamos el gen de la linea siguiente para cruzar
                            if (x == PC)
                            {
                                auxiliarPCl1 = po[PC, y];
                                auxiliarPCl2 = po[PC, y + 1];

                                po[PC, y] = auxiliarPCl2;
                                po[PC, y + 1] = auxiliarPCl1;
                            }
                        }
                    }
                    Console.WriteLine();
                    //imprimimos para verificar que si se cruzaron en el gen del Punto de cruze
                    for (int y = 0; y < Convert.ToInt32(Math.Pow(2, numC)); y++)
                    {
                        for (int x = 0; x < numG; x++)
                        {
                            Console.Write(po[x, y] + ",");
                        }
                        Console.WriteLine();

                    }
                    Console.WriteLine();
                    Console.WriteLine("-------------------mutación---------------------");
                    // entonces tenemos el otro random para iniciar la mutación
                    // agarramos un random que contenga los valores entre 1 a el valor de las entidades.
                    int mtcion = ramdom.Next(1, (numG * Convert.ToInt32(Math.Pow(2, numC))));
                    mtcion = mtcion - 1;
                    //imprimimos lo que es el numero del gen a mutar y entonces ponemos una bandera con la intencion de encontrar
                    //donde es que se esncuentra ese gen que es dado de manera aleatoria.
                    Console.Write("gen a mutar: "+ mtcion);
                    int band = 0;
                    Console.WriteLine();
                    //aqui es donde lo busca, lo encuentra y lo muta, trasformandolo respectivamente de 0 a 1 o de 1 a 0
                    for (int y = 0; y < Convert.ToInt32(Math.Pow(2, numC)); y++)
                    {
                        for (int x = 0; x < numG; x++)
                        {
                            //abrimos una bandera para detenernos en el gen a mutar.
                            band = band + 1;
                            if (band == mtcion) {
                                if (po[x, y] == 0) 
                                {
                                    po[x, y] = 1;
                                }
                                else {
                                    if (po[x, y] == 1)
                                    {
                                        po[x, y] = 0;
                                    }
                                    }                        
                            }    
                            //aqui imprime la matriz ya mutada y este seria el resultado de la matriz hija mutada.
                            Console.Write(po[x, y] + ",");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    Console.WriteLine("----------------evaluacion de segunda generacion----------------");
                    //conversión a decimal la nueva matriz con la misma estructruca con la que evaluamos la primera vez
                    Console.WriteLine("convertidos a decimal:");
                    for (int x = 0; x < Convert.ToInt32(Math.Pow(2, numC)); x++)
                    {
                        int guardar = 1;
                        total = 0;
                        for (int y = 0; y < numG; y++)
                        {
                            total = total + po[y, x] * Math.Pow(2, y);
                            if (po[0, x] == 1)
                            {
                                guardar = -1;
                            }
                        }
                        //aqui imprimimos, el total y guardamos el valor en la matriz para posteriormente utilizarlo.
                        Console.WriteLine("Cromosoma " + (x + 1) + ": " + ((total) * guardar));
                        TotalC[x] = total * guardar;
                    }
                    Console.WriteLine();
                    //evaluamos por segunda vez en funcion de la aptitud dada con anterioridad
                    Console.WriteLine("Evaluando los cromosomas en la funcion aptitud -0.1 * x^(2) - 0.01 * x + 5");
                    for (int x = 0; x < Convert.ToInt32(Math.Pow(2, numC)); x++)
                    {
                        TE[x] = (-0.1) * Math.Pow(TotalC[x], 2) - (0.01) * TotalC[x] + 5;
                        Console.WriteLine("Cromosoma" + (x + 1) + ": " + TE[x]);
                        po[numG, x] = Convert.ToInt32(TE[x]);
                    }
                    Console.WriteLine();

                    for (int y = 0; y < Convert.ToInt32(Math.Pow(2, numC)); y++)
                    {
                        for (int x = 0; x < numG+1; x++)
                        {
                            Console.Write(po[x, y] + ",");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                universal++;
            }
        }
    }
}