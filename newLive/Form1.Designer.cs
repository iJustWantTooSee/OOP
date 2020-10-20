namespace newLive
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.currentSpeedLife = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.scalingFactor = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentSpeedLife)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scalingFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button4);
            this.splitContainer1.Panel1.Controls.Add(this.button5);
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.currentSpeedLife);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.scalingFactor);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.buttonStop);
            this.splitContainer1.Panel1.Controls.Add(this.buttonStart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(953, 619);
            this.splitContainer1.SplitterDistance = 131;
            this.splitContainer1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 345);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 65);
            this.label4.TabIndex = 8;
            this.label4.Text = "Юнит: \r\nГендер: \r\nКоордината X: \r\nКоордината Y: \r\nСытость: ";
            // 
            // currentSpeedLife
            // 
            this.currentSpeedLife.Location = new System.Drawing.Point(8, 66);
            this.currentSpeedLife.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.currentSpeedLife.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.currentSpeedLife.Name = "currentSpeedLife";
            this.currentSpeedLife.Size = new System.Drawing.Size(105, 20);
            this.currentSpeedLife.TabIndex = 7;
            this.currentSpeedLife.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.currentSpeedLife.ValueChanged += new System.EventHandler(this.currentSpeedLife_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Скорость жизни";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(-2, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 27);
            this.button1.TabIndex = 5;
            this.button1.Text = "PAUSE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Period of life: 0";
            // 
            // scalingFactor
            // 
            this.scalingFactor.Location = new System.Drawing.Point(8, 27);
            this.scalingFactor.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.scalingFactor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.scalingFactor.Name = "scalingFactor";
            this.scalingFactor.Size = new System.Drawing.Size(105, 20);
            this.scalingFactor.TabIndex = 3;
            this.scalingFactor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.scalingFactor.ValueChanged += new System.EventHandler(this.scalingFactor_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Масштаб";
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStop.Location = new System.Drawing.Point(-2, 152);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(131, 28);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "STOP";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.Location = new System.Drawing.Point(-2, 86);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(131, 27);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "START";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(3001, 3001);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 800;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(-2, 186);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Добавить Юнита";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(-2, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Добавить Траву";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(-2, 273);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(131, 24);
            this.button4.TabIndex = 12;
            this.button4.Text = "Удалить Траву";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.buttonDelGrass_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(-2, 244);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(131, 24);
            this.button5.TabIndex = 11;
            this.button5.Text = "Удалить Юнита";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.buttonDelUnit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(953, 619);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentSpeedLife)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scalingFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NumericUpDown scalingFactor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown currentSpeedLife;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

