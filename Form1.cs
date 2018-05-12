using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace AlgorithmProjectبولايوحنا
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void MergeSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int middle = (low / 2) + (high / 2);
                MergeSort(arr, low, middle);
                MergeSort(arr, middle + 1, high);
                Merge(arr, low, middle, high);
            }
        }

        private static void Quicksort(int [] elements, int left, int right)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i]>pivot)
                {
                    i++;
                }

                while (elements[j] <pivot)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                   int  tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j);
            }

            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }
 

        private  void Merge(int[] input, int low, int middle, int high)
        {

            int left = low;
            int right = middle + 1;
            int[] tmp = new int[(high - low) + 1];
            int tmpIndex = 0;

            while ((left <= middle) && (right <= high))
            {
                if (input[left] < input[right])
                {
                    tmp[tmpIndex] = input[left];
                    left = left + 1;
                }
                else
                {
                    tmp[tmpIndex] = input[right];
                    right = right + 1;
                }
                tmpIndex = tmpIndex + 1;
            }

            if (left <= middle)
            {
                while (left <= middle)
                {
                    tmp[tmpIndex] = input[left];
                    left = left + 1;
                    tmpIndex = tmpIndex + 1;
                }
            }

            if (right <= high)
            {
                while (right <= high)
                {
                    tmp[tmpIndex] = input[right];
                    right = right + 1;
                    tmpIndex = tmpIndex + 1;
                }
            }

            for (int i = 0; i < tmp.Length; i++)
            {
                input[low + i] = tmp[i];
            }

        }
        public int[] CountingSort(int[] array)
        {
            int[] sortedArray = new int[array.Length];

            // find smallest and largest value
            int minVal = array[0];
            int maxVal = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < minVal) minVal = array[i];
                else if (array[i] > maxVal) maxVal = array[i];
            }

            // init array of frequencies
            int[] counts = new int[maxVal - minVal + 1];

            // init the frequencies
            for (int i = 0; i < array.Length; i++)
            {
                counts[array[i] - minVal]++;
            }

            // recalculate
            counts[0]--;
            for (int i = 1; i < counts.Length; i++)
            {
                counts[i] = counts[i] + counts[i - 1];
            }

            // Sort the array
            for (int i = array.Length - 1; i >= 0; i--)
            {
                sortedArray[counts[array[i] - minVal]--] = array[i];
            }

            return sortedArray;
        }
        private int BinSearch(int []arr,int p,int r,int x)
        {
            int result = 0;
            if (arr.Length == 1)
                return -1;
            else
            {
                int low = 0;
                int high = arr.Length-1;
                int mid = (low + high) / 2;
                try
                {
                    
                    if (arr[mid] == x)
                    {
                        return arr[mid];
                    }
                    else if (arr[mid] > x)
                    {
                        int[] ar = new int[(arr.Length / 2) ];
                        Array.Copy(arr, 0, ar, 0, (arr.Length / 2) - 1);
                        result=BinSearch(ar, low, mid - 1, x);
                    }
                    else if (arr[mid] < x)
                    {
                        int[] ar = new int[(arr.Length / 2) ];
                        Array.Copy(arr, mid + 1, ar, 0,(arr.Length/2) );
                        result=BinSearch(ar, mid+1 , high, x);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return result;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string text;
            if (comboBox1.SelectedItem == "ReadWrite")
            {
                try
                {
                    text = File.ReadAllText("D:\\input.txt");
                    label1.Text = text;
                    //File.WriteAllText(path, text);
                    using (StreamWriter writer = new StreamWriter("D:\\output.txt"))
                    {
                        writer.WriteLine(text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            else if (comboBox1.SelectedItem == "RecursiveBinarySearch")
            {
                if (searchnum.Text == null)
                {
                    MessageBox.Show("Please enter number to search");
                }
                else
                {
                    try
                    {
                        text = File.ReadAllText("D:\\input.txt");
                        string[] arr = text.Split(',');
                        int[] ar = Array.ConvertAll(arr, int.Parse);
                        int index = int.Parse(searchnum.Text);
                        int trg = BinSearch(ar, 0, ar.Length - 1, index);
                        if (trg == -1)
                        {
                            label1.Text = "Not Found";
                        }
                        else
                        {
                            label1.Text = trg.ToString()+" Founded yaaaa";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
            }
            else if (comboBox1.SelectedItem == "MergeSort")
            {

                try
                {
                    text = File.ReadAllText("D:\\input.txt");
                    string[] arr = text.Split(',');
                    int[] ar = Array.ConvertAll(arr, int.Parse);
                    int max = ar.Length-1;
                    MergeSort(ar, 0, max);
                    using (StreamWriter writer = new StreamWriter("D:\\output.txt"))
                    {
                        text = null;
                        for (int i = 0; i < ar.Length; i++)
                        {
                            if (i == ar.Length - 1)
                            {
                                text += ar[i].ToString();
                                goto writer;
                            }
                            text += ar[i].ToString() + ',';
                        }
                    writer:
                        writer.WriteLine(text);
                    MessageBox.Show("TaskCompleted", "Confrmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBox1.SelectedItem == "QuickSort")
            {

                try
                {
                    text = File.ReadAllText("D:\\input.txt");
                    string[] arr = text.Split(',');
                    int[] ar = Array.ConvertAll(arr, int.Parse);
                    int max = ar.Length - 1;
                    Quicksort(ar, 0, max);
                    using (StreamWriter writer = new StreamWriter("D:\\output.txt"))
                    {
                        text = null;
                        for (int i = 0; i < ar.Length; i++)
                        {
                            if (i == ar.Length - 1)
                            {
                                text += ar[i].ToString();
                                goto writer;
                            }
                            text += ar[i].ToString() + ',';
                        }
                    writer:
                        writer.WriteLine(text);
                    MessageBox.Show("TaskCompleted","Confrmation",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Warning",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else if (comboBox1.SelectedItem == "HeapSort")
            {
                try
                {
                    text = File.ReadAllText("D:\\input.txt");
                    string[] arr = text.Split(',');
                    int[] ar = Array.ConvertAll(arr, int.Parse);
                    HeapSort heap = new HeapSort();
                    int[] result = new int[ar.Length];
                    result=heap.PerformHeapSort(ar);
                    using (StreamWriter writer = new StreamWriter("D:\\output.txt"))
                    {
                        text = null;
                        for (int i = 0; i < result.Length; i++)
                        {
                            if (i == result.Length - 1)
                            {
                                text += result[i].ToString();
                                goto writer;
                            }
                            text += result[i].ToString() + ',';
                        }
                    writer:
                        writer.WriteLine(text);
                        MessageBox.Show("TaskCompleted", "Confrmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBox1.SelectedItem == "CountingSort")
            {
                try
                {
                    text = File.ReadAllText("D:\\input.txt");
                    string[] arr = text.Split(',');
                    int[] ar = Array.ConvertAll(arr, int.Parse);
                    int[] result = new int[ar.Length];
                    result = CountingSort(ar);
                    using (StreamWriter writer = new StreamWriter("D:\\output.txt"))
                    {
                        text = null;
                        for (int i = 0; i < result.Length; i++)
                        {
                            if (i == result.Length - 1)
                            {
                                text += result[i].ToString();
                                goto writer;
                            }
                            text += result[i].ToString() + ',';
                        }
                    writer:
                        writer.WriteLine(text);
                        MessageBox.Show("TaskCompleted", "Confrmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            watch.Stop();
            RT.Text = watch.ElapsedMilliseconds.ToString() + " MilliSeconds";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "RecursiveBinarySearch")
            {
                searchnum.Visible = true;
            }
        }
    }
    class HeapSort
    {
        private int heapSize;
        private int[] arr;
        private void BuildHeap(int[] arr)
        {
            heapSize = arr.Length - 1;
            for (int i = heapSize / 2; i >= 0; i--)
            {
                Heapify(arr, i);
            }
        }

        private void Swap(int[] arr, int x, int y)//function to swap elements
        {
            int temp = arr[x];
            arr[x] = arr[y];
            arr[y] = temp;
        }
        private void Heapify(int[] arr, int index)
        {
            int left = 2 * index;
            int right = 2 * index + 1;
            int largest = index;

            if (left <= heapSize && arr[left] > arr[index])
            {
                largest = left;
            }

            if (right <= heapSize && arr[right] > arr[largest])
            {
                largest = right;
            }

            if (largest != index)
            {
                Swap(arr, index, largest);
                Heapify(arr, largest);
            }
        }
        public int[] PerformHeapSort(int[] arr)
        {
            BuildHeap(arr);
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                Swap(arr, 0, i);
                heapSize--;
                Heapify(arr, 0);
            }
            return arr;
        }
        private void DisplayArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            { Console.Write("[{0}]", arr[i]); }
        }
    }
}
