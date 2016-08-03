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
    /// Show Time Events and its datas.
    /// </summary>
    internal partial class TimeEventForm : Form
    {
        /// <summary>
        /// Standart Constructor for easy data set.
        /// </summary>
        /// <param name="te">TimeEvent that will be shown.</param>
        internal TimeEventForm(TimeEvent te)
        {
            InitializeComponent();

            this.Text = "Událost: " + te.Name;

            labelName.Text = te.Name;
            labelDescription.Text = te.Description;
            labelStartDate.Text = te.StartDate.ToString(Constants.UserFriendlyDateTimeFormat);
            labelEndDate.Text = te.EndDate.ToString(Constants.UserFriendlyDateTimeFormat);
            labelNotification.Text = te.Notification ? te.NotificationDate.ToString(Constants.UserFriendlyDateTimeFormat) : "žádné";
        }

        /// <summary>
        /// Close this form due to Clicking OK button.
        /// </summary>
        /// <param name="sender">Standart sender.</param>
        /// <param name="e">Standart event Arguments.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
