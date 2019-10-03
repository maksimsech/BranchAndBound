using System;
using System.Collections.Generic;
using System.Drawing;
using BranchAndBound.Method;
using TreeGenerator;

namespace BranchAndBound.OutputMethod
{
    public class ImageMethod
    {
        private IEnumerable<MethodValue> _values;

        public ImageMethod(IEnumerable<MethodValue> values)
        {
            this._values = values;
        }

        public Image GenetateImage(int width, int height)
        {
            Image result;

            var treeData = CreateDate();
            var treeGenerator = new TreeBuilder(treeData);

            result = Image.FromStream(treeGenerator.GenerateTree(width, height, "1", System.Drawing.Imaging.ImageFormat.Bmp));
            return result;
        }

        private TreeData.TreeDataTableDataTable CreateDate()
        {
            var treeData = new TreeData.TreeDataTableDataTable();

            foreach(var value in _values)
            {
                if(value.CurrentState == MethodValue.State.FinalPath)
                    continue;

                var note = value.CurrentState switch
                {
                    MethodValue.State.Changed => $"-({value.Selected?.row}, {value.Selected?.column})",
                    MethodValue.State.Trimmed => $"\u0304(\u0304{value.Selected?.row}\u0304,\u0304 \u0304{value.Selected?.column}\u0304)",
                    _ => $"({value.Selected?.row}, {value.Selected?.column})"
                };
                var patentId = value.ParentValueId == -1? "" : value.ParentValueId.ToString();

                treeData.AddTreeDataTableRow(value.ValueId.ToString(), value.ParentValueId.ToString(), value.Value.HasValue? value.Value?.ToString() : "", note);
            }            

            return treeData;
        }
    }
}