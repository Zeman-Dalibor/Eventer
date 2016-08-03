using System;
using System.Windows.Forms;

#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// Synchronization form. It contain all gui necessary thing.
    /// </summary>
    public partial class SynchForm : Form
    {
        /// <summary>
        /// Backend instance of Synchronization.
        /// </summary>
        private Synchronization synch;

        /// <summary>
        /// Indicate if this form is already closing.
        /// </summary>
        private bool closing = false;

        /// <summary>
        /// Determine if it is synchronizing.
        /// </summary>
        private bool synchronizing = false;

        /// <summary>
        /// Standart Constructor for easy data set.
        /// </summary>
        /// <param name="device">Synchronization Device with which it is going to synchronize.</param>
        /// <param name="synch">Backend Synchronize instance that support behavior.</param>
        internal SynchForm(SynchronizationDevice device, Synchronization synch)
        {
            this.synch = synch;
            synch.CallbackError += ErrorCallBack;
            
            InitializeComponent();

            this.Text = "Synchronizace: " + device.Name + " - " + device.IP.ToString();

            string lastsynch = device.LastSynchDate == DateTime.MinValue ? "Nikdy" : device.LastSynchDate.ToString(Constants.UserFriendlyDateTimeFormat);

            labelIP.Text = device.IP.ToString();
            labelLastSynch.Text = lastsynch;
            labelName.Text = device.Name;
            labelID.Text = device.DeviceID;

            DialogResult = DialogResult.Cancel;
            synchronizing = false;
        }

        /// <summary>
        /// Callback Method that should be callled when Synchronization (Communication) error occurs.
        /// </summary>
        /// <param name="type">Type of error that occurs.</param>
        internal void ErrorCallBack(Synchronization.ErrorType type)
        {
            Console.WriteLine("Calling");
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { ErrorCallBack(type); });
                return;
            }
            else
            {
                switch (type)
                {
                    case Synchronization.ErrorType.AllreadyBindTCPPort:
                        synch.StopSynchronizing();
                        Close();
                        break;
                    case Synchronization.ErrorType.ConnectionProblem:
                        MessageBox.Show("Nebylo možno navázat spojení, ujistěte se zda je druhá instance stále zapnuta a viditelná.");
                        synch.StopSynchronizing();
                        Close();
                        break;
                    case Synchronization.ErrorType.ConnectionLost:
                        MessageBox.Show("Došlo ke ztrátě spojení.");
                        synch.StopSynchronizing();
                        Close();
                        break;

                }
            }
        }

        /// <summary>
        /// This should be called when button synchronize has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonSynchronize_Click(object sender, EventArgs e)
        {
            synchronizing = true;
            buttonSynchronize.Enabled = false;
            timerLocalChanges.Enabled = false;
            timerRemoteChanges.Enabled = false;
            synch.StartSynchronizing();
        }

        /// <summary>
        /// This should be called when Local Timer tick happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void timerLocalChanges_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                lock (synch.LocalListBoxData)
                {
                    if (synch.LocalListBoxData.Count == 0)
                        break;
                    Change te = synch.LocalListBoxData.Dequeue();
                    if (te == null)
                    {
                        timerLocalChanges.Enabled = false;
                        break;
                    }
                    checkedListBoxLocalChanges.Items.Add(te, true);
                }
            }
            lock (synch.LocalListBoxData)
            {
                int done = checkedListBoxLocalChanges.Items.Count;
                int total = synch.MaxLocal;

                labelProgressLocal.Text = (done) + "/" + (total);

                if (total == 0)
                    progressBarLocal.Value = 0;
                else
                    progressBarLocal.Value = (int)(((double)done / total) * 1000);
            }
        }

        /// <summary>
        /// This should be called when Remote Timer tick happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void timerRemoteChanges_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                lock (synch.RemoteListBoxData)
                {
                    if (synch.RemoteListBoxData.Count == 0)
                        break;
                    Change te = synch.RemoteListBoxData.Dequeue();
                    if (te == null)
                    {
                        timerRemoteChanges.Enabled = false;
                        break;
                    }
                    checkedListBoxRemoteChanges.Items.Add(te, true);
                }
            }
            lock (synch.RemoteListBoxData)
            {
                int done = checkedListBoxRemoteChanges.Items.Count;
                int total = synch.MaxRemote;

                labelProgressRemote.Text = (done) + "/" + (total);

                if (total == 0)
                    progressBarRemote.Value = 0;
                else
                    progressBarRemote.Value = (int)(((double)done / total) * 1000);
            }

        }

        /// <summary>
        /// This should be called when item i listbox has changed its checked state.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void checkedListBoxLocalChanges_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
                synch.DisableLocalChange(((Change)checkedListBoxLocalChanges.Items[e.Index]).TimeEventHash);
            else
                synch.EnableLocalChange(((Change)checkedListBoxLocalChanges.Items[e.Index]).TimeEventHash);
        }

        /// <summary>
        /// This should be called when item i listbox has changed its checked state.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void checkedListBoxRemoteChanges_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
                synch.DisableRemoteChange(((Change)checkedListBoxRemoteChanges.Items[e.Index]).TimeEventHash);
            else
                synch.EnableRemoteChange(((Change)checkedListBoxRemoteChanges.Items[e.Index]).TimeEventHash);
        }

        /// <summary>
        /// This should be called when this form is going to Close.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void SynchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool canClose = synch.SychFormClosing();
            if (!canClose)
            {
                var window = MessageBox.Show("Opravdu chcete uzavřít toto okno? Přeruší se tak synchronizace a může dojít ke ztrátě nebo nekonzistenci dat.", "Uzavřít okno", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (window == DialogResult.No) e.Cancel = true;
                else e.Cancel = false;
                DialogResult = DialogResult.Abort;
            }
        }

        /// <summary>
        /// This should be called when mouse double click on item from Local Listbox happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void checkedListBoxLocalChanges_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Change c = (Change)checkedListBoxLocalChanges.SelectedItem;
            if (c.Type == Change.ChangeType.Del)
                MessageBox.Show("Událost je smazána!");
            else
                using (TimeEventForm teForm = new TimeEventForm(c.TimeEvent))
                {
                    teForm.ShowDialog();
                }
        }

        /// <summary>
        /// This should be called when mouse double click on item from Remote Listbox happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void checkedListBoxRemoteChanges_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Change c = (Change)checkedListBoxRemoteChanges.SelectedItem;
            if (c.Type == Change.ChangeType.Del)
                MessageBox.Show("Událost je smazána!");
            else
                using (TimeEventForm teForm = new TimeEventForm(c.TimeEvent))
                {
                    teForm.ShowDialog();
                }
        }

        /// <summary>
        /// This should be called when timer tick happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void timerSynchronize_Tick(object sender, EventArgs e)
        {
            if (!synchronizing && synch.LocalChangeFull && synch.RemoteChangeFull)
            {
                buttonSynchronize.Enabled = true;
                labelLoading.Visible = false;

                if (synch.LocalChanges.Count == 0 && synch.RemoteChanges.Count == 0)
                {
                    if (closing)
                        return;
                    closing = true;
                    MessageBox.Show("Není co synchronizovat." + Environment.NewLine + "Instance neprovedli od poslední synchronizace rozdílné změny.");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }

            int done = synch.Synchronized;
            int total = synch.SynchronizeTotal;

            labelProgress.Text = (done) + "/" + (total);

            if (total == 0)
                progressBar.Value = 0;
            else
                progressBar.Value = (int)(((double)done / total) * 1000);

            if (synch.SynchronizationCompleted)
            {
                if (closing)
                    return;

                closing = true;
                MessageBox.Show("Synchronizace byla provedena");
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// That should be called after this form was closed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void SynchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            synch.CallbackError -= ErrorCallBack;
        }
    }
}
