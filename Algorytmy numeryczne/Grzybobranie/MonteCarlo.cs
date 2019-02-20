using System;

//Monika Barzowska, Jan Bienias
//238143, 238201


namespace Grzybobranie {
    public class MonteCarlo {
        private Dice dice;
        private GameState firstState;
        private Random random;


        public MonteCarlo(GameState firstState, Dice dice) {
            this.dice = dice;
            this.firstState = firstState;
            random = new Random();
        }

        public double simulate(int simulations) {
            int counter = 0;
            GameState current;
            for (int i = 0; i < simulations; i++) {
                current = firstState;
                while (current.end != true) {
                    int first = current.playerOnePos;
                    int second = current.playerTwoPos;
                    if (current.turn)
                        first += rollTheDice();
                    else if (!current.turn)
                        second += rollTheDice();
                    current = new GameState(current.nextTurn(), first, second, current.mushroomsOneCount, current.mushroomsTwoCount, current.mushroomMap);
                    checkIfEnd(current);
                }
                if (current.win)
                    counter++;
            }
            return (double)counter / simulations;
        }

        private void checkIfEnd(GameState state) {
            if (state.mushroomsOneCount > state.mushroomsTwoCount + state.mushroomsLeftCount) {
                state.win = true;
                state.end = true;
            } else if (state.mushroomsTwoCount > state.mushroomsOneCount + state.mushroomsLeftCount) {
                state.end = true;
                state.win = false;
            } else if (state.playerOnePos == 0 && state.mushroomsOneCount >= state.mushroomsTwoCount) {
                state.win = true;
                state.end = true;
            } else if (state.playerTwoPos == 0 && state.mushroomsOneCount <= state.mushroomsTwoCount) {
                state.end = true;
                state.win = false;
            } else if (state.playerTwoPos == 0 && state.mushroomsOneCount > state.mushroomsTwoCount) {
                state.end = true;
                state.win = true;
            } else if (state.playerOnePos == 0 && state.mushroomsOneCount < state.mushroomsTwoCount) {
                state.end = true;
                state.win = false;
            }
        }

        private int rollTheDice() {
            int generatedValue = modulo(randomInteger(random), dice.probabilitySum);
            int lowerBound = 0;
            int higherBound = dice.probability[0];
            for (int i = 0; i < dice.probability.Length; i++) {
                if (generatedValue >= lowerBound && generatedValue < higherBound) {
                    return dice.values[i];
                }
                lowerBound += dice.probability[i];
                higherBound += dice.probability[i + 1];
            }
            return 0;
        }

        private static int randomInteger(Random random) {
            return random.Next();
        }

        private static int modulo(int x, int m) {
            int r = x % m;
            return r < 0 ? r + m : r;
        }
    }
}
