/* ## 📜 Licencia
*Este script está disponible solo para fines educativos, de investigación o de experimentación personal.*
*Su uso con fines comerciales está estrictamente prohibido bajo los términos de la licencia CC BY-NC 4.0.* */

namespace PlanoPilotes.UI
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSeleccionIndividual = new System.Windows.Forms.Button();
            this.btnSeleccionMultiple = new System.Windows.Forms.Button();
            this.btnSeleccionTodos = new System.Windows.Forms.Button();
            this.cb_Tipo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Para crear los SpotCoordinate elija una opción:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnSeleccionIndividual
            // 
            this.btnSeleccionIndividual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionIndividual.Location = new System.Drawing.Point(37, 56);
            this.btnSeleccionIndividual.Name = "btnSeleccionIndividual";
            this.btnSeleccionIndividual.Size = new System.Drawing.Size(94, 50);
            this.btnSeleccionIndividual.TabIndex = 1;
            this.btnSeleccionIndividual.Text = "Selección Individual";
            this.btnSeleccionIndividual.UseVisualStyleBackColor = true;
            this.btnSeleccionIndividual.Click += new System.EventHandler(this.btnSeleccionIndividual_Click);
            // 
            // btnSeleccionMultiple
            // 
            this.btnSeleccionMultiple.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionMultiple.Location = new System.Drawing.Point(153, 56);
            this.btnSeleccionMultiple.Name = "btnSeleccionMultiple";
            this.btnSeleccionMultiple.Size = new System.Drawing.Size(94, 50);
            this.btnSeleccionMultiple.TabIndex = 2;
            this.btnSeleccionMultiple.Text = "Selección Múltiple";
            this.btnSeleccionMultiple.UseVisualStyleBackColor = true;
            this.btnSeleccionMultiple.Click += new System.EventHandler(this.btnSeleccionMultiple_Click);
            // 
            // btnSeleccionTodos
            // 
            this.btnSeleccionTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionTodos.Location = new System.Drawing.Point(270, 56);
            this.btnSeleccionTodos.Name = "btnSeleccionTodos";
            this.btnSeleccionTodos.Size = new System.Drawing.Size(103, 50);
            this.btnSeleccionTodos.TabIndex = 4;
            this.btnSeleccionTodos.Text = "Seleccionar Todos los Elementos";
            this.btnSeleccionTodos.UseVisualStyleBackColor = true;
            this.btnSeleccionTodos.Click += new System.EventHandler(this.btnSeleccionTodos_Click);
            // 
            // cb_Tipo
            // 
            this.cb_Tipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Tipo.FormattingEnabled = true;
            this.cb_Tipo.Location = new System.Drawing.Point(90, 157);
            this.cb_Tipo.Name = "cb_Tipo";
            this.cb_Tipo.Size = new System.Drawing.Size(220, 23);
            this.cb_Tipo.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(86, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Selección por tipo de ejemplar: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 207);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_Tipo);
            this.Controls.Add(this.btnSeleccionTodos);
            this.Controls.Add(this.btnSeleccionMultiple);
            this.Controls.Add(this.btnSeleccionIndividual);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSeleccionIndividual;
        private System.Windows.Forms.Button btnSeleccionMultiple;
        private System.Windows.Forms.Button btnSeleccionTodos;
        private System.Windows.Forms.ComboBox cb_Tipo;
        private System.Windows.Forms.Label label2;
    }
}