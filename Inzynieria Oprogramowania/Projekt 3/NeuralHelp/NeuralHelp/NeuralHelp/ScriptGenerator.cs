using System;
using System.Linq;

namespace NeuralHelp {
    public static class ScriptGenerator
    {
        static readonly Random _random = new Random();
        static readonly string[] _actions =
        {
            "response.goForward = ",
            "response.goBack = ",
            "response.turnLeft = ",
            "response.turnRight = ",
            "response.cannonLeft = ",
            "response.cannonRight = "
        };

        public static string GenerateScript()
        {
            var rand_time_action_swaps = _random.Next(8, 29);
            int[] time_array = new int[rand_time_action_swaps];
            do
            {
                for (int i = 0; i < time_array.Length; i++)
                {
                    time_array[i] = _random.Next(0, 400) * 100;
                }
            }
            while (time_array.Distinct().Count() == time_array.Length);
            Array.Sort(time_array);



            string generatedScript = "function(e) {\n\tvar response = {};\n";

            generatedScript += "\tif (" + time_array[0] + " > e.data.currentGameTime) {\n";
            generatedScript += randomlyChooseActionsWithValues();
            generatedScript += "\t}";

            int size = 0;


            size = time_array.Length - 1;

            for (int i = 0; i < size; i++)
            {
                generatedScript += "\telse if (" + timeStamp(time_array[i], time_array[i + 1]) + ") {";
                generatedScript += randomlyChooseActionsWithValues();
                generatedScript += "\t}";
            }
            generatedScript += "\telse {\n";
            generatedScript += randomlyChooseActionsWithValues();
            generatedScript += "\t}\n\tif(e.data.myTank.shootCooldown == 0) {\n";
            generatedScript += "\t\t response.shoot = 1;\n";
            generatedScript += "\t}\n";
            generatedScript += "\tself.postMessage(response);\n}";
            return generatedScript;
        }

        static string timeStamp(int element1, int element2)
        {
            return element1 + " <= e.data.currentGameTime && e.data.currentGameTime < " + element2;
        }

        static string randomlyChooseActionsWithValues()
        {
            string actions = String.Empty;

            int[] tab = new int[3];
            tab[0] = _random.Next(0, 2); //0 - 1
            tab[1] = _random.Next(2, 4); //2 - 3
            tab[2] = _random.Next(4, 6); //4 - 5

            int counter = 0;
            for (int i = 0; i < tab.Length; i++)
            {

                if (_random.Next() % 2 == 0)
                {
                    actions += _actions[tab[i]] + 1 + ";\n";
                    counter++;
                }
            }

            if (counter == 0)
            {
                actions += _actions[_random.Next(0, 6)] + 1 + ";\n";
            }

            return actions;
        }

    }
}

