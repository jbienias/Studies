#include <iostream>
#include <iomanip>
#include <fstream>
#include <time.h>
#include <stdlib.h>
#include <ctime>
#include <string>
#include <windows.h>
#include <Eigen/Dense> 
#include <Eigen/SparseLU>

/* Barzowska Monika, 238143
* Bienias Jan, 238201
*/

using namespace std;
using namespace Eigen;

#define CSVName "eigenoutput.csv"
streamsize ss = std::cout.precision();

MatrixXd loadMatrix(string filename) {
	fstream myfile(filename, ios_base::in);
	int rows;
	int columns;
	myfile >> rows;
	myfile >> columns;
	MatrixXd matrix(rows, columns);
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < columns; j++) {
			myfile >> matrix(i, j);
		}
	}
	return matrix;
}

SparseMatrix<double> loadMatrixSparse(string filename, VectorXi vector) {
	fstream myfile(filename, ios_base::in);
	int rows;
	int columns;
	myfile >> rows;
	myfile >> columns;
	SparseMatrix<double> matrix(rows, columns);
	matrix.reserve(vector);
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < columns; j++) {
			double tmp;
			myfile >> tmp;
			if (tmp != 0) { // 0.0
				matrix.insert(i, j) = tmp;
			}
		}
	}
	return matrix;
}

VectorXi sumOfNonZeroElements(MatrixXd matrix) {
	int mRows = matrix.rows();
	int mCols = matrix.cols();
	VectorXi vector(mCols);
	int counter = 0;
	for (int j = 0; j < mCols; j++) {
		counter = 0;
		for (int i = 0; i < mRows; i++) {
			if (matrix(i, j) != 0.0) counter++;
		}
		vector(j) = counter;
	}
	return vector;
}

void createCSV() {
	ofstream myfile;
	myfile.open(CSVName);
	myfile << "size;wynik_partialPivLU;wynik_SparseLU;czas_partialPivLU;czas_SparseLU\n";
	myfile.close();
}

void appendToCSV(string filename, int a, double b, double c, double d, double e) {
	fstream myfile;
	myfile.open(filename, fstream::app);
	myfile << fixed << setprecision(15) << a << ";" << b << ";" << c << ";" << fixed << setprecision(0)<< d << ";" << e << "\n";
	myfile.close();
}

void info() {
	srand((unsigned int)time(0));
	clock_t start;
	clock_t end;

	MatrixXd matrix = loadMatrix("matrix.txt");
	printf("Matrix normal loaded\n");

	int matrixSize = matrix.rows(); 
	
	VectorXi vectorOfNonZeroElem = sumOfNonZeroElements(matrix);
	printf("Non zero calculated\n");
	SparseMatrix<double> matrixSparse = loadMatrixSparse("matrix.txt", vectorOfNonZeroElem);
	SparseLU<Eigen::SparseMatrix<double> > solverA;
	matrixSparse.makeCompressed();
	solverA.analyzePattern(matrixSparse);
	solverA.factorize(matrixSparse);
	printf("Matrix sparse loaded\n");
	
	VectorXd vector = loadMatrix("vector.txt");
	printf("Vector normal loaded\n");

	VectorXd vectorSparse = loadMatrix("vector.txt");
	printf("Vector sparse loaded\n");
	


	start = clock();
	VectorXd resultVectorOfPivLU = matrix.partialPivLu().solve(vector);
	end = clock();
	double pivLUTime = (double(end - start) / CLOCKS_PER_SEC) * 1000;

	start = clock();
	VectorXd resultVectorOfSparseLU = solverA.solve(vectorSparse);
	end = clock();
	double SparseLUTime = (double(end - start) / CLOCKS_PER_SEC) * 1000;

	appendToCSV(CSVName, matrixSize, resultVectorOfPivLU[0], resultVectorOfSparseLU[0], pivLUTime, SparseLUTime);
	
	cout << "DONE" << endl;
}
int main() {
	info();
	return 0;
}
