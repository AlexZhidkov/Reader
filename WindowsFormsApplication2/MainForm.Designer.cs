namespace TxtReader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Timer workTimer;
            this.label1 = new System.Windows.Forms.Label();
            this.addURLfield = new System.Windows.Forms.TextBox();
            this.addFirstBlogButton = new System.Windows.Forms.Button();
            this.addAllBlogsButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.PlayList = new System.Windows.Forms.ListBox();
            this.currentlyPlayingLabel = new System.Windows.Forms.Label();
            this.PlayTimeLabel = new System.Windows.Forms.Label();
            this.AddTenBlogsButton = new System.Windows.Forms.Button();
            this.DebugURLbutton = new System.Windows.Forms.Button();
            this.playingProgressBar = new System.Windows.Forms.ProgressBar();
            workTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // workTimer
            // 
            workTimer.Enabled = true;
            workTimer.Interval = 1000;
            workTimer.Tick += new System.EventHandler(this.workTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Link to blog:";
            // 
            // addURLfield
            // 
            this.addURLfield.Location = new System.Drawing.Point(119, 185);
            this.addURLfield.Name = "addURLfield";
            this.addURLfield.Size = new System.Drawing.Size(256, 20);
            this.addURLfield.TabIndex = 1;
            this.addURLfield.Text = "Put the link to the blog here";
            this.addURLfield.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.addURLfield.Click += new System.EventHandler(this.addURLfield_Click);
            // 
            // addFirstBlogButton
            // 
            this.addFirstBlogButton.Location = new System.Drawing.Point(35, 210);
            this.addFirstBlogButton.Name = "addFirstBlogButton";
            this.addFirstBlogButton.Size = new System.Drawing.Size(113, 38);
            this.addFirstBlogButton.TabIndex = 3;
            this.addFirstBlogButton.Text = "Add 1st blog to playlist";
            this.addFirstBlogButton.UseVisualStyleBackColor = true;
            this.addFirstBlogButton.Click += new System.EventHandler(this.addFirstBlogButton_Click);
            // 
            // addAllBlogsButton
            // 
            this.addAllBlogsButton.Location = new System.Drawing.Point(270, 210);
            this.addAllBlogsButton.Name = "addAllBlogsButton";
            this.addAllBlogsButton.Size = new System.Drawing.Size(118, 37);
            this.addAllBlogsButton.TabIndex = 4;
            this.addAllBlogsButton.Text = "Add all blogs to playlist";
            this.addAllBlogsButton.UseVisualStyleBackColor = true;
            this.addAllBlogsButton.Click += new System.EventHandler(this.addAllBlogsButton_Click);
            // 
            // playButton
            // 
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.playButton.Location = new System.Drawing.Point(35, 23);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(110, 40);
            this.playButton.TabIndex = 5;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(151, 96);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(110, 33);
            this.pauseButton.TabIndex = 6;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(270, 96);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(110, 33);
            this.stopButton.TabIndex = 7;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(35, 96);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(110, 33);
            this.prevButton.TabIndex = 8;
            this.prevButton.Text = "<< Previous";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(386, 96);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(108, 33);
            this.nextButton.TabIndex = 9;
            this.nextButton.Text = "Next >>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // PlayList
            // 
            this.PlayList.FormattingEnabled = true;
            this.PlayList.Location = new System.Drawing.Point(506, 23);
            this.PlayList.Name = "PlayList";
            this.PlayList.Size = new System.Drawing.Size(287, 225);
            this.PlayList.TabIndex = 11;
            this.PlayList.SelectedIndexChanged += new System.EventHandler(this.playList_SelectedIndexChanged_1);
            // 
            // currentlyPlayingLabel
            // 
            this.currentlyPlayingLabel.AutoSize = true;
            this.currentlyPlayingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.currentlyPlayingLabel.Location = new System.Drawing.Point(151, 35);
            this.currentlyPlayingLabel.Name = "currentlyPlayingLabel";
            this.currentlyPlayingLabel.Size = new System.Drawing.Size(54, 16);
            this.currentlyPlayingLabel.TabIndex = 13;
            this.currentlyPlayingLabel.Text = "Reader";
            // 
            // PlayTimeLabel
            // 
            this.PlayTimeLabel.AutoSize = true;
            this.PlayTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PlayTimeLabel.Location = new System.Drawing.Point(413, 23);
            this.PlayTimeLabel.Name = "PlayTimeLabel";
            this.PlayTimeLabel.Size = new System.Drawing.Size(87, 31);
            this.PlayTimeLabel.TabIndex = 14;
            this.PlayTimeLabel.Text = "00:00";
            // 
            // AddTenBlogsButton
            // 
            this.AddTenBlogsButton.Location = new System.Drawing.Point(154, 210);
            this.AddTenBlogsButton.Name = "AddTenBlogsButton";
            this.AddTenBlogsButton.Size = new System.Drawing.Size(110, 38);
            this.AddTenBlogsButton.TabIndex = 15;
            this.AddTenBlogsButton.Text = "Add first 10 blogs to playlist";
            this.AddTenBlogsButton.UseVisualStyleBackColor = true;
            this.AddTenBlogsButton.Click += new System.EventHandler(this.AddTenBlogsButton_Click);
            // 
            // DebugURLbutton
            // 
            this.DebugURLbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.DebugURLbutton.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugURLbutton.Location = new System.Drawing.Point(35, 250);
            this.DebugURLbutton.Name = "DebugURLbutton";
            this.DebugURLbutton.Size = new System.Drawing.Size(226, 29);
            this.DebugURLbutton.TabIndex = 16;
            this.DebugURLbutton.Text = "[TESTS]Set URL to \"http://sethgodin.typepad.com\"";
            this.DebugURLbutton.UseVisualStyleBackColor = false;
            this.DebugURLbutton.Click += new System.EventHandler(this.DebugURLbutton_Click);
            // 
            // playingProgressBar
            // 
            this.playingProgressBar.Location = new System.Drawing.Point(35, 69);
            this.playingProgressBar.Name = "playingProgressBar";
            this.playingProgressBar.Size = new System.Drawing.Size(459, 21);
            this.playingProgressBar.TabIndex = 17;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 291);
            this.Controls.Add(this.playingProgressBar);
            this.Controls.Add(this.DebugURLbutton);
            this.Controls.Add(this.AddTenBlogsButton);
            this.Controls.Add(this.PlayTimeLabel);
            this.Controls.Add(this.currentlyPlayingLabel);
            this.Controls.Add(this.PlayList);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.prevButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.addAllBlogsButton);
            this.Controls.Add(this.addFirstBlogButton);
            this.Controls.Add(this.addURLfield);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox addURLfield;
        private System.Windows.Forms.Button addFirstBlogButton;
        private System.Windows.Forms.Button addAllBlogsButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.ListBox PlayList;
        private System.Windows.Forms.Label currentlyPlayingLabel;
        private System.Windows.Forms.Label PlayTimeLabel;
        private System.Windows.Forms.Button AddTenBlogsButton;
        private System.Windows.Forms.Button DebugURLbutton;
        private System.Windows.Forms.ProgressBar playingProgressBar;
    }
}

