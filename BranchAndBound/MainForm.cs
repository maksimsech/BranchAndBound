using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BranchAndBound.Method;
using BranchAndBound.OutputMethod;

namespace BranchAndBound
{
    public partial class MainForm : Form
    {
        private readonly double[,] defaultMatrix = new double[,]
        {
                {double.PositiveInfinity, 13, 14, 6, 8, 4, 12},
                {12, double.PositiveInfinity, 12, 16, 15, 7, 7},
                {11, 10, double.PositiveInfinity, 8, 8, 5, 17},
                {8, 8, 5, double.PositiveInfinity, 10, 9, 8},
                {6, 8, 9, 7, double.PositiveInfinity, 4, 9},
                {7, 6, 15, 14, 16, double.PositiveInfinity, 10},
                {9, 14, 8, 15, 7, 12, double.PositiveInfinity}
        };

        private readonly int defaultSize = 7;

        public MainForm()
        {
            InitializeComponent();
            InitPictureBox();
        }

        private void InitPictureBox()
        {
            var bitmap = new Bitmap(outputPictureBox.Width, outputPictureBox.Height);
            Graphics.FromImage(bitmap).Clear(Color.White);
            outputPictureBox.Image = bitmap;
        }

        private double[,] GetMatrix()
        {
            var size = matrixGridView.Rows.Count;
            var matrix = new double[size, size];

            for(int i = 0; i < size; i++)
                for(int j = 0; j < size; j++)
                    matrix[i, j] = Convert.ToDouble(matrixGridView.Rows[i].Cells[j].Value);

            return matrix;
        }

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            var matrix = GetMatrix();
            var method = new BranchAndBoundMethod(matrix, matrix.GetLength(0));
            var results = method.Start();
            var imageDraw = new ImageMethod(results);           
            outputPictureBox.Image = imageDraw.GenetateImage(outputPictureBox.Width, outputPictureBox.Height);
            foreach(var result in results)
            {
                outputTextBox.AppendText(result.ToResultString());
            } 
        }

        private void CreateSizeButton_Click(object sender, EventArgs e)
        {
            if(!(int.TryParse(sizeTextBox.Text, out var size) && size < 10 && size > 0))
            {
                MessageBox.Show("Проверьте введенное число!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClearMatrixTable();
            CreateMatrixColumns(size);
            CreateMatrixRows(size);
        }

        private void ClearMatrixTable()
        {
            matrixGridView.Rows.Clear();
            matrixGridView.Columns.Clear();
        }

        private void CreateMatrixColumns(int size)
        {
            var columns = new DataGridViewColumn[size];

            for(int i = 0; i < size; i++)
            {
                var column = new DataGridViewTextBoxColumn();
                column.HeaderText = $"{i + 1}";
                column.Name = $"column{i + 1}";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

                columns[i] = column;
            }

            matrixGridView.Columns.AddRange(columns);
        }

        private void CreateMatrixRows(int size)
        {
            for(int i = 0; i < size; i++)
            {
                var row = new DataGridViewRow();
                row.HeaderCell.Value = $"{i + 1}";
                matrixGridView.Rows.Add(row);
            }
        }

        private void SetDefaultInfoButton_Click(object sender, EventArgs e)
        {
            if(matrixGridView.Rows?.Count != defaultSize)
            {
                MessageBox.Show($"Размер таблицы должен равняться {defaultSize}!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for(int i = 0; i < defaultSize; i++)
            {
                for(int j = 0; j < defaultSize; j++)
                {
                    matrixGridView.Rows[i].Cells[j].Value = defaultMatrix[i, j];
                }
            }
        }
    }
}
