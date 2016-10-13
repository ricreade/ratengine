namespace RatGameEditor
{
    partial class frmMain
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
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmiSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSystemConnections = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSystemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewWorld = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewWorldRealm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewWorldRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewWorldRoom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewCreature = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewCreatureCharacter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewFlag = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewInstructionset = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.msMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSystem,
            this.tsmiView,
            this.tsmiOptions});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(952, 24);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "menuStrip1";
            // 
            // tsmiSystem
            // 
            this.tsmiSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSystemConnections,
            this.toolStripSeparator1,
            this.tsmiSystemQuit});
            this.tsmiSystem.Name = "tsmiSystem";
            this.tsmiSystem.Size = new System.Drawing.Size(57, 20);
            this.tsmiSystem.Text = "System";
            // 
            // tsmiSystemConnections
            // 
            this.tsmiSystemConnections.Name = "tsmiSystemConnections";
            this.tsmiSystemConnections.Size = new System.Drawing.Size(152, 22);
            this.tsmiSystemConnections.Text = "Connections...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiSystemQuit
            // 
            this.tsmiSystemQuit.Name = "tsmiSystemQuit";
            this.tsmiSystemQuit.Size = new System.Drawing.Size(152, 22);
            this.tsmiSystemQuit.Text = "Quit";
            // 
            // tsmiView
            // 
            this.tsmiView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiViewWorld,
            this.tsmiViewCreature,
            this.tsmiViewFlag,
            this.tsmiViewInstructionset});
            this.tsmiView.Name = "tsmiView";
            this.tsmiView.Size = new System.Drawing.Size(44, 20);
            this.tsmiView.Text = "View";
            // 
            // tsmiViewWorld
            // 
            this.tsmiViewWorld.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiViewWorldRealm,
            this.tsmiViewWorldRegion,
            this.tsmiViewWorldRoom});
            this.tsmiViewWorld.Name = "tsmiViewWorld";
            this.tsmiViewWorld.Size = new System.Drawing.Size(152, 22);
            this.tsmiViewWorld.Text = "World";
            // 
            // tsmiViewWorldRealm
            // 
            this.tsmiViewWorldRealm.Name = "tsmiViewWorldRealm";
            this.tsmiViewWorldRealm.Size = new System.Drawing.Size(152, 22);
            this.tsmiViewWorldRealm.Text = "Realm...";
            this.tsmiViewWorldRealm.Click += new System.EventHandler(this.tsmiViewWorldRealm_Click);
            // 
            // tsmiViewWorldRegion
            // 
            this.tsmiViewWorldRegion.Name = "tsmiViewWorldRegion";
            this.tsmiViewWorldRegion.Size = new System.Drawing.Size(152, 22);
            this.tsmiViewWorldRegion.Text = "Region...";
            // 
            // tsmiViewWorldRoom
            // 
            this.tsmiViewWorldRoom.Name = "tsmiViewWorldRoom";
            this.tsmiViewWorldRoom.Size = new System.Drawing.Size(152, 22);
            this.tsmiViewWorldRoom.Text = "Room...";
            // 
            // tsmiViewCreature
            // 
            this.tsmiViewCreature.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewCreatureCharacter});
            this.tsmiViewCreature.Name = "tsmiViewCreature";
            this.tsmiViewCreature.Size = new System.Drawing.Size(152, 22);
            this.tsmiViewCreature.Text = "Creature";
            // 
            // tsmiNewCreatureCharacter
            // 
            this.tsmiNewCreatureCharacter.Name = "tsmiNewCreatureCharacter";
            this.tsmiNewCreatureCharacter.Size = new System.Drawing.Size(152, 22);
            this.tsmiNewCreatureCharacter.Text = "Character...";
            // 
            // tsmiViewFlag
            // 
            this.tsmiViewFlag.Name = "tsmiViewFlag";
            this.tsmiViewFlag.Size = new System.Drawing.Size(152, 22);
            this.tsmiViewFlag.Text = "Flag";
            // 
            // tsmiViewInstructionset
            // 
            this.tsmiViewInstructionset.Name = "tsmiViewInstructionset";
            this.tsmiViewInstructionset.Size = new System.Drawing.Size(152, 22);
            this.tsmiViewInstructionset.Text = "Instruction Set";
            // 
            // tsmiOptions
            // 
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.Size = new System.Drawing.Size(61, 20);
            this.tsmiOptions.Text = "Options";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 434);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(952, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 456);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.msMain);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.Text = "Rat Game Editor";
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystem;
        private System.Windows.Forms.ToolStripMenuItem tsmiView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewWorld;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewWorldRealm;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewWorldRegion;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewWorldRoom;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewCreature;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewCreatureCharacter;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewFlag;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewInstructionset;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystemConnections;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystemQuit;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
    }
}

