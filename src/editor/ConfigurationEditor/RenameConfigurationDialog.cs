// ***********************************************************************
// Copyright (c) 2010 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace NUnit.ProjectEditor
{
	/// <summary>
    /// Displays a dialog for entry of a new name for an
    /// existing configuration. This dialog collects and
    /// validates the name. The caller is responsible for
    /// actually renaming the cofiguration.
    /// </summary>
	public class RenameConfigurationDialog : System.Windows.Forms.Form
	{
		#region Instance Variables

		/// <summary>
		/// The new name to give the configuration
		/// </summary>
		private string configurationName;

		/// <summary>
		/// The original name of the configuration
		/// </summary>
		private string originalName;

        /// <summary>
        /// An array of existing config names used for validation
        /// </summary>
        private string[] configList;

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox configurationNameTextBox;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Construction and Disposal

		public RenameConfigurationDialog( string configurationName, string[] configList )
		{
			InitializeComponent();
			this.configurationName = configurationName;
			this.originalName = configurationName;
            this.configList = configList;
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.configurationNameTextBox = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// configurationNameTextBox
			// 
			this.configurationNameTextBox.Location = new System.Drawing.Point(16, 16);
			this.configurationNameTextBox.Name = "configurationNameTextBox";
			this.configurationNameTextBox.Size = new System.Drawing.Size(264, 22);
			this.configurationNameTextBox.TabIndex = 0;
			this.configurationNameTextBox.Text = "";
			this.configurationNameTextBox.TextChanged += new System.EventHandler(this.configurationNameTextBox_TextChanged);
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(56, 48);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(160, 48);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			// 
			// RenameConfigurationDialog
			// 
			this.AcceptButton = this.okButton;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(291, 79);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.configurationNameTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "RenameConfigurationDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Rename Configuration";
			this.Load += new System.EventHandler(this.ConfigurationNameDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties & Methods

		public string ConfigurationName
		{
			get{ return configurationName; }
			set{ configurationName = value; }
		}
		
		private void ConfigurationNameDialog_Load(object sender, System.EventArgs e)
		{
			if ( configurationName != null )
			{
				configurationNameTextBox.Text = configurationName;
				configurationNameTextBox.SelectAll();
			}
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			configurationName = configurationNameTextBox.Text;
		
            foreach(string existingName in configList)
            {
                if (existingName == configurationName)
                {
    				// TODO: Need general error message display
	    			MessageBox.Show(
                        "A configuration with that name already exists", 
                        "Configuration Name Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
    		Close();
		}

		private void configurationNameTextBox_TextChanged(object sender, System.EventArgs e)
		{
			okButton.Enabled = 
				configurationNameTextBox.TextLength > 0 &&
				configurationNameTextBox.Text != originalName;
		}

		#endregion
	}
}