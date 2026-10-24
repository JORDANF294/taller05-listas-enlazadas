using System;

public class DoublyLinkedList<T> where T : IComparable<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;

    public bool IsEmpty => _head == null;

    // 3.1: Añadir en orden ascendente 
    public void Add(T value)
    {
        Node<T> newNode = new Node<T>(value);

        // Caso 1: lista vacia
        if (IsEmpty)
        {
            _head = _tail = newNode;
            return;
        }

        // Caso 2: El nuevo elemento es mas pequeño que la cabeza (insertar al principio)
        if (value.CompareTo(_head!.Data) < 0)
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
            return;
        }

        // Caso 3: El nuevo elemento es mas grande que la cabeza (insertar al final)
        if (value.CompareTo(_tail!.Data) >= 0)
        {
            _tail.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
            return;
        }

        // Caso 4: insertar en medio
        Node<T> current = _head;
        while (current.Next != null && current.Next.Data.CompareTo(value) < 0)
        {
            current = current.Next;
        }
            // insertar entre 'current' y 'current.Next'
        newNode.Next = current.Next;
        newNode.Previous = current;
        
        if (current.Next != null)
        {
            current.Next.Previous = newNode;
        }
        current.Next = newNode;
    }

    //mostrar hacia adelante
    public void DisplayForward()
    {
        if (IsEmpty)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        Node<T>? current = _head;
        Console.Write("List (forward): ");
        while (current != null)
        {
            Console.Write($"{current.Data} ");
            current = current.Next;
        }
        Console.WriteLine();
    }

    // Mostrar hacia atras
    public void DisplayBackward()
    {
        if (IsEmpty)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        Node<T>? current = _tail;
        Console.Write("List (backward): ");
        while (current != null)
        {
            Console.Write($"{current.Data} ");
            current = current.Previous;
        }
        Console.WriteLine();
    }

    // 3.2: ordenar descendente
    public void SortDescending()
    {
        if (IsEmpty || _head == _tail) return;

        Node<T>? current = _head;
        Node<T>? temp = null;

        // Recorrer la lista e intercambiar los punteros Next y Previous de cada nodo
        while (current != null)
        {
            temp = current.Next;
            current.Next = current.Previous;
            current.Previous = temp;
            
            // Avanzar al siguiente nodo (que ahora está en Previous debido al intercambio)
            current = current.Previous;
        }

        // Intercambiar la cabeza y la cola
        temp = _head;
        _head = _tail;
        _tail = temp;
    }

    // 3.3 Mostrar la(s) moda(s)
    public void DisplayModes()
    {
        if (IsEmpty)
        {
            Console.WriteLine("La lista está vacía, no hay modas.");
            return;
        }

        // Usamos un diccionario para contar las frecuencias de cada elemento
        Dictionary<T, int> frequencies = new Dictionary<T, int>();
        Node<T>? current = _head;

        while (current != null)
        {
            if (frequencies.ContainsKey(current.Data))
            {
                frequencies[current.Data]++;
            }
            else
            {
                frequencies[current.Data] = 1;
            }
            current = current.Next;
        }

        // Encontrar la frecuencia máxima
        int maxFrequency = frequencies.Values.Max();

        // Filtrar todos los elementos que tengan esa frecuencia máxima (para manejar empates)
        var modes = frequencies.Where(pair => pair.Value == maxFrequency)
                               .Select(pair => pair.Key)
                               .ToList();

        if (modes.Count == 1)
        {
            Console.WriteLine($"La moda es {modes[0]} (con {maxFrequency} ocurrencias).");
        }
        else
        {
            Console.WriteLine($"Las modas son: {string.Join(", ", modes)} (con {maxFrequency} ocurrencias cada una).");
        }
    }

    // 3.4 Mostrar gráfico
    public void DisplayChart()
    {
        if (IsEmpty)
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }

        // Contar frecuencias manteniendo el orden de aparición o valores
        Dictionary<T, int> frequencies = new Dictionary<T, int>();
        Node<T>? current = _head;

        while (current != null)
        {
            if (frequencies.ContainsKey(current.Data))
            {
                frequencies[current.Data]++;
            }
            else
            {
                frequencies[current.Data] = 1;
            }
            current = current.Next;
        }

        Console.WriteLine("\n--- Gráfico de Ocurrencias ---");
        foreach (var pair in frequencies)
        {
            string stars = new string('*', pair.Value);
            Console.WriteLine($"{pair.Key} {stars}");
        }
        Console.WriteLine("------------------------------");
    }

    // 3.5 Existe
    public bool Exists(T value)
    {
        if (IsEmpty) return false;

        Node<T>? current = _head;
        while (current != null)
        {
            if (current.Data.Equals(value))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    // 3.6 Eliminar la primera ocurrencia
    public bool DeleteFirst(T value)
    {
        if (IsEmpty) return false;

        Node<T>? current = _head;
        while (current != null)
        {
            if (current.Data.Equals(value))
            {
                // Si es el único elemento
                if (current == _head && current == _tail)
                {
                    _head = _tail = null;
                }
                // Si es la cabeza
                else if (current == _head)
                {
                    _head = _head.Next;
                    _head!.Previous = null;
                }
                // Si es la cola
                else if (current == _tail)
                {
                    _tail = _tail.Previous;
                    _tail!.Next = null;
                }
                // Si está en el medio
                else
                {
                    current.Previous!.Next = current.Next;
                    current.Next!.Previous = current.Previous;
                }
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    // 3.6 Eliminar todas las ocurrencias
    public int DeleteAll(T value)
    {
        if (IsEmpty) return 0;

        int deletedCount = 0;
        Node<T>? current = _head;

        while (current != null)
        {
            Node<T>? nextNode = current.Next; // Guardamos el siguiente antes de desvincular
            if (current.Data.Equals(value))
            {
                if (current == _head && current == _tail)
                {
                    _head = _tail = null;
                }
                else if (current == _head)
                {
                    _head = _head.Next;
                    _head!.Previous = null;
                }
                else if (current == _tail)
                {
                    _tail = _tail.Previous;
                    _tail!.Next = null;
                }
                else
                {
                    current.Previous!.Next = current.Next;
                    current.Next!.Previous = current.Previous;
                }
                deletedCount++;
            }
            current = nextNode;
        }
        return deletedCount;
    }
}