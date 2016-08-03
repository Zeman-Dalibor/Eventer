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
    /// Show Form and let user decide which Change should be applied.
    /// </summary>
    internal partial class SideChooseForm : Form
    {
        /// <summary>
        /// Standart constructor.
        /// </summary>
        /// <param name="local">Local change.</param>
        /// <param name="remote">Remote change.</param>
        internal SideChooseForm(Change local, Change remote)
        {
            this.Text = "Výběr: " + local.TimeEventHash;
            InitializeComponent();

            DialogResult = DialogResult.Cancel;

            labelHash.Text = local.TimeEventHash;

            TimeEvent localTe = local.TimeEvent;
            TimeEvent remoteTe = remote.TimeEvent;
           
            switch (local.Type)
            {
                case Change.ChangeType.Add:
                    labelLocalChange.Text = "Přidána událost:";
                    setLocaleValues(localTe);
                    break;
                case Change.ChangeType.Edit:
                    labelLocalChange.Text = "Editována událost:";
                    setLocaleValues(localTe);
                    break;
                case Change.ChangeType.Del:
                    labelLocalChange.Text = "Smazána událost:";
                    hideLocalValues();
                    break;
                default:
                    break;
            }

            switch (remote.Type)
            {
                case Change.ChangeType.Add:
                    labelRemoteChange.Text = "Přidána událost:";
                    setRemoteValues(remoteTe);
                    break;
                case Change.ChangeType.Edit:
                    labelRemoteChange.Text = "Editována událost:";
                    setRemoteValues(remoteTe);
                    break;
                case Change.ChangeType.Del:
                    labelRemoteChange.Text = "Smazána událost:";
                    hideRemoteValues();
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Set local values for labels by TimeEvent.
        /// </summary>
        /// <param name="localTe">Local TimeEvent.</param>
        private void setLocaleValues(TimeEvent localTe)
        {
            labelLocalName.Text = localTe.Name;
            labelLocalDescription.Text = localTe.Description;
            labelLocalStartDate.Text = localTe.StartDate.ToString(Constants.UserFriendlyDateTimeFormat);
            labelLocalEndDate.Text = localTe.EndDate.ToString(Constants.UserFriendlyDateTimeFormat);
            labelLocalNotification.Text = localTe.Notification ? localTe.NotificationDate.ToString(Constants.UserFriendlyDateTimeFormat) : "žádné";
        }

        /// <summary>
        /// Hide local values.
        /// </summary>
        private void hideLocalValues()
        {
            labelLocalName.Text = "";
            labelLocalDescription.Text = "";
            labelLocalStartDate.Text = "";
            labelLocalEndDate.Text = "";
            labelLocalNotification.Text = "";
        }

        /// <summary>
        /// Set Remote values by TimeEvent.
        /// </summary>
        /// <param name="remoteTe">Remote TimeEvent</param>
        private void setRemoteValues(TimeEvent remoteTe)
        {
            labelRemoteName.Text = remoteTe.Name;
            labelRemoteDescription.Text = remoteTe.Description;
            labelRemoteStartDate.Text = remoteTe.StartDate.ToString(Constants.UserFriendlyDateTimeFormat);
            labelRemoteEndDate.Text = remoteTe.EndDate.ToString(Constants.UserFriendlyDateTimeFormat);
            labelRemoteNotification.Text = remoteTe.Notification ? remoteTe.NotificationDate.ToString(Constants.UserFriendlyDateTimeFormat) : "žádné";
        }

        /// <summary>
        /// Hide Remote values.
        /// </summary>
        private void hideRemoteValues()
        {
            labelRemoteName.Text = "";
            labelRemoteDescription.Text = "";
            labelRemoteStartDate.Text = "";
            labelRemoteEndDate.Text = "";
            labelRemoteNotification.Text = "";
        }

        /// <summary>
        /// Local one has been choosen.
        /// </summary>
        /// <param name="sender">Standart sender.</param>
        /// <param name="e">Stnadart Event Arguments</param>
        private void buttonLocalChoose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        /// <summary>
        /// Remote one has been choosen.
        /// </summary>
        /// <param name="sender">Standart sender.</param>
        /// <param name="e">Stnadart Event Arguments</param>
        private void buttonRemoteChoose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        /// <summary>
        /// None of them has been choosen.
        /// </summary>
        /// <param name="sender">Standart sender.</param>
        /// <param name="e">Stnadart Event Arguments</param>
        private void buttonCancelChoose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
