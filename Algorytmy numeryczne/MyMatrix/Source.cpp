#include <iostream>
#include <iomanip>
#include <fstream>
#include <time.h>
#include <stdlib.h>
#include <ctime>
#include <string>
#include <windows.h>
#include <Eigen/Dense> 
#include <Eigen/LU> 

/* Barzowska Monika, 238143
* Bienias Jan, 238201
*/

using namespace std;
using Eigen::MatrixXd;
using Eigen::VectorXd;

#define generatorDir "Generated"
#define valuesDir "Generated\\Values"
#define gaussErrorsDir "Generated\\GaussErrors"

streamsize ss = std::cout.precision();
static int digitsAfterComma = 2;
static double seedLO = 1.0;
static double seedHI = 20.0;
const int genCount = 5;


MatrixXd generateSquareMatrixDouble(int size) {
	double range = seedHI - seedLO;
	MatrixXd matrix = MatrixXd::Random(size, size);
	matrix = (matrix + MatrixXd::Constant(size, size, 1.0))*range / 2.0;
	matrix = (matrix + MatrixXd::Constant(size, size, seedLO));
	return matrix;
}

MatrixXd roundMatrix(MatrixXd matrix) {
	MatrixXd roundedMatrix = matrix;
	int nRows = roundedMatrix.rows(), nCols = roundedMatrix.cols();
	for (size_t i = 0; i < nRows; i++) {
		for (size_t j = 0; j < nCols; j++) {
			roundedMatrix(i, j) = floor(matrix(i, j) * (pow(10, digitsAfterComma))) / (pow(10, digitsAfterComma));
		}
	}
	return roundedMatrix;
}

double generateRandomNumberDouble() {
	double randomNumber = (double)rand() / RAND_MAX;
	return seedLO + randomNumber * (seedHI - seedLO);
}

VectorXd generateVectorDoubleForSquareMatrix(int size) {
	VectorXd vector(size);
	for (int i = 0; i < vector.size(); i++) {
		vector[i] = generateRandomNumberDouble();
	}
	return vector;
}

VectorXd roundVector(VectorXd vector) {
	VectorXd roundedVector = vector;
	for (int i = 0; i < vector.size(); i++) {
		roundedVector(i) = floor(vector(i) * (pow(10, digitsAfterComma))) / (pow(10, digitsAfterComma));
	}
	return roundedVector;
}

int readIntFromFile(string filename) {
	fstream file;
	file.open(filename + ".txt");
	int output;
	if (!file.is_open()) {
		std::cout << "Missing " << filename << ".txt which provides proper integer size for matrices, ending program." << endl;
		Sleep(5000);
		exit(0);
	}
	else if (file.is_open()) {
		while (!file.eof()) {
			file >> output;
		}
		return output;
	}
	file.close();
}



template <typename T>
void writeGeneratedToFile(T what, string filename) {
	ofstream file(filename + ".txt");
	if (file.is_open()) {
		for (int i = 0; i < what.rows(); i++) {
			for (int j = 0; j < what.cols(); j++) {
				file << what(i, j) << " ";
			}
			file << endl;
		}
	}
	file.close();
}

void writeSolvedToFile(MatrixXd matrix, string filename, double elapsed_secs) {
	ofstream file(filename + ".txt");
	if (file.is_open()) {
		for (int i = 0; i < matrix.rows(); i++) {
			for (int j = 0; j < matrix.cols(); j++) {
				file <<fixed<< setprecision(10) << matrix(i, j) << " ";
			}
			file << endl;
		}
		file << endl;
		file << fixed << setprecision(10) << elapsed_secs << endl;
		file.close();
	}	
}

void writeDoubleToFile(string filename, double value) {
	ofstream file(filename + ".txt");
	if (file.is_open()) {
		file << value;
		file.close();
	}
}

double countDiffBetweenVectors(MatrixXd vector1, MatrixXd vector2) {
	double result = 0.0;
	for (int i = 0; i < vector1.rows(); i++) {
		for (int j = 0; j < vector1.cols(); j++) {
			result += abs(vector1(i, j) - vector2(i, j));
		}
	}
	int count = vector1.rows() * vector1.cols();
	return result / count;
}

void generate() {
	srand((unsigned int)time(0));
	clock_t start;
	clock_t end;
	double elapsedTime;
	double sum = 0;
	double avgTime = 0;

	int size = readIntFromFile("size");

	CreateDirectory(generatorDir, NULL);
	CreateDirectory(gaussErrorsDir, NULL);
	double partialDiff[genCount];
	double fullDiff[genCount];
	for (int j = 0; j < genCount; j++) {
		string dir = valuesDir + to_string(j + 1);
		CreateDirectory(dir.c_str(), NULL);
		VectorXd wektorX = generateVectorDoubleForSquareMatrix(size);
		wektorX = roundVector(wektorX);
		writeGeneratedToFile(wektorX, dir + "\\" + "vector");
		MatrixXd matrix[3];
		for (int i = 0; i < 3; i++) {
			string index = to_string(i + 1);
			matrix[i] = generateSquareMatrixDouble(size);
			matrix[i] = roundMatrix(matrix[i]);
			writeGeneratedToFile(matrix[i], dir + "\\" + "matrix" + index);
		}

		// A * X
		MatrixXd AX;
		start = clock();
		AX = matrix[0] * wektorX;
		end = clock();
		elapsedTime = (double(end - start) / CLOCKS_PER_SEC) * 1000;
		writeSolvedToFile(AX, dir + "\\" + "AX", elapsedTime);

		// (A + B + C) * X
		MatrixXd ABCX;
		start = clock();
		ABCX = (matrix[0] + matrix[1] + matrix[2]) * wektorX;
		end = clock();
		elapsedTime = (double(end - start) / CLOCKS_PER_SEC) * 1000;
		writeSolvedToFile(ABCX, dir + "\\" + "ABCX", elapsedTime);

		// A * (B * C)
		MatrixXd ABC;
		start = clock();
		ABC = matrix[0] * (matrix[1] * matrix[2]);
		end = clock();
		elapsedTime = (double(end - start) / CLOCKS_PER_SEC) * 1000
		writeSolvedToFile(ABC, dir + "\\" + "ABC", elapsedTime);

		// partial
		MatrixXd partial;
		start = clock();
		partial = matrix[0].partialPivLu().solve(wektorX);
		end = clock();
		MatrixXd somewhatVectorPartial = matrix[0] * partial;
		partialDiff[j] = countDiffBetweenVectors(somewhatVectorPartial, wektorX);
		elapsedTime = (double(end - start) / CLOCKS_PER_SEC) * 1000
		writeSolvedToFile(partial, dir + "\\" + "partial", elapsedTime);

		// full
		MatrixXd full;
		start = clock();
		full = matrix[0].fullPivLu().solve(wektorX);
		end = clock();
		MatrixXd somewhatVectorFull = matrix[0] * partial;
		fullDiff[j] = countDiffBetweenVectors(somewhatVectorFull, wektorX);
		elapsedTime = (double(end - start) / CLOCKS_PER_SEC) * 1000
		writeSolvedToFile(full, dir + "\\" + "full", elapsedTime);
		std::cout << "Generated " << j + 1 << " results for size : " << size << "." << endl;
	}
	double avgPartialDiff = 0.0;
	double avgFullDiff = 0.0;
	for (int i = 0; i < genCount; i++) {
		avgPartialDiff += partialDiff[i];
		avgFullDiff += fullDiff[i];
	}
	string directory = gaussErrorsDir;
	avgPartialDiff /= genCount;
	avgFullDiff /= genCount;
	writeDoubleToFile(directory + "\\" + "GaussPartialError", avgPartialDiff);
	writeDoubleToFile(directory + "\\" + "GaussFullError", avgPartialDiff);
	std::cout << "Generated all results!" << endl;
	std::cin.get();
}


int main() {
	generate();
}
