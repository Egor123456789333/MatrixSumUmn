using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibrary
{
    public class Matrix<T>
    {
        public dynamic[,] numbers;
        public int xSize;
        public int ySize;

        public Matrix(int x, int y)
        {
            xSize = x;
            ySize = y;
            numbers = new dynamic[xSize, ySize];
        }

        public dynamic this[int i, int j]
        { 
            get
            {
                return numbers[i, j];
            }
            set
            {
                numbers[i, j] = value;
            }
        }

        public static Matrix<T> operator +(Matrix<T> matrix1, Matrix<T> matrix2)
        {

            if (matrix1.xSize != matrix2.xSize || matrix1.ySize != matrix2.ySize) throw new ArgumentException("Матрицы нельзя сложить");

            Matrix<T> finalMatrix = new Matrix<T>(matrix1.xSize, matrix2.ySize);

            for (int i = 0; i < matrix1.xSize; i++)
            {
                for (int j = 0; j < matrix2.ySize; j++)
                {
                    finalMatrix[i, j] = (dynamic)(matrix1[i, j] + matrix2[i, j]);
                }
            }

            return finalMatrix;
        }

        public static Matrix<T> operator *(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if (matrix1.xSize != matrix2.ySize) throw new ArgumentException("Матрицы нельзя перемножить");

            Matrix<T> finalMatrix = new Matrix<T>(matrix2.xSize, matrix1.ySize);

            for (int i = 0; i < matrix1.ySize; i++)
            {
                for (int j = 0; j < matrix2.xSize; j++)
                {
                    finalMatrix[j, i] = 0;
                    for (int k = 0; k < matrix2.ySize; k++)
                    {
                        finalMatrix[j, i] += (matrix1[k, i] * matrix2[j, k]);
                    }
                }
            }

            return finalMatrix;
        }

        public static Matrix<T> GenerateMatrix(int rows, int columns, Func<int, int, Random, T> f)
        {
            if (rows == 0 || columns == 0) throw new ArgumentException("Матрица не может быть нулевого размера!");
            Matrix<T> newMatrix = new Matrix<T>(rows, columns);
            Random rnd = new Random();

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    newMatrix[j, i] = f(j, i, rnd);
                }

            return newMatrix;
        }


        public void WriteMatrix()
        {
            for (int i = 0; i < ySize; i++)
            {
             
                for (int j = 0; j < xSize; j++)
                {
                    Console.Write(numbers[j, i] );
                }

                Console.WriteLine();
            }
        }
    }
}
