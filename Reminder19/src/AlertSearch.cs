using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Reminder19.src.cntrmsgbox.Dialog;

namespace Reminder19.src
{
    public partial class AlertSearch : Form
    {
        private String sqlQuery;

        public AlertSearch()
        {
            InitializeComponent();
        }

        public String getQuery()
        {
            return sqlQuery;
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            MsgBox.Show( "This is the advanced search.  Type in a query and it will order the results where " +
                "the closest matches are at the top and the least closest matches are at the bottom.  " + 
                "When two items equally match the query, they are sorted alphabetically.  A query in the format " + 
                "\"Two Items\" will search for the phrase Two Items instead of the words two and items. Sorting functionality " +
                "is supposed to behave similarly to a search engine like google, but is still a work in progress." );
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /* This method is currently not done well.  The query is not
         * clean in the slightest. */
        private void searchButton_Click(object sender, EventArgs e)
        {
            performSearch();
        }

        private void queryField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                performSearch();
            }
        }

        private void performSearch()
        {
            if (!titlesCheckbox.Checked && !messagesCheckbox.Checked)
            {
                MsgBox.Show("Must search at least titles or messages!");
                return;
            }

            int count = 2;
            queryField.Text.Trim();
            if (queryField.Text.Equals(""))
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            ArrayList terms = new ArrayList();
            char[] queryChars = queryField.Text.ToCharArray();
            string curWord = "";
            for( int i = 0; i < queryChars.Length; i++ )
            {
                char tmp = queryChars[i];
                if (tmp == '\"')
                {
                    if (++i >= queryChars.Length)
                        break;
                    tmp = queryChars[i];
                    while (tmp != '\"')
                    {
                        curWord += tmp;
                        if (++i >= queryChars.Length)
                        {
                            terms.Add(curWord);
                            curWord = "";
                            break;
                        }
                        tmp = queryChars[i];
                    }
                    terms.Add(curWord);
                    curWord = "";
                }
                else if( tmp == ' ' )
                {
                    if( !curWord.Equals("") )
                    {
                        terms.Add( curWord );
                        curWord = "";
                    }
                }
                else
                {
                    curWord += tmp;
                }
            }
            if (!curWord.Equals(""))
            {
                terms.Add(curWord);
                curWord = "";
            }

            StringBuilder query = new StringBuilder();
            foreach (String term in terms)
            {
                query.Append(" ( a1.AlertId = a" + count + ".AlertId AND ( ");
                if (titlesCheckbox.Checked)
                {
                    query.Append(" a" + count + ".title like \"%" + term + "%\" OR ");
                }
                if (messagesCheckbox.Checked)
                {
                    query.Append(" a" + count + ".message like \"%" + term + "%\" OR ");
                }
                query.Remove(query.Length - 3, 3);
                query.Append(") ) OR ");
                count++;
            }

            if (count > 8)
            {
                MsgBox.Show("Sorry, Reminder 19 does not currently support that many terms");
                return;
            }

            String fromClause = "";
            for (int i = 4; i < count; i++)
            {
                fromClause += ", Alerts AS a" + i;
            }

            sqlQuery = query.ToString(0, query.Length - 4);
            sqlQuery = "SELECT a1.AlertId, a1.Title, a1.Message, a1.Year, a1.DayOfMonth, a1.Month, " +
                " a1.DayOfWeek, a1.Hour, a1.Minute, a1.Snoozed, a1.Valid, a1.WakeUpTime, a1.Background, " +
                " a1.Sound, a1.Command " +
                " FROM Alerts AS a1, Alerts AS a2, Alerts AS a3 " + fromClause +
                " WHERE a2.AlertId = a3.AlertId OR " + sqlQuery +
                " GROUP BY a1.AlertId " +
                " ORDER BY COUNT(a1.AlertId) DESC, upper(a1.Title) ASC ";         
            this.DialogResult = DialogResult.OK;
        }
    }
}