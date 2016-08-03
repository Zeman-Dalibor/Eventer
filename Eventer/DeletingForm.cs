using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Eventer
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class DeletingForm : Form
    {
        /// <summary>
        /// Delete worker, that delete events separatelly.
        /// </summary>
        private TimeEventDeleter deleter;

        /// <summary>
        /// Indicate if all deleting has been completed.
        /// </summary>
        private bool complete = false;

        /// <summary>
        /// Form that indicate progress in deleting.
        /// </summary>
        /// <param name="count">Total count of deleting events.</param>
        /// <param name="eventsForDelete">Events that should be deleted.</param>
        internal DeletingForm(Database db, int count, TimeEvent[] eventsToDelete)
        {
            deleter = new TimeEventDeleter(db, count, eventsToDelete);
            InitializeComponent();
            progressBar.Minimum = 0;
            progressBar.Maximum = count;
        }

        /// <summary>
        /// Handle that should be called when form is going to close.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void DeletingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (complete)
                e.Cancel = false;
            else
            {
                var window = MessageBox.Show("Opravdu chcete uzavřít toto okno? Přeruší se tak mazání událostí.", "Uzavřít okno", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (window == DialogResult.No) e.Cancel = true;
                else
                {
                    deleter.RequestStop();
                    e.Cancel = false;
                }
                DialogResult = DialogResult.Abort;
            }
        }

        /// <summary>
        /// Handle that should be called when Storno button has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonStorno_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handle that should be called when Timer tick happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void timerProgress_Tick(object sender, EventArgs e)
        {
            progressBar.Value = deleter.DeletedCount;
            labelProgress.Text = (deleter.DeletedCount) + "/" + (progressBar.Maximum);

            if (deleter.DeletedCount == progressBar.Maximum)
            {
                complete = true;
                Close();
            }
        }
    }
}
