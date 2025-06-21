using System;

public struct Turno
{
    public string Fecha;
    public string Hora;
    public bool Ocupado;

    public Turno(string fecha, string hora)
    {
        Fecha = fecha;
        Hora = hora;
        Ocupado = false;
    }
}

public class Servicio
{
    public string Cliente { get; set; }
    public string Cedula { get; set; }
    public Turno TurnoAsignado { get; set; }

    public Servicio(string cliente, string cedula, Turno turno)
    {
        Cliente = cliente;
        Cedula = cedula;
        TurnoAsignado = turno;
    }

    public void Mostrar()
    {
        Console.WriteLine($"{Cliente,-17} | {Cedula,-20} | {TurnoAsignado.Fecha} {TurnoAsignado.Hora} | {(TurnoAsignado.Ocupado ? "Asignado" : "Libre")}");
    }
}

public class AgendaServicios
{
    private Servicio[] servicios;
    private Turno[,] horarios;
    private int contador;

    public AgendaServicios(int capacidad)
    {
        servicios = new Servicio[capacidad];
        horarios = new Turno[5, 6]; // 5 días, 6 turnos por día
        contador = 0;

        string[] horas = { "08:00", "09:00", "10:00", "11:00", "14:00", "15:00" };
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                horarios[i, j] = new Turno($"Día {i + 1}", horas[j]);
            }
        }
    }

    public void AgregarServicio(string cliente, string cedula, int dia, int turno)
    {
        if (dia < 0 || dia >= 5 || turno < 0 || turno >= 6)
        {
            Console.WriteLine("Turno fuera de rango.");
            return;
        }

        if (!horarios[dia, turno].Ocupado && contador < servicios.Length)
        {
            horarios[dia, turno].Ocupado = true;
            Servicio nuevo = new Servicio(cliente, cedula, horarios[dia, turno]);
            servicios[contador++] = nuevo;
            Console.WriteLine("Servicio agregado exitosamente.");
        }
        else
        {
            Console.WriteLine("Turno ocupado o agenda llena.");
        }
    }

    public void MostrarServicios()
    {
        Console.WriteLine("\nAGENDA DE SERVICIOS:");
        Console.WriteLine("CLIENTE           | CÉDULA               | FECHA HORA       | ESTADO");
        for (int i = 0; i < contador; i++)
        {
            servicios[i].Mostrar();
        }
    }

    public void MostrarAgenda()
    {
        Console.WriteLine("\nESTADO DE TURNOS:");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                string estado = horarios[i, j].Ocupado ? "Ocupado" : "Disponible";
                Console.WriteLine($"{horarios[i, j].Fecha} {horarios[i, j].Hora}: {estado}");
            }
        }
    }

    public void OrdenarPorCliente()
    {
        Array.Sort(servicios, 0, contador, Comparer<Servicio>.Create(
            (a, b) => string.Compare(a.Cliente, b.Cliente, StringComparison.OrdinalIgnoreCase)
        ));

        Console.WriteLine("\nServicios ordenados por cliente.");
    }
}

class Program
{
    static void Main()
    {
        AgendaServicios agenda = new AgendaServicios(10);

        agenda.AgregarServicio("Consuelo Tapia", "1102557896", 0, 2);
        agenda.AgregarServicio("Carlos Chiriboga", "1103254781", 1, 4);

        agenda.MostrarServicios();
        agenda.OrdenarPorCliente();
        agenda.MostrarServicios();

        agenda.MostrarAgenda();
    }
}