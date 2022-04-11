using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
