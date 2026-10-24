using System;

class Program
{
    static void Main(string[] args)
    {
        int typeOption;

        do
        {
            Console.Clear();
            Console.WriteLine("=== TALLER 5 - LISTAS DOBLEMENTE LIGADAS ===");
            Console.WriteLine("Seleccione el tipo de dato:");
            Console.WriteLine("1. Numeros enteros");
            Console.WriteLine("2. Texto");
            Console.WriteLine("0. Salir");
            Console.Write("\nSeleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out typeOption))
            {
                Console.WriteLine("\nIngrese una opcion valida.");
                Console.ReadKey();
                continue;
            }

            switch (typeOption)
            {
                case 1:
                    RunMenu<int>();
                    break;

                case 2:
                    RunMenu<string>();
                    break;

                case 0:
                    Console.WriteLine("Saliendo del programa...");
                    break;

                default:
                    Console.WriteLine("Opcion no valida.");
                    Console.ReadKey();
                    break;
            }

        } while (typeOption != 0);
    }

    static void RunMenu<T>() where T : IComparable<T>
    {
        DoublyLinkedList<T> list = new DoublyLinkedList<T>();
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("=== TALLER 5 - LISTAS DOBLEMENTE LIGADAS ===");
            Console.WriteLine($"Tipo de dato: {typeof(T).Name}");
            Console.WriteLine("1. Adicionar elemento");
            Console.WriteLine("2. Mostrar hacia adelante");
            Console.WriteLine("3. Mostrar hacia atras");
            Console.WriteLine("4. Ordenar descendentemente");
            Console.WriteLine("5. Mostrar la(s) moda(s)");
            Console.WriteLine("6. Mostrar grafico de ocurrencias");
            Console.WriteLine("7. Verificar si existe un elemento");
            Console.WriteLine("8. Eliminar una ocurrencia");
            Console.WriteLine("9. Eliminar todas las ocurrencias");
            Console.WriteLine("0. Volver al menu principal");
            Console.Write("\nSeleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("\nIngrese una opcion valida.");
                Console.ReadKey();
                continue;
            }

            Console.WriteLine();

            switch (option)
            {
                case 1:
                {
                    Console.Write("Ingrese el valor a adicionar: ");

                    if (TryReadValue<T>(Console.ReadLine(), out T valToAdd))
                    {
                        list.Add(valToAdd);
                        Console.WriteLine("Elemento adicionado en orden ascendente.");
                    }
                    else
                    {
                        Console.WriteLine("Valor no valido.");
                    }

                    break;
                }

                case 2:
                    list.DisplayForward();
                    break;

                case 3:
                    list.DisplayBackward();
                    break;

                case 4:
                    list.SortDescending();
                    Console.WriteLine("La lista ha sido ordenada descendentemente.");
                    break;

                case 5:
                    list.DisplayModes();
                    break;

                case 6:
                    list.DisplayChart();
                    break;

                case 7:
                {
                    Console.Write("Ingrese el valor a buscar: ");

                    if (TryReadValue<T>(Console.ReadLine(), out T valToFind))
                    {
                        bool exists = list.Exists(valToFind);

                        Console.WriteLine(
                            exists
                                ? $"El elemento {valToFind} SI existe en la lista."
                                : $"El elemento {valToFind} NO existe en la lista.");
                    }
                    else
                    {
                        Console.WriteLine("Valor no valido.");
                    }

                    break;
                }

                case 8:
                {
                    Console.Write("Ingrese el valor cuya primera ocurrencia desea eliminar: ");

                    if (TryReadValue<T>(Console.ReadLine(), out T valDelFirst))
                    {
                        bool deleted = list.DeleteFirst(valDelFirst);

                        Console.WriteLine(
                            deleted
                                ? "Primera ocurrencia eliminada con exito."
                                : "El elemento no fue encontrado en la lista.");
                    }
                    else
                    {
                        Console.WriteLine("Valor no valido.");
                    }

                    break;
                }

                case 9:
                {
                    Console.Write("Ingrese el valor cuyas ocurrencias desea eliminar todas: ");

                    if (TryReadValue<T>(Console.ReadLine(), out T valDelAll))
                    {
                        int count = list.DeleteAll(valDelAll);

                        Console.WriteLine(
                            $"Se eliminaron {count} ocurrencia(s) del valor {valDelAll}.");
                    }
                    else
                    {
                        Console.WriteLine("Valor no valido.");
                    }

                    break;
                }

                case 0:
                    Console.WriteLine("Volviendo al menu principal...");
                    break;

                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }

            if (option != 0)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }

        } while (option != 0);
    }

    static bool TryReadValue<T>(string? input, out T value)
        where T : IComparable<T>
    {
        if (typeof(T) == typeof(int))
        {
            if (int.TryParse(input, out int number))
            {
                value = (T)(object)number;
                return true;
            }

            value = default!;
            return false;
        }

        if (typeof(T) == typeof(string))
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                value = (T)(object)input;
                return true;
            }

            value = default!;
            return false;
        }

        value = default!;
        return false;
    }
}

