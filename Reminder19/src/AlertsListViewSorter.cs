using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;

//Sort

namespace Reminder19.src
{
    public class AlertsListViewSorter : IComparer
    {
        //specifies the column to be sorted
        private int ColumnToSort;

        //specifies the order in which to sort (ie Ascending)
        private SortOrder OrderOfSort;

        //case insensitive comparer object
        private CaseInsensitiveComparer ObjectCompare;

        //class constructor.  Initializes various elements
        public AlertsListViewSorter()
        {
            //Initialize the column to '0'
            ColumnToSort = 1;

            //Initialize the sort order to 'none'
            OrderOfSort = SortOrder.Ascending;

            //Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        //This method is inherited from the IComparer interface. It compares
        //compares the two objects passed using a case insensitive
        //comparison.
	    // <param name="x">First object to be compared</param>
	    // <param name="y">Second object to be compared</param>
	    // <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult = 0;
            ListViewItem listviewX, listviewY;

            //Cast the objects to be compared to ListViewItem objects.
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            //Compare the two items
            if (ColumnToSort == 0)
            {
                if (listviewX.Checked && listviewY.Checked)
                    compareResult = 0;
                else if (listviewX.Checked)
                    compareResult = 1;
                else if (listviewY.Checked)
                    compareResult = -1;
            }
            else if (ColumnToSort == 1)
            {
                int ix = System.Convert.ToInt32(listviewX.SubItems[1].Text);
                int iy = System.Convert.ToInt32(listviewY.SubItems[1].Text);
                if (ix == iy)
                    compareResult = 0;
                else if (ix > iy)
                    compareResult = 1;
                else if (iy > ix)
                    compareResult = -1;
            }
            else if (ColumnToSort == 2)
            {
                compareResult = ObjectCompare.Compare(listviewX.SubItems[2].Text, listviewY.SubItems[2].Text);
            }
            else if (ColumnToSort == 3)
            {
                DateTime a = DateTime.ParseExact(listviewX.SubItems[3].Text, "M/d/yyyy h:m:s tt", null);
                DateTime b = DateTime.ParseExact(listviewY.SubItems[3].Text, "M/d/yyyy h:m:s tt", null);
                if (a == b)
                    compareResult = 0;
                else if (a > b)
                    compareResult = 1;
                else if (b > a)
                    compareResult = -1;
            }
            else if (ColumnToSort == 4)
            {
                //This will eventually be a hidden 'rank' column.  Ignore for now.
                return 0;
            }
            else
            {
                return 0;
            }

            //Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                //Ascending sort is selected, return normal result of compae operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                //Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                //Return '0' to indicate they are equal
                return 0;
            }
        }

        //Gets of sets the numver of the column to which to apply the sorting operation( Defaults to '0').
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }
}
