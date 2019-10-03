namespace BranchAndBound.Method
{
    public class MethodValue
    {
        public enum State
        {
            Initial,
            Finish,
            Changed,
            Trimmed,
            FinalPath // Path in matrix haHAA
        }

        public double? Value { get; set; }

        public State? CurrentState { get; set;}

        public string Matrix { get; set;}

        public (int row, int column)? Selected { get; set; }

        public int ValueId { get; set; }

        public int ParentValueId { get; set;} = -1;
    }
}