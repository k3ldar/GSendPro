using System;
using System.Collections;
using System.Windows.Forms;

using Shared;

namespace GSendControls
{
    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private readonly CaseInsensitiveComparer ObjectCompare;

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            SortColumn = 0;

            // Initialize the sort order to 'none'
            SortOrder = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            try
            {
                int compareResult;
                ListViewItem listviewX, listviewY;

                // Cast the objects to be compared to ListViewItem objects
                listviewX = (ListViewItem)x;
                listviewY = (ListViewItem)y;

                // if the items subitems count do not match then say they are the same
                if (listviewX.SubItems.Count != listviewY.SubItems.Count)
                    return 0;

                Int64 val1 = 0;
                Int64 val2 = 0;
                DateTime val1Date = DateTime.Now;
                DateTime val2Date = DateTime.Now;
                decimal val1Dec = decimal.MinValue;
                decimal val2Dec = decimal.MinValue;

                if (SortColumn >= listviewX.SubItems.Count || SortColumn >= listviewY.SubItems.Count)
                    SortColumn = 0;

                if (Utilities.StrIsNumeric(listviewX.SubItems[SortColumn].Text, ref val1) && Utilities.StrIsNumeric(listviewY.SubItems[SortColumn].Text, ref val2))
                    compareResult = ObjectCompare.Compare(val1, val2);
                else if (Utilities.StrIsDate(listviewX.SubItems[SortColumn].Text, ref val1Date) && Utilities.StrIsDate(listviewY.SubItems[SortColumn].Text, ref val2Date))
                    compareResult = ObjectCompare.Compare(val1Date, val2Date);
                else if (Utilities.StrIsCurrency(listviewX.SubItems[SortColumn].Text, ref val1Dec) && Utilities.StrIsCurrency(listviewY.SubItems[SortColumn].Text, ref val2Dec))
                    compareResult = ObjectCompare.Compare(val1Dec, val2Dec);
                else
                    // Compare the two items
                    compareResult = ObjectCompare.Compare(listviewX.SubItems[SortColumn].Text, listviewY.SubItems[SortColumn].Text);

                // Calculate correct return value based on object comparison
                if (SortOrder == SortOrder.Ascending)
                {
                    // Ascending sort is selected, return normal result of compare operation
                    return compareResult;
                }
                else if (SortOrder == SortOrder.Descending)
                {
                    // Descending sort is selected, return negative result of compare operation
                    return -compareResult;
                }
                else
                {
                    // Return '0' to indicate they are equal
                    return 0;
                }
            }
            catch (Exception err)
            {
                if (err.Message.Contains("InvalidArgument=Value of"))
                {
                    return 0;
                }
                else
                    throw;
            }
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn { get; set; }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder SortOrder { get; set; }
    }
}
