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
            this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsmiNewWorld = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewCreature = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewFlag = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewInstructionset = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewWorldRealm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewWorldRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewWorldRoom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewCreatureCharacter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditWorld = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditCreature = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditFlag = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditInstructionset = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditWorldRealm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditWorldRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditWorldRoom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditCreatureCharacter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSystemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSystemConnections = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.msMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSystem,
            this.tsmiNew,
            this.tsmiEdit,
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
            // tsmiNew
            // 
            this.tsmiNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewWorld,
            this.tsmiNewCreature,
            this.tsmiNewFlag,
            this.tsmiNewInstructionset});
            this.tsmiNew.Name = "tsmiNew";
            this.tsmiNew.Size = new System.Drawing.Size(43, 20);
            this.tsmiNew.Text = "New";
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditWorld,
            this.tsmiEditCreature,
            this.tsmiEditFlag,
            this.tsmiEditInstructionset});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(39, 20);
            this.tsmiEdit.Text = "Edit";
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
            // tsmiNewWorld
            // 
            this.tsmiNewWorld.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewWorldRealm,
            this.tsmiNewWorldRegion,
            this.tsmiNewWorldRoom});
            this.tsmiNewWorld.Name = "tsmiNewWorld";
            this.tsmiNewWorld.Size = new System.Drawing.Size(152, 22);
            this.tsmiNewWorld.Text = "World";
            // 
            // tsmiNewCreature
            // 
            this.tsmiNewCreature.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewCreatureCharacter});
            this.tsmiNewCreature.Name = "tsmiNewCreature";
            this.tsmiNewCreature.Size = new System.Drawing.Size(152, 22);
            this.tsmiNewCreature.Text = "Creature";
            // 
            // tsmiNewFlag
            // 
            this.tsmiNewFlag.Name = "tsmiNewFlag";
            this.tsmiNewFlag.Size = new System.Drawing.Size(152, 22);
            this.tsmiNewFlag.Text = "Flag";
            // 
            // tsmiNewInstructionset
            // 
            this.tsmiNewInstructionset.Name = "tsmiNewInstructionset";
            this.tsmiNewInstructionset.Size = new System.Drawing.Size(152, 22);
            this.tsmiNewInstructionset.Text = "Instruction Set";
            // 
            // tsmiNewWorldRealm
            // 
            this.tsmiNewWorldRealm.Name = "tsmiNewWorldRealm";
            this.tsmiNewWorldRealm.Size = new System.Drawing.Size(152, 22);
            this.tsmiNewWorldRealm.Text = "Realm...";
            // 
            // tsmiNewWorldRegion
            // 
            this.tsmiNewWorldRegion.Name = "tsmiNewWorldRegion";
            this.tsmiNewWorldRegion.Size = new System.Drawing.Size(152, 22);
            this.tsmiNewWorldRegion.Text = "Region...";
            // 
            // tsmiNewWorldRoom
            // 
            this.tsmiNewWorldRoom.Name = "tsmiNewWorldRoom";
            this.tsmiNewWorldRoom.Size = new System.Drawing.Size(152, 22);
            this.tsmiNewWorldRoom.Text = "Room...";
            // 
            // tsmiNewCreatureCharacter
            // 
            this.tsmiNewCreatureCharacter.Name = "tsmiNewCreatureCharacter";
            this.tsmiNewCreatureCharacter.Size = new System.Drawing.Size(152, 22);
            this.tsmiNewCreatureCharacter.Text = "Character...";
            // 
            // tsmiEditWorld
            // 
            this.tsmiEditWorld.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditWorldRealm,
            this.tsmiEditWorldRegion,
            this.tsmiEditWorldRoom});
            this.tsmiEditWorld.Name = "tsmiEditWorld";
            this.tsmiEditWorld.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditWorld.Text = "World";
            // 
            // tsmiEditCreature
            // 
            this.tsmiEditCreature.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditCreatureCharacter});
            this.tsmiEditCreature.Name = "tsmiEditCreature";
            this.tsmiEditCreature.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditCreature.Text = "Creature";
            // 
            // tsmiEditFlag
            // 
            this.tsmiEditFlag.Name = "tsmiEditFlag";
            this.tsmiEditFlag.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditFlag.Text = "Flag";
            // 
            // tsmiEditInstructionset
            // 
            this.tsmiEditInstructionset.Name = "tsmiEditInstructionset";
            this.tsmiEditInstructionset.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditInstructionset.Text = "Instruction Set";
            // 
            // tsmiEditWorldRealm
            // 
            this.tsmiEditWorldRealm.Name = "tsmiEditWorldRealm";
            this.tsmiEditWorldRealm.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditWorldRealm.Text = "Realm...";
            // 
            // tsmiEditWorldRegion
            // 
            this.tsmiEditWorldRegion.Name = "tsmiEditWorldRegion";
            this.tsmiEditWorldRegion.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditWorldRegion.Text = "Region...";
            // 
            // tsmiEditWorldRoom
            // 
            this.tsmiEditWorldRoom.Name = "tsmiEditWorldRoom";
            this.tsmiEditWorldRoom.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditWorldRoom.Text = "Room...";
            // 
            // tsmiEditCreatureCharacter
            // 
            this.tsmiEditCreatureCharacter.Name = "tsmiEditCreatureCharacter";
            this.tsmiEditCreatureCharacter.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditCreatureCharacter.Text = "Character...";
            // 
            // tsmiSystemQuit
            // 
            this.tsmiSystemQuit.Name = "tsmiSystemQuit";
            this.tsmiSystemQuit.Size = new System.Drawing.Size(152, 22);
            this.tsmiSystemQuit.Text = "Quit";
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
            // tsmiOptions
            // 
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.Size = new System.Drawing.Size(61, 20);
            this.tsmiOptions.Text = "Options";
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
        private System.Windows.Forms.ToolStripMenuItem tsmiNew;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewWorld;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewWorldRealm;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewWorldRegion;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewWorldRoom;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewCreature;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewCreatureCharacter;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewFlag;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewInstructionset;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditWorld;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditWorldRealm;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditWorldRegion;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditWorldRoom;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditCreature;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditCreatureCharacter;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditFlag;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditInstructionset;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystemConnections;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystemQuit;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
    }
}

