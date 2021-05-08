using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MatrixLibrary;
using System.IO;
using System.Text.RegularExpressions;

namespace MatrixSolution
{
    delegate int NumberOperation(int i, int j, Random rnd);
    public partial class MainWindow : Window
    {
        Matrix<double> numbersMatrix1;
        Matrix<double> numbersMatrix2;
        Matrix<double> numbersResult;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btCalculate_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            int x = Convert.ToInt32(matrixSizeX.Text);
            int y = Convert.ToInt32(matrixSizeY.Text);

            NumberOperation getRandomNumber = new NumberOperation(GetRandomNumber);
            GetRandomMatrix( x, y);
            DrawMatrix();
        }

 

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            CreateFile();
        }

        private void CreateFile()
        {
            Microsoft.Win32.SaveFileDialog sv = new Microsoft.Win32.SaveFileDialog { Filter = "CSV|*.csv|Text Documents|*.txt" };
            sv.ShowDialog();

            if (!string.IsNullOrEmpty(sv.FileName))
            {
                using (StreamWriter sw = new StreamWriter(sv.FileName, false, Encoding.Unicode, 10485760))
                {
                    sw.Write(Clipboard.GetData(TextDataFormat.UnicodeText.ToString()));
                }

                string resultText = "";

                for (int i = 0; i < numbersResult.ySize; i++)
                {
                    for (int j = 0; j < numbersResult.xSize; j++)
                    {
                        resultText += Convert.ToString(numbersResult[j, i]) + ";" ;
                    }
                    resultText += "\n";
                }

                File.WriteAllText(sv.FileName, resultText);
            }
        }

        private void GetRandomMatrix( int x, int y)
        {
            numbersMatrix1 = Matrix<double>.GenerateMatrix(x, y, (x1, y1, rnd) => rnd.Next(-100, 100) + x1 - y1);
            numbersMatrix2 = Matrix<double>.GenerateMatrix(x, y, (x1, y1, rnd) => rnd.Next(-100, 100) + x1 - y1);
            numbersResult = new Matrix<double>(x, y);
        }

        private int GetRandomNumber(int x, int y, Random rnd)
        {
            int value = rnd.Next(-100, 100)+x- y;
            return value;
        }

        private void Calculate()
        {
            DateTime timeStart = DateTime.Now;
            this.Cursor = Cursors.Wait;

            switch (matrixMethod.SelectedIndex)
            {
                case 0:
                    numbersResult = numbersMatrix1 + numbersMatrix2;
                    break;
                case 1:
                    numbersResult = numbersMatrix1 * numbersMatrix2;
                    break;
                default:
                    numbersResult = numbersMatrix1 + numbersMatrix2;
                    break;
            }

            DateTime timeStop = DateTime.Now;     
            
            tbResult.Text = Convert.ToString((timeStop - timeStart).TotalMilliseconds);
            DrawResult();
            numbersResult.WriteMatrix();

            this.Cursor = Cursors.Arrow;
        }

        private void DrawMatrix()
        {
            string matrixText1 = "";
            string matrixText2 = "";

            for (int i = 0; i < numbersMatrix1.ySize; i++)
            {
               
                for (int j = 0; j < numbersMatrix1.xSize; j++)
                {
                    matrixText1 += "----------";
                    matrixText2 += "----------";
                }
                matrixText1 += "\n| ";
                matrixText2 += "\n| ";

                for (int j = 0; j < numbersMatrix1.xSize; j++)
                {             
                    matrixText1 += Convert.ToString(numbersMatrix1[j, i]) + "\t";
                    matrixText1 += " | ";
                    matrixText2 += Convert.ToString(numbersMatrix2[j, i]) + "\t";
                    matrixText2 += " | ";
                }

                matrixText1 += "\n";
                matrixText2 += "\n";
               
              
            }
            for (int j = 0; j < numbersMatrix1.xSize; j++)
            {
                matrixText1 += "----------";
                matrixText2 += "----------";
            }
            matrix1.Text = matrixText1;
            matrix2.Text = matrixText2;
        }

        private void DrawResult()
        {
            string resultText = "";
            for (int j = 0; j < numbersResult.xSize; j++)
            {
                resultText += "----------";
            }
          
            for (int i = 0; i < numbersResult.ySize; i++)
            {
                resultText += "\n| ";
                for (int j = 0; j < numbersResult.xSize; j++)
                {
                    resultText += Convert.ToString(numbersResult[j, i]) + "\t";
                    resultText += " | ";
                }

                resultText += "\n";
            }

            for (int j = 0; j < numbersResult.xSize; j++)
            {
                resultText += "----------";
               
            }

            result.Text = resultText;
        }
    }
}
