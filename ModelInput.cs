using Microsoft.ML.Data;

namespace Interface
{
    public class ModelInput
    {
        [ColumnName("Label")]
        public string Label { get; set; }

        [ColumnName("ImageSource")]
        public string ImageSource { get; set; }
    }
}