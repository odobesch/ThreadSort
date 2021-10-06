using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadSort {
    public class Form6 : Form {        
        int rowHeight = 10; int rowWidth = 200; int spaceBetweenRows = 10; int pbCount = 15;
        int margin = 15;
        ProgressBar[] arrPB;
        private int[] data;
        public void ShowData(int indexA, int indexB) {
            for (int i = 0; i < data.Length; i++) {
                arrPB[i].Value = data[i];
                if (indexA == data[i]) { arrPB[i].ForeColor = Color.Red; }
                else if (indexB == data[i]) { arrPB[i].ForeColor = Color.LightGreen; }
                else { arrPB[i].ForeColor = Color.Blue; }
                arrPB[i].Refresh();

                }
            }
        public Form6(int [] data) {
            this.data = data;
            arrPB = new ProgressBar[data.Length];
            for (int i = 0; i < data.Length; i++) {
                arrPB[i] = new ProgressBar();
                arrPB[i].ForeColor = Color.DarkMagenta;
                arrPB[i].Style = ProgressBarStyle.Continuous;
                SuspendLayout();
                arrPB[i].Location = new Point(margin, 10 + ((i +1)* (rowHeight + spaceBetweenRows)));
                arrPB[i].Name = "$progressBar" + i;
                arrPB[i].Size = new Size(rowWidth, rowHeight);
                Controls.Add(arrPB[i]);                                

                }
                AutoScaleDimensions = new SizeF(8F, 16F);
                AutoScaleMode = AutoScaleMode.Font;
                ClientSize = new Size(rowWidth + 2 * margin, pbCount * (rowHeight + spaceBetweenRows) + 4*margin);                
                Name = "Form6";
                Text = "Bubble Sorting";
                ResumeLayout(false);
                //ShowData();           
            }        
        }
    }
