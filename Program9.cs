using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadSort {
    static class Program {
        private static Thread thread1; private static Thread thread2;
        private static Random r = new Random();
        static int size = 15;
        public static int[] data = new int[size];
        public static int[] data1 = new int[size];                             //{20,45,50,38,75,15,40,60,70,80,35,90};
        public static int[] data2 = new int[size];
        private static Form6 f;
        private static Form6 f2;
        private static bool ready1 = false;
        private static bool ready2 = false;
        private static bool end1 = false;
        private static bool end2 = false;
        private static int indexA1 = 0;
        private static int indexA2 = 0;
        private static int indexB1 = 0;
        private static int indexB2 = 0;
        static EventWaitHandle th1Ready = new AutoResetEvent(false);
        static EventWaitHandle th2Ready = new AutoResetEvent(false);
        static EventWaitHandle th1Go = new AutoResetEvent(false);
        static EventWaitHandle th2Go = new AutoResetEvent(false);


        private static void bubbleSortSimple() {
            int temp;
            for (int i = 0; i < data1.Length - 1; i++) {
                for (int j = 0; j < data1.Length - 1; j++) {
                    indexA1 = j; indexA2 = j + 1;
                    ready1 = false;
                    if (data1[j] > data1[j + 1]) {
                        temp = data1[j + 1];
                        data1[j + 1] = data1[j];
                        data1[j] = temp;
                        }
                    System.Threading.Thread.Sleep(50);                    
                    }
                th1Ready.Set();
                th1Go.WaitOne();
                }
            end1 = true;
            }        
       
        private static void bubbleSortOptim2() {
            bool isSorted;            
            int lastSwap = data1.Length - 1;
            int temp;
            do {

                isSorted = true;
                int currentSwap = 0;

                for (int j = 0; j < lastSwap; j++) {
                    ready2 = false;
                    if (data2[j] > data2[j + 1]) {
                    indexB1 = j; indexB2 = j + 1;
                        temp = data2[j + 1];
                        data2[j + 1] = data2[j];
                        data2[j] = temp;
                        isSorted = false;
                        currentSwap = j;
                        }
                    System.Threading.Thread.Sleep(50);
                    }
                th2Ready.Set();
                th2Go.WaitOne();

                if (isSorted) { return; }
                lastSwap = currentSwap;               

                } while (!isSorted);
            end2 = true;

            }
        [STAThread]
        public static void Main() {
            for (int i = 0; i < data1.Length; i++) {
                data[i] = r.Next(1, 101);
                data1[i] = data[i];
                data2[i] = data[i];
                }
            f = new Form6(data1);
            f2 = new Form6(data2);
            f.Show();
            f2.Show();
            f2.Location = new System.Drawing.Point(f.Right, f.Top);
            //bubbleSortOptim();
            thread1 = new Thread(bubbleSortSimple);
            thread2 = new Thread(bubbleSortOptim2);
            thread1.Start();
            //Thread.Sleep(1000);
            thread2.Start();

            //if (!end1) { th1Go.WaitOne(); }
            //if (!end2) { th2Go.WaitOne(); }
            
            //while (end1 == false || end2 == false) {
            //    f.ShowData(indexA1, indexA2);
            //    f2.ShowData(indexB1, indexB2);
            //    do { Thread.Sleep(30); } while (!th1Ready.Set() || !th2Ready.Set());
            //    if (!end1) { th1Ready.WaitOne(); } 
            //    if (!end2) { th2Ready.WaitOne(); }                
            //    th1Go.Set(); th2Go.Set();
            //    }
            while (end1 == false || end2 == false) {
                f.ShowData(indexA1, indexA2);
                f2.ShowData(indexB1, indexB2);
                //do { Thread.Sleep(30); } while (!th1Ready.Set() || !th2Ready.Set());
                if (!end1) { th1Ready.WaitOne(); }
                if (!end2) { th2Ready.WaitOne(); }
                th1Go.Set(); th2Go.Set();
                }
            thread1.Abort();
            thread2.Abort();
            f.Close();
            f2.Close();
        }        
    }
}
    
    
