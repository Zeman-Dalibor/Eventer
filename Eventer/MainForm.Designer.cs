/*
 * Created by SharpDevelop.
 * User: admin
 * Date: 5. 7. 2016
 * Time: 15:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Eventer
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timerPreviewListbox = new System.Windows.Forms.Timer(this.components);
            this.tabPageSeach = new System.Windows.Forms.TabPage();
            this.labelSearchLoading = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.labelSearchDescription = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelSearchTo = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelSearchNotification = new System.Windows.Forms.Label();
            this.labelSearchFrom = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.labelSearchName = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.listBoxSearch = new System.Windows.Forms.ListBox();
            this.textBoxSearchDescription = new System.Windows.Forms.TextBox();
            this.textBoxSearchName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePickerSearchTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerSearchNotificationDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerSearchFrom = new System.Windows.Forms.DateTimePicker();
            this.monthCalendarSearch = new System.Windows.Forms.MonthCalendar();
            this.tabPageAdd = new System.Windows.Forms.TabPage();
            this.buttonAddDelEvent = new System.Windows.Forms.Button();
            this.buttonAddEditEvent = new System.Windows.Forms.Button();
            this.buttonAddAddEvent = new System.Windows.Forms.Button();
            this.checkBoxAddNotification = new System.Windows.Forms.CheckBox();
            this.textBoxAddDescription = new System.Windows.Forms.TextBox();
            this.textBoxAddName = new System.Windows.Forms.TextBox();
            this.labelAddNotifyMsg = new System.Windows.Forms.Label();
            this.labelAddNotificationDate = new System.Windows.Forms.Label();
            this.labelAddDescription = new System.Windows.Forms.Label();
            this.labelAddTo = new System.Windows.Forms.Label();
            this.labelAddFrom = new System.Windows.Forms.Label();
            this.labelAddHash = new System.Windows.Forms.Label();
            this.labelAddHashLabel = new System.Windows.Forms.Label();
            this.labelAddNameLabel = new System.Windows.Forms.Label();
            this.monthCalendarAdd = new System.Windows.Forms.MonthCalendar();
            this.dateTimePickerAddEndDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerAddNotificationDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerAddStartDate = new System.Windows.Forms.DateTimePicker();
            this.tabPagePreview = new System.Windows.Forms.TabPage();
            this.labelPreviewLoading = new System.Windows.Forms.Label();
            this.labelPreviewErrorMsg = new System.Windows.Forms.Label();
            this.checkBoxPreviewGlobalNotification = new System.Windows.Forms.CheckBox();
            this.buttonPreviewAddEvent = new System.Windows.Forms.Button();
            this.buttonPreviewEditEvent = new System.Windows.Forms.Button();
            this.buttonPreviewDelEvent = new System.Windows.Forms.Button();
            this.labelNotificationLabel = new System.Windows.Forms.Label();
            this.labelPreviewDescription = new System.Windows.Forms.Label();
            this.labelDescLable = new System.Windows.Forms.Label();
            this.labelPreviewTo = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelToLabel = new System.Windows.Forms.Label();
            this.labelPreviewNotification = new System.Windows.Forms.Label();
            this.labelPreviewFrom = new System.Windows.Forms.Label();
            this.labelFromLabel = new System.Windows.Forms.Label();
            this.labelPreviewName = new System.Windows.Forms.Label();
            this.labelNameLabel = new System.Windows.Forms.Label();
            this.monthCalendarPreview = new System.Windows.Forms.MonthCalendar();
            this.dateTimePickerPreviewTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerPreviewFrom = new System.Windows.Forms.DateTimePicker();
            this.listBoxPreview = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSynchronization = new System.Windows.Forms.TabPage();
            this.labelSynchDifferentGroupID = new System.Windows.Forms.Label();
            this.checkBoxSynchChangeVisibility = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.labelSynchSelectedLastSynchDate = new System.Windows.Forms.Label();
            this.labelSynchSelectedAllow = new System.Windows.Forms.Label();
            this.labelSynchSelectedDeviceID = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonSynchFindDevices = new System.Windows.Forms.Button();
            this.buttonSynchSynchronize = new System.Windows.Forms.Button();
            this.buttonSynchAllow = new System.Windows.Forms.Button();
            this.textBoxSynchAppName = new System.Windows.Forms.TextBox();
            this.textBoxSynchGroupID = new System.Windows.Forms.TextBox();
            this.listBoxSynchDevices = new System.Windows.Forms.ListBox();
            this.labelSynchInstanceID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.timerSearchListbox = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timerPreviewBoldedDates = new System.Windows.Forms.Timer(this.components);
            this.timerSynchDevices = new System.Windows.Forms.Timer(this.components);
            this.timerSearchBoldedDates = new System.Windows.Forms.Timer(this.components);
            this.timerNotification = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerNotificationAdder = new System.Windows.Forms.Timer(this.components);
            this.tabPageSeach.SuspendLayout();
            this.tabPageAdd.SuspendLayout();
            this.tabPagePreview.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageSynchronization.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerPreviewListbox
            // 
            this.timerPreviewListbox.Enabled = true;
            this.timerPreviewListbox.Tick += new System.EventHandler(this.timerPreview_Tick);
            // 
            // tabPageSeach
            // 
            this.tabPageSeach.Controls.Add(this.labelSearchLoading);
            this.tabPageSeach.Controls.Add(this.buttonSearch);
            this.tabPageSeach.Controls.Add(this.label7);
            this.tabPageSeach.Controls.Add(this.labelSearchDescription);
            this.tabPageSeach.Controls.Add(this.label10);
            this.tabPageSeach.Controls.Add(this.labelSearchTo);
            this.tabPageSeach.Controls.Add(this.label13);
            this.tabPageSeach.Controls.Add(this.labelSearchNotification);
            this.tabPageSeach.Controls.Add(this.labelSearchFrom);
            this.tabPageSeach.Controls.Add(this.label16);
            this.tabPageSeach.Controls.Add(this.labelSearchName);
            this.tabPageSeach.Controls.Add(this.label18);
            this.tabPageSeach.Controls.Add(this.listBoxSearch);
            this.tabPageSeach.Controls.Add(this.textBoxSearchDescription);
            this.tabPageSeach.Controls.Add(this.textBoxSearchName);
            this.tabPageSeach.Controls.Add(this.label3);
            this.tabPageSeach.Controls.Add(this.label4);
            this.tabPageSeach.Controls.Add(this.label5);
            this.tabPageSeach.Controls.Add(this.label6);
            this.tabPageSeach.Controls.Add(this.label9);
            this.tabPageSeach.Controls.Add(this.dateTimePickerSearchTo);
            this.tabPageSeach.Controls.Add(this.dateTimePickerSearchNotificationDate);
            this.tabPageSeach.Controls.Add(this.dateTimePickerSearchFrom);
            this.tabPageSeach.Controls.Add(this.monthCalendarSearch);
            this.tabPageSeach.Location = new System.Drawing.Point(4, 22);
            this.tabPageSeach.Name = "tabPageSeach";
            this.tabPageSeach.Size = new System.Drawing.Size(700, 595);
            this.tabPageSeach.TabIndex = 3;
            this.tabPageSeach.Text = "Hledání událostí:";
            // 
            // labelSearchLoading
            // 
            this.labelSearchLoading.AutoSize = true;
            this.labelSearchLoading.ForeColor = System.Drawing.Color.DimGray;
            this.labelSearchLoading.Location = new System.Drawing.Point(409, 280);
            this.labelSearchLoading.Name = "labelSearchLoading";
            this.labelSearchLoading.Size = new System.Drawing.Size(107, 13);
            this.labelSearchLoading.TabIndex = 60;
            this.labelSearchLoading.Text = "Probíhá načítání . . .";
            this.labelSearchLoading.Visible = false;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(522, 275);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(170, 23);
            this.buttonSearch.TabIndex = 59;
            this.buttonSearch.Text = "&Hledat";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 572);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Upozornění:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSearchDescription
            // 
            this.labelSearchDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSearchDescription.Location = new System.Drawing.Point(80, 492);
            this.labelSearchDescription.Margin = new System.Windows.Forms.Padding(3);
            this.labelSearchDescription.Name = "labelSearchDescription";
            this.labelSearchDescription.Size = new System.Drawing.Size(612, 55);
            this.labelSearchDescription.TabIndex = 57;
            this.labelSearchDescription.Text = "-";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(38, 492);
            this.label10.Margin = new System.Windows.Forms.Padding(3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 49;
            this.label10.Text = "Popis:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSearchTo
            // 
            this.labelSearchTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSearchTo.AutoSize = true;
            this.labelSearchTo.Location = new System.Drawing.Point(259, 553);
            this.labelSearchTo.Margin = new System.Windows.Forms.Padding(3);
            this.labelSearchTo.Name = "labelSearchTo";
            this.labelSearchTo.Size = new System.Drawing.Size(10, 13);
            this.labelSearchTo.TabIndex = 50;
            this.labelSearchTo.Text = "-";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(229, 553);
            this.label13.Margin = new System.Windows.Forms.Padding(3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 51;
            this.label13.Text = "Do:";
            // 
            // labelSearchNotification
            // 
            this.labelSearchNotification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSearchNotification.AutoSize = true;
            this.labelSearchNotification.Location = new System.Drawing.Point(80, 572);
            this.labelSearchNotification.Margin = new System.Windows.Forms.Padding(3);
            this.labelSearchNotification.Name = "labelSearchNotification";
            this.labelSearchNotification.Size = new System.Drawing.Size(10, 13);
            this.labelSearchNotification.TabIndex = 52;
            this.labelSearchNotification.Text = "-";
            // 
            // labelSearchFrom
            // 
            this.labelSearchFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSearchFrom.AutoSize = true;
            this.labelSearchFrom.Location = new System.Drawing.Point(80, 553);
            this.labelSearchFrom.Margin = new System.Windows.Forms.Padding(3);
            this.labelSearchFrom.Name = "labelSearchFrom";
            this.labelSearchFrom.Size = new System.Drawing.Size(10, 13);
            this.labelSearchFrom.TabIndex = 53;
            this.labelSearchFrom.Text = "-";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(50, 553);
            this.label16.Margin = new System.Windows.Forms.Padding(3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 13);
            this.label16.TabIndex = 54;
            this.label16.Text = "Od:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSearchName
            // 
            this.labelSearchName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSearchName.Location = new System.Drawing.Point(80, 473);
            this.labelSearchName.Margin = new System.Windows.Forms.Padding(3);
            this.labelSearchName.Name = "labelSearchName";
            this.labelSearchName.Size = new System.Drawing.Size(612, 13);
            this.labelSearchName.TabIndex = 55;
            this.labelSearchName.Text = "-";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(33, 473);
            this.label18.Margin = new System.Windows.Forms.Padding(3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 56;
            this.label18.Text = "Jméno:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listBoxSearch
            // 
            this.listBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSearch.DisplayMember = "ListBoxString";
            this.listBoxSearch.FormattingEnabled = true;
            this.listBoxSearch.HorizontalScrollbar = true;
            this.listBoxSearch.Location = new System.Drawing.Point(8, 301);
            this.listBoxSearch.Name = "listBoxSearch";
            this.listBoxSearch.Size = new System.Drawing.Size(684, 160);
            this.listBoxSearch.TabIndex = 48;
            this.listBoxSearch.SelectedIndexChanged += new System.EventHandler(this.listBoxSearch_SelectedIndexChanged);
            this.listBoxSearch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxSearch_MouseDoubleClick);
            // 
            // textBoxSearchDescription
            // 
            this.textBoxSearchDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearchDescription.Location = new System.Drawing.Point(80, 225);
            this.textBoxSearchDescription.Multiline = true;
            this.textBoxSearchDescription.Name = "textBoxSearchDescription";
            this.textBoxSearchDescription.Size = new System.Drawing.Size(612, 44);
            this.textBoxSearchDescription.TabIndex = 34;
            this.toolTip.SetToolTip(this.textBoxSearchDescription, "Pokud položka zůstane prázdná, nebude na ní brán zřetel.");
            // 
            // textBoxSearchName
            // 
            this.textBoxSearchName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearchName.Location = new System.Drawing.Point(80, 199);
            this.textBoxSearchName.Name = "textBoxSearchName";
            this.textBoxSearchName.Size = new System.Drawing.Size(612, 20);
            this.textBoxSearchName.TabIndex = 33;
            this.toolTip.SetToolTip(this.textBoxSearchName, "Pokud položka zůstane prázdná, nebude na ní brán zřetel.");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 281);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "Upozornění:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 228);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Popis:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(366, 180);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Do:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 180);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "Od:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 202);
            this.label9.Margin = new System.Windows.Forms.Padding(3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 46;
            this.label9.Text = "Jméno:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePickerSearchTo
            // 
            this.dateTimePickerSearchTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerSearchTo.Checked = false;
            this.dateTimePickerSearchTo.CustomFormat = "--";
            this.dateTimePickerSearchTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerSearchTo.Location = new System.Drawing.Point(396, 174);
            this.dateTimePickerSearchTo.Name = "dateTimePickerSearchTo";
            this.dateTimePickerSearchTo.ShowCheckBox = true;
            this.dateTimePickerSearchTo.Size = new System.Drawing.Size(212, 20);
            this.dateTimePickerSearchTo.TabIndex = 36;
            this.dateTimePickerSearchTo.ValueChanged += new System.EventHandler(this.dateTimePickerSearchTo_ValueChanged);
            // 
            // dateTimePickerSearchNotificationDate
            // 
            this.dateTimePickerSearchNotificationDate.Checked = false;
            this.dateTimePickerSearchNotificationDate.CustomFormat = "--";
            this.dateTimePickerSearchNotificationDate.Location = new System.Drawing.Point(80, 275);
            this.dateTimePickerSearchNotificationDate.Name = "dateTimePickerSearchNotificationDate";
            this.dateTimePickerSearchNotificationDate.ShowCheckBox = true;
            this.dateTimePickerSearchNotificationDate.Size = new System.Drawing.Size(212, 20);
            this.dateTimePickerSearchNotificationDate.TabIndex = 38;
            // 
            // dateTimePickerSearchFrom
            // 
            this.dateTimePickerSearchFrom.Checked = false;
            this.dateTimePickerSearchFrom.CustomFormat = "--";
            this.dateTimePickerSearchFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerSearchFrom.Location = new System.Drawing.Point(80, 174);
            this.dateTimePickerSearchFrom.Name = "dateTimePickerSearchFrom";
            this.dateTimePickerSearchFrom.ShowCheckBox = true;
            this.dateTimePickerSearchFrom.Size = new System.Drawing.Size(212, 20);
            this.dateTimePickerSearchFrom.TabIndex = 35;
            this.dateTimePickerSearchFrom.ValueChanged += new System.EventHandler(this.dateTimePickerSearchFrom_ValueChanged);
            // 
            // monthCalendarSearch
            // 
            this.monthCalendarSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monthCalendarSearch.CalendarDimensions = new System.Drawing.Size(4, 1);
            this.monthCalendarSearch.Location = new System.Drawing.Point(40, 9);
            this.monthCalendarSearch.MaxSelectionCount = 90000;
            this.monthCalendarSearch.Name = "monthCalendarSearch";
            this.monthCalendarSearch.TabIndex = 32;
            this.monthCalendarSearch.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarSearch_DateSelected);
            // 
            // tabPageAdd
            // 
            this.tabPageAdd.Controls.Add(this.buttonAddDelEvent);
            this.tabPageAdd.Controls.Add(this.buttonAddEditEvent);
            this.tabPageAdd.Controls.Add(this.buttonAddAddEvent);
            this.tabPageAdd.Controls.Add(this.checkBoxAddNotification);
            this.tabPageAdd.Controls.Add(this.textBoxAddDescription);
            this.tabPageAdd.Controls.Add(this.textBoxAddName);
            this.tabPageAdd.Controls.Add(this.labelAddNotifyMsg);
            this.tabPageAdd.Controls.Add(this.labelAddNotificationDate);
            this.tabPageAdd.Controls.Add(this.labelAddDescription);
            this.tabPageAdd.Controls.Add(this.labelAddTo);
            this.tabPageAdd.Controls.Add(this.labelAddFrom);
            this.tabPageAdd.Controls.Add(this.labelAddHash);
            this.tabPageAdd.Controls.Add(this.labelAddHashLabel);
            this.tabPageAdd.Controls.Add(this.labelAddNameLabel);
            this.tabPageAdd.Controls.Add(this.monthCalendarAdd);
            this.tabPageAdd.Controls.Add(this.dateTimePickerAddEndDate);
            this.tabPageAdd.Controls.Add(this.dateTimePickerAddNotificationDate);
            this.tabPageAdd.Controls.Add(this.dateTimePickerAddStartDate);
            this.tabPageAdd.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdd.Name = "tabPageAdd";
            this.tabPageAdd.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdd.Size = new System.Drawing.Size(700, 595);
            this.tabPageAdd.TabIndex = 1;
            this.tabPageAdd.Text = "Přidávání / Změna Událostí:";
            // 
            // buttonAddDelEvent
            // 
            this.buttonAddDelEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddDelEvent.Enabled = false;
            this.buttonAddDelEvent.Location = new System.Drawing.Point(232, 532);
            this.buttonAddDelEvent.Name = "buttonAddDelEvent";
            this.buttonAddDelEvent.Size = new System.Drawing.Size(144, 29);
            this.buttonAddDelEvent.TabIndex = 8;
            this.buttonAddDelEvent.Text = "&Smazat událost";
            this.buttonAddDelEvent.UseVisualStyleBackColor = true;
            this.buttonAddDelEvent.Click += new System.EventHandler(this.buttonAddDelEvent_Click);
            // 
            // buttonAddEditEvent
            // 
            this.buttonAddEditEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddEditEvent.Enabled = false;
            this.buttonAddEditEvent.Location = new System.Drawing.Point(382, 532);
            this.buttonAddEditEvent.Name = "buttonAddEditEvent";
            this.buttonAddEditEvent.Size = new System.Drawing.Size(144, 29);
            this.buttonAddEditEvent.TabIndex = 8;
            this.buttonAddEditEvent.Text = "&Editovat událost";
            this.buttonAddEditEvent.UseVisualStyleBackColor = true;
            this.buttonAddEditEvent.Click += new System.EventHandler(this.buttonAddEditEvent_Click);
            // 
            // buttonAddAddEvent
            // 
            this.buttonAddAddEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddAddEvent.Location = new System.Drawing.Point(532, 532);
            this.buttonAddAddEvent.Name = "buttonAddAddEvent";
            this.buttonAddAddEvent.Size = new System.Drawing.Size(144, 29);
            this.buttonAddAddEvent.TabIndex = 9;
            this.buttonAddAddEvent.Text = "&Přidat událost";
            this.buttonAddAddEvent.UseVisualStyleBackColor = true;
            this.buttonAddAddEvent.Click += new System.EventHandler(this.buttonAddAddEvent_Click);
            // 
            // checkBoxAddNotification
            // 
            this.checkBoxAddNotification.AutoSize = true;
            this.checkBoxAddNotification.Location = new System.Drawing.Point(80, 413);
            this.checkBoxAddNotification.Name = "checkBoxAddNotification";
            this.checkBoxAddNotification.Size = new System.Drawing.Size(134, 17);
            this.checkBoxAddNotification.TabIndex = 6;
            this.checkBoxAddNotification.Text = "Upozornění na událost";
            this.checkBoxAddNotification.UseVisualStyleBackColor = true;
            this.checkBoxAddNotification.CheckedChanged += new System.EventHandler(this.checkBoxAddNotification_CheckedChanged);
            // 
            // textBoxAddDescription
            // 
            this.textBoxAddDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddDescription.Location = new System.Drawing.Point(80, 229);
            this.textBoxAddDescription.Multiline = true;
            this.textBoxAddDescription.Name = "textBoxAddDescription";
            this.textBoxAddDescription.Size = new System.Drawing.Size(612, 130);
            this.textBoxAddDescription.TabIndex = 3;
            this.textBoxAddDescription.TextChanged += new System.EventHandler(this.textBoxAddDescription_TextChanged);
            // 
            // textBoxAddName
            // 
            this.textBoxAddName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddName.Location = new System.Drawing.Point(80, 203);
            this.textBoxAddName.Name = "textBoxAddName";
            this.textBoxAddName.Size = new System.Drawing.Size(612, 20);
            this.textBoxAddName.TabIndex = 2;
            this.textBoxAddName.TextChanged += new System.EventHandler(this.textBoxAddName_TextChanged);
            // 
            // labelAddNotifyMsg
            // 
            this.labelAddNotifyMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAddNotifyMsg.ForeColor = System.Drawing.Color.Red;
            this.labelAddNotifyMsg.Location = new System.Drawing.Point(379, 577);
            this.labelAddNotifyMsg.Name = "labelAddNotifyMsg";
            this.labelAddNotifyMsg.Size = new System.Drawing.Size(297, 13);
            this.labelAddNotifyMsg.TabIndex = 24;
            // 
            // labelAddNotificationDate
            // 
            this.labelAddNotificationDate.AutoSize = true;
            this.labelAddNotificationDate.Location = new System.Drawing.Point(8, 442);
            this.labelAddNotificationDate.Name = "labelAddNotificationDate";
            this.labelAddNotificationDate.Size = new System.Drawing.Size(66, 13);
            this.labelAddNotificationDate.TabIndex = 24;
            this.labelAddNotificationDate.Text = "Upozornění:";
            this.labelAddNotificationDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAddDescription
            // 
            this.labelAddDescription.AutoSize = true;
            this.labelAddDescription.Location = new System.Drawing.Point(38, 232);
            this.labelAddDescription.Margin = new System.Windows.Forms.Padding(3);
            this.labelAddDescription.Name = "labelAddDescription";
            this.labelAddDescription.Size = new System.Drawing.Size(36, 13);
            this.labelAddDescription.TabIndex = 15;
            this.labelAddDescription.Text = "Popis:";
            this.labelAddDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAddTo
            // 
            this.labelAddTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAddTo.AutoSize = true;
            this.labelAddTo.Location = new System.Drawing.Point(349, 371);
            this.labelAddTo.Margin = new System.Windows.Forms.Padding(3);
            this.labelAddTo.Name = "labelAddTo";
            this.labelAddTo.Size = new System.Drawing.Size(41, 13);
            this.labelAddTo.TabIndex = 17;
            this.labelAddTo.Text = "Konec:";
            // 
            // labelAddFrom
            // 
            this.labelAddFrom.AutoSize = true;
            this.labelAddFrom.Location = new System.Drawing.Point(24, 371);
            this.labelAddFrom.Margin = new System.Windows.Forms.Padding(3);
            this.labelAddFrom.Name = "labelAddFrom";
            this.labelAddFrom.Size = new System.Drawing.Size(50, 13);
            this.labelAddFrom.TabIndex = 20;
            this.labelAddFrom.Text = "Začátek:";
            this.labelAddFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAddHash
            // 
            this.labelAddHash.AutoSize = true;
            this.labelAddHash.Location = new System.Drawing.Point(80, 183);
            this.labelAddHash.Margin = new System.Windows.Forms.Padding(3);
            this.labelAddHash.Name = "labelAddHash";
            this.labelAddHash.Size = new System.Drawing.Size(10, 13);
            this.labelAddHash.TabIndex = 22;
            this.labelAddHash.Text = "-";
            this.labelAddHash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAddHashLabel
            // 
            this.labelAddHashLabel.AutoSize = true;
            this.labelAddHashLabel.Location = new System.Drawing.Point(39, 183);
            this.labelAddHashLabel.Margin = new System.Windows.Forms.Padding(3);
            this.labelAddHashLabel.Name = "labelAddHashLabel";
            this.labelAddHashLabel.Size = new System.Drawing.Size(35, 13);
            this.labelAddHashLabel.TabIndex = 22;
            this.labelAddHashLabel.Text = "Hash:";
            this.labelAddHashLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAddNameLabel
            // 
            this.labelAddNameLabel.AutoSize = true;
            this.labelAddNameLabel.Location = new System.Drawing.Point(33, 206);
            this.labelAddNameLabel.Margin = new System.Windows.Forms.Padding(3);
            this.labelAddNameLabel.Name = "labelAddNameLabel";
            this.labelAddNameLabel.Size = new System.Drawing.Size(41, 13);
            this.labelAddNameLabel.TabIndex = 22;
            this.labelAddNameLabel.Text = "Jméno:";
            this.labelAddNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // monthCalendarAdd
            // 
            this.monthCalendarAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monthCalendarAdd.CalendarDimensions = new System.Drawing.Size(4, 1);
            this.monthCalendarAdd.Location = new System.Drawing.Point(40, 9);
            this.monthCalendarAdd.MaxSelectionCount = 90000;
            this.monthCalendarAdd.Name = "monthCalendarAdd";
            this.monthCalendarAdd.TabIndex = 1;
            this.monthCalendarAdd.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarAdd_DateSelected);
            // 
            // dateTimePickerAddEndDate
            // 
            this.dateTimePickerAddEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerAddEndDate.CustomFormat = "--";
            this.dateTimePickerAddEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerAddEndDate.Location = new System.Drawing.Point(396, 365);
            this.dateTimePickerAddEndDate.Name = "dateTimePickerAddEndDate";
            this.dateTimePickerAddEndDate.Size = new System.Drawing.Size(212, 20);
            this.dateTimePickerAddEndDate.TabIndex = 5;
            this.dateTimePickerAddEndDate.ValueChanged += new System.EventHandler(this.dateTimePickerAddEndDate_ValueChanged);
            // 
            // dateTimePickerAddNotificationDate
            // 
            this.dateTimePickerAddNotificationDate.CustomFormat = "--";
            this.dateTimePickerAddNotificationDate.Enabled = false;
            this.dateTimePickerAddNotificationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerAddNotificationDate.Location = new System.Drawing.Point(80, 436);
            this.dateTimePickerAddNotificationDate.Name = "dateTimePickerAddNotificationDate";
            this.dateTimePickerAddNotificationDate.Size = new System.Drawing.Size(212, 20);
            this.dateTimePickerAddNotificationDate.TabIndex = 7;
            // 
            // dateTimePickerAddStartDate
            // 
            this.dateTimePickerAddStartDate.CustomFormat = "--";
            this.dateTimePickerAddStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerAddStartDate.Location = new System.Drawing.Point(80, 365);
            this.dateTimePickerAddStartDate.Name = "dateTimePickerAddStartDate";
            this.dateTimePickerAddStartDate.Size = new System.Drawing.Size(212, 20);
            this.dateTimePickerAddStartDate.TabIndex = 4;
            this.dateTimePickerAddStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerAddStartDate_ValueChanged);
            // 
            // tabPagePreview
            // 
            this.tabPagePreview.Controls.Add(this.labelPreviewLoading);
            this.tabPagePreview.Controls.Add(this.labelPreviewErrorMsg);
            this.tabPagePreview.Controls.Add(this.checkBoxPreviewGlobalNotification);
            this.tabPagePreview.Controls.Add(this.buttonPreviewAddEvent);
            this.tabPagePreview.Controls.Add(this.buttonPreviewEditEvent);
            this.tabPagePreview.Controls.Add(this.buttonPreviewDelEvent);
            this.tabPagePreview.Controls.Add(this.labelNotificationLabel);
            this.tabPagePreview.Controls.Add(this.labelPreviewDescription);
            this.tabPagePreview.Controls.Add(this.labelDescLable);
            this.tabPagePreview.Controls.Add(this.labelPreviewTo);
            this.tabPagePreview.Controls.Add(this.label11);
            this.tabPagePreview.Controls.Add(this.labelToLabel);
            this.tabPagePreview.Controls.Add(this.labelPreviewNotification);
            this.tabPagePreview.Controls.Add(this.labelPreviewFrom);
            this.tabPagePreview.Controls.Add(this.labelFromLabel);
            this.tabPagePreview.Controls.Add(this.labelPreviewName);
            this.tabPagePreview.Controls.Add(this.labelNameLabel);
            this.tabPagePreview.Controls.Add(this.monthCalendarPreview);
            this.tabPagePreview.Controls.Add(this.dateTimePickerPreviewTo);
            this.tabPagePreview.Controls.Add(this.dateTimePickerPreviewFrom);
            this.tabPagePreview.Controls.Add(this.listBoxPreview);
            this.tabPagePreview.Controls.Add(this.label2);
            this.tabPagePreview.Controls.Add(this.label1);
            this.tabPagePreview.Location = new System.Drawing.Point(4, 22);
            this.tabPagePreview.Name = "tabPagePreview";
            this.tabPagePreview.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePreview.Size = new System.Drawing.Size(700, 595);
            this.tabPagePreview.TabIndex = 0;
            this.tabPagePreview.Text = "Přehled:";
            // 
            // labelPreviewLoading
            // 
            this.labelPreviewLoading.AutoSize = true;
            this.labelPreviewLoading.ForeColor = System.Drawing.Color.DimGray;
            this.labelPreviewLoading.Location = new System.Drawing.Point(8, 165);
            this.labelPreviewLoading.Name = "labelPreviewLoading";
            this.labelPreviewLoading.Size = new System.Drawing.Size(107, 13);
            this.labelPreviewLoading.TabIndex = 61;
            this.labelPreviewLoading.Text = "Probíhá načítání . . .";
            this.labelPreviewLoading.Visible = false;
            // 
            // labelPreviewErrorMsg
            // 
            this.labelPreviewErrorMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreviewErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.labelPreviewErrorMsg.Location = new System.Drawing.Point(354, 447);
            this.labelPreviewErrorMsg.Name = "labelPreviewErrorMsg";
            this.labelPreviewErrorMsg.Size = new System.Drawing.Size(297, 13);
            this.labelPreviewErrorMsg.TabIndex = 25;
            // 
            // checkBoxPreviewGlobalNotification
            // 
            this.checkBoxPreviewGlobalNotification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxPreviewGlobalNotification.AutoSize = true;
            this.checkBoxPreviewGlobalNotification.Location = new System.Drawing.Point(11, 565);
            this.checkBoxPreviewGlobalNotification.Name = "checkBoxPreviewGlobalNotification";
            this.checkBoxPreviewGlobalNotification.Size = new System.Drawing.Size(141, 17);
            this.checkBoxPreviewGlobalNotification.TabIndex = 17;
            this.checkBoxPreviewGlobalNotification.Text = "Upozorňovat na události";
            this.checkBoxPreviewGlobalNotification.UseVisualStyleBackColor = true;
            // 
            // buttonPreviewAddEvent
            // 
            this.buttonPreviewAddEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPreviewAddEvent.Location = new System.Drawing.Point(566, 558);
            this.buttonPreviewAddEvent.Name = "buttonPreviewAddEvent";
            this.buttonPreviewAddEvent.Size = new System.Drawing.Size(126, 29);
            this.buttonPreviewAddEvent.TabIndex = 16;
            this.buttonPreviewAddEvent.Text = "&Přidat událost";
            this.buttonPreviewAddEvent.UseVisualStyleBackColor = true;
            this.buttonPreviewAddEvent.Click += new System.EventHandler(this.buttonPreviewAddEvent_Click);
            // 
            // buttonPreviewEditEvent
            // 
            this.buttonPreviewEditEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPreviewEditEvent.Enabled = false;
            this.buttonPreviewEditEvent.Location = new System.Drawing.Point(357, 463);
            this.buttonPreviewEditEvent.Name = "buttonPreviewEditEvent";
            this.buttonPreviewEditEvent.Size = new System.Drawing.Size(168, 26);
            this.buttonPreviewEditEvent.TabIndex = 15;
            this.buttonPreviewEditEvent.Text = "&Editovat označenou Událost";
            this.buttonPreviewEditEvent.UseVisualStyleBackColor = true;
            this.buttonPreviewEditEvent.Click += new System.EventHandler(this.buttonPreviewEditEvent_Click);
            // 
            // buttonPreviewDelEvent
            // 
            this.buttonPreviewDelEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPreviewDelEvent.Enabled = false;
            this.buttonPreviewDelEvent.Location = new System.Drawing.Point(531, 463);
            this.buttonPreviewDelEvent.Name = "buttonPreviewDelEvent";
            this.buttonPreviewDelEvent.Size = new System.Drawing.Size(161, 26);
            this.buttonPreviewDelEvent.TabIndex = 15;
            this.buttonPreviewDelEvent.Text = "&Smazat označenou Událost";
            this.buttonPreviewDelEvent.UseVisualStyleBackColor = true;
            this.buttonPreviewDelEvent.Click += new System.EventHandler(this.buttonPreviewDelEvent_Click);
            // 
            // labelNotificationLabel
            // 
            this.labelNotificationLabel.AutoSize = true;
            this.labelNotificationLabel.Location = new System.Drawing.Point(8, 451);
            this.labelNotificationLabel.Name = "labelNotificationLabel";
            this.labelNotificationLabel.Size = new System.Drawing.Size(66, 13);
            this.labelNotificationLabel.TabIndex = 14;
            this.labelNotificationLabel.Text = "Upozornění:";
            this.labelNotificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPreviewDescription
            // 
            this.labelPreviewDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreviewDescription.Location = new System.Drawing.Point(80, 371);
            this.labelPreviewDescription.Margin = new System.Windows.Forms.Padding(3);
            this.labelPreviewDescription.Name = "labelPreviewDescription";
            this.labelPreviewDescription.Size = new System.Drawing.Size(612, 55);
            this.labelPreviewDescription.TabIndex = 13;
            this.labelPreviewDescription.Text = "-";
            // 
            // labelDescLable
            // 
            this.labelDescLable.AutoSize = true;
            this.labelDescLable.Location = new System.Drawing.Point(38, 371);
            this.labelDescLable.Margin = new System.Windows.Forms.Padding(3);
            this.labelDescLable.Name = "labelDescLable";
            this.labelDescLable.Size = new System.Drawing.Size(36, 13);
            this.labelDescLable.TabIndex = 12;
            this.labelDescLable.Text = "Popis:";
            this.labelDescLable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPreviewTo
            // 
            this.labelPreviewTo.AutoSize = true;
            this.labelPreviewTo.Location = new System.Drawing.Point(259, 432);
            this.labelPreviewTo.Margin = new System.Windows.Forms.Padding(3);
            this.labelPreviewTo.Name = "labelPreviewTo";
            this.labelPreviewTo.Size = new System.Drawing.Size(10, 13);
            this.labelPreviewTo.TabIndex = 12;
            this.labelPreviewTo.Text = "-";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 73);
            this.label11.Margin = new System.Windows.Forms.Padding(3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Do:";
            // 
            // labelToLabel
            // 
            this.labelToLabel.AutoSize = true;
            this.labelToLabel.Location = new System.Drawing.Point(229, 432);
            this.labelToLabel.Margin = new System.Windows.Forms.Padding(3);
            this.labelToLabel.Name = "labelToLabel";
            this.labelToLabel.Size = new System.Drawing.Size(24, 13);
            this.labelToLabel.TabIndex = 12;
            this.labelToLabel.Text = "Do:";
            // 
            // labelPreviewNotification
            // 
            this.labelPreviewNotification.AutoSize = true;
            this.labelPreviewNotification.Location = new System.Drawing.Point(80, 451);
            this.labelPreviewNotification.Margin = new System.Windows.Forms.Padding(3);
            this.labelPreviewNotification.Name = "labelPreviewNotification";
            this.labelPreviewNotification.Size = new System.Drawing.Size(10, 13);
            this.labelPreviewNotification.TabIndex = 12;
            this.labelPreviewNotification.Text = "-";
            // 
            // labelPreviewFrom
            // 
            this.labelPreviewFrom.AutoSize = true;
            this.labelPreviewFrom.Location = new System.Drawing.Point(80, 432);
            this.labelPreviewFrom.Margin = new System.Windows.Forms.Padding(3);
            this.labelPreviewFrom.Name = "labelPreviewFrom";
            this.labelPreviewFrom.Size = new System.Drawing.Size(10, 13);
            this.labelPreviewFrom.TabIndex = 12;
            this.labelPreviewFrom.Text = "-";
            // 
            // labelFromLabel
            // 
            this.labelFromLabel.AutoSize = true;
            this.labelFromLabel.Location = new System.Drawing.Point(50, 432);
            this.labelFromLabel.Margin = new System.Windows.Forms.Padding(3);
            this.labelFromLabel.Name = "labelFromLabel";
            this.labelFromLabel.Size = new System.Drawing.Size(24, 13);
            this.labelFromLabel.TabIndex = 12;
            this.labelFromLabel.Text = "Od:";
            this.labelFromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPreviewName
            // 
            this.labelPreviewName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreviewName.Location = new System.Drawing.Point(80, 352);
            this.labelPreviewName.Margin = new System.Windows.Forms.Padding(3);
            this.labelPreviewName.Name = "labelPreviewName";
            this.labelPreviewName.Size = new System.Drawing.Size(612, 13);
            this.labelPreviewName.TabIndex = 12;
            this.labelPreviewName.Text = "-";
            // 
            // labelNameLabel
            // 
            this.labelNameLabel.AutoSize = true;
            this.labelNameLabel.Location = new System.Drawing.Point(33, 352);
            this.labelNameLabel.Margin = new System.Windows.Forms.Padding(3);
            this.labelNameLabel.Name = "labelNameLabel";
            this.labelNameLabel.Size = new System.Drawing.Size(41, 13);
            this.labelNameLabel.TabIndex = 12;
            this.labelNameLabel.Text = "Jméno:";
            this.labelNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // monthCalendarPreview
            // 
            this.monthCalendarPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monthCalendarPreview.CalendarDimensions = new System.Drawing.Size(3, 1);
            this.monthCalendarPreview.Location = new System.Drawing.Point(232, 16);
            this.monthCalendarPreview.MaxSelectionCount = 90000;
            this.monthCalendarPreview.Name = "monthCalendarPreview";
            this.monthCalendarPreview.TabIndex = 3;
            this.monthCalendarPreview.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarPreview_DateSelected);
            // 
            // dateTimePickerPreviewTo
            // 
            this.dateTimePickerPreviewTo.AllowDrop = true;
            this.dateTimePickerPreviewTo.CustomFormat = "--";
            this.dateTimePickerPreviewTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerPreviewTo.Location = new System.Drawing.Point(8, 92);
            this.dateTimePickerPreviewTo.Name = "dateTimePickerPreviewTo";
            this.dateTimePickerPreviewTo.Size = new System.Drawing.Size(212, 20);
            this.dateTimePickerPreviewTo.TabIndex = 2;
            this.dateTimePickerPreviewTo.ValueChanged += new System.EventHandler(this.dateTimePickerPreviewTo_ValueChanged);
            // 
            // dateTimePickerPreviewFrom
            // 
            this.dateTimePickerPreviewFrom.CustomFormat = "--";
            this.dateTimePickerPreviewFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerPreviewFrom.Location = new System.Drawing.Point(8, 35);
            this.dateTimePickerPreviewFrom.Name = "dateTimePickerPreviewFrom";
            this.dateTimePickerPreviewFrom.Size = new System.Drawing.Size(212, 20);
            this.dateTimePickerPreviewFrom.TabIndex = 1;
            this.dateTimePickerPreviewFrom.ValueChanged += new System.EventHandler(this.dateTimePickerPreviewFrom_ValueChanged);
            // 
            // listBoxEventsListPreview
            // 
            this.listBoxPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPreview.DisplayMember = "ListBoxString";
            this.listBoxPreview.FormattingEnabled = true;
            this.listBoxPreview.HorizontalScrollbar = true;
            this.listBoxPreview.Location = new System.Drawing.Point(8, 186);
            this.listBoxPreview.Name = "listBoxEventsListPreview";
            this.listBoxPreview.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxPreview.Size = new System.Drawing.Size(684, 160);
            this.listBoxPreview.TabIndex = 4;
            this.listBoxPreview.SelectedIndexChanged += new System.EventHandler(this.listBoxEventsListPreview_SelectedIndexChanged);
            this.listBoxPreview.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxEventsListPreview_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(-124, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Do:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Od:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPagePreview);
            this.tabControl.Controls.Add(this.tabPageAdd);
            this.tabControl.Controls.Add(this.tabPageSeach);
            this.tabControl.Controls.Add(this.tabPageSynchronization);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(708, 621);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageSynchronization
            // 
            this.tabPageSynchronization.Controls.Add(this.labelSynchDifferentGroupID);
            this.tabPageSynchronization.Controls.Add(this.checkBoxSynchChangeVisibility);
            this.tabPageSynchronization.Controls.Add(this.label20);
            this.tabPageSynchronization.Controls.Add(this.label14);
            this.tabPageSynchronization.Controls.Add(this.labelSynchSelectedLastSynchDate);
            this.tabPageSynchronization.Controls.Add(this.labelSynchSelectedAllow);
            this.tabPageSynchronization.Controls.Add(this.labelSynchSelectedDeviceID);
            this.tabPageSynchronization.Controls.Add(this.label12);
            this.tabPageSynchronization.Controls.Add(this.buttonSynchFindDevices);
            this.tabPageSynchronization.Controls.Add(this.buttonSynchSynchronize);
            this.tabPageSynchronization.Controls.Add(this.buttonSynchAllow);
            this.tabPageSynchronization.Controls.Add(this.textBoxSynchAppName);
            this.tabPageSynchronization.Controls.Add(this.textBoxSynchGroupID);
            this.tabPageSynchronization.Controls.Add(this.listBoxSynchDevices);
            this.tabPageSynchronization.Controls.Add(this.labelSynchInstanceID);
            this.tabPageSynchronization.Controls.Add(this.label8);
            this.tabPageSynchronization.Controls.Add(this.label15);
            this.tabPageSynchronization.Controls.Add(this.label19);
            this.tabPageSynchronization.Location = new System.Drawing.Point(4, 22);
            this.tabPageSynchronization.Name = "tabPageSynchronization";
            this.tabPageSynchronization.Size = new System.Drawing.Size(700, 595);
            this.tabPageSynchronization.TabIndex = 4;
            this.tabPageSynchronization.Text = "Synchronizace:";
            // 
            // labelSynchDifferentGroupID
            // 
            this.labelSynchDifferentGroupID.AutoSize = true;
            this.labelSynchDifferentGroupID.ForeColor = System.Drawing.Color.Red;
            this.labelSynchDifferentGroupID.Location = new System.Drawing.Point(374, 486);
            this.labelSynchDifferentGroupID.Name = "labelSynchDifferentGroupID";
            this.labelSynchDifferentGroupID.Size = new System.Drawing.Size(225, 13);
            this.labelSynchDifferentGroupID.TabIndex = 7;
            this.labelSynchDifferentGroupID.Text = "Tato instance má rozdílný identifikátor skupiny";
            this.labelSynchDifferentGroupID.Visible = false;
            // 
            // checkBoxSynchChangeVisibility
            // 
            this.checkBoxSynchChangeVisibility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSynchChangeVisibility.AutoSize = true;
            this.checkBoxSynchChangeVisibility.Location = new System.Drawing.Point(405, 366);
            this.checkBoxSynchChangeVisibility.Name = "checkBoxSynchChangeVisibility";
            this.checkBoxSynchChangeVisibility.Size = new System.Drawing.Size(134, 17);
            this.checkBoxSynchChangeVisibility.TabIndex = 6;
            this.checkBoxSynchChangeVisibility.Text = "Zviditelnit tuto instanci:";
            this.checkBoxSynchChangeVisibility.UseVisualStyleBackColor = true;
            this.checkBoxSynchChangeVisibility.CheckedChanged += new System.EventHandler(this.checkBoxSynchChangeVisibility_CheckedChanged);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(5, 402);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(126, 13);
            this.label20.TabIndex = 5;
            this.label20.Text = "Povolená synchronizace:";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 433);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(123, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Poslední synchronizace:";
            // 
            // labelSynchSelectedLastSynchDate
            // 
            this.labelSynchSelectedLastSynchDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSynchSelectedLastSynchDate.AutoSize = true;
            this.labelSynchSelectedLastSynchDate.Location = new System.Drawing.Point(137, 433);
            this.labelSynchSelectedLastSynchDate.Name = "labelSynchSelectedLastSynchDate";
            this.labelSynchSelectedLastSynchDate.Size = new System.Drawing.Size(10, 13);
            this.labelSynchSelectedLastSynchDate.TabIndex = 5;
            this.labelSynchSelectedLastSynchDate.Text = "-";
            // 
            // labelSynchSelectedAllow
            // 
            this.labelSynchSelectedAllow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSynchSelectedAllow.AutoSize = true;
            this.labelSynchSelectedAllow.Location = new System.Drawing.Point(137, 373);
            this.labelSynchSelectedAllow.Name = "labelSynchSelectedAllow";
            this.labelSynchSelectedAllow.Size = new System.Drawing.Size(10, 13);
            this.labelSynchSelectedAllow.TabIndex = 5;
            this.labelSynchSelectedAllow.Text = "-";
            // 
            // labelSynchSelectedDeviceID
            // 
            this.labelSynchSelectedDeviceID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSynchSelectedDeviceID.AutoSize = true;
            this.labelSynchSelectedDeviceID.Location = new System.Drawing.Point(137, 402);
            this.labelSynchSelectedDeviceID.Name = "labelSynchSelectedDeviceID";
            this.labelSynchSelectedDeviceID.Size = new System.Drawing.Size(10, 13);
            this.labelSynchSelectedDeviceID.TabIndex = 5;
            this.labelSynchSelectedDeviceID.Text = "-";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 373);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Identifikáto Instance:";
            // 
            // buttonSynchFindDevices
            // 
            this.buttonSynchFindDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSynchFindDevices.Location = new System.Drawing.Point(550, 360);
            this.buttonSynchFindDevices.Name = "buttonSynchFindDevices";
            this.buttonSynchFindDevices.Size = new System.Drawing.Size(142, 26);
            this.buttonSynchFindDevices.TabIndex = 4;
            this.buttonSynchFindDevices.Text = "&Hledat instance";
            this.buttonSynchFindDevices.UseVisualStyleBackColor = true;
            this.buttonSynchFindDevices.Click += new System.EventHandler(this.buttonSynchFindDevices_Click);
            // 
            // buttonSynchSynchronize
            // 
            this.buttonSynchSynchronize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSynchSynchronize.Enabled = false;
            this.buttonSynchSynchronize.Location = new System.Drawing.Point(377, 502);
            this.buttonSynchSynchronize.Name = "buttonSynchSynchronize";
            this.buttonSynchSynchronize.Size = new System.Drawing.Size(123, 26);
            this.buttonSynchSynchronize.TabIndex = 4;
            this.buttonSynchSynchronize.Text = "&Synchronizovat";
            this.buttonSynchSynchronize.UseVisualStyleBackColor = true;
            this.buttonSynchSynchronize.Click += new System.EventHandler(this.buttonSynchSynchronize_Click);
            // 
            // buttonSynchAllow
            // 
            this.buttonSynchAllow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSynchAllow.Enabled = false;
            this.buttonSynchAllow.Location = new System.Drawing.Point(506, 502);
            this.buttonSynchAllow.Name = "buttonSynchAllow";
            this.buttonSynchAllow.Size = new System.Drawing.Size(186, 26);
            this.buttonSynchAllow.TabIndex = 4;
            this.buttonSynchAllow.Text = "&Povolit instanci pro synchronizaci";
            this.buttonSynchAllow.UseVisualStyleBackColor = true;
            this.buttonSynchAllow.Click += new System.EventHandler(this.buttonSynchAllow_Click);
            // 
            // textBoxSynchAppName
            // 
            this.textBoxSynchAppName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSynchAppName.Location = new System.Drawing.Point(448, 34);
            this.textBoxSynchAppName.Name = "textBoxSynchAppName";
            this.textBoxSynchAppName.Size = new System.Drawing.Size(186, 20);
            this.textBoxSynchAppName.TabIndex = 3;
            this.textBoxSynchAppName.TextChanged += new System.EventHandler(this.textBoxSynchAppName_TextChanged);
            // 
            // textBoxSynchGroupID
            // 
            this.textBoxSynchGroupID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSynchGroupID.Location = new System.Drawing.Point(448, 60);
            this.textBoxSynchGroupID.Name = "textBoxSynchGroupID";
            this.textBoxSynchGroupID.Size = new System.Drawing.Size(186, 20);
            this.textBoxSynchGroupID.TabIndex = 3;
            this.textBoxSynchGroupID.TextChanged += new System.EventHandler(this.textBoxSynchGroupID_TextChanged);
            // 
            // listBoxSynchDevices
            // 
            this.listBoxSynchDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSynchDevices.DisplayMember = "ListBoxString";
            this.listBoxSynchDevices.FormattingEnabled = true;
            this.listBoxSynchDevices.Location = new System.Drawing.Point(8, 114);
            this.listBoxSynchDevices.Name = "listBoxSynchDevices";
            this.listBoxSynchDevices.Size = new System.Drawing.Size(684, 212);
            this.listBoxSynchDevices.TabIndex = 2;
            this.listBoxSynchDevices.SelectedIndexChanged += new System.EventHandler(this.listBoxSearchDevices_SelectedIndexChanged);
            // 
            // labelSynchInstanceID
            // 
            this.labelSynchInstanceID.AutoSize = true;
            this.labelSynchInstanceID.Location = new System.Drawing.Point(169, 37);
            this.labelSynchInstanceID.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.labelSynchInstanceID.Name = "labelSynchInstanceID";
            this.labelSynchInstanceID.Size = new System.Drawing.Size(10, 13);
            this.labelSynchInstanceID.TabIndex = 1;
            this.labelSynchInstanceID.Text = "-";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(297, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(145, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Identifikátor skupiny událostí:";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(299, 37);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(143, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Uživatelské jméno programu:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 37);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(155, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Indetifikátor instance programu:";
            // 
            // timerSearchListbox
            // 
            this.timerSearchListbox.Tick += new System.EventHandler(this.timerSearch_Tick);
            // 
            // timerPreviewBoldedDates
            // 
            this.timerPreviewBoldedDates.Tick += new System.EventHandler(this.timerPreviewBoldedDates_Tick);
            // 
            // timerSynchDevices
            // 
            this.timerSynchDevices.Tick += new System.EventHandler(this.timerSynchDevices_Tick);
            // 
            // timerSearchBoldedDates
            // 
            this.timerSearchBoldedDates.Tick += new System.EventHandler(this.timerSearchBoldedDates_Tick);
            // 
            // timerNotification
            // 
            this.timerNotification.Enabled = true;
            this.timerNotification.Tick += new System.EventHandler(this.timerNotification_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Eventer - událostní připomínátko";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            // 
            // timerNotificationAdder
            // 
            this.timerNotificationAdder.Enabled = true;
            this.timerNotificationAdder.Interval = 30000;
            this.timerNotificationAdder.Tick += new System.EventHandler(this.timerNotificationAdder_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(708, 621);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(724, 660);
            this.Name = "MainForm";
            this.Text = "Eventer - událostní připomínátko";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabPageSeach.ResumeLayout(false);
            this.tabPageSeach.PerformLayout();
            this.tabPageAdd.ResumeLayout(false);
            this.tabPageAdd.PerformLayout();
            this.tabPagePreview.ResumeLayout(false);
            this.tabPagePreview.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageSynchronization.ResumeLayout(false);
            this.tabPageSynchronization.PerformLayout();
            this.ResumeLayout(false);

		}
        private System.Windows.Forms.Timer timerPreviewListbox;
        private System.Windows.Forms.TabPage tabPageSeach;
        private System.Windows.Forms.MonthCalendar monthCalendarSearch;
        private System.Windows.Forms.TabPage tabPageAdd;
        private System.Windows.Forms.Button buttonAddAddEvent;
        private System.Windows.Forms.CheckBox checkBoxAddNotification;
        private System.Windows.Forms.TextBox textBoxAddDescription;
        private System.Windows.Forms.TextBox textBoxAddName;
        private System.Windows.Forms.Label labelAddNotificationDate;
        private System.Windows.Forms.Label labelAddDescription;
        private System.Windows.Forms.Label labelAddTo;
        private System.Windows.Forms.Label labelAddFrom;
        private System.Windows.Forms.Label labelAddHashLabel;
        private System.Windows.Forms.Label labelAddNameLabel;
        private System.Windows.Forms.MonthCalendar monthCalendarAdd;
        private System.Windows.Forms.DateTimePicker dateTimePickerAddEndDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerAddNotificationDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerAddStartDate;
        private System.Windows.Forms.TabPage tabPagePreview;
        private System.Windows.Forms.Label labelNotificationLabel;
        private System.Windows.Forms.Label labelPreviewDescription;
        private System.Windows.Forms.Label labelDescLable;
        private System.Windows.Forms.Label labelPreviewTo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelToLabel;
        private System.Windows.Forms.Label labelPreviewNotification;
        private System.Windows.Forms.Label labelPreviewFrom;
        private System.Windows.Forms.Label labelFromLabel;
        private System.Windows.Forms.Label labelPreviewName;
        private System.Windows.Forms.Label labelNameLabel;
        private System.Windows.Forms.MonthCalendar monthCalendarPreview;
        private System.Windows.Forms.DateTimePicker dateTimePickerPreviewTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerPreviewFrom;
        private System.Windows.Forms.ListBox listBoxPreview;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Label labelAddHash;
        private System.Windows.Forms.Button buttonAddEditEvent;
        private System.Windows.Forms.Label labelAddNotifyMsg;
        private System.Windows.Forms.TextBox textBoxSearchDescription;
        private System.Windows.Forms.TextBox textBoxSearchName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePickerSearchTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerSearchNotificationDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerSearchFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelSearchDescription;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelSearchTo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelSearchNotification;
        private System.Windows.Forms.Label labelSearchFrom;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label labelSearchName;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ListBox listBoxSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TabPage tabPageSynchronization;
        private System.Windows.Forms.Label labelSynchInstanceID;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Timer timerSearchListbox;
        private System.Windows.Forms.Button buttonAddDelEvent;
        private System.Windows.Forms.Button buttonSynchFindDevices;
        private System.Windows.Forms.Button buttonSynchSynchronize;
        private System.Windows.Forms.Button buttonSynchAllow;
        private System.Windows.Forms.TextBox textBoxSynchGroupID;
        private System.Windows.Forms.ListBox listBoxSynchDevices;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelSynchSelectedLastSynchDate;
        private System.Windows.Forms.Label labelSynchSelectedDeviceID;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label labelSynchSelectedAllow;
        private System.Windows.Forms.Timer timerPreviewBoldedDates;
        private System.Windows.Forms.CheckBox checkBoxSynchChangeVisibility;
        private System.Windows.Forms.Timer timerSynchDevices;
        private System.Windows.Forms.Timer timerSearchBoldedDates;
        private System.Windows.Forms.Timer timerNotification;
        private System.Windows.Forms.TextBox textBoxSynchAppName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelSynchDifferentGroupID;
        private System.Windows.Forms.CheckBox checkBoxPreviewGlobalNotification;
        private System.Windows.Forms.Button buttonPreviewAddEvent;
        private System.Windows.Forms.Button buttonPreviewEditEvent;
        private System.Windows.Forms.Button buttonPreviewDelEvent;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer timerNotificationAdder;
        private System.Windows.Forms.Label labelPreviewErrorMsg;
        private System.Windows.Forms.Label labelSearchLoading;
        private System.Windows.Forms.Label labelPreviewLoading;
    }
}
