using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimerObligatorio
{
    class Program
    {
        static void Main(string[] args)
        {
            int dias = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
            string[] categorias = new string[10];
            int[,] gastos = new int[categorias.Length, dias];

            int opcion = 0;

            while (opcion != 5)
            {
                Menu();
                opcion = PidoOpcion();
                ProcesoPrincipal(opcion, categorias, gastos);
            }
        }

        static int PidoOpcion()
        {
            int valor = 0;
            try
            {
                valor = Convert.ToInt32(Console.ReadLine());
                return valor;
            }
            catch
            {
                return 0;
            }
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("\n\n\t\t* Menú *\n\n");
            Console.WriteLine("\t1- Agregar categoria de gasto.");
            Console.WriteLine("\t2- Agregar monto de gasto.");
            Console.WriteLine("\t3- Mostrar el gasto total del mes actual para cada categoria.");
            Console.WriteLine("\t4- Mostrar el gasto total del dia actual para cada categoria.");
            Console.WriteLine("\t5- Salir.");
            Console.Write("\n\tIngrese la opción deseada: ");
        }

        static void ProcesoPrincipal(int op, string[] cat, int[,] gastos)
        {

            switch (op)
            {
                case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("\n\t* Categoria de gastos *");
                        Cargar(cat);
                        break;
                    }

                case 2:
                    {
                        Console.Clear();
                        Console.WriteLine("\n\t* Agregar monto de gasto *");
                        if (cat[0] != null)
                        {
                            Mostrar(cat);
                            CargarMatriz(cat, gastos);
                        }
                        else
                        {
                            Console.WriteLine("\n\t!!!Debe agregar categorias");
                            Console.WriteLine("\t!!!Enter para volver al menu");
                            Console.ReadLine();
                        }
                        break;
                    }
                
                case 3:
                    {
                        Console.Clear();
                        Console.WriteLine("\n\t* Gasto total del mes actual para cada categoria *");
                        if (cat[0] != null)
                        {
                            Informe1(cat, gastos);
                        }
                        else
                        {
                            Console.WriteLine("\n\t!!!Debe agregar categorias");
                            Console.WriteLine("\t!!!Enter para volver al Menu");
                            Console.ReadLine();
                        }
                        break; 
                    }
                
                case 4:
                    {
                        Console.Clear();
                        Console.WriteLine("\n\t* Gasto total del dia actual para cada categoria *");
                        if (cat[0] != null)
                        {
                            Informe2(cat, gastos);
                        }
                        else
                        {
                            Console.WriteLine("\n\t!!!Debe agregar categorias");
                            Console.WriteLine("\t!!!Enter para volver al Menu");
                            Console.ReadLine();
                        }
                        break;  
                    }
                case 5:
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("!!!Opcion no valida");
                        Console.WriteLine("!!!Enter para continuar");
                        Console.ReadLine();
                        break;
                    }

                
            }
        }

        static void Cargar(string[] cat)
        {
            int indice = 0;
            bool encontre = false;
            int posicionVacia = -1;

            do
            {
                if (cat[indice] == null)
                {
                    encontre = true;
                    posicionVacia = indice;
                }
                indice++;

            } while (!encontre && indice < cat.Length);

            if (!encontre)
            {
                Console.WriteLine("\n\t!!!No hay mas lugar");
                Console.WriteLine("\n\t!!!Enter para volver al Menu");
                Console.ReadLine();
            }
            else
            {
                Console.Write("Agregar categoria: ");
                string categorias = Console.ReadLine().Trim().ToUpper();
                if (!ExisteCategoria(cat, categorias))
                {
                    Console.WriteLine("\n\t!!!La categoria  ya ha sido ingresada");
                    Console.ReadKey();
                }
                else
                {
                    if (categorias == String.Empty)
                    {
                        Console.WriteLine("\n\t!!!Debe agregar categoria");
                        Console.ReadKey();
                    }
                    else
                    {
                        cat[posicionVacia] = categorias;
                        Console.WriteLine("\n\t!!!Categoria registrada");
                        Console.ReadKey();
                    }
                }
            }
        }

        static bool ExisteCategoria(string[] cat, string categorias)
        {
            int indice = 0;
            bool encontre = false;
            while (!encontre && indice < cat.Length)
            {
                if (cat[indice] == categorias)
                {
                    return false;
                } 
                indice++;
            }
            return true;
        }

        static void Mostrar(string[] cat)
        {
            for (int i = 0; i < cat.Length; i++)
            {
                if (cat[i] != null)
                {
                    Console.WriteLine((i+1) + "- " + cat[i]);
                }
            }
        }

        static void CargarMatriz(string[] cat, int[,] gastos)
        {
            int i = 0, j = 0;
            int gasto = 0;

            i = PidoValorCat(cat);

            j = ControlDia(gastos);

            gasto = PidoValorGasto();

            gastos[(i-1), (j-1)] += gasto;

        }

        static int PidoValorCat(string[] cat)
        {
            int res = 0;
            bool erroIngreso = true;

            while (erroIngreso)
            {
                try
                {
                    Console.Write("- Elija la categoria: ");
                    res = Convert.ToInt32(Console.ReadLine());
                    if (cat[res - 1] != null)
                    {
                        erroIngreso = false;
                    }
                    else
                    {
                        Console.WriteLine("\n\t!!!El valor ingresado no es correcto.");
                        Console.WriteLine("\t!!!Agregue el valor correctamente...\n");
                    }

                }
                catch
                {
                    Console.WriteLine("\n\t!!!Lo ingresado no tiene formato numerico");
                    Console.WriteLine("\t!!!Agregue valor con el formato correcto...\n");
                }
            }
            return res;
            
        }

        static int PidoValorGasto()
        {
            int res = 0;
            bool erroIngreso = true;
            while (erroIngreso)
            {
                try
                {
                    Console.Write("- Ingrese gasto: ");
                    res = Convert.ToInt32(Console.ReadLine());
                    erroIngreso = false;
                }
                catch
                {
                    Console.WriteLine("\n\t!!!Lo ingresado no tiene formato numerico");
                    Console.WriteLine("\t!!!Agregue valor con el formato correcto...\n");
                }
            }
            return res;
        }

        static int ControlDia(int[,] gas)
        {
            int fecha = 0;
            bool errorIngreso = true;
            while (errorIngreso)
            {
                try
                {
                    Console.Write("- Dia: ");
                    fecha = Convert.ToInt32(Console.ReadLine());
                    if (fecha > gas.GetLength(1) || fecha == 0)
                    {
                        Console.WriteLine("\n\t!!!El valor ingresado no es correcto.");
                        Console.WriteLine("\t!!!Agregue el valor correctamente...\n");
                    }
                    else
                    {
                        errorIngreso = false;
                    }
                }
                catch
                {
                    Console.WriteLine("\n\t!!!El valor ingresado no tiene formato numerico");
                    Console.WriteLine("\tAgregue valor con el formato correcto...\n");
                    Console.ReadLine();
                }
            }
            return fecha;
        }

        static void Informe1(string[] cat, int[,] gastos)
        {
            
            for (int i = 0; i < cat.Length; i++)
            {
                int total = 0;
                if (cat[i] != null)
                {
                    Console.Write(cat[i] + "\t"); 
                    for (int j = 0; j < gastos.GetLength(1) -1; j++)
                    {
                        total += gastos[i, j];
                    }
                    Console.Write(total);
                    Console.WriteLine();   
                }
            }
            Console.WriteLine("\n\tEnter para volver al Menu");
            Console.ReadLine();
        }

        static void Informe2(string[] cat, int[,] gastos)
        {
            for (int i = 0; i <= cat.Length - 1; i++)
            {
                if (cat[i] != null)
                {
                    Console.WriteLine(" - " + cat[i]);
                    for (int dia = 0; dia <= gastos.GetLength(1) - 1; dia++)
                    {
                        Console.Write(dia + 1);
                        Console.Write("\t");
                    }
                    Console.WriteLine();
                    for (int dia = 0; dia <= gastos.GetLength(1) - 1; dia++)
                    {
                        Console.Write(gastos[i, dia]);
                        Console.Write("\t");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("\n\tEnter para volver al Menu");
            Console.ReadLine();
        }

    }
}
