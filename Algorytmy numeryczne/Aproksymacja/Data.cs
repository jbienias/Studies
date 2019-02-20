using System;

//Monika Barzowska, Jan Bienias
//238143, 238201

namespace Aproksymacja {
    public class Data {
        public int mapSize;
        public int mushroomCount;
        public int playerOnePos;
        public int playerTwoPos;
        public int diceSize;
        public int probabilitySum;
        public bool[] mushroomMap;
        public int[] diceValues;
        public int[] diceProbabilities;

        public override string ToString() {
            string diceVals = "", diceProbs = "", mushrooms = "";
            for (int i = 0; i < diceSize; i++) {
                diceVals += diceValues[i] + " ";
                diceProbs += diceProbabilities[i] + " ";
            }
            for (int i = 0; i < mapSize; i++) {
                mushrooms += mushroomMap[i] + " ";
            }
            return "DATA :\n" + "Map size : " + mapSize + "\n" +
                "Player 1 position : " + playerOnePos + "\n" +
                "Player 2 position : " + playerTwoPos + "\n" +
                "Mushroom map : " + mushrooms + "\n" +
                "Mushroom count : " + mushroomCount + "\n" +
                "Dice values : " + diceVals + "\n" +
                "Dice probabilities : " + diceProbs + "\n" +
                "Sum of probabilities : " + probabilitySum + "\n";
        }
    }
}
