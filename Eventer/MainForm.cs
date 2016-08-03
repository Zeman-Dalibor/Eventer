using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// MainForm, the the core of application.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Determine if other events should be invoke, or not.
        /// </summary>
        private bool bypassInvokeEvents;

        /// <summary>
        /// Instance of backend database.
        /// </summary>
        private Database db;

        /// <summary>
        /// Instance of backend Synchronization.
        /// </summary>
        private Synchronization synch;

        /// <summary>
        /// Data for ListBox in Preview page
        /// </summary>
        private Queue<TimeEvent> listboxPreviewData = new Queue<TimeEvent>();
        /// <summary>
        /// Data for ListBoxBoldedDates in Preview page
        /// </summary>
        private Queue<DateTime> previewBoldedDates = new Queue<DateTime>();

        /// <summary>
        /// Data for ListBox in Search page
        /// </summary>
        private Queue<TimeEvent> listboxSearchData = new Queue<TimeEvent>();
        /// <summary>
        /// Data for ListBoxBoldedDates in Search page
        /// </summary>
        private Queue<DateTime> searchBoldedDates = new Queue<DateTime>();

        /// <summary>
        /// Data for ListBox in Synch page
        /// </summary>
        private Queue<SynchronizationDevice> listboxSynchData = new Queue<SynchronizationDevice>();

        /// <summary>
        /// TimeEvents that should be notified.
        /// </summary>
        private Queue<TimeEvent> notificationEvents = new Queue<TimeEvent>();

        /// <summary>
        /// Instance of notification adder. It is adding TimEvents into queue.
        /// </summary>
        private NotificationAdder notificationAdder;

        /// <summary>
        /// LastShown Time Event for click Event purposse.
        /// </summary>
        private TimeEvent lastShownEvent;

        /// <summary>
        /// Preview Worker, it filling preview's data.
        /// </summary>
        private PreviewWorker previewWorker;
        /// <summary>
        /// Search Worker, it filling search's data.
        /// </summary>
        private SearchWorker searchWorker;

        /// <summary>
        /// If is is actually searching Events or not.
        /// </summary>
        private bool searchSearchingEvents = false;
        /// <summary>
        /// If is is actually searching Dates or not.
        /// </summary>
        private bool searchSearchingDates = false;

        /// <summary>
        /// Determine if it should be set value from database.
        /// </summary>
        private bool visibilityCheckBoxBypass = false;

        /// <summary>
        /// Standart Constructor
        /// </summary>
        public MainForm()
        {
            visibilityCheckBoxBypass = false;
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            db = new Database("Database");
            synch = new Synchronization(db);

            notificationAdder = new NotificationAdder(db, notificationEvents);
            previewWorker = new PreviewWorker(db, this, listboxPreviewData, previewBoldedDates);
            searchWorker = new SearchWorker(db, this, listboxSearchData, searchBoldedDates);

            synch.CallbackError += ErrorCallBack;

            bypassInvokeEvents = false;

            // Preview start values
            dateTimePickerPreviewFrom.CustomFormat = Constants.UserFriendlyDateTimeFormat;
            dateTimePickerPreviewTo.CustomFormat = Constants.UserFriendlyDateTimeFormat;

            dateTimePickerPreviewFrom.Value = DateTime.Now;
            dateTimePickerPreviewTo.Value = DateTime.Now.AddDays(7);

            // Add start values
            dateTimePickerAddStartDate.CustomFormat = Constants.UserFriendlyDateTimeFormat;
            dateTimePickerAddEndDate.CustomFormat = Constants.UserFriendlyDateTimeFormat;
            dateTimePickerAddNotificationDate.CustomFormat = Constants.UserFriendlyDateTimeFormat;

            dateTimePickerAddStartDate.Value = DateTime.Now;
            dateTimePickerAddEndDate.Value = DateTime.Now.AddDays(7);

            // Search start values
            dateTimePickerSearchFrom.CustomFormat = Constants.UserFriendlyDateTimeFormat;
            dateTimePickerSearchTo.CustomFormat = Constants.UserFriendlyDateTimeFormat;
            dateTimePickerSearchNotificationDate.CustomFormat = Constants.UserFriendlyDateTimeFormat;

            // Synch start values
            labelSynchInstanceID.Text = synch.InstanceID;
            ApplicationInfo appInfo = db.GetApplicationInfo();
            textBoxSynchAppName.Text = appInfo.Name;
            textBoxSynchGroupID.Text = appInfo.GroupID;
            checkBoxPreviewGlobalNotification.Checked = appInfo.NotifyEvents;

            if (!visibilityCheckBoxBypass)
                checkBoxSynchChangeVisibility.Checked = appInfo.Visible;
        }

        /// <summary>
        /// Do everything necessary before FormClosing.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            previewWorker.RequestStop();
            searchWorker.RequestStop();
            notificationAdder.RequestStop();
            lock (listboxSearchData)
                lock (listboxPreviewData)
                {
                    Monitor.PulseAll(listboxPreviewData);
                    Monitor.PulseAll(listboxSearchData);
                    synch.MainFormClosing(checkBoxPreviewGlobalNotification.Checked);
                    db.CloseDatabase();
                }
            synch.CallbackError -= ErrorCallBack;
            lock (notificationEvents)
                Monitor.PulseAll(notificationEvents);
        }

        /// <summary>
        /// This should be called when Selected Index (selected page) has changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            timerPreviewListbox.Enabled = false;
            timerPreviewBoldedDates.Enabled = false;
            timerSearchListbox.Enabled = false;
            timerSearchBoldedDates.Enabled = false;
            if (tabControl.SelectedIndex == 0)
            {
                if (isPreviewDateValid())
                    previewWorker.PreviewShowEvents(dateTimePickerPreviewFrom.Value, dateTimePickerPreviewTo.Value);
            }
            if (tabControl.SelectedIndex == 2)
            {
                if (searchSearchingEvents)
                    timerSearchListbox.Enabled = true;
                if (searchSearchingDates)
                    timerSearchBoldedDates.Enabled = true;
            }
        }


        #region notification
        //----------------------------------- Notification  ---------------------------------

        /// <summary>
        /// This should be called when Notification short Period Timer tick happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void timerNotification_Tick(object sender, EventArgs e)
        {
            lock (notificationEvents)
            {
                if (notificationEvents.Count == 0)
                    return;
                TimeEvent te = notificationEvents.Peek();
                if (te.NotificationDate < DateTime.Now)
                {
                    notificationEvents.Dequeue();

                    lastShownEvent = te;
                    if (checkBoxPreviewGlobalNotification.Checked)
                        notifyIcon.ShowBalloonTip(10000, "Eventer: " + te.Name,
                            "Čas: " + te.NotificationDate.ToString(Constants.UserFriendlyDateTimeFormat) + Environment.NewLine + "Popis: " + te.Description, ToolTipIcon.Info);
                }
            }
        }

        /// <summary>
        /// This should be called when Notification Long Period Timer tick happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void timerNotificationAdder_Tick(object sender, EventArgs e)
        {
            notificationAdder.AddNewEventsRequest();
        }

        /// <summary>
        /// This should be called when User clicked on BallonTip.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            using (TimeEventForm teForm = new TimeEventForm(lastShownEvent))
            {
                teForm.ShowDialog();
            }
        }

        //----------------------------------- Notification  ---------------------------------
        #endregion notification

        #region Preview Tab
        //----------------------------------- Preview  ---------------------------------

        /// <summary>
        /// This should be called when selected dates in Calendar in Prewiew page has been changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void monthCalendarPreview_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (bypassInvokeEvents)
                return;
            bypassInvokeEvents = true;
            dateTimePickerPreviewFrom.Value = monthCalendarPreview.SelectionStart;
            dateTimePickerPreviewTo.Value = monthCalendarPreview.SelectionEnd;
            bypassInvokeEvents = false;
            previewWorker.PreviewShowEvents(dateTimePickerPreviewFrom.Value, dateTimePickerPreviewTo.Value);
        }

        /// <summary>
        /// This should be called when DateTime picker From value in Prewiew page has been changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void dateTimePickerPreviewFrom_ValueChanged(object sender, EventArgs e)
        {
            if (bypassInvokeEvents)
                return;

            if (!isPreviewDateValid())
                return;

            bypassInvokeEvents = true;
            monthCalendarPreview.SelectionStart = dateTimePickerPreviewFrom.Value;
            monthCalendarPreview.SelectionEnd = dateTimePickerPreviewTo.Value;
            bypassInvokeEvents = false;
            previewWorker.PreviewShowEvents(dateTimePickerPreviewFrom.Value, dateTimePickerPreviewTo.Value);
        }

        /// <summary>
        /// This should be called when DateTime picker To value in Prewiew page has been changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void dateTimePickerPreviewTo_ValueChanged(object sender, EventArgs e)
        {
            if (bypassInvokeEvents)
                return;

            if (!isPreviewDateValid())
                return;

            bypassInvokeEvents = true;
            monthCalendarPreview.SelectionStart = dateTimePickerPreviewFrom.Value;
            monthCalendarPreview.SelectionEnd = dateTimePickerPreviewTo.Value;
            bypassInvokeEvents = false;
            previewWorker.PreviewShowEvents(dateTimePickerPreviewFrom.Value, dateTimePickerPreviewTo.Value);
        }

        /// <summary>
        /// Enable Timer for Preview behavior.
        /// </summary>
        public void EnablePreviewTimer()
        {
            timerPreviewListbox.Enabled = true;
            timerPreviewBoldedDates.Enabled = true;
        }

        /// <summary>
        /// Clear Preview's ListBox and Calendar
        /// </summary>
        public void ClearPreview()
        {
            listBoxPreview.Items.Clear();
            monthCalendarPreview.RemoveAllBoldedDates();
        }

        /// <summary>
        /// Make a loading Msg visible.
        /// </summary>
        public void ShowPreviewLoadingMsg()
        {
            labelPreviewLoading.Visible = true;
        }

        /// <summary>
        /// Check if Date inserted in Preview is valid.
        /// </summary>
        /// <returns>True if valid, False otherwise.</returns>
        private bool isPreviewDateValid()
        {
            if (dateTimePickerPreviewFrom.Value > dateTimePickerPreviewTo.Value)
            {
                resetPreviewSearching();
                labelPreviewErrorMsg.Text = "Zadané datum \"Od:\" je menší než zadané datum \"Do:\"";
                labelPreviewErrorMsg.Visible = true;
                resetPreviewValues();
                return false;
            }
            labelPreviewErrorMsg.Visible = false;
            return true;
        }

        /// <summary>
        /// Reset Preview searching.
        /// </summary>
        private void resetPreviewSearching()
        {
            resetPreviewValues();
            previewWorker.ChangePreviewSearchingData(DateTime.MaxValue, DateTime.MinValue);
            listBoxPreview.Items.Clear();
            monthCalendarPreview.RemoveAllBoldedDates();
            listboxPreviewData.Clear();
            previewBoldedDates.Clear();
        }

        /// <summary>
        /// Reset Previes values connected with selected item. It reset all label related with it.
        /// </summary>
        private void resetPreviewValues()
        {
            labelPreviewName.Text = "-";
            labelPreviewDescription.Text = "-";
            labelPreviewFrom.Text = "-";
            labelPreviewTo.Text = "-";
            labelPreviewNotification.Text = "-";
            buttonPreviewDelEvent.Enabled = false;
            buttonPreviewEditEvent.Enabled = false;
        }

        /// <summary>
        /// This should be called when ListBox selected item in Prewiew page has been changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void listBoxEventsListPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPreview.SelectedIndex == -1)
                resetPreviewValues();
            else
            {
                labelPreviewName.Text = ((TimeEvent)listBoxPreview.SelectedItem).Name;
                labelPreviewDescription.Text = ((TimeEvent)listBoxPreview.SelectedItem).Description;
                labelPreviewFrom.Text = ((TimeEvent)listBoxPreview.SelectedItem).StartDate.ToString(Constants.UserFriendlyDateTimeFormat);
                labelPreviewTo.Text = ((TimeEvent)listBoxPreview.SelectedItem).EndDate.ToString(Constants.UserFriendlyDateTimeFormat);
                labelPreviewNotification.Text = ((TimeEvent)listBoxPreview.SelectedItem).Notification ?
                    ((TimeEvent)listBoxPreview.SelectedItem).NotificationDate.ToString() :
                    "žádné";
                if (listBoxPreview.SelectedItems.Count != 1)
                    buttonPreviewEditEvent.Enabled = false;
                else
                    buttonPreviewEditEvent.Enabled = true;

                buttonPreviewDelEvent.Enabled = true;
            }
        }

        /// <summary>
        /// This should be called when double clicked onto item of Listbox in Prewiew page happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void listBoxEventsListPreview_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxPreview.SelectedIndex == -1)
                return;
            TimeEvent te = (TimeEvent)listBoxPreview.SelectedItem;
            tabControl.SelectedIndex = 1;
            setValuesByTimeEvent(te);
            buttonAddEditEvent.Enabled = true;
            buttonAddDelEvent.Enabled = true;
        }

        /// <summary>
        /// This should be called when Button Edit Event in Prewiew page has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonPreviewEditEvent_Click(object sender, EventArgs e)
        {
            if (listBoxPreview.SelectedItems.Count != 1)
                return;
            TimeEvent te = (TimeEvent)listBoxPreview.SelectedItem;
            tabControl.SelectedIndex = 1;
            setValuesByTimeEvent(te);
            buttonAddEditEvent.Enabled = true;
        }

        /// <summary>
        /// This should be called when Button Delete Event in Prewiew page has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonPreviewDelEvent_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            if (listBoxPreview.SelectedItems.Count != 1)
            {
                dr = MessageBox.Show("Opravdu si přejete smazat " + listBoxPreview.SelectedItems.Count + " událostí ?" + Environment.NewLine
                    + "Tuto akci nelze vzít zpět. Všechny data z označených událostí budou navždy ztraceny.",
                    "Smazat události", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr.Equals(DialogResult.No))
                    return;

                TimeEvent[] eventsToDelete = new TimeEvent[listBoxPreview.SelectedItems.Count];

                listBoxPreview.SelectedItems.CopyTo(eventsToDelete, 0);

                timerPreviewListbox.Enabled = false;
                timerPreviewBoldedDates.Enabled = false;

                using (DeletingForm form = new DeletingForm(db, listBoxPreview.SelectedItems.Count, eventsToDelete))
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    form.ShowDialog();
                    Console.WriteLine(sw.Elapsed);
                }

                timerPreviewListbox.Enabled = true;
                timerPreviewBoldedDates.Enabled = true;

                resetSearchSearching();
                resetPreviewSearching();
                previewBoldedDates.Clear();
                searchBoldedDates.Clear();
                previewWorker.PreviewShowEvents(dateTimePickerPreviewFrom.Value, dateTimePickerPreviewTo.Value);
            }
            else
            {
                TimeEvent te = (TimeEvent)listBoxPreview.SelectedItem;
                dr = MessageBox.Show("Přejete si opravdu smazat událost: " + te.Name + " (ID: " + te.Hash + ") ?", "Smazat událost", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr.Equals(DialogResult.No))
                    return;

                db.DeleteTimeEvent(te.Hash);
                resetSearchSearching();
                resetPreviewSearching();
                previewBoldedDates.Clear();
                searchBoldedDates.Clear();
                previewWorker.PreviewShowEvents(dateTimePickerPreviewFrom.Value, dateTimePickerPreviewTo.Value);
            }
        }

        /// <summary>
        /// This should be called when Button Add Event in Prewiew page has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonPreviewAddEvent_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
            dateTimePickerAddStartDate.Value = dateTimePickerPreviewFrom.Value;
            dateTimePickerAddEndDate.Value = dateTimePickerPreviewTo.Value;
            clearAddForm();
        }

        /// <summary>
        /// This should be called when Timer Preview tick happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void timerPreview_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                lock (listboxPreviewData)
                {
                    Monitor.PulseAll(listboxPreviewData);

                    if (listboxPreviewData.Count == 0)
                        break;

                    TimeEvent te = listboxPreviewData.Dequeue();
                    labelPreviewLoading.Visible = false;
                    if (te == null)
                    {
                        if (!previewWorker.Working && listboxPreviewData.Count == 0)
                        {
                            timerPreviewListbox.Enabled = false;
                        }
                        break;
                    }
                    listBoxPreview.Items.Add(te);
                }
            }
        }

        /// <summary>
        /// This should be called when Timer Preview Bolded dates tick happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void timerPreviewBoldedDates_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                lock (previewBoldedDates)
                {
                    Monitor.PulseAll(previewBoldedDates);
                    if (previewBoldedDates.Count == 0)
                    {
                        break;
                    }
                    DateTime dt = previewBoldedDates.Dequeue();
                    if (dt == DateTime.MinValue)
                    {
                        if (!previewWorker.Working && previewBoldedDates.Count == 0)
                        {
                            timerPreviewBoldedDates.Enabled = false;
                            monthCalendarPreview.UpdateBoldedDates();
                        }
                        break;
                    }
                    monthCalendarPreview.AddBoldedDate(dt);
                }
            }
        }
        //----------------------------------- End of Preview  ---------------------------------
        #endregion

        #region Add/Edit Tab
        //----------------------------------- Add/Edit Tab  ---------------------------------

        /// <summary>
        /// Set Add values by the given TimeEvent. It set all textBoxes and labels by that event.
        /// </summary>
        /// <param name="te">Event with datas, that should be showed as values.</param>
        private void setValuesByTimeEvent(TimeEvent te)
        {
            labelAddHash.Text = te.Hash;
            textBoxAddName.Text = te.Name;
            textBoxAddDescription.Text = te.Description;
            dateTimePickerAddStartDate.Value = te.StartDate;
            dateTimePickerAddEndDate.Value = te.EndDate;
            checkBoxAddNotification.Checked = te.Notification;
            if (te.NotificationDate == DateTime.MinValue)
                dateTimePickerAddNotificationDate.Value = DateTime.Now;
            else
                dateTimePickerAddNotificationDate.Value = te.NotificationDate;
        }

        /// <summary>
        /// Check if is Add's Date valid. Used in after button click check.
        /// </summary>
        /// <returns>True if valid, False otherwise.</returns>
        private bool isAddingValuesValid()
        {
            // --- Errors ---
            if (textBoxAddName.Text == "")
            {
                MessageBox.Show("Nelze přidat událost s prázdným jménem.", "Neplatné parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (SupportMethods.ContainIllegalChars(textBoxAddDescription.Text))
            {
                MessageBox.Show("Popis události nesmí obsahovat znaky: " + Constants.IllegalCharsInString, "Neplatné parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (SupportMethods.ContainIllegalChars(textBoxAddName.Text))
            {
                MessageBox.Show("Jméno události nesmí obsahovat znaky: " + Constants.IllegalCharsInString, "Neplatné parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dateTimePickerAddStartDate.Value > dateTimePickerAddEndDate.Value)
            {
                MessageBox.Show("Koncové datum události nastane dříve než počáteční datum události.", "Neplatné parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // --- Questions ---
            if (dateTimePickerAddEndDate.Value < DateTime.Now)
            {
                DialogResult dr = MessageBox.Show("Tato událost již zkončila. Chcete i přesto pokračovat?", "Staré datum", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.Equals(DialogResult.No))
                    return false;
            }
            else if (dateTimePickerAddStartDate.Value < DateTime.Now)
            {
                DialogResult dr = MessageBox.Show("Tato událost již začala. Chcete i přesto pokračovat?", "Staré datum", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.Equals(DialogResult.No))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check if Add's Date is Valid.
        /// </summary>
        /// <returns>True if valid, False otherwise.</returns>
        private bool isAddValidDateAndValues()
        {
            checkAddValidValues();
            if (dateTimePickerAddStartDate.Value > dateTimePickerAddEndDate.Value)
                return false;
            return true;
        }

        /// <summary>
        /// Check if Add's Date is Valid, used in time. If text is changed.
        /// </summary>
        private void checkAddValidValues()
        {
            buttonAddDelEvent.Enabled = false;
            if (dateTimePickerAddStartDate.Value > dateTimePickerAddEndDate.Value)
            {
                labelAddNotifyMsg.Text = "Koncové datum události nastane dříve než počáteční.";
            }
            else if (SupportMethods.ContainIllegalChars(textBoxAddName.Text))
            {
                labelAddNotifyMsg.Text = "Jméno události nesmí obsahovat znaky: " + Constants.IllegalCharsInString;
            }
            else if (SupportMethods.ContainIllegalChars(textBoxAddDescription.Text))
            {
                labelAddNotifyMsg.Text = "Popis události nesmí obsahovat znaky: " + Constants.IllegalCharsInString;
            }
            else
                labelAddNotifyMsg.Text = "";
        }

        /// <summary>
        /// Clear all text inserted in textBoxes in Add Page. Reset all values.
        /// </summary>
        private void clearAddForm()
        {
            labelAddHash.Text = "-";
            textBoxAddName.Text = "";
            textBoxAddDescription.Text = "";
            dateTimePickerAddStartDate.Value = DateTime.Now;
            dateTimePickerAddEndDate.Value = DateTime.Now.AddDays(7);
            checkBoxAddNotification.Checked = false;
            dateTimePickerAddNotificationDate.Value = DateTime.Now;
            buttonAddDelEvent.Enabled = false;
            buttonAddEditEvent.Enabled = false;
        }

        /// <summary>
        /// This should be called when checkbox Notification in Add page has been changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void checkBoxAddNotification_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerAddNotificationDate.Enabled = checkBoxAddNotification.Checked;
            buttonAddDelEvent.Enabled = false;
        }

        /// <summary>
        /// This should be called when Calendar selected Date in Add page has been changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void monthCalendarAdd_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (bypassInvokeEvents)
                return;
            bypassInvokeEvents = true;
            dateTimePickerAddStartDate.Value = monthCalendarAdd.SelectionStart;
            dateTimePickerAddEndDate.Value = monthCalendarAdd.SelectionEnd;
            bypassInvokeEvents = false;
        }

        /// <summary>
        /// This should be called when DateTime picker StartDate in Add page has been changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void dateTimePickerAddStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (bypassInvokeEvents)
                return;

            if (!isAddValidDateAndValues())
                return;

            bypassInvokeEvents = true;
            monthCalendarAdd.SelectionStart = dateTimePickerAddStartDate.Value;
            monthCalendarAdd.SelectionEnd = dateTimePickerAddEndDate.Value;
            bypassInvokeEvents = false;
        }

        /// <summary>
        /// This should be called when DateTime picker EndDate in Add page has been changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void dateTimePickerAddEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (bypassInvokeEvents)
                return;

            if (!isAddValidDateAndValues())
                return;

            bypassInvokeEvents = true;
            monthCalendarAdd.SelectionStart = dateTimePickerAddStartDate.Value;
            monthCalendarAdd.SelectionEnd = dateTimePickerAddEndDate.Value;
            bypassInvokeEvents = false;
        }

        /// <summary>
        /// This should be called when text box Name in Add page has been changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void textBoxAddName_TextChanged(object sender, EventArgs e)
        {
            checkAddValidValues();
        }

        /// <summary>
        /// This should be called when text box Description in Add page has been changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void textBoxAddDescription_TextChanged(object sender, EventArgs e)
        {
            checkAddValidValues();
        }

        /// <summary>
        /// This should be called when Add button in Add page has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonAddAddEvent_Click(object sender, EventArgs e)
        {
            if (!isAddingValuesValid())
                return;
            TimeEvent te = TimeEvent.CreateTimeEvent(
                textBoxAddName.Text,
                textBoxAddDescription.Text,
                dateTimePickerAddStartDate.Value,
                dateTimePickerAddEndDate.Value,
                checkBoxAddNotification.Checked,
                dateTimePickerAddNotificationDate.Value
                );
            while (db.ExistTimeEventWithHash(te.Hash))
                te = TimeEvent.CreateTimeEventWithDuplicitHash(te);
            db.InsertTimeEvent(te);
            labelAddHash.Text = te.Hash;

            resetPreviewSearching();
            resetSearchSearching();
            clearAddForm();
        }

        /// <summary>
        /// This should be called when Edit button in Add page has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonAddEditEvent_Click(object sender, EventArgs e)
        {
            if (!isAddingValuesValid())
                return;
            db.UpdateTimeEvent(
                labelAddHash.Text,
                textBoxAddName.Text,
                textBoxAddDescription.Text,
                dateTimePickerAddStartDate.Value,
                dateTimePickerAddEndDate.Value,
                checkBoxAddNotification.Checked,
                dateTimePickerAddNotificationDate.Value
                );
            resetPreviewSearching();
            resetSearchSearching();
            MessageBox.Show("Událost editována.");
        }

        /// <summary>
        /// This should be called when Del button in Add page has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonAddDelEvent_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Určitě si přejete smazat událost: " + textBoxAddName.Text + " (ID: " + labelAddHash.Text + ") ?", "Smazat událost", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr.Equals(DialogResult.No))
                return;
            db.DeleteTimeEvent(labelAddHash.Text);
            resetSearchSearching();
            resetPreviewSearching();
            previewBoldedDates.Clear();
            searchBoldedDates.Clear();
            clearAddForm();
        }

        //----------------------------------- End of Add/Edit Tab  ---------------------------------
        #endregion

        #region Search Tab
        //----------------------------------- Search Tab  ---------------------------------

        /// <summary>
        /// This should be called when new or another data has been selected.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void monthCalendarSearch_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (bypassInvokeEvents)
                return;
            bypassInvokeEvents = true;
            dateTimePickerSearchFrom.Value = monthCalendarSearch.SelectionStart;
            dateTimePickerSearchTo.Value = monthCalendarSearch.SelectionEnd;
            bypassInvokeEvents = false;
        }

        /// <summary>
        /// This should be called when selected DateTime picker Search From changed its value.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void dateTimePickerSearchFrom_ValueChanged(object sender, EventArgs e)
        {
            if (bypassInvokeEvents)
                return;

            if (!isSearchValidDate())
                return;

            bypassInvokeEvents = true;
            monthCalendarSearch.SelectionStart = dateTimePickerSearchFrom.Value;
            monthCalendarSearch.SelectionEnd = dateTimePickerSearchTo.Value;
            bypassInvokeEvents = false;
        }

        /// <summary>
        /// This should be called when selected DateTime picker Search To changed its value.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void dateTimePickerSearchTo_ValueChanged(object sender, EventArgs e)
        {
            if (bypassInvokeEvents)
                return;

            if (!isSearchValidDate())
                return;

            bypassInvokeEvents = true;
            monthCalendarSearch.SelectionStart = dateTimePickerSearchFrom.Value;
            monthCalendarSearch.SelectionEnd = dateTimePickerSearchTo.Value;
            bypassInvokeEvents = false;
        }

        /// <summary>
        /// This should be called when selected item changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void listBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSearch.SelectedIndex == -1)
            {
                ClearSearchValues();
            }
            else
            {
                labelSearchName.Text = ((TimeEvent)listBoxSearch.SelectedItem).Name;
                labelSearchDescription.Text = ((TimeEvent)listBoxSearch.SelectedItem).Description;
                labelSearchFrom.Text = ((TimeEvent)listBoxSearch.SelectedItem).StartDate.ToString(Constants.UserFriendlyDateTimeFormat);
                labelSearchTo.Text = ((TimeEvent)listBoxSearch.SelectedItem).EndDate.ToString(Constants.UserFriendlyDateTimeFormat);
                labelSearchNotification.Text = ((TimeEvent)listBoxSearch.SelectedItem).Notification ?
                    ((TimeEvent)listBoxSearch.SelectedItem).NotificationDate.ToString() :
                    "žádné";
            }
        }

        /// <summary>
        /// This should be called when Mouse Double clicked happened on ListBox item.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void listBoxSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxSearch.SelectedIndex == -1)
                return;
            TimeEvent te = (TimeEvent)listBoxSearch.SelectedItem;
            tabControl.SelectedIndex = 1;
            setValuesByTimeEvent(te);
            buttonAddEditEvent.Enabled = true;
            buttonAddDelEvent.Enabled = true;
        }

        /// <summary>
        /// Check if Search's Dates are valid.
        /// </summary>
        /// <returns>True if valid, False otherwise.</returns>
        private bool isSearchValidDate()
        {
            if (dateTimePickerSearchFrom.Value > dateTimePickerSearchTo.Value)
                return false;
            return true;
        }

        /// <summary>
        /// Check if search values are valid.
        /// </summary>
        /// <returns>True if valid, False otherwise.</returns>
        private bool isSearchingValuesValid()
        {
            if (SupportMethods.ContainIllegalChars(textBoxSearchName.Text))
            {
                MessageBox.Show("Jméno události nesmí obsahovat znaky: " + Constants.IllegalCharsInString, "Neplatné parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (SupportMethods.ContainIllegalChars(textBoxSearchDescription.Text))
            {
                MessageBox.Show("Popis události nesmí obsahovat znaky: " + Constants.IllegalCharsInString, "Neplatné parametry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// This should be called when Button Search has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            labelSearchLoading.Visible = true;
            buttonSearch.Enabled = false;
            resetSearchSearching();
            searchSearchingEvents = true;
            searchSearchingDates = true;
            searchWorker.SearchShowEvents(
                 textBoxSearchName.Text,
                 textBoxSearchDescription.Text,
                 dateTimePickerSearchFrom.Checked ? dateTimePickerSearchFrom.Value : DateTime.MinValue,
                 dateTimePickerSearchTo.Checked ? dateTimePickerSearchTo.Value : DateTime.MinValue,
                 dateTimePickerSearchNotificationDate.Checked ? dateTimePickerSearchNotificationDate.Value : DateTime.MinValue
                 );
        }

        /// <summary>
        /// Enable Search's Timers for his job.
        /// </summary>
        public void EnableSearchTimer()
        {
            timerSearchListbox.Enabled = true;
            timerSearchBoldedDates.Enabled = true;
        }

        /// <summary>
        /// Clear Search's items of listBox and remove all bolded dates.
        /// </summary>
        public void ClearSearchListBox()
        {
            listBoxSearch.Items.Clear();
            monthCalendarSearch.RemoveAllBoldedDates();
        }

        /// <summary>
        /// Clear and reset all values and label in Search page.
        /// </summary>
        private void ClearSearchValues()
        {
            labelSearchName.Text = "-";
            labelSearchDescription.Text = "-";
            labelSearchFrom.Text = "-";
            labelSearchTo.Text = "-";
            labelSearchNotification.Text = "-";
        }

        /// <summary>
        /// Reset Search's searching. It stop searching events.
        /// </summary>
        private void resetSearchSearching()
        {
            searchWorker.TemporaryStopRequest();
            listBoxSearch.Items.Clear();
            monthCalendarSearch.RemoveAllBoldedDates();

            lock (listboxSearchData)
                listboxSearchData.Clear();

            lock (searchBoldedDates)
                searchBoldedDates.Clear();

            ClearSearchValues();
        }

        /// <summary>
        /// This should be called when Timer Search tick happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void timerSearch_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                lock (listboxSearchData)
                {
                    Monitor.PulseAll(listboxSearchData);
                    if (listboxSearchData.Count == 0)
                        break;

                    TimeEvent te = listboxSearchData.Dequeue();
                    labelSearchLoading.Visible = false;
                    buttonSearch.Enabled = true;
                    if (te == null)
                    {
                        if (!searchWorker.Working && listboxSearchData.Count == 0)
                        {
                            searchSearchingEvents = false;
                            timerSearchListbox.Enabled = false;
                        }
                        break;
                    }
                    listBoxSearch.Items.Add(te);
                }
            }
        }

        /// <summary>
        /// This should be called when Timer Search Bolded Dates tick happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void timerSearchBoldedDates_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                lock (searchBoldedDates)
                {
                    Monitor.PulseAll(searchBoldedDates);
                    if (searchBoldedDates.Count == 0)
                        break;
                    DateTime dt = searchBoldedDates.Dequeue();
                    if (dt == DateTime.MinValue)
                    {
                        if (!searchWorker.Working && listboxSearchData.Count == 0)
                        {
                            timerSearchBoldedDates.Enabled = false;
                            searchSearchingDates = false;
                            monthCalendarSearch.UpdateBoldedDates();
                        }
                        break;
                    }
                    monthCalendarSearch.AddBoldedDate(dt);
                }
            }
        }

        //----------------------------------- End of Search Tab  ---------------------------------
        #endregion

        #region Synchronization Tab
        //----------------------------------- End of Synchronization Tab  ---------------------------------

        /// <summary>
        /// This should be called when Synch Find Device has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonSynchFindDevices_Click(object sender, EventArgs e)
        {
            buttonSynchFindDevices.Enabled = false;
            timerSynchDevices.Enabled = true;
            listboxSynchData.Clear();
            listBoxSynchDevices.Items.Clear();
            synch.FindDevices(listboxSynchData);
        }


        /// <summary>
        /// Reset Synch Values like labels, button enabled or warning visibility
        /// </summary>
        private void resetSynchValues()
        {
            labelSynchSelectedAllow.Text = "-";
            labelSynchSelectedLastSynchDate.Text = "-";
            labelSynchSelectedDeviceID.Text = "-";
            buttonSynchAllow.Text = "-";
            buttonSynchAllow.Enabled = false;
            buttonSynchSynchronize.Enabled = false;
            labelSynchDifferentGroupID.Visible = false;
        }

        /// <summary>
        /// This should be called when selection of item has changed or its all unselected.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void listBoxSearchDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = ((SynchronizationDevice)listBoxSynchDevices.SelectedItem);
            if (listBoxSynchDevices.SelectedIndex == -1)
            {
                resetSynchValues();
            }
            else
            {
                if (item.GroupID == synch.GroupID)
                    labelSynchDifferentGroupID.Visible = false;
                else
                    labelSynchDifferentGroupID.Visible = true;

                labelSynchSelectedAllow.Text = item.DeviceID;
                labelSynchSelectedLastSynchDate.Text = item.LastSynchDate == DateTime.MinValue ? "Nikdy" : item.LastSynchDate.ToString(Constants.UserFriendlyDateTimeFormat);
                labelSynchSelectedDeviceID.Text = item.Allow ? "Ano" : "Ne";
                buttonSynchAllow.Text = item.Allow ? "Zakázat Synchronizaci" : "Povolit Synchronizaci";
                buttonSynchAllow.Enabled = true;
                buttonSynchSynchronize.Enabled = true;
            }
        }

        /// <summary>
        /// This should be called checkBox ChangeVisibility in Synch page has changed its CheckState.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void checkBoxSynchChangeVisibility_CheckedChanged(object sender, EventArgs e)
        {
            synch.Responding = checkBoxSynchChangeVisibility.Checked;
        }

        /// <summary>
        /// This should be called when Synch Device Timer tick happened.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void timerSynchDevices_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                lock (listboxSynchData)
                {
                    Monitor.PulseAll(listboxSynchData);
                    if (listboxSynchData.Count == 0)
                        break;
                    SynchronizationDevice str = listboxSynchData.Dequeue();
                    if (str == null)
                    {
                        timerSynchDevices.Enabled = false;
                        buttonSynchFindDevices.Enabled = true;
                        break;
                    }
                    listBoxSynchDevices.Items.Add(str);
                }
            }
        }

        /// <summary>
        /// This should be called when Synchronization Application Name and its text has changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void textBoxSynchAppName_TextChanged(object sender, EventArgs e)
        {
            string newText = textBoxSynchAppName.Text;
            if (newText.Contains("=") || newText.Contains(",") || SupportMethods.ContainIllegalChars(newText))
                textBoxSynchAppName.Text = synch.Name;
            else
                synch.Name = textBoxSynchAppName.Text;
        }

        /// <summary>
        /// This should be called when Synchronization Group ID and its text has changed.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void textBoxSynchGroupID_TextChanged(object sender, EventArgs e)
        {
            string newText = textBoxSynchGroupID.Text;
            if (newText.Contains("=") || newText.Contains(",") || SupportMethods.ContainIllegalChars(newText))
                textBoxSynchGroupID.Text = synch.GroupID;
            else
                synch.SetEventsGroupID(textBoxSynchGroupID.Text);
        }

        /// <summary>
        /// Callback for Synchronization Errors. It react on error, that is related to it.
        /// </summary>
        /// <param name="type">Type of error that occurs.</param>
        internal void ErrorCallBack(Synchronization.ErrorType type)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { ErrorCallBack(type); });
                return;
            }
            else
            {
                switch (type)
                {
                    case Synchronization.ErrorType.AllreadyBindUDPPort:
                        visibilityCheckBoxBypass = true;
                        checkBoxSynchChangeVisibility.Checked = false;
                        MessageBox.Show("Aktualně již na tomto počítači běží naslouchání UPD s portem: " + Constants.UDPPort);
                        break;
                    case Synchronization.ErrorType.AllreadyBindTCPPort:
                        MessageBox.Show("Aktualně již na tomto počítači běží TCP server s portem: " + Constants.TCPPort);
                        break;
                }
            }
        }

        /// <summary>
        /// This should be called when button Allow has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonSynchAllow_Click(object sender, EventArgs e)
        {
            if ((SynchronizationDevice)listBoxSynchDevices.SelectedItem == null ||
               listBoxSynchDevices.SelectedIndex == -1)
            {
                resetSynchValues();
                return;
            }
            SynchronizationDevice device = (SynchronizationDevice)listBoxSynchDevices.SelectedItem;
            device.Allow = !device.Allow;
            db.UpdateSynchronizationDevice(device);
            refreshSynchDevicessListBox();
        }

        /// <summary>
        /// Refresh items in ListBox, by resetting DisplayMember.
        /// </summary>
        private void refreshSynchDevicessListBox()
        {
            string display = listBoxSynchDevices.DisplayMember;
            listBoxSynchDevices.DisplayMember = "";
            listBoxSynchDevices.DisplayMember = display;
        }

        /// <summary>
        /// This should be called when Synchronization button has been clicked.
        /// </summary>
        /// <param name="sender">Standart sender of event.</param>
        /// <param name="e">Standart EventArgs of event.</param>
        private void buttonSynchSynchronize_Click(object sender, EventArgs e)
        {
            if ((SynchronizationDevice)listBoxSynchDevices.SelectedItem == null ||
                listBoxSynchDevices.SelectedIndex == -1)
                return;
            synch.SynchronizeWith((SynchronizationDevice)listBoxSynchDevices.SelectedItem);
            refreshSynchDevicessListBox();
            resetPreviewSearching();
        }


        //----------------------------------- End of Synchronization Tab  ---------------------------------
        #endregion
    }
}
