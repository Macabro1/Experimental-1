// Archivo: Program.cs
using System;
using System.Collections.Generic;

public struct Turno {
    public string Fecha;
    public string Hora;
    public bool Ocupado;

    public Turno(string fecha, string hora) {
        Fecha = fecha;
        Hora = hora;
        Ocupado = false;
    }
}

public class Paciente {
    public string Nombre { get; set; }
    public string Cedula { get; set; }
    public Turno TurnoAsignado { get; set; }

    public Paciente(string nombre, string cedula, Turno turno) {
        Nombre = nombre;
        Cedula = cedula;
        TurnoAsignado = turno;
    }
}

public class AgendaTurnos {
    private List<Paciente> pacientes = new List<Paciente>();
    private Turno[,] horarios = new Turno[5, 6]; // 5 días, 6 turnos por día

    public AgendaTurnos() {
        string[] horas = { "08:00", "09:00", "10:00", "11:00", "14:00", "15:00" };
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 6; j++) {
                horarios[i, j] = new Turno("Día " + (i + 1), horas[j]);
            }
        }
    }

    public bool AsignarTurno(string nombre, string cedula, int dia, int turno) {
        if (!horarios[dia, turno].Ocupado) {
            horarios[dia, turno].Ocupado = true;
            Paciente nuevo = new Paciente(nombre, cedula, horarios[dia, turno]);
            pacientes.Add(nuevo);
            return true;
        }
        return false;
    }

    public void VerPacientes() {
        Console.WriteLine("Lista de pacientes:");
        foreach (var p in pacientes) {
            Console.WriteLine($"Nombre: {p.Nombre}, Cédula: {p.Cedula}, Turno: {p.TurnoAsignado.Fecha} - {p.TurnoAsignado.Hora}");
        }
    }

    public void VerAgenda() {
        Console.WriteLine("Agenda de turnos:");
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 6; j++) {
                string estado = horarios[i, j].Ocupado ? "Ocupado" : "Disponible";
                Console.WriteLine($"{horarios[i, j].Fecha} {horarios[i, j].Hora}: {estado}");
            }
        }
    }
}

class Program {
    static void Main(string[] args) {
        AgendaTurnos agenda = new AgendaTurnos();
        agenda.AsignarTurno("Consuelo Tapia", "1102557896", 0, 2);
        agenda.AsignarTurno("Carlos Chiriboga", "1103254781", 1, 4);
        agenda.VerPacientes();
        agenda.VerAgenda();
    }
}
