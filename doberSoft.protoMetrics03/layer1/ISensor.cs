using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.ScaleFunctions;
using doberSoft.protoMetrics03.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.protoMetrics03.layer1
{
    /// <summary>
    /// Interfaccia di base per i sensori: espone le proprietà e i metodi non generici
    /// </summary>
    public interface ISensor
    {
        event EventHandler<SensorEventArgs> ValueChanged;
        /// <summary>
        /// Identificativo del sensore
        /// </summary>
        int Id { get; }
        /// <summary>
        /// Etichetta assegnata al sensore
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Tipo di sensore > senza namespace e senza "Sensor"
        /// </summary>
        string Type { get; }
        /// <summary>
        /// Restituisce il pacchetto (data) contenente il nome, il tipo e l'id del sensore, l'ultimo valore, il timestamp della lettura
        /// </summary>
        /// <returns></returns>
        string ToJson();
        void On();
        void Off();
    }
    /// <summary>
    /// Interfaccia parametrica che espone proprietà e metodi specifici per ogni tipo di sensore
    /// </summary>
    /// <typeparam name="Tin">Tipo dello/gli ingresso/i utilizzato/i dal sensore: bool, int, decimal</typeparam>
    /// <typeparam name="Tout">Tipo del dato prodotto dal sensore: bool, int, decimal, Position</typeparam>
    public interface ISensor<Tin, Tout>: ISensor
    {
        /// <summary>
        /// Regole per l'osservazione del dato: Intervallo di scansione, modalità di scansione, isteresi
        /// </summary>
        IRules<Tin> Rules { get; }
        /// <summary>
        /// Accesso al dato scalato
        /// </summary>
        /// <returns></returns>
        Tout GetValue();
        /// <summary>
        /// Funzione parametrica di scalatura, per ogni coppia Tin-Tout
        /// </summary>
        IScale<Tin, Tout> ScaleFunction { get; }
    }
}
