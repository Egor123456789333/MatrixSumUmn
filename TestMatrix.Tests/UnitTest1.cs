using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MatrixLibrary.Tests
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void Get_Right_Plus_Result()
        {
            const int size = 4;
            dynamic[,] realResult = new dynamic[size, size] { { 0, 2, 4, 6 }, { 0, 2, 4, 6 }, { 0, 2, 4, 6 }, { 0, 2, 4, 6 } };

            Matrix<double> matrix1 = new Matrix<double>(size, size);
            Matrix<double> matrix2 = new Matrix<double>(size, size);
            Matrix<double> resultMatrix1;
            dynamic[,] resultMatrix2 = new dynamic[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix1[j, i] = i;
                    matrix2[j, i] = i;
                }
            }

            resultMatrix1 = matrix1 + matrix2;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    resultMatrix2[j, i] = resultMatrix1[j, i];
                }
            }

            CollectionAssert.AreEqual(realResult, resultMatrix2);
        }

        [TestMethod]
        public void Get_Right_Multiply_Result()
        {
            const int size = 4;
            dynamic[,] realResult = new dynamic[size, size] { { 0, 12, 24, 36 }, { 0, 12, 24, 36 }, { 0, 12, 24, 36 }, { 0, 12, 24, 36 } };

            Matrix<double> matrix1 = new Matrix<double>(size, size);
            Matrix<double> matrix2 = new Matrix<double>(size, size);
            Matrix<double> resultMatrix1;
            dynamic[,] resultMatrix2 = new dynamic[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix1[j, i] = i+i;
                    matrix2[j, i] = i;
                }
            }

            resultMatrix1 = matrix1 * matrix2;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    resultMatrix2[j, i] = resultMatrix1[j, i];
                }
            }

            CollectionAssert.AreEqual(realResult, resultMatrix2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Multiply_Negative_Matrix()
        {
            Matrix<double> resultMatrix1;
            Matrix<double> matrix1 = new Matrix<double>(3, 5);
            Matrix<double> matrix2 = new Matrix<double>(3, 5);

            resultMatrix1 = matrix1 * matrix2;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Plus_Negative_Matrix()
        {
            Matrix<double> resultMatrix1;
            Matrix<double> matrix1 = new Matrix<double>(2, 2);
            Matrix<double> matrix2 = new Matrix<double>(6, 6);

            resultMatrix1 = matrix1 + matrix2;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Negative_Matrix_With_0()
        {
            Matrix<double>.GenerateMatrix(0, 4, (x1, y1, rnd) => rnd.Next(-100, 100) + x1 - y1);
        }
    }
}
