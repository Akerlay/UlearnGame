namespace UlearnGame.View
{
	public partial class GameView
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox pictureBox1;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				components?.Dispose();
			}

			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize) (pictureBox1)).BeginInit();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.BackColor = System.Drawing.SystemColors.WindowText;
			pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBox1.Location = new System.Drawing.Point(12, 12);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(221, 226);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			pictureBox1.Paint += OnPaint;
			// 
			// Form1
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(665, 667);
			Controls.Add(pictureBox1);
			KeyPreview = true;
			Name = "GameView";
			Text = "GameView";
			Load += OnLoad;
			KeyPress += OnKeyPress;
			((System.ComponentModel.ISupportInitialize) pictureBox1).EndInit();
			ResumeLayout(false);
		}
	}
}