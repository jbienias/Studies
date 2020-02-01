//Jan Bienias 238201
//Seabird folder - 1st task
//PluggableAdapter - 2nd task

//Zadanie 1
//Rozważmy program Seabird.
//Czy byłoby możliwe utworzenie instancji obiektu Aircraft zamiast obiektu Seacraft, zmieniając metody wewnątrz Seabird odpowiednio?
//Jeśli tak, należy dokonać zmian takich zmian.
//Jeśli nie, proszę wyjaśnić, w jaki sposób obecny program będzie musiał zostać zmieniony by to osiągnąć, a następnie dokonać zmian takich zmian.

//Zadanie 2
//Opracować przykład ilustrujący praktyczne wykorzystanie techniki tworzenia adapterów klas, zaprezentowaną w projekcie PluggableAdapter.

//Client class for PluggableAdapter

using Adapter.SeabirdExample;
using System;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            //Classic usage of an adapter
            Console.WriteLine("\nExperiment 1.1: Use the engine in the Seabird:");
            IAircraft seabird = new Seabird();
            seabird.TakeOff(); //And automatically increases speed
            Console.WriteLine("The Seabird took off");

            //Two-way adapter: using seacraft instructions on an IAircraft object (where they are not in the IAircraft interface)
            Console.WriteLine("\nExperiment 1.2: Increase the speed of the Seabird:");
            (seabird as ISeacraft).IncreaseRevs();
            (seabird as ISeacraft).IncreaseRevs();
            if (seabird.Airborne)
                Console.WriteLine("Seabird flying at height " + seabird.Height +
                    " meters and speed " + (seabird as ISeacraft).Speed + " knots");
            Console.WriteLine("Experiments successful; the Seabird flies!");

            Console.WriteLine("\nExperiment 2.1: Increase the speed of the SeabirdAircraft:");
            ISeacraft seabird2 = new SeabirdAircraft();
            seabird2.IncreaseRevs();
            seabird2.IncreaseRevs();
            Console.WriteLine($"SeabirdAircraft has speed {seabird2.Speed} knots");

            Console.WriteLine("\nExperiment 2.2: Use the engine in the SeabirdAircraft");
            (seabird2 as IAircraft).TakeOff();
            Console.WriteLine("The SeabirdAircraft took off");
            if ((seabird2 as IAircraft).Airborne)
                Console.WriteLine("SeabirdAircraft flying at height " + (seabird2 as IAircraft).Height +
                    " meters and speed " + seabird2.Speed + " knots");
            Console.WriteLine("Experiments successful; the SeabirdAircraft flies!");
        }
    }
}
