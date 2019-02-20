using System;

//Monika Barzowska, Jan Bienias
//238143, 238201

namespace Aproksymacja {
    public class Dice {
        public int[] values;
        public int[] probability;
        public int probabilitySum;

        public Dice(Data data) {
            values = (int[])data.diceValues.Clone();
            probability = (int[])data.diceProbabilities.Clone();
            probabilitySum = data.probabilitySum;
        }

        public override string ToString() {

            string tmp = "Dice : \nValues : ";
            for (int i = 0; i < values.Length; i++) {
                tmp += values[i] + " ";
            }
            tmp += "\nProbability : ";
            for (int i = 0; i < probability.Length; i++) {
                tmp += probability[i] + " ";
            }
            tmp += "\nSum of probabilities : " + probabilitySum + "\n";
            return tmp;
        }
    }
}
