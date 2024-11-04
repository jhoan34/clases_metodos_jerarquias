using System;
using System.Collections.Generic;
//jhoan sebastian garzon monsalve
// Interface para las operaciones básicas del sistema
public interface IGestorTransporte
{
    void AgregarRuta(Ruta ruta);
    void RegistrarVehiculo(Vehiculo vehiculo);
    void RegistrarConductor(Conductor conductor);
    void RegistrarPasajero(Pasajero pasajero);
}

// Clase abstracta Persona, base para todos los actores en el sistema
public abstract class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Persona(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}

// Clase abstracta Empleado, derivada de Persona
public abstract class Empleado : Persona
{
    public string Licencia { get; set; }
    public bool LicenciaValida { get; set; }

    protected Empleado(int id, string nombre, string licencia, bool licenciaValida)
        : base(id, nombre)
    {
        Licencia = licencia;
        LicenciaValida = licenciaValida;
    }

    public abstract void AsignarRuta(Ruta ruta);
}

// Clase Conductor, derivada de Empleado
public class Conductor : Empleado
{
    public List<Ruta> Rutas { get; private set; }

    public Conductor(int id, string nombre, string licencia, bool licenciaValida)
        : base(id, nombre, licencia, licenciaValida)
    {
        Rutas = new List<Ruta>();
    }

    public override void AsignarRuta(Ruta ruta)
    {
        Rutas.Add(ruta);
    }
}

// Clase para conductores de reserva
public class ConductorReserva : Conductor
{
    public ConductorReserva(int id, string nombre, string licencia, bool licenciaValida)
        : base(id, nombre, licencia, licenciaValida) { }

    public override void AsignarRuta(Ruta ruta)
    {
        // Lógica para asignar rutas específicas a conductores de reserva
        Console.WriteLine($"Conductor de reserva asignado a la ruta {ruta.Origen} - {ruta.Destino}");
        base.AsignarRuta(ruta);
    }
}

// Clase Ruta, representa las rutas del sistema 
public class Ruta
{
    public int Id { get; set; }
    public string Origen { get; set; }
    public string Destino { get; set; }
    public string Distancia { get; set; }
    public string HorariosSalida { get; set; }

    public Ruta(int id, string origen, string destino, string distancia, string horariosSalida)
    {
        Id = id;
        Origen = origen;
        Destino = destino;
        Distancia = distancia;
        HorariosSalida = horariosSalida;
    }
}

// Clase Vehículo, representa los vehículos en el sistema 
public class Vehiculo
{
    public int Id { get; set; }
    public string Placa { get; set; }
    public string Modelo { get; set; }
    public int Capacidad { get; set; }
    public bool Disponibilidad { get; set; }
    public Ruta RutaAsignada { get; set; } // Agregación

    public Vehiculo(int id, string placa, string modelo, int capacidad, bool disponibilidad)
    {
        Id = id;
        Placa = placa;
        Modelo = modelo;
        Capacidad = capacidad;
        Disponibilidad = disponibilidad;
        RutaAsignada = null;
    }

    public void AsignarRuta(Ruta ruta)
    {
        RutaAsignada = ruta;
    }
}

// Clase Pasajero, representa los pasajeros que pueden comprar boletos
public class Pasajero : Persona
{
    public List<Boleto> Boletos { get; set; }

    public Pasajero(int id, string nombre) : base(id, nombre)
    {
        Boletos = new List<Boleto>();
    }

    public bool ComprarBoleto(Boleto boleto)
    {
        Boletos.Add(boleto);
        return true;
    }
}

// Clase Boleto, representa los boletos que compran los pasajeros
public class Boleto
{
    public int Id { get; set; }
    public Pasajero Pasajero { get; set; }
    public Ruta Ruta { get; set; }
    public string Fecha { get; set; }

    public Boleto(int id, Pasajero pasajero, Ruta ruta, string fecha)
    {
        Id = id;
        Pasajero = pasajero;
        Ruta = ruta;
        Fecha = fecha;
    }
}

// Clase que gestiona las operaciones del sistema
public class SistemaTransporte : IGestorTransporte
{
    public List<Ruta> rutas = new List<Ruta>();
    public List<Vehiculo> vehiculos = new List<Vehiculo>();
    public List<Conductor> conductores = new List<Conductor>();
    public List<Pasajero> pasajeros = new List<Pasajero>();

    public void AgregarRuta(Ruta ruta)
    {
        rutas.Add(ruta);
        Console.WriteLine("Ruta agregada.");
    }

    public void RegistrarVehiculo(Vehiculo vehiculo)
    {
        vehiculos.Add(vehiculo);
        Console.WriteLine("Vehículo registrado.");
    }

    public void RegistrarConductor(Conductor conductor)
    {
        conductores.Add(conductor);
        Console.WriteLine("Conductor registrado.");
    }

    public void RegistrarPasajero(Pasajero pasajero)
    {
        pasajeros.Add(pasajero);
        Console.WriteLine("Pasajero registrado.");
    }
}

class Program
{
    static void Main()
    {
        // Ejemplo de uso del sistema
        var sistema = new SistemaTransporte();

        var ruta1 = new Ruta(1, "Bogotá", "Medellín", "200km", "7am");
        var vehiculo1 = new Vehiculo(1, "ABC123", "ModeloX", 45, true);
        var conductor1 = new Conductor(1, "Carlos", "123456", true);
        var pasajero1 = new Pasajero(1, "Luis");

        var boleto1 = new Boleto(1, pasajero1, ruta1, "2024-09-20");
        bool result = pasajero1.ComprarBoleto(boleto1);

        sistema.AgregarRuta(ruta1);
        sistema.RegistrarVehiculo(vehiculo1);
        sistema.RegistrarConductor(conductor1);
        sistema.RegistrarPasajero(result ? pasajero1 : null);

        vehiculo1.AsignarRuta(ruta1);
        conductor1.AsignarRuta(ruta1);
        Console.WriteLine(ruta1);
        Console.WriteLine(vehiculo1);
        Console.WriteLine(conductor1);
        Console.WriteLine(pasajero1);
        Console.ReadKey();
    }
}

