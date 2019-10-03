using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BranchAndBound.Method
{
    public class BranchAndBoundMethod
    {
        private readonly double[,] _originalMatrix;
        private readonly int _originalSize;

        public BranchAndBoundMethod(double[,] matrix, int size)
        {
            this._originalMatrix = matrix;
            this._originalSize = size;
        }

        public IEnumerable<MethodValue> Start()
        {
            var rowNums = new List<int>();
            var colNums = new List<int>();
            for (int number = 1; number <= _originalSize; number++)
            {
                rowNums.Add(number);
                colNums.Add(number);
            }
            var matrix = MakeCopy(_originalMatrix);
            var size = _originalSize;
            var result = new Dictionary<(int row, int col), double>();
            var lastId = 1;
            var lastParentId = 1;
            yield return new MethodValue
            {
                CurrentState = MethodValue.State.Initial,
                ValueId = lastId,
            };

            while (size != 2)
            {
                var minusMatrix = DeleteByRowsAndCols(MakeCopy(matrix), size, out var sum);
                var (i, j) = FindBiggestSumByElem(MakeCopy(minusMatrix), size);

                var rowToInfinity = colNums.FindIndex(col => col == rowNums[i]);
                var colToInfinity = rowNums.FindIndex(row => row == colNums[j]);

                var trimmedMatrix = TrimMatrix(MakeCopy(minusMatrix), i, j, rowToInfinity, colToInfinity);
                trimmedMatrix = DeleteByRowsAndCols(trimmedMatrix, trimmedMatrix.GetLength(0), out var trimmedResult);
                var changedMatrix = ChangeMatrix(MakeCopy(minusMatrix), i, j);
                changedMatrix = DeleteByRowsAndCols(changedMatrix, changedMatrix.GetLength(0), out var changedResult);
                if (changedResult < trimmedResult)
                {
                    result.Add((rowNums[i], colNums[j]), sum + changedResult);
                    matrix = changedMatrix;                    

                    yield return new MethodValue
                    {
                        CurrentState = MethodValue.State.Changed,
                        Selected = (rowNums[i], colNums[j]),
                        Matrix = PrintMatrix(changedMatrix, rowNums, colNums),
                        Value = sum + changedResult,
                        ValueId = ++lastId,
                        ParentValueId = lastParentId
                    };

                    yield return new MethodValue
                    {
                        CurrentState = MethodValue.State.Trimmed,
                        Selected = (rowNums[i], colNums[j]),
                        Matrix = PrintMatrix(trimmedMatrix, rowNums, colNums),
                        Value = sum + trimmedResult,
                        ValueId = ++lastId,
                        ParentValueId = lastParentId
                    };

                    lastParentId = lastId - 1;
                    
                }
                else
                {
                    result.Add((rowNums[i], colNums[j]), sum + trimmedResult);
                    
                    yield return new MethodValue
                    {
                        CurrentState = MethodValue.State.Trimmed,
                        Selected = (rowNums[i], colNums[j]),
                        Matrix = PrintMatrix(trimmedMatrix, rowNums, colNums),
                        Value = sum + trimmedResult,
                        ValueId = ++lastId,
                        ParentValueId = lastParentId
                    };

                    yield return new MethodValue
                    {
                        CurrentState = MethodValue.State.Changed,
                        Selected = (rowNums[i], colNums[j]),
                        Matrix = PrintMatrix(changedMatrix, rowNums, colNums),
                        Value = sum + changedResult,
                        ValueId = ++lastId,
                        ParentValueId = lastParentId
                    };

                    lastParentId = lastId - 1;

                    rowNums.Remove(rowNums[i]);
                    colNums.Remove(colNums[j]);
                    size--;
                    matrix = trimmedMatrix;
                }
            }

            var lastPositions = Calc2x2(matrix);

            foreach (var position in lastPositions)
            {
                yield return new MethodValue
                {
                    CurrentState = MethodValue.State.Finish,
                    Selected = (rowNums[position.row], colNums[position.col]),
                    ValueId = ++lastId,
                    ParentValueId = lastParentId
                };
                lastParentId = lastId;
                result.Add((rowNums[position.row], colNums[position.col]), 0d);
            }
            yield return new MethodValue
            {
                CurrentState = MethodValue.State.FinalPath,
                Matrix = GetPathString(result)
            };
        }

        private double[,] MakeCopy(double[,] matrix) => (double[,])matrix.Clone();

        private double[] FindByRowMin(double[,] matrix, int size)
        {
            var min = new double[size];
            for (int i = 0; i < size; i++)
            {
                min[i] = double.PositiveInfinity;
                for (int j = 0; j < size; j++)
                {
                    if (min[i] > matrix[i, j])
                        min[i] = matrix[i, j];
                }
            }

            return min;
        }

        private double[,] DeleteByRow(double[,] matrix, double[] row, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    matrix[i, j] -= row[i];
            }
            return matrix;
        }

        private double[] FindByColMin(double[,] matrix, int size)
        {
            var min = new double[size];

            for (int i = 0; i < size; i++)
            {
                min[i] = double.PositiveInfinity;
                for (int j = 0; j < size; j++)
                {
                    if (min[i] > matrix[j, i])
                    {
                        min[i] = matrix[j, i];
                    }
                }
            }

            return min;
        }

        private double[,] DeleteByCol(double[,] matrix, double[] col, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[j, i] -= col[i];
                }
            }

            return matrix;
        }

        private double[,] DeleteByRowsAndCols(double[,] matrix, int size, out double sum)
        {
            var rowMin = FindByRowMin(matrix, size);
            var matrixMinusRows = DeleteByRow(matrix, rowMin, size);
            var colMin = FindByColMin(matrixMinusRows, size);
            var matrixMinusCols = DeleteByCol(matrixMinusRows, colMin, size);
            sum = CalcMinimals(rowMin, colMin, size);
            return matrixMinusCols;
        }

        private double CalcMinimals(double[] rowMin, double[] colMin, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += rowMin[i] + colMin[i];
            }

            return sum;
        }

        private (int, int) FindBiggestSumByElem(double[,] matrix, int size)
        {

            double CalcSum(double[,] _matrix, int i, int j)
            {
                var minRow = double.PositiveInfinity;
                for (int colNumber = 0; colNumber < size; colNumber++)
                {
                    if (j != colNumber && minRow > _matrix[i, colNumber])
                    {
                        minRow = _matrix[i, colNumber];
                    }
                }

                var minCol = double.PositiveInfinity;
                for (int rowNumber = 0; rowNumber < size; rowNumber++)
                {
                    if (i != rowNumber && minCol > _matrix[rowNumber, j])
                    {
                        minCol = _matrix[rowNumber, j];
                    }
                }

                return minRow + minCol;
            }

            var allValues = new Dictionary<(int, int), double>();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        var value = CalcSum(matrix, i, j);
                        allValues.Add((i, j), value);
                    }
                }
            }

            var result = allValues.Aggregate((left, right) => left.Value < right.Value ? right : left).Key;
            return result;
        }

        private double[,] TrimMatrix(double[,] matrix, int row, int column, int rowToInfinity, int colToInfinity)
        {
            if (rowToInfinity > -1 && colToInfinity > -1)
            {
                matrix[rowToInfinity, colToInfinity] = double.PositiveInfinity;
            }

            var rows = matrix.GetLength(0) - 1;
            var cols = matrix.GetLength(1) - 1;
            var result = new double[rows, cols];

            for (int i = 0, j = 0; i < matrix.GetLength(0); i++)
            {
                if (i == row)
                    continue;

                for (int k = 0, u = 0; k < matrix.GetLength(1); k++)
                {
                    if (k == column)
                        continue;

                    result[j, u] = matrix[i, k];
                    u++;
                }
                j++;
            }

            return result;
        }

        private double[,] ChangeMatrix(double[,] matrix, int row, int column)
        {
            var result = matrix;
            result[row, column] = double.PositiveInfinity;
            return result;
        }

        private IEnumerable<(int row, int col)> Calc2x2(double[,] matrix)
        {
            bool RowOnlyZeros(int rowNumber) => matrix[rowNumber, 0] == 0 && matrix[rowNumber, 1] == 0;

            var result = new List<(int row, int col)>();

            if (RowOnlyZeros(0))
            {
                if (matrix[1, 0] == 0)
                {
                    result.Add((1, 0));
                    result.Add((0, 1));
                }
                else
                {
                    result.Add((1, 1));
                    result.Add((0, 0));
                }
            }
            else if (RowOnlyZeros(1))
            {
                if (matrix[0, 0] == 0)
                {
                    result.Add((0, 0));
                    result.Add((1, 1));
                }
                else
                {
                    result.Add((0, 1));
                    result.Add((1, 0));
                }
            }
            else
            {
                if (matrix[0, 0] == 0)
                {
                    result.Add((0, 0));
                    result.Add((1, 1));
                }
                else
                {
                    result.Add((0, 1));
                    result.Add((1, 0));
                }
            }
            return result;
        }

        private string GetPathString(Dictionary<(int row, int col), double> results)
        {
            var result = new StringBuilder();

            var sum = results.Sum(elem => elem.Value);

            var currentElem = 1;
            result.Append($"{currentElem} -> ");

            for (int steps = 0; steps < _originalSize - 1; steps++)
            {
                var toValue = results.Where(elem => elem.Key.row == currentElem).First().Key.col;
                result.Append($"{toValue} -> ");
                currentElem = toValue;
            }
            var lastValue = results.Where(elem => elem.Key.row == currentElem).First().Key.col;
            result.Append($"{lastValue} ");

            result.Append($"Sum = {sum}");

            return result.ToString();
        }

        private string PrintMatrix(double[,] matrix, List<int> rows, List<int> cols)
        {
            var result = new StringBuilder();

            result.Append("  ");
            foreach (var col in cols)
            {
                result.Append($"{col} ");
            }
            result.AppendLine();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                result.Append($"{rows[i]} ");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result.Append($"{matrix[i, j]} ");
                }
                result.AppendLine();
            }

            return result.ToString();
        }
    }
}